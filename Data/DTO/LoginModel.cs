namespace Data.DTO
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
        public string? userRole { get; set; }
        public object? userID { get; set; }

    }
}
