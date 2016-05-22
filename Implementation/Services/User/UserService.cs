using Core.DatabaseModels.Security;
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
        public UserService(IUnitOfWork unitOfWork, 
            IRepository<Core.DatabaseModels.Security.User> userRepository) : base(unitOfWork)
        {
            _userRepository = userRepository;
        }


        public Task<Core.DatabaseModels.Security.User> GetUserAsync(int id)
        {
            return _userRepository.GetByIDAsync(id);
        }


        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
