using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class JwtConfig
{
    public static void AddJwtConfig(this IServiceCollection service)
    {
        var key=Encoding.ASCII.GetBytes( "HVvmZi/Zd/Yg8l/PQ6Fh/THTgTPmyB3QfRbZjtKKqwE=");
        service.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey =true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        })
        ;
        
    }
}