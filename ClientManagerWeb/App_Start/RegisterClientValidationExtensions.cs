using DataAnnotationsExtensions.ClientValidation;

[assembly: WebActivator.PreApplicationStartMethod(typeof(ClientManagerWeb.App_Start.RegisterClientValidationExtensions), "Start")]
 
namespace ClientManagerWeb.App_Start 
{
    /// <summary>
    /// The register client validation extensions.
    /// </summary>
    public static class RegisterClientValidationExtensions 
    {
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public static void Start() 
        {
            DataAnnotationsModelValidatorProviderExtensions.RegisterValidationExtensions();            
        }
    }
}