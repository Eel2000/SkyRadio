namespace SkyRadio.Client.Identity.Services.Interfaces
{
    public interface ILoginService
    {
        Task LoginAsync(string username, string password);
    }
}
