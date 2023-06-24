using Microsoft.AspNetCore.Identity;
using OCA.API.Interfaces;
using OCA.API.Models;
using OCA.API.Models.Responses;

namespace OCA.API.Helpers
{
    public static class ApiResponseHelpers
    {
        public static ApiResponse<IEnumerable<Cryptocurrency>> InvalidApiKey()
        {          
            return new()
            { 
                RequestError = "Invalid API key",
                RequestStatusCode = 404,
                DbResult = new DbResult<IEnumerable<Cryptocurrency>>() { Data = Enumerable.Empty<Cryptocurrency>() }
            };
        }
        
        public static ApiResponse<IEnumerable<Cryptocurrency>> ValidApiKey()
        {          
            return new()
            { 
                RequestError = string.Empty,
                RequestStatusCode = 200,
                DbResult = new DbResult<IEnumerable<Cryptocurrency>>() { Data = Enumerable.Empty<Cryptocurrency>() }
            };
        }

        public static ApiResponse<IEnumerable<Cryptocurrency>> InternalError()
        {          
            return new()
            {
                RequestError = "Something has gone wrong, please try again in a few minutes",
                RequestStatusCode = 500,
                DbResult = new DbResult<IEnumerable<Cryptocurrency>>() { Data = Enumerable.Empty<Cryptocurrency>() }
            };
        }

        public static ApiResponse<IEnumerable<Cryptocurrency>> NotFound(this ApiResponse<IEnumerable<Cryptocurrency>> apiResponse, string cryptocurrencyName)
        {
            apiResponse.RequestError = "Cryptocurrency: {Name} not found, please try a different request";
            apiResponse.RequestStatusCode = 404;
            apiResponse.DbResult = new DbResult<IEnumerable<Cryptocurrency>>() { Data = Enumerable.Empty<Cryptocurrency>() };
            return apiResponse;
        }
        
        public static ApiResponse<IEnumerable<Cryptocurrency>> NotFound(this ApiResponse<IEnumerable<Cryptocurrency>> apiResponse, int rank)
        {
            apiResponse.RequestError = "Rank: {Rank} not found, please use 1 <= rank <= 100";
            apiResponse.RequestStatusCode = 404;
            apiResponse.DbResult = new DbResult<IEnumerable<Cryptocurrency>>() { Data = Enumerable.Empty<Cryptocurrency>() };
            return apiResponse;
        }

        public static ApiResponse<IEnumerable<Cryptocurrency>> Success(IEnumerable<Cryptocurrency> data)
        {          
            return new()
            {
                RequestStatusCode = 200,
                DbResult = new DbResult<IEnumerable<Cryptocurrency>> { Data = data },
                IsValid = true
            };
        }

        public static ICustomResponse FailureResponse<T>(IEnumerable<IdentityError> identityErrors) where T : ICustomResponse, new()
        {
            T registrationResponse = new()
            {
                Success = false,
                ErrorMessages = identityErrors.Select(x => x.Description)
            };

            return registrationResponse;
        }

        public static ICustomResponse SuccessResponse<TReturnImplementation>() where TReturnImplementation : ICustomResponse, new()
        {
            return new TReturnImplementation()
            {
                Success = true
            };
        }

        public static ICustomResponse AuthenticationFailure<T>() where T : ICustomResponse, new()
        {
            return new T()
            {
                Success = false
            };
        }

        public static ICustomResponse ServerError<T>() where T : ICustomResponse, new()
        {
            return new T() { Success = false, ErrorMessages = new string[] { "Sorry something unexpected has happened, please try again" } };
        }
    }
}
