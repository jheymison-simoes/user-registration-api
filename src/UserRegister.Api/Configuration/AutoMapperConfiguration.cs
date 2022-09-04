using AutoMapper;
using UserRegister.Api.ViewModels;
using UserRegister.Business.EntityModels;
using UserRegister.Business.Models;
using UserRegister.Business.Response;

namespace UserRegister.Api.Configuration;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<CreateUserViewModel, CreateUserModel>().ReverseMap();
        CreateMap<CreateUserAddressViewModel, CreateUserAddressModel>().ReverseMap();
        CreateMap<CreateUserPhoneViewModel, CreateUserPhoneModel>().ReverseMap();
        CreateMap<CreateUserModel, User>().ReverseMap();
        CreateMap<CreateUserAddressModel, Address>().ReverseMap();
        CreateMap<CreateUserPhoneModel, UserPhone>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<Address, AddressResponse>().ReverseMap();
        CreateMap<UserPhone, UserPhoneResponse>().ReverseMap();
    }
    
}