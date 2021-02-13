FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY . .
WORKDIR /RealWorldOne.UserManagement.Api/

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY --from=build /RealWorldOne.UserManagement.Api/out app
WORKDIR /app
EXPOSE 5001
ENTRYPOINT ["dotnet", "RealWorldOne.UserManagement.Api.dll"]