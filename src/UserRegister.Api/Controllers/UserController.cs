using System.Globalization;
using System.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserRegister.Api.ViewModels;
using UserRegister.Business.Exceptions;
using UserRegister.Business.Interfaces.Services;
using UserRegister.Business.Models;
using UserRegister.Business.Response;

namespace UserRegister.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController<UserController>
{
    private readonly IUserService _userService;
    
    public UserController(
        ILogger<UserController> logger, 
        IMapper mapper,
        ResourceManager resourceManager,
        CultureInfo cultureInfo,
        IUserService userService) 
        : base(logger, mapper)
    {
        resourceManager.GetResourceSet(cultureInfo, true, true);
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<ActionResult<BaseResponse<UserResponse>>> Create(CreateUserViewModel req)
    {
        try
        {
            var reqModel = Mapper.Map<CreateUserModel>(req);
            var result = await _userService.CreateUser(reqModel);
            return BaseResponseSuccess(result);
        }
        catch (CustomException cEx)
        {
            return BaseResponseError<UserResponse>(cEx.Message);
        }
        catch (Exception ex)
        {
            return BaseResponseInternalError<UserResponse>(ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<BaseResponse<List<UserResponse>>>> GetAll()
    {
        try
        {
            var result = await _userService.GetAll();
            return BaseResponseSuccess(result);
        }
        catch (CustomException cEx)
        {
            return BaseResponseError<List<UserResponse>>(cEx.Message);
        }
        catch (Exception ex)
        {
            return BaseResponseInternalError<List<UserResponse>>(ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<BaseResponse<UserResponse>>> Delete(Guid id)
    {
        try
        {
            var result = await _userService.Delete(id);
            return BaseResponseSuccess(result);
        }
        catch (CustomException cEx)
        {
            return BaseResponseError<UserResponse>(cEx.Message);
        }
        catch (Exception ex)
        {
            return BaseResponseInternalError<UserResponse>(ex.Message);
        }
    }
}