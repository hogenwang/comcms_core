# COMCMS_NETCORE

COMCMS NETCORE版本,一个简单的CMS后台管理系统，带前台演示。
主要是演示了.net 10 一个系统后台如何搭建。前台如何使用。可以简单的完成一个企业站。通过二次开发，可以支持商城系统、小程序、app服务器端等...

### 更新日志
- 2026-08-13 完成安全与认证第一轮加固：统一 Cookie/JWT 认证、密码渐进迁移、会话撤销、CSRF、防重放、上传和异常响应防护；移除旧 WebUploader 项目依赖并统一使用 jQuery 3
- 2025-11-13 升级到.net 10
- 2024-06-26 升级到.net 8 调整后台UI，精简js插件
- 2022-11-23 升级到.net 7 和使用SkiaSharp 替换掉System.Drawing
- 2021-02-02 升级组件，删除ZKWeb组件，增加文章Tag标签
- 2020-12-02 升级到.net5;增加文章栏目、商品分类快速修改排序；增加jwt授权登录
- 2020-11-11 管理组管理增加控制普通管理组的文章和商品权限，文章、商品操作增加权限判断
- 2020-10-11 更新组件，兼容IE11
- 2020-08-06 增加数据字典管理
- 2020-04-27 增加程序初始化数据，支持Mysql（windows和linux）、Sqlserver 数据库
- 2020-03-06 更新新的demo地址

### 技术简要

- .net 10 (请注意升级本地SDK 或者runtime)
- 数据库：Mysql 5.7 /Mysql 8.0 / SqlServer 2008+
- ORM:数据库操作使用XCode，目前支持mysql、sqlserver。详细见：https://github.com/NewLifeX/X
- 据库驱动使用：MySQL官方驱动，9.5
- 后台模板是H+
- 富文本编辑器采用自托管 Jodit 4，支持截图粘贴、图片拖拽上传、附件上传和 MP4/WebM/Ogg 视频上传；其他图片字段继续使用 WebUploader

### 文件夹介绍

- COMCMS.API 目前还没用到，预留api的第三对接类库

- COMCMS.Common 通用帮助函数类库

- COMCMS.Core 数据库操作核心业务逻辑和实体 采用XCode

- COMCMS.Web .net 10 的MVC网站。

- XCoder 代码生成器，跟上述的没任何关系，只是为了生成数据库操作业务逻辑和实体而已。

- Lib.Core 部分中间件

- WebUploader 前端组件提供大文件上传功能

- WebTest 已升级到 .NET 10，仅作为兼容性测试站点，不包含旧版 ASP.NET Core 2.2 依赖

- `database/install` 是新安装基线 SQL，`database/migrations` 是现有数据库的版本化迁移；`data` 仅保留历史参考，不能用于新安装。

### 演示地址

```
演示地址1（windows server 2016 + IIS）：前台：[前台演示地址](http://demo.comcms.com) 后台：[后台演示地址](http://demo.comcms.com/AdminCP)
演示地址2（CentOS + Nginx）：前台：[前台演示地址](http://demo.comcms.cn) 后台：[后台演示地址](http://demo.comcms.cn/AdminCP)

演示站点账号以站点公告为准。源码和新安装流程不会内置 `admin/admin` 默认管理员。
```

### 安全与认证更新

当前安全方案的代码实现完成度约为 85%（第一至第五阶段中可在现有宿主内落地的核心项已完成）。后台和同站会员网页使用 ASP.NET Core 加密 Cookie；移动端和独立 API 客户端使用短期 JWT，刷新令牌只保存哈希并支持轮换、重放检测、设备撤销和全量下线。密码新写入 PBKDF2，旧版 MD5 在成功登录后渐进升级；登录限流、账号锁定、CSRF、可信代理、ProblemDetails、上传路径和 SSRF 防护已接入。`COMCMS.SecurityTests` 当前包含 40 项安全回归测试。

仓库配置不保存数据库、JWT、支付或微信密钥。开发机请创建被 `.gitignore` 忽略的 `COMCMS.Web/appsettings.Development.local.json`，例如：

```json
{
  "connectionStrings": {
    "dbconn": {
      "connectionString": "Server=.;Port=3306;Database=comcms;Uid=root;Pwd=root;charset=utf8mb4",
      "providerName": "MySql.Data.MySqlClient"
    }
  }
}
```

生产环境必须通过环境变量或密钥服务配置 Redis、数据库、RSA JWT 密钥、可信代理和支付/微信/SMTP 凭据，并在上线前轮换历史已暴露值。生产启动会拒绝空 Redis、空 RSA 密钥和占位 `kid`；数据库迁移脚本位于 `database/migrations`，不会由应用自动改表。

仍需单独排期的增强项：CSP 从 Report-Only 切换为正式策略、结构化指标和告警、密码迁移窗口结束后的旧算法关闭，以及 MFA/OIDC/微信统一登录。旧 API 已限制分页并使用公开字段投影，后续可继续把匿名投影整理为共享强类型 DTO。


### 技术交流群

**1600800**

### 贡献者

漫遊者(hogenwang)、笑笑(xxred)、一只萌新(HTR)

### 捐赠

- 如果您觉得本源码对您有所帮助，可以给我捐赠一杯咖啡

![捐赠微信码](https://images.gitee.com/uploads/images/2018/1202/202616_4bcf10db_390643.jpeg "")       ![捐赠支付宝码](https://images.gitee.com/uploads/images/2018/1202/202707_fd6b1cb4_390643.jpeg "")
