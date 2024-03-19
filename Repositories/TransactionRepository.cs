using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ITransactionDAO _transactionDAO;
        public TransactionRepository(ITransactionDAO transactionDAO)
        {
            _transactionDAO = transactionDAO;
        }
        public async Task CommitAsync()
        {
            await _transactionDAO.CommitAsync();
        }

        public async Task CreateTransactionAsync()
        {
            await _transactionDAO.CreateTransactionAsync();
        }

        public async Task DisposeAsync()
        {
            await _transactionDAO.DisposeAsync();
        }

        public bool IsExist()
        {
            return _transactionDAO.IsExist();
        }

        public async Task RollbackAsync()
        {
            await _transactionDAO.RollbackAsync();
        }
    }
}
