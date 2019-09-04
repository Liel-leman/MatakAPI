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

namespace MatakAPI.Controllers
{
    public class AuthController : Controller
    {
  
        [HttpPost("token")]
        public IActionResult Token()
        {
            DbconfigReader DBread = JsonConvert.DeserializeObject<DbconfigReader>(System.IO.File.ReadAllText(@"DbConfig.json"));
            string errorString = null;
            try
            {
                var header = Request.Headers["Authorization"];
                UserModel usrModel = new UserModel();
                List<User> obj = usrModel.getAllUsers(out errorString);

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
                        foreach (var item in obj)
                        {
                            if (userAuth.Email == item.Email)
                                userAuth = item;
                        }
                        var claimsdata = new[] { new Claim("Email",userAuth.Email.ToString())
                                            ,new Claim("FirstName", userAuth.FirstName.ToString())
                                            ,new Claim("LastName", userAuth.OrgId.ToString())
                                            ,new Claim("OrgId", userAuth.OrgId.ToString())
                                            ,new Claim("PermissionId", userAuth.PermissionId.ToString())
                                            ,new Claim("UserId", userAuth.UserId.ToString())
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DBread.JWTencoding));
                        var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                        var token = new JwtSecurityToken(
                            issuer: "http://212.179.205.15/MatakAPI",
                            audience: "http://212.179.205.15/MatakAPI",
                            expires: DateTime.Now.AddDays(3),
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
        [HttpPost("CoresCheck")]
        public IActionResult CoresCheck()
        {
            return Ok("working");
        }
    }
}