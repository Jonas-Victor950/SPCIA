using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BRCSystem.ClassLibrary.Data
{
    public static class DbContextExtensions
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var awsSecretsManager = new AmazonSecretsManagerClient();
            var secretName = "/brc/brc-pos-backend-spcia";


            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            var response = awsSecretsManager.GetSecretValueAsync(request).Result;

            var secretString = response.SecretString;
            var awsSecretsConfig = JsonConvert.DeserializeObject<AwsSecretsConfiguration>(secretString);
            var connectionString = awsSecretsConfig.DefaultConnection;

            services.AddDbContext<DataContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b =>
                {
                    b.MigrationsAssembly("BRCSystem.Models");
                    b.EnableStringComparisonTranslations();
                })
            );

            return services;
        }
    }
}
