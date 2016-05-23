using AuthApi.Encryption;
using Core.DatabaseModels.Security;
using Core.DomainModels.Security;
using Core.Interfaces;
using Core.Interfaces.Security;
using Implementation.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        /// <summary>
        /// Check user credentials and generate token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<TokenInfo> Authenticate(string email, string password)
        {

            var user = await _userRepository.GetSingleOrDefaultAsync(m => m.Email == email, includeProperties: "UserTokens");
        
            if (user == null) return null;
          
            if (user.PasswordHash != Encryption.Encrypt(password, user.Salt))
                return null;

            var dbToken = user.UserTokens.SingleOrDefault(m => m.ExpiresOn > DateTime.Now);

            if (dbToken == null)
            {
                dbToken = new UserToken
                {
                    CreatedOn = DateTime.Now,
                    ExpiresOn = DateTime.Now.AddSeconds(double.Parse(ConfigurationManager.AppSettings["AuthTokenExpirySeconds"])),
                    Token = Guid.NewGuid().ToString(),
                    User = user
                };
                _userTokenRepository.Insert(dbToken);
            }
            else
            {
                dbToken.ExpiresOn = DateTime.Now.AddSeconds(double.Parse(ConfigurationManager.AppSettings["AuthTokenExpirySeconds"]));
                _userTokenRepository.Update(dbToken);
            }

            var count = await _unitOfWork.SaveAsync();

            if (count == 0)
                return null;
            else
            {
                return new TokenInfo(dbToken);
            }

        }

        public async Task<TokenInfo> ValidateToken(string token)
        {

            var dbToken = await _userTokenRepository.GetSingleOrDefaultAsync(m => m.Token == token);

            if (dbToken == null)
                return null;

            return new TokenInfo(dbToken);

        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
