#!/bin/bash

# 加载 .env 文件
if [ -f .env ]; then
    echo "正在加载 .env 配置..."
    export $(cat .env | grep -v '^#' | grep -v '^$' | xargs)
else
    echo "警告: .env 文件不存在，使用默认配置"
fi

# 设置数据库连接字符串（使用环境变量）
export ConnectionStrings__dbconn__ConnectionString="Server=${MYSQL_HOST:-localhost};Port=${MYSQL_PORT:-3306};Database=${MYSQL_DATABASE:-comcms};Uid=${MYSQL_USER:-root};Pwd=${MYSQL_PASSWORD:-root};charset=utf8mb4"
export ConnectionStrings__dbconn__providerName="MySql.Data.MySqlClient"

# 设置 Redis 连接字符串
export RedisCache__ConnectionString="server=${REDIS_HOST:-127.0.0.1}:${REDIS_PORT:-6379};password=${REDIS_PASSWORD};db=${REDIS_DB:-2}"

# 设置其他配置
export SystemSetting__JwtSecret="${JWT_SECRET:-comcms0b311362786dabe03af7b484ed032b91698a4eb9377687bd031d837082}"
export SystemSetting__SignSalt="${SIGN_SALT:-comcms}"

# 设置环境
export ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENVIRONMENT:-Development}"

# 显示配置信息（隐藏密码）
echo "================================"
echo "启动配置："
echo "  MySQL: ${MYSQL_HOST}:${MYSQL_PORT}/${MYSQL_DATABASE}"
echo "  Redis: ${REDIS_HOST}:${REDIS_PORT}"
echo "  环境: ${ASPNETCORE_ENVIRONMENT}"
echo "================================"

# 进入项目目录并运行
cd COMCMS.Web
dotnet run --urls "http://localhost:${PORT:-5000}"
