using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Store.Sts.Background;
using Store.Sts.Data;
using Store.Sts.Model;
using System;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Store.Sts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store.STS", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var cs = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite();
                options.UseOpenIddict();
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "Store.STS", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var cs = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlite();
                options.UseOpenIddict();
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = Claims.Role;
                options.ClaimsIdentity.EmailClaimType = Claims.Email;
            });

            services.AddOpenIddict()
                .AddCore(options => { options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>(); })
                .AddServer(options =>
                {
                    options.UseDataProtection()
                       .PreferDefaultAccessTokenFormat()
                       .PreferDefaultRefreshTokenFormat()
                       .PreferDefaultUserCodeFormat();
                    // ��������� ������ ��������� 
                    options
                        .SetTokenEndpointUris("/connect/token")
                        .SetIntrospectionEndpointUris("/connect/introspect")
                        .SetUserinfoEndpointUris("/connect/userinfo");
                    options
                        .AllowPasswordFlow()
                        .AllowRefreshTokenFlow()
                        .AllowCustomFlow("verification_token");
                    options
                        .AddEphemeralEncryptionKey()
                        .AddEphemeralSigningKey()
                        .DisableAccessTokenEncryption();
                    options.UseReferenceAccessTokens()
                        .UseReferenceRefreshTokens();
                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(60));
                    options.SetRefreshTokenLifetime(TimeSpan.FromMinutes(30));
                    options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles, "api");
                    options.UseAspNetCore()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserinfoEndpointPassthrough()
                       .DisableTransportSecurityRequirement();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                    options.UseDataProtection();
                });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = Schemes.Bearer;
                options.DefaultChallengeScheme = Schemes.Bearer;
            });


            services.AddHostedService<TestData>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store.Sts v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
