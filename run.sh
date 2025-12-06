#!/bin/bash
set -e

# 检查运行模式
MODE="${1:-dev}"

# 开发模式才加载 .env 文件
if [ "$MODE" != "prod" ]; then
    if [ -f .env ]; then
        echo "正在加载 .env 配置..."
        export $(cat .env | grep -v '^#' | grep -v '^$' | xargs)
    else
        echo "提示: .env 文件不存在，使用默认配置"
    fi
fi

# 设置数据库连接字符串（使用环境变量，注意：冒号用双下划线替代）
export connectionStrings__dbconn__connectionString="Server=${MYSQL_HOST:-localhost};Port=${MYSQL_PORT:-3306};Database=${MYSQL_DATABASE:-comcms};Uid=${MYSQL_USER:-root};Pwd=${MYSQL_PASSWORD:-root};charset=utf8mb4;AllowUserVariables=True"
export connectionStrings__dbconn__providerName="MySql.Data.MySqlClient"

# 设置 Redis 连接字符串
export RedisCache__ConnectionString="server=${REDIS_HOST:-127.0.0.1};password=${REDIS_PASSWORD};db=${REDIS_DB:-2}"
export RedisCache__InstanceName="${REDIS_INSTANCE_NAME:-COMCMS:}"
export RedisCache__DefaultDatabase="${REDIS_DB:-2}"

# 设置其他配置
export SystemSetting__JwtSecret="${JWT_SECRET:-comcms0b311362786dabe03af7b484ed032b91698a4eb9377687bd031d837082}"
export SystemSetting__SignSalt="${SIGN_SALT:-comcms}"

# 设置环境
export ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENVIRONMENT:-Development}"

# 显示配置信息（隐藏密码）
echo "================================"
echo "COMCMS 启动配置 [模式: $MODE]"
echo "  MySQL: ${MYSQL_HOST}:${MYSQL_PORT}/${MYSQL_DATABASE}"
echo "  Redis: ${REDIS_HOST}"
echo "  环境: ${ASPNETCORE_ENVIRONMENT}"
echo "================================"

# 根据模式启动
if [ "$MODE" = "prod" ]; then
    # 生产模式：使用已编译的dll（Docker容器中已在正确目录）
    echo "生产模式启动..."
    if [ -f "COMCMS.Web.dll" ]; then
        exec dotnet COMCMS.Web.dll
    else
        cd COMCMS.Web
        exec dotnet COMCMS.Web.dll
    fi
else
    # 开发模式：使用dotnet run
    echo "开发模式启动..."
    cd COMCMS.Web
    dotnet run --urls "http://localhost:${PORT:-5000}"
fi
