namespace HomeScout.Auth.Models
{
    public class AuthResponseModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
