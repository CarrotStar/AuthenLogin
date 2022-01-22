using AuthenLoginDeploy.Entities;
using AuthenLoginDeploy.Models;
using AuthenLoginDeploy.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuthenLoginDeploy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        protected readonly AuthenDbContext DbContext;

        private readonly IConfiguration _configuration;

        public UsersController(ILogger<UsersController> logger, AuthenDbContext dbContext, IConfiguration configuration)
        {
            _logger = logger;
            DbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public resultinfo GetUserLogin(TblUsersInfo GetUserLogin)
        {

            resultinfo luser = DbContext.GetUserLogin(GetUserLogin);

            return luser;
        }
        [HttpPost("insertUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public bool insertUser(TblUsersInfo Userinfo)
        {

            bool updateSuccess = DbContext.insertUser(Userinfo);

            return updateSuccess;

        }
        [HttpPost("UpdateUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public bool UpdateUser(TblUsersInfo Userinfo)
        {
            bool updateSuccess = DbContext.UpdateUser(Userinfo);
            return updateSuccess;

        }
    }
}