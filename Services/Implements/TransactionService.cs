using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implements
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task CommitAsync()
        {
            await _transactionRepository.CommitAsync();
        }

        public async Task CreateTransactionAsync()
        {
            await _transactionRepository.CreateTransactionAsync();
        }

        public async Task DisposeAsync()
        {
            await _transactionRepository.DisposeAsync();
        }

        public bool IsExist()
        {
            return _transactionRepository.IsExist();
        }

        public async Task RollbackAsync()
        {
            await _transactionRepository.RollbackAsync();
        }
    }
}
