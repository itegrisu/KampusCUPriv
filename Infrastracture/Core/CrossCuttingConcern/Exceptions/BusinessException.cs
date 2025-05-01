namespace Core.CrossCuttingConcern.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string? message)
            : base(message)
        {
        }

        public BusinessException(string? message, System.Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
