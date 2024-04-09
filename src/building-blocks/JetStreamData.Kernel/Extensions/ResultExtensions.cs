using System.Net;
using CSharpFunctionalExtensions;
using JetStreamData.Kernel.Api;
using Microsoft.AspNetCore.Mvc;

namespace JetStreamData.Kernel.Extensions;

public static class ResultExtensions
{
    public static ObjectResult ToActionResult<T>(this ApiResponse<T> result)
    {
        return new ObjectResult(result) { StatusCode = (int)result.ResponseStatusCode };
    }

    public static ObjectResult ToActionResult(this ApiResponse result)
    {
        return new ObjectResult(result) { StatusCode = (int)result.ResponseStatusCode };
    }

    public static async Task<ObjectResult> ToActionResultAsync<T>(this Task<Result<T>> task,
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        var response = await task;
        return new ObjectResult(response.IsSuccess ? response.Value : ApiResponse.ClientError(response.Error))
        {
            StatusCode = response.IsSuccess ? (int)successStatusCode : (int)HttpStatusCode.BadRequest
        };
    }

    public static async Task<ObjectResult> ToActionResultAsync<T>(this Task<ApiResponse<T>> task,
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        var response = await task;
        return new ObjectResult(response.IsCallSuccessful ? response.Payload : ApiResponse.ClientError(response.ResponseMessage))
        {
            StatusCode = response.IsCallSuccessful ? (int)successStatusCode : (int)HttpStatusCode.BadRequest
        };
    }

    public static async Task<ObjectResult> ToActionResultAsync<T>(this Task<Maybe<T>> task)
    {
        var response = await task;
        return new ObjectResult(response.HasValue ? response.Value : null) { StatusCode = response.HasValue ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound };
    }

    public static async Task<ObjectResult> ToActionResultAsync(this Task<Result> task,
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        var response = await task;
        return new ObjectResult(response.IsSuccess ? null : ApiResponse.ClientError(response.Error))
        {
            StatusCode = response.IsSuccess ? (int)successStatusCode : (int)HttpStatusCode.BadRequest
        };
    }

    public static async Task<ObjectResult> AsResultOkAsync(this Task<Result> task,
        HttpStatusCode successStatusCode = HttpStatusCode.OK)
    {
        return await task.ToActionResultAsync();
    }

    public static async Task<ObjectResult> ToActionResultAsync<T>(this Task<ApiResponse<T>> result)
    {
        var r = await result;
        return new ObjectResult(r) { StatusCode = (int)r.ResponseStatusCode };
    }
}
