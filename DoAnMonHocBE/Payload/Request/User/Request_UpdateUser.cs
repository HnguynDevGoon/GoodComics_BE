namespace DoAnMonHocBE.PayLoad.Request.User
{
    public class Request_UpdateUser
    {
        public int Id { get; set; }
        public IFormFile? Urlavartar { get; set; }
    }
}
