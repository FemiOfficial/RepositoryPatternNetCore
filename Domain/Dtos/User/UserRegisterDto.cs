namespace repopractise.Domain.Dtos.User
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Type { get; set; } // client or staff
        public string Lastname { get; set; }

    }
}