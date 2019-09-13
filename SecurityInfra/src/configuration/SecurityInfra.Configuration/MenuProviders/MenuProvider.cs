using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityInfra.Configuration.MenuProviders
{
    public class MenuProvider
    {
        protected MenuProvider()
        {

        }

        public MenuProvider(string uri, string title)
        {
            Id = Guid.NewGuid().ToString();
            Enabled = true;
            Title = title;
            Uri = uri;
        }
        public string Id { get; set; }

        public string Uri { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public void Disable()
        {
            Enabled = false;
        }
    }
}
