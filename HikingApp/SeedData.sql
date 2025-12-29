-- Seed Data for Difficulties
INSERT INTO Difficulties (Id, Name) VALUES 
('54466f17-02af-48e7-8ed3-5a4a8bfacf6f', 'Easy'),
('ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', 'Medium'),
('f808ddcd-b5e5-4d80-b732-1ca523e48434', 'Hard');

-- Seed Data for Regions
INSERT INTO Regions (Id, Code, Name, RegionImageUrl) VALUES 
('f7248fc3-2585-4efb-8d1d-1c555f4087f6', 'AKL', 'Auckland', 'https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1'),
('6884f7d7-ad1f-4101-8df3-7a6fafe29d66', 'WLG', 'Wellington', 'https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1'),
('14ce90d5-8658-484e-b366-402633a48c50', 'NSN', 'Nelson', 'https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1');

-- Seed Data for Walks
INSERT INTO Walks (Id, Name, Description, LengthInKM, WalkImageUrl, DifficultyId, RegionId) VALUES 
('17d95674-6e2a-4c21-b0c4-131e17f19448', 'Mount Eden Summit Walk', 'A moderate walk to the summit of Mount Eden offering panoramic views.', 2.5, 'https://images.pexels.com/photos/931018/pexels-photo-931018.jpeg', '54466f17-02af-48e7-8ed3-5a4a8bfacf6f', 'f7248fc3-2585-4efb-8d1d-1c555f4087f6'),
('c7823528-9840-4208-8971-c4e3a65c622c', 'Makara Peak Loop', 'A challenging loop track with steep sections and coastal views.', 6.0, 'https://images.pexels.com/photos/3408744/pexels-photo-3408744.jpeg', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '6884f7d7-ad1f-4101-8df3-7a6fafe29d66'),
('19616d19-f693-4b98-96c9-a05f77a55c09', 'Abel Tasman Coastal Track', 'One of New Zealands Great Walks, featuring golden beaches and native bush.', 10.5, 'https://images.pexels.com/photos/1659438/pexels-photo-1659438.jpeg', 'f808ddcd-b5e5-4d80-b732-1ca523e48434', '14ce90d5-8658-484e-b366-402633a48c50');

-- Seed Data for WalkDetails
-- Note: UUIDs for Id are newly generated. WalkId matches the Walks above.
INSERT INTO WalkDetails (Id, WalkId, RouteGeometry, ElevationGainMeters, EstimatedDurationMinutes, IsAccessible, Features) VALUES 
('d14467d3-05c0-4352-87c2-8356d781cba5', '17d95674-6e2a-4c21-b0c4-131e17f19448', '[[174.7633, -36.8783], [174.7640, -36.8790]]', 196, 45, 1, '["Family Friendly", "Views", "Dog Friendly"]'),
('52ee5968-3d85-4845-8120-e26090740924', 'c7823528-9840-4208-8971-c4e3a65c622c', '[[174.7200, -41.2800], [174.7210, -41.2810]]', 400, 120, 0, '["Coastal", "Steep", "Remote"]'),
('6b216964-6725-4c01-9257-27b2c0199042', '19616d19-f693-4b98-96c9-a05f77a55c09', '[[173.0000, -40.9000], [173.0100, -40.9100]]', 20, 240, 1, '["Beaches", "Camping", "Swimming"]');
