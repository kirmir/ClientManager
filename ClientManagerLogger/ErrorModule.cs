using System;
using System.IO;
using System.Web;
using log4net;

namespace ClientManagerLogger
{
    /// <summary>
    /// The error handler.
    /// </summary>
    public class ErrorHandler : IHttpModule
    {
        private static readonly ILog Logger = LogManager.GetLogger("ClientManagerLog");

        /// <summary>
        /// Initialize the specified application.
        /// </summary>
        /// <param name="application">The application.</param>
        public void Init(HttpApplication application)
        {
            application.Error += ApplicationError;
            
            // Take's configuration file
            var config = new FileInfo(application.Context.Server.MapPath("~\\Log4Net.config"));

            // Configure
            log4net.Config.XmlConfigurator.ConfigureAndWatch(config);
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"/>.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Applications the error.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void ApplicationError(object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;

            // Get the inner most exception
            Exception exception;
            for (exception = ctx.Server.GetLastError(); exception.InnerException != null; exception = exception.InnerException) { }

            // Ignore HTTP 404 errors
            if (exception is HttpException && ((HttpException)exception).GetHttpCode() == 404) return;
            
            Logger.Error("ErrorModule caught an unhandled exception", exception);
        }
    }
}