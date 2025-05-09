namespace HomeScout.DAL.Parameters
{
    public class UserParameters : QueryStringParameters
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
