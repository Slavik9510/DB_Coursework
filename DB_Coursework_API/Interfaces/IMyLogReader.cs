namespace DB_Coursework_API.Interfaces
{
    public interface IMyLogReader
    {
        Task<IEnumerable<string>> GetLogEntriesAsync(DateTime date);
    }
}
