﻿

namespace DoAnMonHocBE.PayLoad.DTO
{
    public class DTO_User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Urlavartar { get; set; }
        public int RoleId { get; set; }

    }
}
