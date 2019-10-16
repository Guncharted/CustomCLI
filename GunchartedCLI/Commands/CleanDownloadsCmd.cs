using GunchartedCLI.Tools;
using McMaster.Extensions.CommandLineUtils;

namespace GunchartedCLI.Commands
{
    [Command(Name ="clean-downloads", Description ="Moves and sorts all files from standard downloads folder to destination folder")]
    class CleanDownloadsCmd
    {
        private readonly DownloadsFolderCleaner _folderCleaner;

        [Option(CommandOptionType.SingleValue, LongName ="source", ShortName ="s", Description ="Source folder. If not specified then value will be taken from config")]
        public string SourceFolder { get; set; }
        [Option(CommandOptionType.SingleValue, LongName = "destination", ShortName = "d", Description = "Destination folder. If not specified then value will be taken from config")]
        public string DestinationFolder { get; set; }

        public CleanDownloadsCmd(DownloadsFolderCleaner folderCleaner)
        {
            _folderCleaner = folderCleaner;
        }

        protected void OnExecute(CommandLineApplication app)
        {
            _folderCleaner.StartMove(SourceFolder, DestinationFolder);
        }
    }
}
