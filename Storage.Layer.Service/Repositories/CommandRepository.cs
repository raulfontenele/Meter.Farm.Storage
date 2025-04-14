using Meter.Farm.DTO.Repository;
using Microsoft.EntityFrameworkCore;
using Storage.Layer.Service.Data;
using Storage.Layer.Service.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Layer.Service.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        private readonly MeterFarmDbContext _context;

        public CommandRepository(MeterFarmDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommandStorageObject>> GetAllAsync()
        {
            return await _context.CommandStorageObjects.ToListAsync();
        }

        public async Task<CommandStorageObject> GetByIdAsync(int id)
        {
            return await _context.CommandStorageObjects.FindAsync(id);
        }

        public async Task AddAsync(CommandStorageObject command)
        {
            await _context.CommandStorageObjects.AddAsync(command);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
