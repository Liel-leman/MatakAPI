using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MatakDBConnector;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MatakAPI.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost("token")]
        public IActionResult Token()
        {
            string errorString = null;
            var header = Request.Headers["Authorization"];
            UserModel usrModel = new UserModel();
            User user = new User();
            

            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic ".Length).Trim();
                var usernameAndPassENC = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));//admin:pass
                var usernameAndPass = usernameAndPassENC.Split(":");
                User userAuth = new User();
                userAuth.Email = usernameAndPass[0];
                userAuth.Password = usernameAndPass[1];
                if (usrModel.authenticateUser(usernameAndPass[0],usernameAndPass[1],out errorString))
                {
                    
                    var claimsdata = new[] { new Claim("role", usernameAndPass[0])
                                            ,new Claim("usrId","100")};// יהיה מתודה שמחזירה את שם הארגון אם הוא מת"ק usernameAndPass[0] 
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretPass"));
                    var signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                    var token = new JwtSecurityToken(
                        issuer: "MatakAPP.com",//????
                        audience: "MatakAPP.com",//????????
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
    }
}