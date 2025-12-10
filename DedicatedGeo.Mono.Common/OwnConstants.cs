using System.Net;
using DedicatedGeo.Mono.Common.Exceptions;

namespace DedicatedGeo.Mono.Common;

public static class OwnConstants
{

    public const string DeviceId = "797f309e-b0b7-43a1-9c17-5784d01052e6";
    
    public static class EnvironmentKeys
    {
        public const string MySqlConnectionString = "IDEALTEX_MYSQL_CONNECTION_STRING";
        public const string AspnetcoreEnvironment = "ASPNETCORE_ENVIRONMENT";
        public const string LogFolder = "IDEALTEX_LOG_FOLDER";
    }
    
    public static class ErrorTemplates
    {
        public const string RequestExceptionMessage = "Request was ended with Code: {Code} and Message: {Message}";

        public static ExceptionMaker ResourceNotFound => new(
            "The requested {Resource} was not found.",
            HttpStatusCode.NotFound
        );
    }
    
}