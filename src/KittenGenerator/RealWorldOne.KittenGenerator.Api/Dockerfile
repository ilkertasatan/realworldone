FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
COPY . /src/RealWorldOne.KittenGenerator.Api
WORKDIR /src/RealWorldOne.KittenGenerator.Api/

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
COPY --from=build /src/RealWorldOne.KittenGenerator.Api/out app
WORKDIR /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "RealWorldOne.KittenGenerator.Api.dll"]