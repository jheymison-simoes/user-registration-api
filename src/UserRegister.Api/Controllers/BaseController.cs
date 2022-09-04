using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserRegister.Api.ViewModels;

namespace UserRegister.Api.Controllers;

public abstract class BaseController<TController> : ControllerBase
{
    protected ILogger<TController> Logger;
    protected IMapper Mapper;
    
    public BaseController(
        ILogger<TController> logger,
        IMapper mapper)
    {
        Logger = logger;
        Mapper = mapper;
    }

    protected ActionResult<BaseResponse<T>> BaseResponseSuccess<T>(T response)
    {
        return StatusCode((int)HttpStatusCode.OK, GenerateBaseResponse<T>(response));
    }
    
    protected ActionResult<BaseResponse<T>> BaseResponseError<T>(string messageError)
    {
        return StatusCode((int)HttpStatusCode.BadRequest, GenerateBaseResponse(messageError));
    }
    
    protected ActionResult<BaseResponse<T>> BaseResponseInternalError<T>(string messageError)
    {
        return StatusCode((int)HttpStatusCode.InternalServerError, GenerateBaseResponse(messageError));
    }

    #region Private Methods
    private static BaseResponse<T> GenerateBaseResponse<T>(T response)
    {
        return new BaseResponse<T>()
        {
            ContainError = false,
            MessageError = null,
            Response = response
        };
    }
    
    private static BaseResponse<string> GenerateBaseResponse(string messageError)
    {
        return new BaseResponse<string>()
        {
            ContainError = true,
            MessageError = messageError,
            Response = null
        };
    }
    #endregion

}