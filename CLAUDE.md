# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## 项目概述

COMCMS NetCore 是一个基于 .NET 10 的内容管理系统（CMS），包含后台管理和前台展示功能。支持二次开发扩展为商城系统、小程序、APP服务端等。

## 常用命令

### 构建和运行
```bash
# 构建整个解决方案
dotnet build COMCMS_NETCORE.sln

# 运行 Web 项目（开发模式）
cd COMCMS.Web
dotnet run

# 发布 Web 项目
dotnet publish -c Release -o ./publish

# 使用 Docker 运行
cd COMCMS.Web
docker build -t comcms .
docker run -p 80:80 comcms
```

### 数据库初始化
项目支持 MySQL 5.7/8.0 和 SQL Server 2008+ 数据库。初始化脚本位于 `data` 目录：
- `comcms_for_windows.sql` - Windows 环境的 MySQL 初始化脚本
- `comcms_for_linux.sql` - Linux 环境的 MySQL 初始化脚本
- `comcms_sqlserver.sql` - SQL Server 初始化脚本

### 测试
```bash
# 运行所有测试（如果存在）
dotnet test

# 运行特定项目测试
dotnet test [项目名称]
```

## 项目架构

### 核心组件
1. **COMCMS.Web** - ASP.NET Core MVC 网站，包含前台和后台管理界面
2. **COMCMS.Core** - 核心业务逻辑和数据访问层，使用 XCode ORM
3. **COMCMS.Common** - 通用工具类库
4. **COMCMS.API** - API 接口层（预留）
5. **Lib.Core** - 中间件库
6. **NewLife.UserGroup.WebUploader** - 大文件上传组件
7. **XCoder** - 代码生成器

### 数据层架构
- 使用 **XCode ORM** 进行数据访问，支持 MySQL 和 SQL Server
- 实体类采用部分类设计，包含实体定义（.cs）和业务逻辑（.Biz.cs）
- 每个实体类使用 `[BindTable]` 特性映射到数据库表
- 支持缓存机制，使用 Redis 作为分布式缓存

### Web 层结构
- **前台控制器**：位于 `Controllers/` 目录
- **后台管理**：位于 `Areas/AdminCP/`，使用 MVC Areas 组织
- **API 接口**：位于 `Controllers/api/`，提供 RESTful API
- **视图**：Razor 视图，后台使用 H+ 模板
- **静态资源**：位于 `wwwroot/`，包含编辑器（CKEditor）、上传组件（WebUploader）等

### 功能模块
- **文章系统**：文章管理、栏目管理、标签系统
- **商城模块**：商品管理、订单管理、购物车、优惠券
- **用户系统**：用户管理、角色权限、地址管理、余额管理
- **系统管理**：系统配置、数据字典、菜单权限、日志管理
- **微信集成**：微信公众号、微信支付

## 技术栈

- **.NET 10** - 主框架
- **MySQL/SQL Server** - 数据库
- **XCode ORM** - 数据访问层
- **Redis** - 分布式缓存
- **JWT** - 身份认证
- **CKEditor** - 富文本编辑器
- **WebUploader** - 文件上传
- **Senparc Weixin SDK** - 微信开发

## 配置说明

### 数据库连接
在 `appsettings.json` 中配置数据库连接字符串：
```json
"connectionStrings": {
  "dbconn": {
    "connectionString": "Server=.;Port=3306;Database=comcms;Uid=root;Pwd=root;charset=utf8mb4",
    "providerName": "MySql.Data.MySqlClient"
  }
}
```

### Redis 缓存配置
```json
"RedisCache": {
  "ConnectionString": "server=127.0.0.1;password=;db=2",
  "InstanceName": "COMCMS:",
  "DefaultDatabase": 2
}
```

### 系统配置
```json
"SystemSetting": {
  "FileAllowedExtensions": "zip,rar,7z,doc,docx,xls,xlsx,ppt,pptx,csv,pdf,txt,gz,gzip,gif,png,jpg,jpeg,bmp,avi,fla,flv,swf,mp4,mp3,mov,mid,mpg,mpeg,rm,rmvb,wma,wav,wmv",
  "ImageAllowedExtensions": "gif,png,jpg,jpeg,bmp",
  "JwtSecret": "your-jwt-secret",
  "COMCMSPrefixKey": "abc_"
}
```

## 开发指南

### 添加新功能模块
1. 在 `COMCMS.Core` 中创建实体类和业务逻辑
2. 使用 XCoder 代码生成器生成基础代码
3. 在 `COMCMS.Web` 中添加控制器和视图
4. 更新菜单权限配置

### 权限控制
- 后台使用 `AdminBaseController` 进行登录验证
- 使用 `[MyAuthorize]` 特性进行权限控制
- 菜单和事件权限在 `后台菜单` 和 `目标事件` 表中配置

### 缓存使用
- 通过 `ICacheService` 接口使用缓存
- 支持分布式缓存（Redis）和内存缓存
- 在 `CacheSettings` 中配置不同类型数据的过期时间

### 文件上传
- 使用 WebUploader 组件处理文件上传
- 在 `UploadController` 中处理上传逻辑
- 支持图片、文件、媒体等多种类型

## 部署说明

### Docker 部署
项目包含 Dockerfile，支持容器化部署：
```dockerfile
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
COPY . .
ENTRYPOINT ["dotnet", "COMCMS.Web.dll"]
```

注意：Dockerfile 中的 .NET 版本需要更新到与项目一致的版本。

### 环境要求
- .NET 10 Runtime 或 SDK
- MySQL 5.7+ 或 SQL Server 2008+
- Redis（可选，用于分布式缓存）

### 默认账号
- 后台管理员账号：admin / admin
- 登录地址：/AdminCP

## 重要提示

- 项目使用中文命名实体类和字段，这是为了便于中文开发者理解
- 修改数据库结构后需要更新相应的实体类
- 部署前确保正确配置数据库连接字符串和系统配置
- 建议使用源码管理保存自定义修改，避免升级时覆盖