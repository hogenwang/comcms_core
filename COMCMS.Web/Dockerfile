#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80

# 创建必要的目录
RUN mkdir -p /app/wwwroot/userfiles \
    && mkdir -p /app/logs

# 设置权限
RUN chown -R app:app /app
USER app

COPY . .

# 健康检查
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost/ || exit 1

ENTRYPOINT ["dotnet", "COMCMS.Web.dll"]