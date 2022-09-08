using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using rapsoSuiteMaster.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace rapsoSuiteMaster.Services
{

    public class rapsoTokenServices
    {
        private readonly string issuer;
        private readonly string audience;
        private readonly int addMinutes;
        private readonly SecurityKey securityKey;
        private readonly SigningCredentials credentials;
        private readonly JwtSecurityTokenHandler tokenHandler;

        private readonly IConfiguration _config;
        public readonly string stringToken;

        public rapsoTokenServices(IConfiguration config)
        {
            _config = config;

            issuer = _config["Jwt:Issuer"];
            audience = _config["Jwt:Audience"];
            addMinutes = (int)Convert.ChangeType(_config["Jwt:addMinutes"], typeof(int));
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            JwtSecurityToken token = handler.CreateJwtSecurityToken(
                audience: audience,
                issuer: issuer,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(addMinutes),
                signingCredentials: credentials);

            stringToken = tokenHandler.WriteToken(token);
        }


    }

    public class emailData
    {
        public readonly string fromEmail;
        public readonly string fromUser;
        public readonly string toEmail;
        public readonly string toUser;
        public readonly string subject;
        public readonly string body;

        public emailData(string fromEmail, string fromUser, string toEmail, string toUser, string subject, string body)
        {
            this.fromEmail = fromEmail;
            this.fromUser = fromUser;
            this.toEmail = toEmail;
            this.toUser = toUser;
            this.subject = subject;
            this.body = body;
        }
    }


    public class emailConf
    {
        public readonly string smtp;
        public readonly int port;
        public readonly string userEmail;
        public readonly string passEmail;

        public emailConf(string smtp, int port, string userEmail, string passEmail)
        {
            this.smtp = smtp;
            this.port = port;
            this.userEmail = userEmail;
            this.passEmail = passEmail;
        }
    }

}
