using BL;
using BO;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using XAct.Messages;

namespace KLTN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IBLAuth _bLAuth;

        public AuthController(IBLAuth bLAuth)
        {
            _bLAuth = bLAuth;
        }

        [HttpPost("signin")]
        public ActionResult SignIn([FromBody] Employee employee)
        {
            ServiceResult serviceResult = new ServiceResult();
            bool check = _bLAuth.CheckAccountExist(employee);

            if (check)
            {
                return Ok(new ServiceResult
                {
                    Success = true,
                    Data = GenerateToken(employee)
                });
            }
            else
            {
                return Ok(new ServiceResult
                {
                    Success = false
                });
            }
        }

        [HttpPost("signup")]
        public ActionResult SignUp([FromBody] Employee employee)
        {
            ServiceResult serviceResult = new ServiceResult();
            bool check = _bLAuth.CheckAccountExist(employee);
            if (!check)
            {
                bool addUser = _bLAuth.AddUser(employee);
                if (addUser)
                {
                    return Ok(new ServiceResult
                    {
                        Success = true
                    });
                }
                else
                {
                    return Ok(new ServiceResult
                    {
                        Success = false,
                        Message = "Có lỗi xảy ra khi thêm vào db"
                    });
                }
            }
            else
            {
                return Ok(new ServiceResult
                {
                    Success = false,
                    Message = "Tài khoản đã tồn tại"
                });
            }
        }

        [HttpPost("refreshtoken")]
        public ActionResult Refreshtoken([FromBody] Employee employee)
        {
            try
            {
                ServiceResult serviceResult = new ServiceResult();
                return Ok(new ServiceResult
                {
                    Success = true,
                    Data = GenerateToken(employee)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResult
                {
                    Success = false,
                    Data = null,
                    Message = "Something went wrong"
                });
            }
        }

        private TokenModel GenerateToken(Employee employee)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("HoangMinhDucAnhHoangMinhDucAnh10");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userName", employee.Username.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);


            TokenModel tokenModel = new TokenModel();
            tokenModel.accessToken = tokenHandler.WriteToken(token);
            tokenModel.refreshToken = GenerateRefreshToken();

            return tokenModel;
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }
    }
}
