namespace EcommerceAPI.Data
{
    public class CustomErrorException : Exception
    {
        public CustomErrorException() : base()
        {
        }

        public CustomErrorException(string message) : base(message)
        {
        }

        public CustomErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
