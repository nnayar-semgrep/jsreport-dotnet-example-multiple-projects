using jsreport.Binary;
using jsreport.Local;
using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rendering template");

            var rs = new LocalReporting()
                .UseBinary(JsReportBinary.GetBinary())
                .KillRunningJsReportProcesses()
                .Configure(cfg => cfg.FileSystemStore().BaseUrlAsWorkingDirectory())
                .AsUtility()
                .Create();

            var report = rs.RenderByNameAsync("myTemplate", null).Result;

            using (var fs = File.Create("test.pdf"))
            {
                report.Content.CopyTo(fs);
            }
        }
    }
}
