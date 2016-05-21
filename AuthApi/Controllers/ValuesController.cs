using Core.DatabaseModels.Security;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace AuthApi.Controllers
{
    public class ValuesController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<User> _userRepository;
        private IRepository<UserToken> _userTokenRepository;
        public ValuesController(IUnitOfWork unitOfWork, IRepository<User> userRepository, IRepository<UserToken> userTokenRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
        }

        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {

            //var a = await _userRepository.GetByIDAsync(1);

            //  = await _userRepository.GetByIDAsync(1);
            // a.Email = "rssdobiskdsdaro@ggg.coo";
            var tokena = await _userTokenRepository.GetByIDAsync(1);
            // _unitOfWork.Update(a);
            _userTokenRepository.Delete(tokena);
           await _unitOfWork.SaveAsync();


            return new string[] { "value1", "value2" };
        }

    }
}
