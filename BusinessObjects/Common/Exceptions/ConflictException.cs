namespace BusinessObjects.Common.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException()
            : base()
        {
        }

        public ConflictException(string message)
            : base(message)
        {
        }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ConflictException(string name, object key)
            : base($"Dữ liệu \"{name}\" ({key}) đã tồn tại")
        {
        }
    }
}
