using DoAnMonHocBE.PayLoad.DTO;
using DoAnMonHocBE.PayLoad.Request.User;
using DoAnMonHocBE.PayLoad.Response;

namespace DoAnMonHocBE.Service.Interfaces
{
    public interface IService_User
    {
        public ResponseBase CreateUser(Request_CreateUser request);
        public IQueryable<DTO_User> GetListUser(int pageSize, int pageNumber);
        public ResponseObject<DTO_User> GetUserById(int userId);
        public ResponseObject<DTO_User> DeleteUser(int userId);
        public ResponseBase ChangePassword(int userId, string oldPass, string newPass);
        public ResponseObject<DTO_User> UpdateAvatar(Request_UpdateUser request);
        public ResponseBase AccountVerification(string code);
        public ResponseObject<DTO_User> UserLogin(Request_Login request);

        public ResponseBase ForgotPassword(string email);
        public ResponseBase CheckOtp(int otp);
        public ResponseBase UpdatePassAfterOtp(int userId ,string newPass, string confirmPass);
    }
}
