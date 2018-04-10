using PrimalityTest.Views;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace PrimalityTest
{
    public partial class App : Application
    {
        private static MainWindow app;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Trace.TraceInformation("Application Startup");

            // For catching Global uncaught exception
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionOccured);

            Trace.TraceInformation("Starting App");
            app = new MainWindow();
            app.Show();

            if (e.Args.Length == 1) //make sure an argument is passed
            {
                Trace.TraceInformation("File type association: " + e.Args[0]);
                FileInfo file = new FileInfo(e.Args[0]);
                if (file.Exists) //make sure it's actually a file
                {
                    // Here, add you own code
                }
            }
        }

        static void UnhandledExceptionOccured(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Trace.TraceError("Application has crashed", e);
        }

    }
}
