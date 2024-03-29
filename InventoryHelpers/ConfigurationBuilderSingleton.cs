﻿using Microsoft.Extensions.Configuration;
using System.IO;

namespace InventoryHelpers
{
    public sealed class ConfigurationBuilderSingleton
    {
        private static ConfigurationBuilderSingleton _instance = null;
        private static readonly object instanceLock = new object();

        private static IConfigurationRoot _configuration;

        private ConfigurationBuilderSingleton()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static ConfigurationBuilderSingleton Instance
        {
            get
            {
                lock (instanceLock) // avoid multi thread
                {
                    // null-coalescing assignment operator  
                    _instance ??= new ConfigurationBuilderSingleton();
                    return _instance;
                }
            }
        }

        public static IConfigurationRoot ConfigurationRoot
        {
            get
            {
                if (_configuration == null) _ = Instance;　// just call Instance, no use return value
                return _configuration;
            }
        }

    }
}
