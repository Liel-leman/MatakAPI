using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MatakAPI.Models;
using MatakDBConnector;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MatakAPI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("token")]
        public IActionResult Token()
        {
            string errorString = null;
            try
            {
                var header = Request.Headers["Authorization"];
                UserModel usrModel = new UserModel();

                if (header.ToString().StartsWith("Basic"))
                {
                    var credValue = header.ToString().Substring("Basic ".Length).Trim();
                    var usernameAndPassENC = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));//admin:pass
                    var usernameAndPass = usernameAndPassENC.Split(":");
                    User userAuth = new User();
                    userAuth.Email = usernameAndPass[0];
                    userAuth.Password = usernameAndPass[1];
                    if (usrModel.authenticateUser(userAuth.Email, userAuth.Password, out errorString))
                    {
                        userAuth = usrModel.getUserByEmail(userAuth.Email,out errorString);


                        var claimsdata = new[] { new Claim("Email",userAuth.Email.ToString())
                                            ,new Claim("FirstName", userAuth.FirstName.ToString())
                                            ,new Claim("LastName", userAuth.OrgId.ToString())
                                            ,new Claim("OrgId", userAuth.OrgId.ToString())
                                            ,new Claim("PermissionId", userAuth.PermissionId.ToString())
                                            ,new Claim("UserId", userAuth.UserId.ToString())
                        };


                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));//***problem with the serv
                        var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                        var token = new JwtSecurityToken(
                            issuer: "http://212.179.205.15/MatakAPI",
                            audience: "http://212.179.205.15/MatakAPI",
                            expires: DateTime.Now.AddMinutes(30),
                            claims: claimsdata,
                            signingCredentials: signInCred
                            );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(tokenString);
                    }
                }
                
                return BadRequest("wrong request");
            }
            catch (Exception e)
            {
                return BadRequest(e + "\n" + errorString);
            }
        }



        /*
        [HttpPost("{refreshToken}/refresh")]
        public IActionResult RefreshToken([FromRoute]string refreshToken)
        {
            int userID = // TODO new RefreshTokenModel.chekvalidity(refreshToken);
                
            if (userID == null)
            {
                return NotFound("Refresh token not found");
            }
            User userAuth = new UserModel().getUserByUserId(userID); 

            var userclaim = new[] { new Claim("Email",userAuth.Email.ToString())
                                            ,new Claim("FirstName", userAuth.FirstName.ToString())
                                            ,new Claim("LastName", userAuth.OrgId.ToString())
                                            ,new Claim("OrgId", userAuth.OrgId.ToString())
                                            ,new Claim("PermissionId", userAuth.PermissionId.ToString())
                                            ,new Claim("UserId", userAuth.UserId.ToString())
                        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            Refreshtoken = Guid.NewGuid().ToString();

            

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), refToken = _refreshToken.Refreshtoken });
        }
        
    */




        [HttpPost("CoresCheck")]
        public IActionResult CoresCheck()
        {
            return Ok("working");
        }



    }



}