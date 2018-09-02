using System;
using System.Diagnostics;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using Glob;

namespace dotnet_versioninfo
{
    [Command(Description = "Display version information of .NET Core assemblies")]
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void ProcessFile(string fileName)
        {
            var vi = FileVersionInfo.GetVersionInfo(fileName);
            Console.WriteLine($"{fileName}");
            Console.WriteLine($"\tFileVersion:\t{vi.FileVersion}");
            Console.WriteLine($"\tProductVersion:\t{vi.ProductVersion}");
        }

        private int OnExecute()
        {
            var root = new DirectoryInfo(".");
            var allDllFiles = root.GlobFiles("**/*.dll").ToList();
            allDllFiles.ForEach(fileInfo => ProcessFile(fileInfo.FullName));
            return 0;
        }
    }
}