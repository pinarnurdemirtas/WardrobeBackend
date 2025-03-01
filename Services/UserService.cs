using WardrobeBackend;
using WardrobeBackend.Model;
using WardrobeBackend.Repositories;
using WardrobeBackend.Repositories;
using static WardrobeBackend.Security;

namespace WardrobeBackend.Services
{
    public class UserService
    {

        private readonly IUserRepository _userRepository;
        private readonly Security _security;
        private readonly EmailService _emailService;

        public UserService(IUserRepository userRepository, Security security, EmailService emailService)
        {
            _userRepository = userRepository;
            _security = security;
            _emailService = emailService;
        }

        public async Task<(bool Success, string Message, string Token, Users User)> LoginUser(LoginModel loginUser)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginUser.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
                return (false, "Geçersiz kullanıcı adı veya şifre.", null, null);
            }

            if (!user.Is_verified)
            {
                return (false, "Hesabınız doğrulanmamış.", null, null);
            }

            var token = _security.CreateToken(user);
            return (true, "Giriş başarılı.", token, user);
        }

        public async Task<bool> RegisterUser(Users newUser)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(newUser.Username);
            if (existingUser != null)
                return false;

            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Is_verified = false;
            
            bool result = await _userRepository.AddUserAsync(newUser);
            if (!result) return false;

            var verificationUrl = $"http://192.168.192.62:5000/User/register/verify/{newUser.Username}";
            
            await _emailService.SendConfirmationEmail(newUser.Email, verificationUrl, newUser.Fullname);

            return true;
        }

        public async Task<bool> VerifyUser(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || user.Is_verified) return false;

            user.Is_verified = true;
            bool result = await _userRepository.UpdateUserAsync(user);
            
            if (result)
                await _emailService.SendAccountVerifiedEmail(user.Email);

            return result;
        }
        
        public async Task<List<Users>> GetAllUsers()
        {
            return await _userRepository.GetAllUsersAsync(); 
        }


    }
}
