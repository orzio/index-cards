using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Responses
{
    public class AppResult<T>
    {
        public enum StatusCodes
        {
            Ok,
            BadRequest,
            AlreadyExists,
            ValidationError,
            NotFound,
            ApplicationError,
        }

        public List<Error> ErrorMessages { get; set; }
        public T Value { get; set; }
        public StatusCodes StatusCode { get; set; }

        public static AppResult<T> NotFound<K>(string name, K key)
        {
            return Failure(StatusCodes.NotFound, name, $"{name} ({key}) could not be found");
        }
        public static AppResult<T> Failure(StatusCodes code, string propertyName, string message)
        {
            return new AppResult<T>
            {
                StatusCode = code,
                ErrorMessages = new List<Error>
                    { new Error()
                        {
                            PropertyName = propertyName,
                            ErrorMessage = message
                        }
                    }
            };
        }
        public static AppResult<T> Failure(StatusCodes code, List<Error> errors)
        {
            return new AppResult<T>
            {
                StatusCode = code,
                ErrorMessages = errors
            };
        }

        public static AppResult<T> Success(T value)
        {
            return new AppResult<T>()
            {
                StatusCode = StatusCodes.Ok,
                Value = value
            };
        }
    }
}
