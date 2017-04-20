using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using EM.ViewModels;
using AutoMapper;


[assembly: OwinStartup(typeof(EM.Startup))]

namespace EM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
   
}
