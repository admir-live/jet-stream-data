// Copyright (c) Admir Mujkic for Journey Mentor Cyprus LTD.All Rights Reserved.Licensed under the MIT license.See License.txt in the project root for license information.

using FluentValidation;
using JetStreamData.Kernel.Dispatcher;
using Microsoft.AspNetCore.Mvc;

namespace JetStreamData.Kernel.Api;

public abstract class BaseController(IDispatcher dispatcher) : ControllerBase
{
    protected readonly IDispatcher Dispatcher = dispatcher;

    protected ApiResponse Validate<TCommand>(TCommand command)
    {
        var type = typeof(TCommand);
        var validator = Validator(type);

        while (validator == null && type.BaseType != null)
        {
            type = type.BaseType;
            validator = Validator(type);
        }

        if (validator == null)
        {
            return ApiResponse.OperationInProgress();
        }

        var validationContext = new ValidationContext<TCommand>(command);
        var validationResult = validator.Validate(validationContext);

        if (validationResult.IsValid)
        {
            return ApiResponse.OperationInProgress();
        }

        var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
        return ApiResponse.ClientErrors(errors);
    }

    private IValidator Validator(Type type)
    {
        return (IValidator)HttpContext.RequestServices.GetService(typeof(IValidator<>).MakeGenericType(type));
    }
}
