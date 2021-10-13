using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using Store.Sts.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Sts.Background
{
    public class TestData : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        
        public TestData(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope =  _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.EnsureCreatedAsync(cancellationToken);
            var manager = scope.ServiceProvider.GetRequiredService < IOpenIddictApplicationManager> ();

            if (await manager.FindByClientIdAsync("admin-client") is null)

                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "admin-client",
                    ClientSecret = "e2249b2e-1752-4f12-96eb-c8c0ccf9abac",
                    DisplayName = "Default mobile application",
                    Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Permissions.Prefixes.GrantType+"verification_token",
                    OpenIddictConstants.Permissions.Prefixes.Scope+"mobile-api"
                }
                });


            if (await manager.FindByClientIdAsync("mobile-client") is null)
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "mobile-client",
                    ClientSecret = "499056FA-8478-5199-BA61-82980431C318",
                    DisplayName = "Default mobile application",
                    Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                    OpenIddictConstants.Permissions.Prefixes.GrantType+"verification_token",
                    OpenIddictConstants.Permissions.Prefixes.Scope+"mobile-api"
                    

                }
                });

            await CreateIntrospectionClient(manager, "test-resource", "d023b5eb-a021-4e53-875e-72b7f69d0f73", cancellationToken);
            await CreateIntrospectionClient(manager, "product-resource", "92ac5e71-4bbc-45a9-9267-6da2ef2e7b6d", cancellationToken);
            await CreateIntrospectionClient(manager, "localization-resource", "48e54d51-a024-4198-b9ff-a6c1d32c88d8", cancellationToken);
            await CreateIntrospectionClient(manager, "order-resource", "9343fe4c-ac1d-4b18-beec-16ed337324e9", cancellationToken);
            await CreateScopesAsync(scope, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;


        public async Task CreateIntrospectionClient(IOpenIddictApplicationManager manager,
                                                    string clientId,
                                                    string clientSecret,
                                                    CancellationToken cancellationToken)
        {
            if(await manager.FindByClientIdAsync(clientId, cancellationToken) is null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Introspection
                    }

                }, cancellationToken);
            }
        }

        private async Task CreateScopesAsync(IServiceScope scope, CancellationToken cancellationToken)
        {
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

            if(await manager.FindByNameAsync("mobile-api", cancellationToken) == null)
            {
                var descriptor = new OpenIddictScopeDescriptor
                {
                    Name = "mobile-api",
                    Resources = { "test-resource",
                                  "product-resource",
                                  "localization-resource",
                                  "order-resource"
                    }
                };
                await manager.CreateAsync(descriptor, cancellationToken);
                
            }
        }




    }
}
