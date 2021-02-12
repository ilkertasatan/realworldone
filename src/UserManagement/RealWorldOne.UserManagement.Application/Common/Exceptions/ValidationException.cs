using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace RealWorldOne.UserManagement.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, List<ValidationFailure> failures) : base(message)
        {
            Failures = failures;
        }

        public List<ValidationFailure> Failures { get; }
    }
}