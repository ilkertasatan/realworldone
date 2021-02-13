FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
COPY . .
WORKDIR /RealWorldOne.KittenGenerator.Api

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*
     
COPY --from=build /RealWorldOne.KittenGenerator.Api/out app
WORKDIR /app
EXPOSE 5000
ENTRYPOINT ["dotnet", "RealWorldOne.KittenGenerator.Api.dll"]