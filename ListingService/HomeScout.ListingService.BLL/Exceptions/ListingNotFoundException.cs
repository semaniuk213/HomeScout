namespace HomeScout.ListingService.BLL.Exceptions
{
    public class ListingNotFoundException : Exception
    {
        public ListingNotFoundException(string message) : base(message) { }
    }
}
