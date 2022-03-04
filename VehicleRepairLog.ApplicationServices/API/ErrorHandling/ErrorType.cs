namespace VehicleRepairLog.ApplicationServices.API.ErrorHandling
{
    /// <summary>
    /// Stores const names values of <see cref="System.String"/> type for HttpCodes.
    /// </summary>
    public static class ErrorType
    {
        public const string InternalServerError = "INTERNAL_SERVER_ERROR";
        public const string ValidationError = "VALIDATION_ERROR";
        public const string NotAuthenticated = "NOT_AUTHENTICATED";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string NotFound = "NOT_FOUND";
        public const string UnsupportedMediaType = "UNSUPPORTED_MEDIA_TYPE";
        public const string MethodNotAllowed = "METHOD_NOT_ALLOWED";
        public const string RequestEntityTooLarge = "REQUEST_ENTITY_TOO_LARGE";
        public const string TooManyRequests = "TOO_MANY_REQUESTS";
    }
}
