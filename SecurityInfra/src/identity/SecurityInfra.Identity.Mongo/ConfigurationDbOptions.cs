using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Identity.Mongo
{
    public class ConfigurationDbOptions
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public bool IsSSL { get; set; } = false;
    }
}
