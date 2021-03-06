#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
RUN apt-get update -qq && apt-get -y install libgdiplus libc6-dev
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SCGL.OMS.IMEX.TAX.Api/SCGL.OMS.IMEX.TAX.Api.csproj", "SCGL.OMS.IMEX.TAX.Api/"]
COPY ["SCGL.Common/SCGL.Common.csproj", "SCGL.Common/"]
RUN dotnet restore "SCGL.OMS.IMEX.TAX.Api/SCGL.OMS.IMEX.TAX.Api.csproj"
COPY . .
WORKDIR "/src/SCGL.OMS.IMEX.TAX.Api"
RUN dotnet build "SCGL.OMS.IMEX.TAX.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCGL.OMS.IMEX.TAX.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV TZ=Asia/Bangkok
RUN apt-get install tzdata -y

ENTRYPOINT ["dotnet", "SCGL.OMS.IMEX.TAX.Api.dll", "--environment=Development"]