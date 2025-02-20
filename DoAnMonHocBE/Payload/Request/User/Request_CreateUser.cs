namespace DoAnMonHocBE.PayLoad.Request.User
{
    public class Request_CreateUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IFormFile? Urlavartar { get; set; }
    }
}
