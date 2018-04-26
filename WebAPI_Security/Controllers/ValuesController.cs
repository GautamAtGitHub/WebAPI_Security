using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI_Security.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        ///Method to generate Sample tocken. This is sample tocken generator method
        ///In case of production environment tocken can be generated from forms authentication 
        ///or from secure tocken server
        ///or from OAuth server
        ///
        [Route("GetToken")]
        [HttpGet]
        public IActionResult GetToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "jeremy@jeremylikness.com"),
                new Claim(JwtRegisteredClaimNames.Jti, System.Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Startup.SecretKEY_AES256));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("http://localhost:5001",
                "http://localhost:5000",
                claims,
                expires: System.DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var tokenEncoded = new JwtSecurityTokenHandler().WriteToken(token);

            return new OkObjectResult(new { token = tokenEncoded });
        }
    }
}
