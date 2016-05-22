using AuthApi.Encryption;
using Core.DatabaseModels.Security;
using Core.DomainModels.Security;
using Core.Interfaces;
using Core.Interfaces.Security;
using Implementation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.User.Services
{
    public class UserService : Service, IUserService
    {

        IRepository<Core.DatabaseModels.Security.User> _userRepository;
        IRepository<Core.DatabaseModels.Security.UserToken> _userTokenRepository;

        public UserService(IUnitOfWork unitOfWork, 
            IRepository<Core.DatabaseModels.Security.User> userRepository,
            IRepository<Core.DatabaseModels.Security.UserToken> userTokenRepository) : base(unitOfWork)
        {
            _userRepository = userRepository;

            _userTokenRepository = userTokenRepository;

        }


        public Task<Core.DatabaseModels.Security.User> GetUserAsync(int id)
        {
            return _userRepository.GetByIDAsync(id);
        }

        public async Task<bool> ValidateToken(string token)
        {
            var dbToken =  await _userTokenRepository.GetSingleOrDefaultAsync(m => m.Token == token);
            if (dbToken == null)
                return false;

            var tokenInfo =  new TokenInfo(dbToken);

            if (tokenInfo.Expired)
                return false;
            else
            {
                dbToken.ExpiresOn = DateTime.Now.AddMinutes(15);
                _userTokenRepository.Update(dbToken);
                await _unitOfWork.SaveAsync();
                return true;
            }

        }

        public async Task<int> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetSingleOrDefaultAsync(m => m.Email == email);
            if (user == null) return 0;

            if (user.PasswordHash != Encryption.Encrypt(password, user.Salt))
                return 0;

            return user.Id;

        }

        public async Task<int> AddUser(CreateUser newUser)
        {
            var user = new Core.DatabaseModels.Security.User();

            user.Email = newUser.Email;
            //generate salt
            user.Salt = Encryption.GetSalt();

            //generate password hash
            user.PasswordHash = Encryption.Encrypt(newUser.Password, user.Salt);


            _userRepository.Insert(user);
            await _unitOfWork.SaveAsync();

            return user.Id;
        }


        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
