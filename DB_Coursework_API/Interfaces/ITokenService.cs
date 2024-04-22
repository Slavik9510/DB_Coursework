namespace DB_Coursework_API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(int id, string role);
    }
}
