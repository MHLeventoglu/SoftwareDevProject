# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Projeyi kopyala ve restore yap
COPY ./ ./
RUN dotnet restore

# Yayınla (publish) Release modunda
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Uygulamayı çalıştır
ENTRYPOINT ["dotnet", "backend.dll"]

EXPOSE 5070
