using Domain.Enum;

namespace Domain.Response;

public interface IBaseResponce<T> { T Data { get; set; } }

public class BaseResponse<T> : IBaseResponce<T>
{
    public string Description { get; set; }
    public StatusCode StatusCode { get; set; }
    public T Data { get; set; }
}

