using Microsoft.Extensions.DependencyInjection;
using MoscowTrafficRestriction.ApplicationServices.GetTrafficRestrictionListUseCase;
using MoscowTrafficRestriction.ApplicationServices.Ports.Cache;
using MoscowTrafficRestriction.ApplicationServices.Repositories;
using MoscowTrafficRestriction.DesktopClient.InfrastructureServices.ViewModels;
using MoscowTrafficRestriction.DomainObjects;
using MoscowTrafficRestriction.DomainObjects.Ports;
using MoscowTrafficRestriction.InfrastructureServices.Cache;
using MoscowTrafficRestriction.InfrastructureServices.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MoscowTrafficRestriction.DesktopClient 
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDomainObjectsCache<TrafficRestriction>, DomainObjectsMemoryCache<TrafficRestriction>>();
            services.AddSingleton<NetworkTrafficRestrictionRepository>(
                x => new NetworkTrafficRestrictionRepository("localhost", 80, useTls: false, x.GetRequiredService<IDomainObjectsCache<TrafficRestriction>>())
            );
            services.AddSingleton<CachedReadOnlyTrafficRestrictionRepository>(
                x => new CachedReadOnlyTrafficRestrictionRepository(
                    x.GetRequiredService<NetworkTrafficRestrictionRepository>(),
                    x.GetRequiredService<IDomainObjectsCache<TrafficRestriction>>()
                )
            );
            services.AddSingleton<IReadOnlyTrafficRestrictionRepository>(x => x.GetRequiredService<CachedReadOnlyTrafficRestrictionRepository>());
            services.AddSingleton<IGetTrafficRestrictionListUseCase, GetTrafficRestrictionListUseCase>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs args)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
