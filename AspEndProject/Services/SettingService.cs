using AspEndProject.DAL;
using AspEndProject.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace AspEndProject.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;
        public SettingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string, string>> GetAll()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
        }
    }

}
