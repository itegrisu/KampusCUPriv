# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Projeyi kopyala
COPY . .

# API projesine geç ve restore/publish işlemini yap
WORKDIR /src/Presentation/API
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Build aşamasından yayınlanan dosyaları kopyala
COPY --from=build /app/publish .

# Uygulamanın dışarıya açılacağı portu belirt
EXPOSE 5000

# ASP.NET Core'un tüm IP'lerden dinlemesi için ortam değişkeni
ENV ASPNETCORE_URLS=http://0.0.0.0:5000

# Uygulamayı başlat
ENTRYPOINT ["dotnet", "API.dll"]