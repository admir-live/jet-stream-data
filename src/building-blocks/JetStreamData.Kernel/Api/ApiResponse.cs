// Copyright (c) Admir Mujkic for Journey Mentor Cyprus LTD. All Rights Reserved.
// Licensed under the MIT license. See License.txt in the project root for license information.

using System.Net;
using System.Text.Json.Serialization;
using CSharpFunctionalExtensions;

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

/// <summary>
///     Extends ApiResponse to include a payload of a specific type, facilitating detailed API responses.
///     Inherits common properties and introduces a mechanism for returning typed data.
/// </summary>
/// <typeparam name="TResult">Type of the payload included in the response.</typeparam>
public class ApiTypedResponse<TResult> : ApiResponse
{
    /// <summary>
    ///     Payload of the API response, containing data returned by the operation.
    /// </summary>
    public TResult Payload { get; set; }

    /// <summary>
    ///     Facilitates fluent configuration of the response by setting the payload and returning the updated instance.
    /// </summary>
    /// <param name="data">Payload to be included in the response.</param>
    /// <returns>The updated ApiTypedResponse with the specified payload.</returns>
    private ApiTypedResponse<TResult> WithPayload(TResult data)
    {
        Payload = data;
        return this;
    }

    /// <summary>
    ///     Factory method to create a success ApiTypedResponse with an OK (200) status, optionally without a payload.
    ///     This can be employed when the need to signal success is paramount and specific data is optional.
    /// </summary>
    /// <returns>An ApiTypedResponse configured with an OK status.</returns>
    private static ApiTypedResponse<TResult> OperationSuccess()
    {
        return new ApiTypedResponse<TResult> { ResponseStatusCode = HttpStatusCode.OK };
    }

    /// <summary>
    ///     Generates an ApiTypedResponse to signify that a requested resource could not be found (NotFound, 404), with an
    ///     optional message.
    /// </summary>
    /// <param name="optionalMessage">Optional message providing additional context.</param>
    /// <returns>An ApiTypedResponse indicating a NotFound status, possibly with a message.</returns>
    public static ApiTypedResponse<TResult> ResourceNotFound(string optionalMessage = "")
    {
        return new ApiTypedResponse<TResult> { ResponseStatusCode = HttpStatusCode.NotFound, ResponseMessage = optionalMessage };
    }

    /// <summary>
    ///     Factory method to create an ApiTypedResponse indicating a client-side error (BadRequest, 400) with a specific
    ///     message.
    ///     Inherits and tailors the base class's ClientError method for typed responses.
    /// </summary>
    /// <param name="errorMessage">Detailing message for the client-side error.</param>
    /// <returns>An ApiTypedResponse<TResult> with a BadRequest status and the provided error message.</returns>
    public static new ApiTypedResponse<TResult> ClientError(string errorMessage)
    {
        return new ApiTypedResponse<TResult> { ResponseStatusCode = HttpStatusCode.BadRequest, ResponseMessage = errorMessage };
    }

    /// <summary>
    ///     Allows seamless integration with CSharpFunctionalExtensions's Result type, converting a Result into an
    ///     ApiTypedResponse.
    ///     This supports the uniform handling of success or error states.
    /// </summary>
    /// <param name="operationResult">The Result instance to convert.</param>
    /// <returns>An ApiTypedResponse based on the operationResult.</returns>
    public static implicit operator ApiTypedResponse<TResult>(Result<TResult> operationResult)
    {
        return operationResult.IsSuccess ? OperationSuccess().WithPayload(operationResult.Value) : ClientError(operationResult.Error);
    }

    /// <summary>
    ///     Enables the representation of a Maybe (potentially absent value) as an ApiTypedResponse, providing a consistent API
    ///     response model.
    /// </summary>
    /// <param name="maybeValue">The Maybe instance to convert.</param>
    /// <returns>An ApiTypedResponse based on the presence or absence of the maybeValue.</returns>
    public static implicit operator ApiTypedResponse<TResult>(Maybe<TResult> maybeValue)
    {
        return maybeValue.HasValue ? OperationSuccess().WithPayload(maybeValue.Value) : ResourceNotFound();
    }
}
