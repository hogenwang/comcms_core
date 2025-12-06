#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# 构建阶段
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 复制项目文件
COPY ["COMCMS.Web/COMCMS.Web.csproj", "COMCMS.Web/"]
COPY ["COMCMS.Core/COMCMS.Core.csproj", "COMCMS.Core/"]
COPY ["COMCMS.Common/COMCMS.Common.csproj", "COMCMS.Common/"]
COPY ["COMCMS.API/COMCMS.API.csproj", "COMCMS.API/"]
COPY ["Lib.Core/Lib.Core.csproj", "Lib.Core/"]
COPY ["NewLife.UserGroup.WebUploader/NewLife.UserGroup.WebUploader.csproj", "NewLife.UserGroup.WebUploader/"]

# 还原依赖
RUN dotnet restore "COMCMS.Web/COMCMS.Web.csproj"

# 复制所有源代码
COPY . .

# 构建项目
WORKDIR "/src/COMCMS.Web"
RUN dotnet build "COMCMS.Web.csproj" -c Release -o /app/build

# 发布阶段
FROM build AS publish
RUN dotnet publish "COMCMS.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 运行时阶段
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80

# 创建必要的目录
RUN mkdir -p /app/wwwroot/userfiles \
    && mkdir -p /app/logs

# 从发布阶段复制文件
COPY --from=publish /app/publish .

# 设置权限
RUN chown -R app:app /app
USER app

# 健康检查
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost/ || exit 1

ENTRYPOINT ["dotnet", "COMCMS.Web.dll"]