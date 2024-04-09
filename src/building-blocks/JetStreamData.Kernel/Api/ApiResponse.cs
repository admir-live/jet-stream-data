// Copyright (c) Admir Mujkic for Journey Mentor Cyprus LTD. All Rights Reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System.Net;
using System.Text.Json.Serialization;

namespace JetStreamData.Kernel.Api;

/// <summary>
///     Represents a standardized API response mechanism without a specific payload.
///     Enables the creation of responses with common HTTP status codes and messages.
///     It also provides utility methods for wrapping operation execution into a unified response structure.
/// </summary>
public class ApiResponse
{
    /// <summary>
    ///     Descriptive message accompanying the API response, potentially including error details or summary of the operation.
    /// </summary>
    public string ResponseMessage { get; set; }

    /// <summary>
    ///     HTTP status code indicating the result of the API call.
    ///     This property is excluded from JSON serialization to prioritize the use of standard HTTP response codes.
    /// </summary>
    [JsonIgnore]
    public HttpStatusCode ResponseStatusCode { get; set; }

    /// <summary>
    ///     Indicator of whether the API call succeeded, based on the HTTP status code being within the successful range
    ///     (200-299).
    /// </summary>
    [JsonIgnore]
    public bool IsCallSuccessful => ResponseStatusCode is >= (HttpStatusCode)200 and <= (HttpStatusCode)299;

    /// <summary>
    ///     Wraps the execution of a function, returning its outcome as an ApiResponse instance.
    ///     This method abstracts the try/catch logic or other operation-specific handling into a consistent response format.
    /// </summary>
    /// <param name="operation">The function to be executed.</param>
    /// <returns>An ApiResponse reflecting the operation's outcome.</returns>
    public static ApiResponse ExecuteOperation(Func<ApiResponse> operation)
    {
        return operation();
    }

    /// <summary>
    ///     Constructs an ApiResponse indicating a client-side error (BadRequest, 400) with a specific message.
    ///     Useful for communicating issues such as validation failures.
    /// </summary>
    /// <param name="errorMessage">Message detailing the nature of the error.</param>
    /// <returns>An ApiResponse signaling a BadRequest status with the provided error message.</returns>
    public static ApiResponse ClientError(string errorMessage)
    {
        return new ApiResponse { ResponseStatusCode = HttpStatusCode.BadRequest, ResponseMessage = errorMessage };
    }

    /// <summary>
    ///     Generates an ApiResponse for a BadRequest (400) status with multiple error messages.
    ///     Allows aggregation of multiple errors into a single response.
    /// </summary>
    /// <param name="errorMessages">Array of strings detailing individual errors.</param>
    /// <returns>An ApiResponse configured with a BadRequest status and a concatenated message of all errors.</returns>
    public static ApiResponse ClientErrors(string[] errorMessages)
    {
        return new ApiResponse { ResponseStatusCode = HttpStatusCode.BadRequest, ResponseMessage = string.Join(", ", errorMessages) };
    }

    /// <summary>
    ///     Produces an ApiResponse representing an in-progress operation (Continue, 100 status).
    ///     Suitable for situations where an action has commenced but has yet to complete.
    /// </summary>
    /// <returns>An ApiResponse indicating a Continue status.</returns>
    public static ApiResponse OperationInProgress()
    {
        return new ApiResponse { ResponseStatusCode = HttpStatusCode.Continue };
    }
}
