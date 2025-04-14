using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using BRCSystem.ClassLibrary.Authentication.Entities;
using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BRCSystem.ClassLibrary.Authorization;

public interface IJwtUtils
{
    public string GenerateJwtToken(User user);
    public int? ValidateJwtToken(string token);
}

public class JwtUtils : IJwtUtils
{
    private readonly AppSettings _appSettings;

    public JwtUtils(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var awsSecretsManager = new AmazonSecretsManagerClient();
        var secretName = "/brc/brc-pos-backend-spcia";


        var request = new GetSecretValueRequest
        {
            SecretId = secretName
        };

        var response = awsSecretsManager.GetSecretValueAsync(request).Result;

        var secretString = response.SecretString;
        var awsSecretsConfig = JsonConvert.DeserializeObject<AwsSecretsConfiguration>(secretString);

        var key = Encoding.ASCII.GetBytes(awsSecretsConfig.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateJwtToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();

        var awsSecretsManager = new AmazonSecretsManagerClient();
        var secretName = "/brc/brc-pos-backend-spcia";


        var request = new GetSecretValueRequest
        {
            SecretId = secretName
        };

        var response = awsSecretsManager.GetSecretValueAsync(request).Result;

        var secretString = response.SecretString;
        var awsSecretsConfig = JsonConvert.DeserializeObject<AwsSecretsConfiguration>(secretString);

        var key = Encoding.ASCII.GetBytes(awsSecretsConfig.Secret);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // return user id from JWT token if validation successful
            return userId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }
}