namespace DocumentationGenerator.Service.Dtos;

public class ResponseDto<T>
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }


    public ResponseDto() { }

    public ResponseDto(T data)
    {
        Code = 0;
        Message = "Success";
        Data = data;
    }

    public ResponseDto(int code, string message, T? data)
    {
        Code = code;
        Message = message;
        Data = data;
    }
}