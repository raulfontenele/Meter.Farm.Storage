using Meter.Farm.DTO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Layer.Service.Repositories.Interfaces
{
    public interface ICommandRepository
    {
        Task<IEnumerable<CommandStorageObject>> GetAllAsync();
        Task<CommandStorageObject> GetByIdAsync(int id);
        Task AddAsync(CommandStorageObject command);
        Task SaveChangesAsync();
    }
}
