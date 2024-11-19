using Domain.Enum;

namespace Domain.Response;

public interface IBaseResponce<T>
{
    T Data { get; set; }
}

public class BaseResponce<T> : IBaseResponce<T>
{
    public string Description { get; set; }
    
    public StatusCode StatusCode { get; set; }
    
    public T Data { get; set; }
}