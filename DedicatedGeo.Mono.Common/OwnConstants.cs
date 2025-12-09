using System.Net;
using DedicatedGeo.Mono.Common.Exceptions;

namespace DedicatedGeo.Mono.Common;

public static class OwnConstants
{
   
    
    public static class EnvironmentKeys
    {
        public const string InitAdminUserPassword = "INIT_ADMIN_USER_PASSWORD";
        public const string InitAdminUserLogin = "INIT_ADMIN_USER_LOGIN";
        public const string MySqlConnectionString = "IDEALTEX_MYSQL_CONNECTION_STRING";
        public const string AspnetcoreEnvironment = "ASPNETCORE_ENVIRONMENT";
        public const string LogFolder = "IDEALTEX_LOG_FOLDER";
    }
    
    public static class ErrorTemplates
    {
        public const string RequestExceptionMessage = "Request was ended with Code: {Code} and Message: {Message}";

        public static ExceptionMaker ResourceNotFoundException => new(
            "Resource {0} with {1} not found",
            HttpStatusCode.NotFound
        );

        public static ExceptionMaker LoginFailedException => new(
            "Your password or email are not valid",
            HttpStatusCode.BadRequest
        );

        public static ExceptionMaker ResourceAlreadyExistsException => new(
            "Resource {0} with {1} already exists",
            HttpStatusCode.Conflict
        );

        public static ExceptionMaker ResourceInQuarantine => new(
            "Resource {0} with {1} is in quarantine",
            HttpStatusCode.Conflict
        );

        public static ExceptionMaker ResourceRevisionConflictException => new(
            "Resource {0} with {1} has revision conflict",
            HttpStatusCode.Conflict
        );

        public static ExceptionMaker ResourceRevisionTheSameException => new(
            "Resource {0} with {1} up to date",
            HttpStatusCode.Conflict
        );

        public static ExceptionMaker TokenIsNotValid => new(
            "Token is not valid. {0}",
            HttpStatusCode.Forbidden
        );

        public static ExceptionMaker OrderIsConfirmed => new(
            "The order cannot change, because order is confirmed. order number: {0}",
            HttpStatusCode.BadRequest
        );

        public static ExceptionMaker OrderIsNotPublish => new(
            "Order is not published. order number: {0}",
            HttpStatusCode.BadRequest
        );

        public static ExceptionMaker ExportDoesntExist => new(
            "Export does not exist. Export name: {0}",
            HttpStatusCode.BadRequest
        );

        public static ExceptionMaker Forbidden => new(
            "User doesn't have rights to use this resource",
            HttpStatusCode.Forbidden
        );

        public static ExceptionMaker OnlyOneLayerOfRepliesAllowed => new(
            "Reply comment can be created only for no-reply comment",
            HttpStatusCode.Forbidden
        );
    }

    public static class Claims
    {
        public const string UserIdClaim = "uid";
        public const string IsRefreshClaim = "rfsht";
    }
    
}