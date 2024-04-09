using System.Net;
using CSharpFunctionalExtensions;

namespace JetStreamData.Kernel.Api;

/// <summary>
///     Extends ApiResponse to include a payload of a specific type, facilitating detailed API responses.
///     Inherits common properties and introduces a mechanism for returning typed data.
/// </summary>
/// <typeparam name="TResult">Type of the payload included in the response.</typeparam>
public class ApiResponse<TResult> : ApiResponse
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
    private ApiResponse<TResult> WithPayload(TResult data)
    {
        Payload = data;
        return this;
    }

    /// <summary>
    ///     Factory method to create a success ApiTypedResponse with an OK (200) status, optionally without a payload.
    ///     This can be employed when the need to signal success is paramount and specific data is optional.
    /// </summary>
    /// <returns>An ApiTypedResponse configured with an OK status.</returns>
    private static ApiResponse<TResult> OperationSuccess()
    {
        return new ApiResponse<TResult> { ResponseStatusCode = HttpStatusCode.OK };
    }

    /// <summary>
    ///     Generates an ApiTypedResponse to signify that a requested resource could not be found (NotFound, 404), with an
    ///     optional message.
    /// </summary>
    /// <param name="optionalMessage">Optional message providing additional context.</param>
    /// <returns>An ApiTypedResponse indicating a NotFound status, possibly with a message.</returns>
    public static ApiResponse<TResult> ResourceNotFound(string optionalMessage = "")
    {
        return new ApiResponse<TResult> { ResponseStatusCode = HttpStatusCode.NotFound, ResponseMessage = optionalMessage };
    }

    /// <summary>
    ///     Factory method to create an ApiTypedResponse indicating a client-side error (BadRequest, 400) with a specific
    ///     message.
    ///     Inherits and tailors the base class's ClientError method for typed responses.
    /// </summary>
    /// <param name="errorMessage">Detailing message for the client-side error.</param>
    /// <returns>An ApiTypedResponse<TResult> with a BadRequest status and the provided error message.</returns>
    public static new ApiResponse<TResult> ClientError(string errorMessage)
    {
        return new ApiResponse<TResult> { ResponseStatusCode = HttpStatusCode.BadRequest, ResponseMessage = errorMessage };
    }

    /// <summary>
    ///     Allows seamless integration with CSharpFunctionalExtensions's Result type, converting a Result into an
    ///     ApiTypedResponse.
    ///     This supports the uniform handling of success or error states.
    /// </summary>
    /// <param name="operationResult">The Result instance to convert.</param>
    /// <returns>An ApiTypedResponse based on the operationResult.</returns>
    public static implicit operator ApiResponse<TResult>(Result<TResult> operationResult)
    {
        return operationResult.IsSuccess ? OperationSuccess().WithPayload(operationResult.Value) : ClientError(operationResult.Error);
    }

    /// <summary>
    ///     Enables the representation of a Maybe (potentially absent value) as an ApiTypedResponse, providing a consistent API
    ///     response model.
    /// </summary>
    /// <param name="maybeValue">The Maybe instance to convert.</param>
    /// <returns>An ApiTypedResponse based on the presence or absence of the maybeValue.</returns>
    public static implicit operator ApiResponse<TResult>(Maybe<TResult> maybeValue)
    {
        return maybeValue.HasValue ? OperationSuccess().WithPayload(maybeValue.Value) : ResourceNotFound();
    }
}
