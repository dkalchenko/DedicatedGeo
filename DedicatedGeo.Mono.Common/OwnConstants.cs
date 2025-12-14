using System.Net;
using DedicatedGeo.Mono.Common.Exceptions;

namespace DedicatedGeo.Mono.Common;

public static class OwnConstants
{

    public const int UpdateDeviceStatusBackgroundDelayInMillisecond = 20000;
    public const int DeviceIsInactivityStatusAfterInMinutes = 2;
    
    public static class DeviceStatusNames
    {
        public const string BatteryLevel = "batteryLevel";
        public const string IsInAlarm = "isInAlarm";
        public const string IsButtonPressed = "isButtonPressed";
        public const string IsInCharge = "isInCharge";
        public const string IsGPSOnline = "isGPSOnline";
        public const string IsDeviceOnline = "isDeviceOnline";
        
        public static readonly IEnumerable<string> AllNames =
        [
            BatteryLevel,
            IsInAlarm,
            IsButtonPressed,
            IsInCharge,
            IsGPSOnline,
            IsDeviceOnline
        ];
    }
    
    public static class Claims
    {
        public const string UserIdClaim = "uid";
        public const string IsRefreshClaim = "rfsht";
    }
    
    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string DeviceUser = "DeviceUser";
    }
    
    public static class EnvironmentKeys
    {
        public const string MySqlConnectionString = "MYSQL_CONNECTION_STRING";
        public const string AspnetcoreEnvironment = "ASPNETCORE_ENVIRONMENT";
        public const string LogFolder = "LOG_FOLDER";
        public const string JwtSecretKey = "JWT_SECRET_KEY";
    }
    
    public static class ErrorTemplates
    {
        public const string RequestExceptionMessage = "Request was ended with Code: {Code} and Message: {Message}";

        public static ExceptionMaker ResourceNotFound => new(
            "The requested {Resource} was not found.",
            HttpStatusCode.NotFound
        );
        
        public static ExceptionMaker TokenIsNotValid => new(
            "Token is not valid. {0}",
            HttpStatusCode.Forbidden
        );

        public static ExceptionMaker LoginFailedException => new(
            "Your password or email are not valid",
            HttpStatusCode.BadRequest
        );

    }
    
}