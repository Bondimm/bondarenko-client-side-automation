﻿using System;
using Autofac;
using Kpi.MetaUa.ClientTests.Domain.Login;
using Kpi.MetaUa.ClientTests.Domain.Search;
using Kpi.MetaUa.ClientTests.Domain.SendEmail;
using Kpi.MetaUa.ClientTests.Model.Domain.Login;
using Kpi.MetaUa.ClientTests.Model.Domain.Search;
using Kpi.MetaUa.ClientTests.Model.Domain.SendEmail;
using Kpi.MetaUa.ClientTests.Model.Domain.UserInfo;
using Kpi.MetaUa.ClientTests.Model.Platform.Communication;
using Kpi.MetaUa.ClientTests.Model.Platform.Drivers;
using Kpi.MetaUa.ClientTests.Platform.Communication;
using Kpi.MetaUa.ClientTests.Platform.Configuration.Environment;
using Kpi.MetaUa.ClientTests.Platform.Configuration.Run;
using Kpi.MetaUa.ClientTests.Platform.Driver;
using Kpi.MetaUa.ClientTests.UI.Login;
using Kpi.MetaUa.ClientTests.UI.MailBox;
using Kpi.MetaUa.ClientTests.UI.Search;
using Kpi.MetaUa.ClientTests.UI.UserInfo;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Serilog;
using Serilog.Events;

namespace Kpi.MetaUa.ClientTests.Bootstrap
{
    public class Bootstraper
    {
        private ContainerBuilder _builder;

        public ContainerBuilder Builder => _builder ??= new ContainerBuilder();

        public void ConfigureServices(IConfigurationBuilder configurationBuilder)
        {
            var configurationRoot = configurationBuilder.Build();
            Builder.Register<ILogger>((c, p) => new LoggerConfiguration()
                    .WriteTo.File(
                        $"Logs/log_{DateTime.UtcNow:yyyy_MM_dd_hh_mm_ss}.txt",
                        LogEventLevel.Verbose,
                        "{Timestamp:dd-MM-yyyy HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                    .CreateLogger())
                .SingleInstance();

            // Configurations
            Builder.Register<IEnvironmentConfiguration>(context => configurationRoot.Get<EnvironmentConfiguration>())
                .SingleInstance();
            Builder.Register<IRunSettings>(cont => configurationRoot.Get<RunSettings>())
                .SingleInstance();

            Builder.RegisterType<Client>().As<IClient>().InstancePerDependency();
            Builder.RegisterType<RestClient>().As<IRestClient>().InstancePerDependency();

            // Logic
            Builder.RegisterType<LoginContext>().As<ILoginContext>().SingleInstance();
            Builder.RegisterType<LoginSteps>().As<ILoginSteps>().SingleInstance();
            Builder.RegisterType<SearchSteps>().As<ISearchSteps>().SingleInstance();
            Builder.RegisterType<SearchContext>().As<ISearchContext>().SingleInstance();
            Builder.RegisterType<UserInfoSteps>().As<IUserInfoSteps>().SingleInstance();
            Builder.RegisterType<SendEmailSteps>().As<ISendEmailSteps>().SingleInstance();
            Builder.RegisterType<SendEmailContext>().As<ISendEmailContext>().SingleInstance();

            Builder.RegisterType<WebDriver>().As<IWebDriver>().SingleInstance();
        }
    }
}