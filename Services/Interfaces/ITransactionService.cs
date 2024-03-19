using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync();
        Task CommitAsync();
        Task DisposeAsync();
        Task RollbackAsync();
        bool IsExist();
    }
}
