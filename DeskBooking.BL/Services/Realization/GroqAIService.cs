using DeskBooking.BL.Services.Abstraction;
using DeskBooking.Domain.Common.Settings;
using GroqApiLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DeskBooking.BL.Services.Realization;

public class GroqAIService : IGroqAIService
{
    private readonly IConfiguration _configuration;
    private readonly GroqSettings _groqSettings;

    public GroqAIService(IConfiguration configuration, GroqSettings groqSettings)
    {
        _configuration = configuration;
        _groqSettings = groqSettings;
    }

    public async Task<string> GenerateResponseAsync(string question, string data, CancellationToken cancellationToken)
    {
        var groqApi = new GroqApiClient(_groqSettings.GroqAPI_KEY);

        var request = new JsonObject
        {
            ["model"] = _groqSettings.GroqMODEL_NAME,
            ["messages"] = new JsonArray
            {
                new JsonObject
                {
                    ["role"] = "user",
                    ["content"] = JsonSerializer.Serialize(new
                    {
                        question,
                        data,
                        importantInfo = "Answer in the same language I wrote to you. Give the shortest and most constructive answer to my question. If the question is unclear or unsupported, return a fallback message: 'Sorry, I didn’t understand that. Please try rephrasing your question.'. Convert all datetimes to normal DD-MM-YY HH-mm format. If user asks for some data that you have in JSON, deserialize it first. I have provided you all data of user bookings, response base on it"
                    })
                }
            }
        };

        var jsonResult = await groqApi.CreateChatCompletionAsync(request);
        var result = jsonResult?["choices"]?[0]?["message"]?["content"]?.ToString();

        if (result is null)
        {
            throw new RequestException(StatusCodes.Status500InternalServerError, ErrorMessages.Status500InternalServerError);
        }

        return result;
    }
}
