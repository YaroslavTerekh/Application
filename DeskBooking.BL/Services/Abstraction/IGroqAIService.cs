namespace DeskBooking.BL.Services.Abstraction;

public interface IGroqAIService
{
    public Task<string> GenerateResponseAsync(string question, string data, CancellationToken cancellationToken);
}
