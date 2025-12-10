import React, { useEffect, useRef, useState } from 'react';
import mapboxgl from 'mapbox-gl';
import 'mapbox-gl/dist/mapbox-gl.css';

const Map = () => {
  const mapContainer = useRef(null);
  const map = useRef(null);
  const geolocateControlRef = useRef(null);
  const [style, setStyle] = useState('mapbox://styles/mapbox/outdoors-v12'); // Default to outdoors for hiking
  const [isHiking, setIsHiking] = useState(false);
  const [distance, setDistance] = useState(0);
  const [routeDistance, setRouteDistance] = useState(0);
  const [destination, setDestination] = useState(null);

  const lastPositionRef = useRef(null);
  const isHikingRef = useRef(isHiking);
  const userLocationRef = useRef(null);

  // Islamabad Trails Data
  const trailsData = [
    {
      id: 'trail3',
      name: 'Trail 3',
      description: 'Popular and steep trail leading to Monal.',
      difficulty: 'Moderate',
      length: '5.5 km',
      image: 'https://images.unsplash.com/photo-1625505826533-5c80aca7d157?q=80&w=200&auto=format&fit=crop',
      start: [73.055, 33.725],
      end: [73.065, 33.745], // Approx Monal
      coordinates: [
        [73.055, 33.725],
        [73.058, 33.730],
        [73.060, 33.735],
        [73.062, 33.740],
        [73.065, 33.745]
      ]
    },
    {
      id: 'trail5',
      name: 'Trail 5',
      description: 'Scenic trail with a stream, easier than Trail 3.',
      difficulty: 'Easy/Moderate',
      length: '4.8 km',
      image: 'https://images.unsplash.com/photo-1551632811-561732d1e306?q=80&w=200&auto=format&fit=crop',
      start: [73.078, 33.735],
      end: [73.085, 33.750],
      coordinates: [
        [73.078, 33.735],
        [73.080, 33.740],
        [73.082, 33.745],
        [73.085, 33.750]
      ]
    },
    {
      id: 'trail6',
      name: 'Trail 6',
      description: 'Quiet and less crowded trail.',
      difficulty: 'Moderate',
      length: '4.0 km',
      image: 'https://images.unsplash.com/photo-1464822759023-fed622ff2c3b?q=80&w=200&auto=format&fit=crop',
      start: [73.090, 33.740],
      end: [73.095, 33.755],
      coordinates: [
        [73.090, 33.740],
        [73.092, 33.745],
        [73.094, 33.750],
        [73.095, 33.755]
      ]
    }
  ];

  useEffect(() => {
    isHikingRef.current = isHiking;
    if (!isHiking) {
      lastPositionRef.current = null;
    }
  }, [isHiking]);

  const calculateDistance = (lat1, lon1, lat2, lon2) => {
    const R = 6371;
    const dLat = deg2rad(lat2 - lat1);
    const dLon = deg2rad(lon2 - lon1);
    const a =
      Math.sin(dLat / 2) * Math.sin(dLat / 2) +
      Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) *
      Math.sin(dLon / 2) * Math.sin(dLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    return R * c;
  };

  const deg2rad = (deg) => deg * (Math.PI / 180);

  const getRoute = async (end) => {
    if (!userLocationRef.current) {
      alert("Please enable location services to calculate a route.");
      return;
    }

    const start = [userLocationRef.current.lng, userLocationRef.current.lat];
    const query = await fetch(
      `https://api.mapbox.com/directions/v5/mapbox/walking/${start[0]},${start[1]};${end[0]},${end[1]}?steps=true&geometries=geojson&access_token=${mapboxgl.accessToken}`,
      { method: 'GET' }
    );
    const json = await query.json();
    const data = json.routes[0];

    if (!data) return;

    const route = data.geometry.coordinates;
    const geojson = {
      type: 'Feature',
      properties: {},
      geometry: {
        type: 'LineString',
        coordinates: route
      }
    };

    if (map.current.getSource('dynamic-route')) {
      map.current.getSource('dynamic-route').setData(geojson);
    } else {
      map.current.addLayer({
        id: 'dynamic-route',
        type: 'line',
        source: {
          type: 'geojson',
          data: geojson
        },
        layout: {
          'line-join': 'round',
          'line-cap': 'round'
        },
        paint: {
          'line-color': '#3887be',
          'line-width': 5,
          'line-opacity': 0.75
        }
      });
    }

    setRouteDistance(data.distance / 1000);
  };

  const addTrails = () => {
    trailsData.forEach(trail => {
      // Add Route Line
      if (!map.current.getSource(trail.id)) {
        map.current.addSource(trail.id, {
          'type': 'geojson',
          'data': {
            'type': 'Feature',
            'properties': {},
            'geometry': {
              'type': 'LineString',
              'coordinates': trail.coordinates
            }
          }
        });

        map.current.addLayer({
          'id': trail.id,
          'type': 'line',
          'source': trail.id,
          'layout': {
            'line-join': 'round',
            'line-cap': 'round'
          },
          'paint': {
            'line-color': '#ff5722', // Orange for trails
            'line-width': 4,
            'line-dasharray': [1, 1]
          }
        });
      }

      // Add Start Marker with Popup
      const popupHTML = `
            <div style="width: 200px;">
                <h3 style="margin: 0 0 5px 0;">${trail.name}</h3>
                <img src="${trail.image}" style="width: 100%; border-radius: 4px; margin-bottom: 5px;" />
                <p style="margin: 0; font-size: 0.9em;">${trail.description}</p>
                <div style="margin-top: 5px; font-size: 0.8em;">
                    <strong>Length:</strong> ${trail.length}<br/>
                    <strong>Difficulty:</strong> ${trail.difficulty}<br/>
                    <span style="color: green;">● Open Now</span>
                </div>
            </div>
        `;

      new mapboxgl.Marker({ color: '#ff5722' })
        .setLngLat(trail.start)
        .setPopup(new mapboxgl.Popup({ offset: 25 }).setHTML(popupHTML))
        .addTo(map.current);
    });
  };

  useEffect(() => {
    if (map.current) return;

    mapboxgl.accessToken = import.meta.env.VITE_MAPBOX_ACCESS_TOKEN;

    if (!mapboxgl.accessToken) {
      console.error("Mapbox access token is missing!");
      return;
    }

    map.current = new mapboxgl.Map({
      container: mapContainer.current,
      style: style,
      center: [73.065, 33.735], // Center on Margalla Hills, Islamabad
      zoom: 13,
      pitch: 50,
    });

    // Add GeolocateControl
    geolocateControlRef.current = new mapboxgl.GeolocateControl({
      positionOptions: {
        enableHighAccuracy: true
      },
      trackUserLocation: true,
      showUserHeading: true
    });
    map.current.addControl(geolocateControlRef.current);

    geolocateControlRef.current.on('geolocate', (e) => {
      const currentLat = e.coords.latitude;
      const currentLng = e.coords.longitude;
      userLocationRef.current = { lat: currentLat, lng: currentLng };

      if (isHikingRef.current) {
        if (lastPositionRef.current) {
          const dist = calculateDistance(
            lastPositionRef.current.lat,
            lastPositionRef.current.lng,
            currentLat,
            currentLng
          );
          setDistance(prevDistance => prevDistance + dist);
        }
        lastPositionRef.current = { lat: currentLat, lng: currentLng };
      }
    });

    map.current.on('load', () => {
      map.current.addSource('mapbox-dem', {
        'type': 'raster-dem',
        'url': 'mapbox://mapbox.mapbox-terrain-dem-v1',
        'tileSize': 512,
        'maxzoom': 14
      });
      map.current.setTerrain({ 'source': 'mapbox-dem', 'exaggeration': 1.5 });

      addTrails();
    });

    map.current.on('click', (event) => {
      const coords = Object.keys(event.lngLat).map((key) => event.lngLat[key]);
      setDestination(coords);
      getRoute(coords);
    });

    map.current.addControl(new mapboxgl.NavigationControl());

  }, []);

  useEffect(() => {
    if (map.current) {
      map.current.setStyle(style);
      map.current.once('style.load', () => {
        addTrails(); // Re-add trails on style change
        // Note: Dynamic route layer would also need re-adding if it existed
      });
    }
  }, [style]);

  const toggleHike = () => {
    if (isHiking) {
      setIsHiking(false);
    } else {
      setIsHiking(true);
      setDistance(0);
      if (geolocateControlRef.current) {
        geolocateControlRef.current.trigger();
      }
    }
  };

  return (
    <div style={{ position: 'relative', width: '100%', height: '100vh' }}>
      <div style={{
        position: 'absolute',
        top: '10px',
        left: '10px',
        zIndex: 1,
        backgroundColor: 'white',
        padding: '10px',
        borderRadius: '4px',
        boxShadow: '0 0 10px rgba(0,0,0,0.1)',
        display: 'flex',
        flexDirection: 'column',
        gap: '10px',
        maxHeight: '90vh',
        overflowY: 'auto'
      }}>
        <div style={{ display: 'flex', gap: '10px', alignItems: 'center' }}>
          <label style={{ fontWeight: 'bold' }}>Style:</label>
          <select onChange={(e) => setStyle(e.target.value)} value={style} style={{ padding: '5px' }}>
            <option value="mapbox://styles/mapbox/outdoors-v12">Outdoors</option>
            <option value="mapbox://styles/mapbox/streets-v12">Streets</option>
            <option value="mapbox://styles/mapbox/satellite-streets-v12">Satellite</option>
          </select>
        </div>

        <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
          <button
            onClick={toggleHike}
            style={{
              backgroundColor: isHiking ? '#d9534f' : '#007cbf',
              color: 'white',
              border: 'none',
              padding: '8px 16px',
              borderRadius: '4px',
              cursor: 'pointer',
              fontWeight: 'bold'
            }}
          >
            {isHiking ? 'Stop Hike' : 'Start Hike'}
          </button>
        </div>

        {routeDistance > 0 && (
          <div style={{ fontWeight: 'bold' }}>
            Route to Dest: {routeDistance.toFixed(2)} km
          </div>
        )}

        {isHiking && (
          <div style={{ fontWeight: 'bold', color: '#007cbf' }}>
            Hiked: {distance.toFixed(2)} km
          </div>
        )}

        <div style={{ fontSize: '0.8em', color: '#666' }}>
          Click map to route.<br />
          Click markers for trail info.
        </div>
      </div>
      <div ref={mapContainer} style={{ width: '100%', height: '100%' }} />
    </div>
  );
};

export default Map;
