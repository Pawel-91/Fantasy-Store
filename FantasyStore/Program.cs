﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace FantasyStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider(options =>
                options.ValidateScopes = false)
            .UseStartup<Startup>();
    }
}
