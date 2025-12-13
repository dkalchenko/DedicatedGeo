using System.Net;
using DedicatedGeo.Mono.Common.Exceptions;

namespace DedicatedGeo.Mono.Common;

public static class OwnConstants
{

    public const int UpdateDeviceStatusBackgroundDelayInMillisecond = 120000;
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