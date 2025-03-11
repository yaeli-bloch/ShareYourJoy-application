using Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Server.Data.Repositories
{
    public class FileRepository
    {
        private readonly DataContext _context;

        public FileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<OFile> GetFileByIdAsync(string id)
        {
            return await _context.Files
                .Include(o => o.User)  // אם אתה צריך את פרטי המשתמש הקשור לקובץ
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<OFile>> GetAllFilesAsync()
        {
            return await _context.Files.ToListAsync();
        }

        public async Task AddFileAsync(OFile oFile)
        {
            await _context.Files.AddAsync(oFile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFileAsync(OFile orderFile)
        {
            _context.Files.Update(orderFile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFileAsync(string id)
        {
            var orderFile = await GetFileByIdAsync(id);
            if (orderFile != null)
            {
                _context.Files.Remove(orderFile);
                await _context.SaveChangesAsync();
            }
        }
    }
}
