namespace Wishlist.Exceptions
{
    public class InvalidIdException : ApplicationException
    {
        public InvalidIdException() { }
        public InvalidIdException(string message) : base(message) { }
    }
}
