using Azure.Core;
using DoAnMonHocBE.DataContext;
using DoAnMonHocBE.Entities;
using DoAnMonHocBE.Handle;
using DoAnMonHocBE.PayLoad.Converter;
using DoAnMonHocBE.PayLoad.DTO;
using DoAnMonHocBE.PayLoad.Request.User;
using DoAnMonHocBE.PayLoad.Response;
using DoAnMonHocBE.Service.Interfaces;
using System.Data;

namespace DoAnMonHocBE.Service.Implements
{
    public class Service_User : IService_User
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseBase responseBase;
        private readonly ResponseObject<DTO_User> responseObject;
        private readonly Converter_User converter_User;

        public Service_User(AppDbContext dbContext, ResponseBase responseBase, ResponseObject<DTO_User> responseObject, Converter_User converter_User)
        {
            this.dbContext = dbContext;
            this.responseBase = responseBase;
            this.responseObject = responseObject;
            this.converter_User = converter_User;
        }

        public ResponseBase ChangePassword(int userId, string oldPass, string newPass)
        {
            if (string.IsNullOrWhiteSpace(oldPass) || string.IsNullOrWhiteSpace(newPass))
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Giá trị nhập không hợp lệ");
            }

            var user = dbContext.users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return responseBase.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại!");
            }

            string checkPassword = CheckInput.IsPassWord(newPass);
            if (checkPassword != newPass)
            {
                return responseBase.ResponseError(400, checkPassword);
            }

            if (BCrypt.Net.BCrypt.Verify(oldPass, user.Password)) 
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(newPass);
                dbContext.users.Update(user);
                dbContext.SaveChanges();

                return responseBase.ResponseSuccess("Đổi mật khẩu thành công!");
            }

            return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Sai mật khẩu cũ!");
        }

        public ResponseBase CheckOtp(int otp)
        {
            var confirmEmail = dbContext.confirmemails.FirstOrDefault(x => x.Code == otp.ToString());
            if (confirmEmail == null)
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Mã OTP không hợp lệ !");
            }

            if (DateTime.Now > confirmEmail.Expiredtime)
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Mã OTP đã hết hạn !");
            }

            var user = dbContext.users.FirstOrDefault(x => x.Id == confirmEmail.UserId);

            return responseBase.ResponseSuccess($"{user.Id}");
        }

        public ResponseBase CreateUser(Request_CreateUser request)
        {
            if (dbContext.users.Any(x => x.Username == request.Username))
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Tên đăng nhập đã tồn tại");
            }
            string checkUsername = CheckInput.IsValidUsername(request.Username);
            if (checkUsername != request.Username)
            {
                return responseBase.ResponseError(400, checkUsername);
            }
            string checkPassword = CheckInput.IsPassWord(request.Password);

            if (checkPassword != request.Password)
            {
                return responseBase.ResponseError(400, checkPassword);
            }

            bool checkEmail = CheckInput.IsValiEmail(request.Email);
            if (!checkEmail)
            {
                return responseBase.ResponseError(400, "Email không hợp lệ !");
            }

            int emailCount = dbContext.users.Count(x => x.Email == request.Email);
            if (emailCount >= 1)
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, 
                                                "Email đã có quá 1 tài khoản đăng ký. Vui lòng chọn email khác !");
            }

            var userAccount = new User()
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email,
            };

            string UrlAvt = null;
            if (request.Urlavartar == null)
            {
                UrlAvt = "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=";
            }
            else
            {

                if (!CheckInput.IsImage(request.Urlavartar))
                {
                    return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ !");
                }
                CloudinaryService cloudinaryService = new CloudinaryService();
                UrlAvt = cloudinaryService.UploadImage(request.Urlavartar);
            }
            userAccount.Urlavartar = UrlAvt;


            dbContext.users.Add(userAccount);
            dbContext.SaveChanges();

            // Email To
            //Random r = new Random();
            //int code = r.Next(100000, 999999);
            //var emailTo = new EmailTo();
            //emailTo.Mail = request.Email;
            //emailTo.Subject = "Nhận mã";
            //emailTo.Content = $"Mã xác nhận của bạn là: {code}. Mã của bạn sẽ hết hạn sau 2 phút !";
            //emailTo.SendEmailAsync(emailTo);

            // ConfirmEmail
            //var confirmEmail = new ConfirmEmail();
            //confirmEmail.Code = code.ToString();
            //confirmEmail.Message = "Xác nhận đăng kí !";
            //confirmEmail.Starttime = DateTime.Now;
            //confirmEmail.Expiredtime = DateTime.Now.AddMinutes(2);
            //confirmEmail.UserId = userAccount.Id;
            //dbContext.confirmemails.Add(confirmEmail);
            //dbContext.SaveChanges();


            return responseBase.ResponseSuccess("Đăng ký tài khoản thành công !");
        }

        public ResponseObject<DTO_User> DeleteUser(int userId)
        {
            var searchUserId = dbContext.users.FirstOrDefault(x => x.Id == userId);
            if (searchUserId == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "User không tồn tại", null);
            }
            dbContext.users.Remove(searchUserId);
            dbContext.SaveChanges();
            return responseObject.ResponseObjectSuccess("Xóa thành công !", null);
        }

        public ResponseBase ForgotPassword(string email)
        {
           var user = dbContext.users.FirstOrDefault(x => x.Email == email);
            if (user == null) 
            {
                return  responseBase.ResponseError(StatusCodes.Status404NotFound, "Email này chưa tạo tài khoản !");
            }

            Random r = new Random();
            int code = r.Next(100000, 999999);
            var emailTo = new EmailTo();
            emailTo.Mail = email;
            emailTo.Subject = "Nhận mã";
            emailTo.Content = $"Mã xác nhận của bạn là: {code}. Mã của bạn sẽ hết hạn sau 2 phút !";
            emailTo.SendEmailAsync(emailTo);

            var confirmEmail = new ConfirmEmail();
            confirmEmail.Code = code.ToString();
            confirmEmail.Message = "Xác nhận mã";
            confirmEmail.Starttime = DateTime.Now;
            confirmEmail.Expiredtime = DateTime.Now.AddMinutes(2);
            confirmEmail.UserId = user.Id;
            dbContext.confirmemails.Add(confirmEmail);
            dbContext.SaveChanges();

            return responseBase.ResponseSuccess("Mã OTP đang gửi vào email của bạn !");

        }

        public IQueryable<DTO_User> GetListUser(int pageSize, int pageNumber)
        {
            return dbContext.users.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => converter_User.EntityToDTO(x));
        }

        public ResponseObject<DTO_User> GetUserById(int userId)
        {
            var searchUserId = dbContext.users.FirstOrDefault(x => x.Id == userId);
            if (searchUserId == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Không tìm thấy user", null);
            }

            // Chuyển đổi role entity thành DTO
            var userDto = converter_User.EntityToDTO(searchUserId);

            return responseObject.ResponseObjectSuccess("Tìm user thành công", userDto);
        }

        public ResponseBase UpdatePassAfterOtp(int userId, string newPass, string confirmPass)
        {
            if (string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu không được để trống !");
            }

            var user = dbContext.users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return responseBase.ResponseError(StatusCodes.Status404NotFound, "Không có user nào được tìm thấy !");
            }

            string checkPassword = CheckInput.IsPassWord(newPass);
            if (checkPassword != newPass)
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, checkPassword);
            }

            if (confirmPass != newPass)
            {
                return responseBase.ResponseError(StatusCodes.Status400BadRequest, "Xác nhận mật khẩu sai !");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPass);

            dbContext.users.Update(user);
            dbContext.SaveChanges();

            return responseBase.ResponseSuccess("Đổi mật khẩu thành công !");
        }


        public ResponseObject<DTO_User> UpdateAvatar(Request_UpdateUser request)
        {
            var user = dbContext.users.FirstOrDefault(x => x.Id == request.Id);
            if (user == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status404NotFound, "Không tìm thấy user", null);
            }

            if (request.Urlavartar != null)
            {
                CloudinaryService cloudinaryService = new CloudinaryService();
                user.Urlavartar = cloudinaryService.UploadImage(request.Urlavartar);
            }

            dbContext.users.Update(user);
            dbContext.SaveChanges();

            var userDto = converter_User.EntityToDTO(user);
            return responseObject.ResponseObjectSuccess("Cập nhật ảnh đại diện thành công!", userDto);
        }



        public ResponseObject<DTO_User> UserLogin(Request_Login request)
        {
            if (string.IsNullOrWhiteSpace(request.Username)
                || string.IsNullOrWhiteSpace(request.Password))
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Giá trị nhập không hợp lệ", null);
            }

            var user = dbContext.users.FirstOrDefault(x => x.Username == request.Username);
            if (user == null)
            {
                return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Tên tài khoản hoặc mật khẩu không hợp lệ!", null);
            }

            if (BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return responseObject.ResponseObjectSuccess("Xác nhận mật khẩu thành công", converter_User.EntityToDTO(user));
            }
            return responseObject.ResponseObjectError(StatusCodes.Status400BadRequest, "Sai mật khẩu", null);

        }

        //public ResponseBase AccountVerification(string code)
        //{
        //    var confirmEmail = dbContext.confirmemails.FirstOrDefault(x => x.Code.Equals(code));
        //    if (confirmEmail == null)
        //    {
        //        return responseBase.ResponseError(400, "Mã xác nhận không đúng !"); 
        //    }
        //    var nguoiDung = dbContext.users.FirstOrDefault(x => x.Id == confirmEmail.UserId);
        //    nguoiDung.Active = true;
        //    dbContext.users.Update(nguoiDung);
        //    dbContext.confirmemails.Update(confirmEmail);
        //    dbContext.SaveChanges();
        //    return responseBase.ResponseSuccess("Xác thực thành công !");
        //}





    }
}
