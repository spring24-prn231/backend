using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class TransactionDAO : ITransactionDAO
    {

        private readonly BirthdayBlitzContext _context;
        private IDbContextTransaction? _transaction;
        public TransactionDAO(BirthdayBlitzContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            if (_transaction != null) await _transaction.CommitAsync();
        }

        public async Task CreateTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public bool IsExist()
        {
            return _transaction != null;
        }
        public async Task RollbackAsync()
        {
            if (_transaction != null) await _transaction.RollbackAsync();
        }
    }
}
