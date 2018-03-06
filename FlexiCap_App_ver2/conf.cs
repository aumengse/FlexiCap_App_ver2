using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexiCap_App_ver2.Conf
{
    class conf
    {
        private String connectionString;

        public String getConnectionString()
        {
            // Calls Connection String thru ConfigureManager getting the content from the App.config
            connectionString = ConfigurationManager.ConnectionStrings["DBConsoleApp.Properties.Settings.ConsoleDB_ConnectionString"].ConnectionString; // Gets the <connectionStrings> tag information in the App.config
            return connectionString; // Returns the ConnectionString
        }
    }
}
