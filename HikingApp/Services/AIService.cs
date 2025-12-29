using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HikingApp.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AIService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["Gemini:ApiKey"];
        }

        public async Task<string> GetChatResponseAsync(string userMessage)
        {
            // Note: This is a stateless implementation. For a real chatbot, you should manage conversation history.
            var requestBody = new
            {
                contents = new[]
                {
                    new {
                        role = "user",
                        parts = new[] { new { text = userMessage } }
                    }
                }
            };

            return await CallGeminiAsync(requestBody);
        }

        public async Task<string> GenerateDescriptionAsync(string trailName, string region)
        {
            var prompt = $"Describe the {trailName} trail in {region}.";
            var requestBody = new
            {
                contents = new[]
                {
                    new {
                        role = "user",
                        parts = new[] { new { text = prompt } }
                    }
                }
            };

            return await CallGeminiAsync(requestBody);
        }

        private async Task<string> CallGeminiAsync(object requestBody)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "API Key is missing. Please configure Gemini:ApiKey in appsettings.json";
            }

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            // 1. Trying Flash-Lite Preview as other models reported 0-quota
            var modelName = "gemini-2.5-flash";
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{modelName}:generateContent?key={_apiKey}";

            try 
            {
                var response = await _httpClient.PostAsync(url, content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    
                    // IF 404, ATTEMPT TO LIST MODELS TO HELP DEBUGGING
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        var listUrl = $"https://generativelanguage.googleapis.com/v1beta/models?key={_apiKey}";
                        var listResponse = await _httpClient.GetAsync(listUrl);
                        if (listResponse.IsSuccessStatusCode)
                        {
                            var listJson = await listResponse.Content.ReadAsStringAsync();
                            return $"Error: Model '{modelName}' not found. Available Models: {listJson}";
                        }
                    }

                    return $"Error from AI Provider: {response.StatusCode} - {error}";
                }

                var responseString = await response.Content.ReadAsStringAsync();
                
                // Parse safely
                try 
                {
                    using var doc = JsonDocument.Parse(responseString);
                    if (doc.RootElement.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                    {
                        var candidate = candidates[0];
                        if (candidate.TryGetProperty("content", out var contentElem) && 
                            contentElem.TryGetProperty("parts", out var parts) && 
                            parts.GetArrayLength() > 0)
                        {
                            return parts[0].GetProperty("text").GetString() ?? "No text generated.";
                        }
                    }
                }
                catch(Exception parseEx)
                {
                    return $"Error parsing response: {parseEx.Message}. Raw: {responseString}";
                }
                
                return "No valid response format from AI. Raw: " + responseString;
            }
            catch (Exception ex)
            {
                return $"Exception calling AI: {ex.Message}";
            }
        }
    }
}