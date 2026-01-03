FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["OnlineLearningPlatform.csproj", "./"]
RUN dotnet restore "OnlineLearningPlatform.csproj"
COPY . .
RUN dotnet build "OnlineLearningPlatform.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineLearningPlatform.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineLearningPlatform.dll"]
