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


      

        public async Task<TokenInfo> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetSingleOrDefaultAsync(m => m.Email == email);
            if (user == null) return null;

            if (user.PasswordHash != Encryption.Encrypt(password, user.Salt))
                return null;

            var token = new UserToken
            {
                CreatedOn = DateTime.Now,
                ExpiresOn = DateTime.Now.AddMinutes(15),
                Token = Guid.NewGuid().ToString(),
                User = user
            };
            _userTokenRepository.Insert(token);

            var count = await _unitOfWork.SaveAsync();

            if (count == 0)
                return null;
            else
            {
                return new TokenInfo(token);
            }

        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
