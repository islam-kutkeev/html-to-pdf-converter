namespace DocumentationGenerator.Service.Dtos;

public class ResponseDto
{
    public ResponseDto()
    {
        Code = 0;
        Message = "Success";
    }

    public ResponseDto(int code, string message)
    {
        Code = code;
        Message = message;
    }

    /// <summary>
    /// Indicates successful of the response
    /// </summary>
    /// <example>0</example>
    public int Code { get; set; }


    /// <summary>
    /// Additional message
    /// </summary>
    /// <example>Success</example>
    public string Message { get; set; }
}

public class ResponseDto<T> : ResponseDto
{
    public ResponseDto() : base() { }
    public ResponseDto(T? data) : base()
    {
        Data = data;
    }

    public ResponseDto(int code, string message, T? data) : base(code, message)
    {
        Data = data;
    }

    /// <summary>
    /// Response payload
    /// </summary>
    public T? Data { get; set; }
}