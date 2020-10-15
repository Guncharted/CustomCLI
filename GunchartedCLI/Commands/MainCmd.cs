using McMaster.Extensions.CommandLineUtils;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace GunchartedCLI.Commands
{
    [Command(Name = "hnchr", OptionsComparison = System.StringComparison.InvariantCultureIgnoreCase)]
    [Subcommand(typeof(CleanDownloadsCmd))]
    class MainCmd
    {
        protected Task<int> OnExecute(CommandLineApplication app)
        {
            app.ShowHelp();

            if((File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "appsettings.json"))))
                Console.WriteLine("INFO: Configuration file found succesfully");
            else
                Console.WriteLine("WARNING: Configuration file has not been found");

            return Task.FromResult(0);
        }
    }
}
