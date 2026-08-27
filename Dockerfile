FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish EmployeeSalaryManagementSystem.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_ENVIRONMENT=Production
# Disable file-watching for config reload — containers often can't use
# inotify (the Linux file-watching feature), causing a crash on startup
# otherwise. Config doesn't need to hot-reload in production anyway.
ENV DOTNET_hostBuilder__reloadConfigOnChange=false
CMD ASPNETCORE_URLS=http://0.0.0.0:$PORT dotnet EmployeeSalaryManagementSystem.dll
