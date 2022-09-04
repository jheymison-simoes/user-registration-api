using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserRegister.Api.ViewModels;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Interfaces.Services;

namespace UserRegister.Api.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ExempleController : BaseController<ExempleController>
{
    private readonly IExempleService _exempleService;
    public ExempleController(
        ILogger<ExempleController> logger,
        IMapper mapper,
        IExempleService exempleService) : base(logger, mapper)
    {
        _exempleService = exempleService;
    }
    
    [HttpGet("Exemple")]
    public async Task<ActionResult<BaseResponse<string>>> UserRegister()
    {
        try
        {
            var result = await _exempleService.GetString();
            return BaseResponseSuccess(result);
        }
        catch (CustomException cEx)
        {
            return BaseResponseError(cEx.Message);
        }
        catch (Exception ex)
        {
            return BaseResponseInternalError(ex.Message);
        }
    }
}