using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using rapsoSuiteMaster.Data;
using rapsoSuiteMaster.interfaces;
using rapsoSuiteMaster.Models;
using rapsoSuiteMaster.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using static rapsoSuiteMaster.Controllers.Admin.NetUsersController;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace rapsoSuiteMaster.WebApi
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class userApiController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly UserManager<IdentityUser> _userMgr;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly userServices _userServices;


        public userApiController(SignInManager<IdentityUser> signInManager, IConfiguration config, UserManager<IdentityUser> userMgr, userServices userServices)
        {
            _userMgr = userMgr;
            _signInManager = signInManager;
            _config = config;
            _userServices = userServices;
        }

        #region "Region Anonymous"
        [HttpPost("v{version:apiVersion}/getToken")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<IActionResult> getToken([FromBody] userRapsoSuites _usuario)
        {
            taskResponse validateUser;

            var identityUsr = await _userMgr.FindByEmailAsync(_usuario.email);

            if (await _userMgr.CheckPasswordAsync(identityUsr, _usuario.password))
            {
                var IsInRoleAsync = await _userMgr.IsInRoleAsync(identityUsr, "userRapsoSuites");
                if (IsInRoleAsync == true)
                {
                    rapsoTokenServices _rapsoTokenServices = new(_config);
                    validateUser = new("getToken", "Ok", _rapsoTokenServices.stringToken);
                    return Ok(validateUser);
                }
            }

            validateUser = new("getToken", "Fail", "User or Password Fail");
            return Ok(validateUser);

        }

        [HttpPost("v{version:apiVersion}/validateUserName/{userName}")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<taskResponse> validateUserName(string userName)
        {
            return await _userServices.validateUserName(userName);
        }

        [HttpPost("v{version:apiVersion}/validateEmail/{email}")]
        [ApiVersion("1.0")]
        [AllowAnonymous]
        public async Task<taskResponse> validateEmail(string email)
        {
            return await _userServices.validateEmail(email);
        }
        #endregion

        #region "Authorize"
        [HttpPost("v{version:apiVersion}/createUser")]
        [ApiVersion("1.0")]
        public async Task<IActionResult> createUser([FromBody] basicNetUserInfo _basicNetUserInfo)
        {
            var resp = await _userServices.createUser(_basicNetUserInfo, _basicNetUserInfo.password);
            return Ok(resp);

        }

        #endregion

        #region "aprendizaje"
        [AllowAnonymous]
        [HttpGet("leeApi")]
        public async Task<string> leeApi()
        {
            using (var client = new HttpClient())
            {
                string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NTkxMDc1MzYsImV4cCI6MTY1OTEwNzU5NiwiaWF0IjoxNjU5MTA3NTM2LCJpc3MiOiJXZWJBcGlKd3QuY29tIiwiYXVkIjoibG9jYWxob3N0In0.1PhG_nuM0tF8uUpLHfUD1pJSGv_fkT7p-RNFB2edBvg";
                var url = "https://localhost:44327/netroles";
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    var response = await client.GetStringAsync(url);
                    return response;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                    throw;
                }
            }
        }

        [AllowAnonymous]
        [HttpGet("abreVentana")]
        public async Task<IActionResult> abreVentana()
        {
            using (var client = new HttpClient())
            {

                string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NTkxMDgxMzIsImV4cCI6MTY1OTExMTczMiwiaWF0IjoxNjU5MTA4MTMyLCJpc3MiOiJXZWJBcGlKd3QuY29tIiwiYXVkIjoibG9jYWxob3N0In0.B098pUKzXiZ1Me9widsfroTT2mO5YIr5_xpAHJBEHv4";
                var url = "https://localhost:44327/netroles";
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    return RedirectToAction("index", "netroles");

                }
                catch (Exception ex)
                {
                    return null;
                    throw;
                }
            }
        }
        [HttpGet("user")]
        public IEnumerable<string> user()
        {
            try
            {
                //var id = "5addac29-798b-40c0-9fa6-ea5c9cacbca3";
                //Guid _Guid = new Guid(id);

                //var _user = _Iroles.Read<AspNetRole>(_Iroles.getContext().AspNetRoles, id).Result;

                //string jsonRole = JsonSerializer.Serialize(_user.Item3);

                //switch (_user.Item2)
                //{
                //    case 1:
                //        return new string[] { "User Found it", "1", jsonRole };
                //    default:
                //        return new string[] { "Not Found it", "0", "" };
                //}
            }
            catch (Exception ex)
            {
                return new string[] { "Error", ex.Message.ToString() };
            }




            return new string[] { "value666", "value777" };
        }
        #endregion





    }
}
