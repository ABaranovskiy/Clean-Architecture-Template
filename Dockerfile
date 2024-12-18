# Сборка ShishByzh.Client
FROM node:20 AS client-build
WORKDIR /home/Shish-Byzh.Client
COPY ["ShishByzh.Client/", "."]
RUN apt update
RUN npm install
RUN npm install -g @angular/cli@18.0.2
RUN ng build --configuration production --output-path=/home/wwwroot

# Сборка ShishByzh.Server
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS server-build
WORKDIR /home/ShishByzh.Server
COPY ["ShishByzh.Server/ShishByzh.Server.csproj", "ShishByzh.Server/"]
RUN dotnet restore "ShishByzh.Server/ShishByzh.Server.csproj"
COPY . . 
RUN dotnet build "ShishByzh.Server/ShishByzh.Server.csproj" -c Release -o /home/ShishByzh.Server/build
RUN dotnet publish "ShishByzh.Server/ShishByzh.Server.csproj" -c Release -o /home/ShishByzh.Server/publish /p:UseAppHost=true

# Сборка ShishByzh.Identity
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS identity-build
WORKDIR /home/ShishByzh.Identity
COPY ["ShishByzh.Identity/ShishByzh.Identity.csproj", "ShishByzh.Identity/"]
RUN dotnet restore "ShishByzh.Identity/ShishByzh.Identity.csproj"
COPY . . 
RUN dotnet build "ShishByzh.Identity/ShishByzh.Identity.csproj" -c Release -o /home/ShishByzh.Identity/build
RUN dotnet publish "ShishByzh.Identity/ShishByzh.Identity.csproj" -c Release -o /home/ShishByzh.Identity/publish /p:UseAppHost=true

# Общий базовый образ для Server и Identity
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base

# Финальный образ для ShishByzh.Server
FROM base AS server-final
WORKDIR /home/ShishByzh.Server/publish
COPY --from=server-build ["/home/ShishByzh.Server/publish", "/home/ShishByzh.Server/publish"]
EXPOSE 8080
ENTRYPOINT ["dotnet", "ShishByzh.Server.dll"]

# Финальный образ для ShishByzh.Identity
FROM base AS identity-final
WORKDIR /home/ShishByzh.Identity/publish
COPY --from=identity-build ["/home/ShishByzh.Identity/publish", "/home/ShishByzh.Identity/publish"]
EXPOSE 8081
ENTRYPOINT ["dotnet", "ShishByzh.Identity.dll"]

# Финальный образ для Angular приложения
FROM nginx:alpine AS client-final
COPY --from=client-build /home/wwwroot /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
