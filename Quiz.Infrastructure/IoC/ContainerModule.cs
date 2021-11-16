using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<RepositoryModule>();
            //builder.RegisterModule<MongoModule>();
            //builder.RegisterModule<SqlModule>();
            //builder.RegisterModule<ServiceModule>();
            //builder.RegisterModule(new SettingsModule(_configuration));
        }
    }
}
