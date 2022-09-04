#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/UserRegister.Api/UserRegister.Api.csproj", "src/UserRegister.Api/"]
COPY ["src/UserRegister.Data/UserRegister.Data.csproj", "src/UserRegister.Data/"]
COPY ["src/UserRegister.Business/UserRegister.Business.csproj", "src/UserRegister.Business/"]
COPY ["src/UserRegister.Application/UserRegister.Application.csproj", "src/UserRegister.Application/"]
RUN dotnet restore "src/UserRegister.Api/UserRegister.Api.csproj"
COPY . .
WORKDIR "/src/src/UserRegister.Api"
RUN dotnet build "UserRegister.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserRegister.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserRegister.Api.dll"]