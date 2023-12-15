#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ExaminationRooms.Web/ExaminationRooms.Web.csproj", "ExaminationRooms.Web/"]
COPY ["ExaminationRooms.Infrastructure/ExaminationRooms.Infrastructure.csproj", "ExaminationRooms.Infrastructure/"]
COPY ["ExaminationRooms.Domain/ExaminationRooms.Domain.csproj", "ExaminationRooms.Domain/"]
RUN dotnet restore "ExaminationRooms.Web/ExaminationRooms.Web.csproj"
COPY . .
WORKDIR "/src/ExaminationRooms.Web"
RUN dotnet build "ExaminationRooms.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExaminationRooms.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExaminationRooms.Web.dll"]