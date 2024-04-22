namespace DB_Coursework_API.Interfaces
{
    public interface IMyLogger
    {
        Task LogAsync(string message);
        Task LogWarningAsync(string message);
    }
}
