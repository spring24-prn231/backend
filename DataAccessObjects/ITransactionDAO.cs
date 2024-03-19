namespace DataAccessObjects
{
    public interface ITransactionDAO
    {
        Task CreateTransactionAsync();
        Task CommitAsync();
        Task DisposeAsync();
        Task RollbackAsync();
        bool IsExist();
    }
}
