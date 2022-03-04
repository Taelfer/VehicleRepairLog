using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VehicleRepairLog.ApplicationServices.API.Domain;
using VehicleRepairLog.ApplicationServices.API.ErrorHandling;

namespace VehicleRepairLog.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IMediator mediator;

        public ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Validates data send in user Request. Checks if Responses from Handlers are correct.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> HandleRequest<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
            where TResponse: ErrorResponseBase
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(
                    this.ModelState
                    .Where(x => x.Value.Errors.Any())
                    .Select(x => new { property = x.Key, errors = x.Value.Errors }));
            }

            var response = await this.mediator.Send(request);

            if (response.Error != null)
            {
                return this.ErrorResponse(response.Error);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Passes on error type name from Handlers to <see cref="GetHttpStatusCode(string)"/> method.
        /// </summary>
        /// <returns>HTTP status code name in <see cref="System.Int32"/> and <see cref="System.String"/> format.</returns>
        private IActionResult ErrorResponse(ErrorModel errorModel)
        {
            var httpCode = GetHttpStatusCode(errorModel.Error);
            return StatusCode((int)httpCode, errorModel);
        }

        /// <summary>
        /// Aligns HTTP status code to HTTP status code name send from <see cref="ErrorResponse(ErrorModel)"/> method.
        /// </summary>
        /// <returns>HTTP status code in <see cref="System.Int32"/> format.</returns>
        private static HttpStatusCode GetHttpStatusCode(string errorType)
        {
            switch (errorType)
            {
                case ErrorType.InternalServerError:
                    return HttpStatusCode.InternalServerError;
                case ErrorType.Unauthorized:
                    return HttpStatusCode.Unauthorized;
                case ErrorType.NotFound:
                    return HttpStatusCode.NotFound;
                case ErrorType.UnsupportedMediaType:
                    return HttpStatusCode.UnsupportedMediaType;
                case ErrorType.RequestEntityTooLarge:
                    return HttpStatusCode.RequestEntityTooLarge;
                case ErrorType.MethodNotAllowed:
                    return HttpStatusCode.MethodNotAllowed;
                case ErrorType.TooManyRequests:
                    return HttpStatusCode.TooManyRequests;
                default:
                    return HttpStatusCode.BadRequest;
            }
        }
    }
}
