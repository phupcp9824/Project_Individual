
namespace Back_end.Controllers
{
    internal class RefreshToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
    }
}