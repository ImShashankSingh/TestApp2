using System;
namespace TestApp.Services
{
    public interface IRestResult
    {
        bool IsSuccess { get; }
        string Message { get; set; }
        int StatusCode { get; set; }
    }

    public interface IRestResult<T> : IRestResult
    {
        T Data { get; set; }
    }
}
