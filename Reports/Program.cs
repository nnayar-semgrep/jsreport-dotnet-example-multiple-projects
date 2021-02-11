using jsreport.Binary;
using jsreport.Local;
using System;
using System.Diagnostics;
using System.IO;

namespace Reports
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsreportDirectory = Path.Combine(System.Environment.CurrentDirectory.Substring(0, System.Environment.CurrentDirectory.LastIndexOf("bin")), "jsreport");
            Console.WriteLine("Starting jsreport in " + jsreportDirectory);
         
            var rs = new LocalReporting()
             .UseBinary(JsReportBinary.GetBinary())
             .KillRunningJsReportProcesses()
             .RunInDirectory(jsreportDirectory)
             .Configure(cfg => cfg.CreateSamples()
                .FileSystemStore()
                .BaseUrlAsWorkingDirectory())
             .AsWebServer()
             .RedirectOutputToConsole() 
             .Create();

            rs.StartAsync().Wait();

            Process.Start(new ProcessStartInfo("cmd", $"/c start http://localhost:5488"));

            Console.ReadKey();

            rs.KillAsync().Wait();
        }
    }
}
