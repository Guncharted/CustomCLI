using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace GunchartedCLI.Tools
{
    public class DownloadsFolderCleaner
    {
        private string sourceFolder;
        private string destinationFolder;
        private Dictionary<string, string> extensionsDictionary;

        private readonly IConfiguration _configuration;

        public DownloadsFolderCleaner(IConfiguration configuration)
        {
            _configuration = configuration;
            //reading extensions from JSON
            extensionsDictionary = _configuration.GetSection("fileExtensions")
                .GetChildren()
                .Select(i => new KeyValuePair<string, string>(i.Key, i.Value))
                .ToDictionary(i => i.Key, i => i.Value);
        }

        private void InitializeFolders(string source, string destination)
        {
            this.sourceFolder = source ?? _configuration.GetSection("source").Value;
            this.destinationFolder = destination ?? _configuration.GetSection("destination").Value;
        }

        public void StartMove(string source = null, string destination = null)
        {
            InitializeFolders(source, destination);

            Console.WriteLine($"-- Moving files --\nFROM: {sourceFolder}\nTO: {destinationFolder}");

            var files = ReadDirectory(sourceFolder);
            if (files.Count() > 0)
            {
                MoveFilesByDestination(files);
            }
            else
            {
                Console.WriteLine("No files to move");
            }
        }

        private IEnumerable<string> ReadDirectory(string folderPath)
        {
            var filesToMove = Directory.GetFiles(folderPath);
            return filesToMove;
        }

        private void MoveFilesByDestination(IEnumerable<string> filesToMove)
        {
            foreach (var item in filesToMove)
            {
                ProcessFile(item);
            }
        }

        private void ProcessFile(string fullFileName)
        {
            var sourceFileInfo = new FileInfo(fullFileName);
            var extension = sourceFileInfo.Extension.ToLower();
            var subfolder = extensionsDictionary.FirstOrDefault(kvp => kvp.Key == extension).Value ?? "other";
            var fullDestinationPath = Path.Combine(destinationFolder, subfolder, sourceFileInfo.Name);
            if (!File.Exists(fullDestinationPath))
            {
                new FileInfo(fullDestinationPath).Directory.Create();
                File.Move(fullFileName, fullDestinationPath);
                Console.WriteLine($"File {sourceFileInfo.Name} moved to destinaton folder succesfully.");
            }
            else
            {
                Console.WriteLine($"ERROR: File {sourceFileInfo.Name} has not been moved.");
            }
        }
    }
}
