namespace PQAMCAPI.Interfaces
{
    public interface IUserContextService
    {
        HttpContext CurrentContext();
        string GetCurrentUserId();
        bool IsCurrentUserAdmin();
        string GetRequesterURL();
    }
}
