using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using Web.Utils;

[assembly: HostingStartup(typeof(Web.Areas.Identity.IdentityHostingStartup))]
namespace Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {

        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DataContext>(options => {
                        options.UseSqlite(context.Configuration.GetConnectionString("WebContextConnection"), b => b.MigrationsAssembly("Web"));
                    }
                );


                services.AddIdentity<Data.User, IdentityRole>(options =>
                {
                    options.Stores.MaxLengthForKeys = 128;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                  .AddEntityFrameworkStores<DataContext>()
                  .AddDefaultTokenProviders();


                /*services.AddAuthentication()
                .AddCookie(cfg =>
                {
                    //cfg.LoginPath = "/Identity/Account/Login";
                    cfg.SlidingExpiration = true;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidAudience = AuthOptions.AUDIENCE,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    };
                });*/

                services.AddTransient<IEmailSender, EmailSender>();
            });
        }
    }
}