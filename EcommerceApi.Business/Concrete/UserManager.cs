using AutoMapper;
using EcommerceApi.Business.Abstract;
using EcommerceApi.Core.Utilities.Business;
using EcommerceApi.Core.Utilities.Result.Abstract;
using EcommerceApi.Core.Utilities.Result.Concrete.ErrorResult;
using EcommerceApi.Core.Utilities.Result.Concrete.SuccessResult;
using EcommerceApi.Core.Utilities.Security.Hashing;
using EcommerceApi.Core.Utilities.Security.JWT;
using EcommerceApi.DataAccess.Abstract;
using EcommerceApi.Entities.Concrete;
using EcommerceApi.Entities.DTOs.UserDTOs;
using EcommerceApi.Entities.SharedModels;
using MassTransit;

namespace EcommerceApi.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDAL _userDAL;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public UserManager(IUserDAL userDAL, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _userDAL = userDAL;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        //CheckUserPasswordVerify burada  istifade olundu
        // İstifadəçinin giriş etməsini təmin edən metod.
        public IResult Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                // İş qaydalarını yoxlamaq üçün Check metodu istifadə edilir.
                var result = BusinessRules.Check(CheckUserConfirmedEmail(userLoginDTO.Email),
                    CheckUserPasswordVerify(userLoginDTO.Email, userLoginDTO.Password),
                    CheckUserLoginAttempt(userLoginDTO.Email));

                // Giriş etməyə çalışan istifadəçinin məlumatlarını verilənlər bazasından çəkir.
                var user = _userDAL.Get(x => x.Email == userLoginDTO.Email);

                // İş qaydalarından hər hansı biri uğurlu olmazsa xəta qaytarılır.
                if (!result.Success)
                    return new ErrorResult("Email is not confirmed!");

                // İstifadəçi varsa xəta qaytarılır və loginAttempt artırılır.
                if (CheckUserExist(userLoginDTO.Email).Success)
                {
                    user.LoginAttempt += 1;
                    return new ErrorResult("User does not exist");
                }

                user.LoginAttempt = 0;

                // Uğurlu girişdən sonra istifadəçiyə JWT token yaratır.
                var token = Token.TokenGenerator(user, "User");

                // Token ilə SuccessResult qaytarılır.
                return new SuccessResult(token);
            }
            catch (Exception ex)
            {
                // Xəta halında təfərrüatlı loglama edilərək ErrorResult qaytarılır.
                // LogException(ex);
                return new ErrorResult("An error occurred during login. Please try again later.");
            }
        }

        // Yeni bir istifadəçi qeydiyyatı yaradan metod.
        public IResult Register(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                // İstifadəçinin varlığını yoxlamaq üçün Check metodu istifadə edilir.
                //Meselen burda CheckUserExist istifade edilmesi
                var result = BusinessRules.Check(CheckUserExist(userRegisterDTO.Email));

                // Əgər istifadəçi artıq varsa ErrorResult qaytarılır.
                if (!result.Success)
                    return new ErrorResult("Email exists");

                // UserRegisterDTO'yu User entitysinə çevirir.
                var map = _mapper.Map<User>(userRegisterDTO);

                // Şifrəni hashləyərək PasswordHash və PasswordSalt dəyərlərini yaratır.
                HashingHelper.HashPassword(userRegisterDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

                // Yaradılan dəyərləri User entitysinə əlavə edir.
                map.PasswordSalt = passwordSalt;
                map.PasswordHash = passwordHash;

                // İstifadəçiyə xüsusi bir token yaratır.
                map.Token = Guid.NewGuid().ToString();

                // Tokenin müddətini təyin edir.
                map.TokenExpiresDate = DateTime.Now.AddMinutes(10);

                // İstifadəçini verilənlər bazasına əlavə edir.
                _userDAL.Add(map);

                SendEmailCommand sendEmailCommand = new()
                {
                    Lastname = map.LastName,
                    Firstname = map.FirstName,
                    Token = map.Token,
                    Email = map.Email
                };

                _publishEndpoint.Publish<SendEmailCommand>(sendEmailCommand);

                // Uğurlu bir şəkildə qeydiyyatdan keçdiyini göstərmək üçün SuccessResult qaytarılır.
                return new SuccessResult("User registered successfully");
            }
            catch (Exception ex)
            {
                // Xəta halında təfərrüatlı loglama edilərək ErrorResult qaytarılır.
                // LogException(ex);

                return new ErrorResult("An error occurred during registration. Please try again later.");
            }
        }

        // İstifadəçinin emailini və təsdiq tokenını istifadə edərək email ünvanını təsdiqləyən metod.
        public IResult VerifyEmail(string email, string verifyToken)
        {
            try
            {
                // Email ünvanına sahib istifadəçini verilənlər bazasından çəkir.
                var user = _userDAL.Get(x => x.Email == email);

                // İstifadəçi yoxdursa ErrorResult qaytarılır.
                if (user == null) return new ErrorResult("User does not exist");

                // Token düzgünsə və müddəti bitməmişsə, istifadəçinin emailConfirmed dəyərini true edir.
                if (user.Token == verifyToken)
                {
                    if (DateTime.Compare(user.TokenExpiresDate, DateTime.Now) < 0)
                    {
                        return new ErrorResult("Token has expired");
                    }
                    user.EmailConfirmed = true;
                    _userDAL.Update(user);
                    return new SuccessResult("Email verification successful");
                }

                // Token səhvdirsə ErrorResult qaytarılır.
                return new ErrorResult("Invalid verification token");
            }
            catch (Exception ex)
            {
                // Xəta halında təfərrüatlı loglama edilərək ErrorResult qaytarılır.
                // LogException(ex);

                return new ErrorResult("An error occurred during email verification. Please try again later.");
            }
        }

        // İstifadəçinin emaili təsdiqlənməmişdirsə, verilənlər bazasından bu emaili silən metod.
        private IResult CheckUserConfirmedEmail(string email)
        {
            try
            {
                var user = _userDAL.Get(x => x.Email == email);

                // Əgər istifadəçinin emailConfirmed dəyəri false-dirsmi, istifadəçini verilənlər bazasından silir.
                if (!user.EmailConfirmed)
                {
                    user.TokenExpiresDate = DateTime.Now.AddMinutes(10);
                    SendEmailCommand sendEmailCommand = new()
                    {
                        Lastname = user.LastName,
                        Firstname = user.FirstName,
                        Token = user.Token,
                        Email = user.Email
                    };

                    _publishEndpoint.Publish<SendEmailCommand>(sendEmailCommand);
                }

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                // Xəta halında təfərrüatlı loglama edilərək ErrorResult qaytarılır.
                // LogException(ex);
                return new ErrorResult("An error occurred during email confirmation check.");
            }
        }

        // İstifadəçinin var olub olmadığını yoxlayan metod.
        // Bu methodu hazir yazib her defe cagirib yoxlayiriq
        private IResult CheckUserExist(string email)
        {
            try
            {
                var user = _userDAL.Get(x => x.Email == email);

                // Əgər istifadəçi varsa ErrorResult qaytarılır.
                if (user != null)
                    return new ErrorResult();

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                // Xəta halında təfərrüatlı loglama edilərək ErrorResult qaytarılır.
                // LogException(ex);

                return new ErrorResult("An error occurred during user existence check.");
            }
        }

        // İstifadəçinin şifrəsini doğrulayan metod.
        //Login olarken istifade edirik
        private IResult CheckUserPasswordVerify(string email, string password)
        {
            try
            {
                var user = _userDAL.Get(x => x.Email == email);
                var result = HashingHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);

                // Əgər şifrə doğru deyilsə ErrorResult qaytarılır.
                if (!result)
                    return new ErrorResult("Email or password is incorrect!");

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                // Xəta halında təfərrüatlı loglama edilərək ErrorResult qaytarılır.
                // LogException(ex);

                return new ErrorResult("An error occurred during password verification.");
            }
        }

        // İstifadəçinin giriş cəhdi (attempt) durumunu yoxlayan metod.
        //Login Attempt eklemek
        private IResult CheckUserLoginAttempt(string email)
        {
            try
            {
                var user = _userDAL.Get(x => x.Email == email);

                // Əgər istifadəçinin giriş cəhdi sayı 3-dən çoxdursa və müddəti bitməmişsə ErrorResult qaytarılır.
                if (user.LoginAttempt > 3)
                {
                    if (user.LoginAttemptExpires == null)
                    {
                        user.LoginAttemptExpires = DateTime.Now.AddMinutes(10);
                    }
                    return new ErrorResult("Login attempt exceeded. Please wait for 10 minutes.");
                }
                // Əgər müddəti bitmişsə SuccessResult qaytarılır.
                if (DateTime.Compare(user.LoginAttemptExpires, DateTime.Now) < 0)
                {
                    return new SuccessResult();
                }

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                // Xəta halında təfərrüatlı loglama edilərək ErrorResult qaytarılır.
                // LogException(ex);

                return new ErrorResult("An error occurred during login attempt check.");
            }
        }

        public IDataResult<UserOrderDTO> GetUserOrders(int userId)
        {
            var result = _userDAL.GetUserOrders(userId);
            var map = _mapper.Map<UserOrderDTO>(result);
            return new SuccessDataResult<UserOrderDTO>(map);
        }
    }
}
