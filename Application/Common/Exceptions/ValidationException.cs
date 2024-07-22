using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            //Errors = new Dictionary<string, string[]>();
            Errors = new Dictionary<string, IEnumerable<CustomizedValidationError>>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            //Errors = failures
            //    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            //    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            foreach (var failuresInProperty in failures.GroupBy(e => e.PropertyName))
            {
                var errorsToAdd = new List<CustomizedValidationError>();

                foreach (ValidationFailure failure in failuresInProperty)
                {
                    errorsToAdd.Add(new CustomizedValidationError(failure.ErrorMessage, failure.ErrorCode));
                }

                Errors.Add(failuresInProperty.Key, errorsToAdd);
            }
        }

        //public IDictionary<string, string[]> Errors { get; }
        public IDictionary<string, IEnumerable<CustomizedValidationError>> Errors { get; private set; }
    }

    public class CustomizedValidationError
    {
        public string ValidatorKey { get; private set; }
        public string Message { get; private set; }
        public CustomizedValidationError(string message, string validatorKey = "")
        {
            ValidatorKey = validatorKey;
            Message = message;
        }
    }
}
