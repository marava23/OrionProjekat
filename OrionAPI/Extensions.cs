using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrionAPI
{
    public static class Extensions
    {
        public static IActionResult ToUnprocessableEntity(this IEnumerable<ValidationFailure> errors)
        {
            return new UnprocessableEntityObjectResult(new
            {
                Errors = errors.Select(x => new
                {
                    x.ErrorMessage,
                    x.PropertyName
                })
            });
        }
    }
}
