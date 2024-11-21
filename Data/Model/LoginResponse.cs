namespace Data.Model
{
    public class LoginResponse
    {

        public bool Successfull { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string Role { get; set; }
        public string userID { get; set; }

    }
}
