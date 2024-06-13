namespace AspEndProject.Services.Interface
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetAll();
    }
}
