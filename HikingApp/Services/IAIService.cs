using System.Threading.Tasks;

namespace HikingApp.Services
{
    public interface IAIService
    {
        // Define the contract for trail descriptions
        Task<string> GenerateDescriptionAsync(string trailName, string region);

        // Define the contract for the chatbot
        Task<string> GetChatResponseAsync(string userMessage);
    }
}