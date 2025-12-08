# COMCMS_NETCORE

COMCMS NETCORE版本,一个简单的CMS后台管理系统，带前台演示。
主要是演示了.net 10 一个系统后台如何搭建。前台如何使用。可以简单的完成一个企业站。通过二次开发，可以支持商城系统、小程序、app服务器端等...

### 更新日志
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
- 编辑器采用CKeditor 支持截图粘贴进去，上传图片采用webuploader

### 文件夹介绍

- COMCMS.API 目前还没用到，预留api的第三对接类库

- COMCMS.Common 通用帮助函数类库

- COMCMS.Core 数据库操作核心业务逻辑和实体 采用XCode

- COMCMS.Web .net 10 的MVC网站。

- XCoder 代码生成器，跟上述的没任何关系，只是为了生成数据库操作业务逻辑和实体而已。

- Lib.Core 部分中间件

- NewLife.UserGroup.WebUploader 大文件上传

- WebTest 测试站点；暂时屏蔽

- data 目录是初始化sql ，目前是mysql，注意linux版本要还原comcms_for_linux.sql
<<<<<<< HEAD

### 演示地址

```
演示地址1（windows server 2016 + IIS）：前台：[前台演示地址](http://demo.comcms.com) 后台：[后台演示地址](http://demo.comcms.com/AdminCP)
演示地址2（CentOS + Nginx）：前台：[前台演示地址](http://demo.comcms.cn) 后台：[后台演示地址](http://demo.comcms.cn/AdminCP)

账号密码都是admin
```
=======

### Docker 快速部署

#### 一键启动

```bash
# 克隆项目
git clone https://github.com/hogenwang/comcms_core.git
cd comcms_core

# 启动所有服务(MySQL + Redis + Web)
docker-compose up -d

# 查看日志
docker logs -f comcms_web
```

#### 访问地址

- **前台首页**: http://localhost:8080
- **后台管理**: http://localhost:8080/AdminCP
- **默认账号**: admin
- **默认密码**: admin

#### 自动初始化说明

Docker环境会在首次启动时**自动初始化数据库**:

1. MySQL容器启动时检测到数据库为空
2. 自动执行 `./data/comcms_for_linux.sql` 初始化脚本
3. 创建所有表结构和初始数据(包括默认管理员账号)
4. Web应用启动后即可直接使用

如需重新初始化数据库:

```bash
# 停止并删除所有容器和数据卷
docker-compose down -v

# 重新启动(会自动初始化)
docker-compose up -d
```

#### 环境配置

所有配置通过环境变量管理,无需修改代码:

- **MySQL配置**: 在 `compose.yaml` 中修改 `MYSQL_*` 环境变量
- **Redis配置**: 在 `compose.yaml` 中修改 `REDIS_*` 环境变量
- **数据持久化**: 数据自动保存在Docker卷中,重启不丢失

### 本地开发

```bash
# 使用 run.sh 脚本启动(需要先配置本地MySQL和Redis)
./run.sh

# 访问地址
http://localhost:5000
```

### 演示地址

- **演示地址1**（Windows Server 2016 + IIS）
  - 前台：http://demo.comcms.com
  - 后台：http://demo.comcms.com/AdminCP
  
- **演示地址2**（CentOS + Nginx）
  - 前台：http://demo.comcms.cn
  - 后台：http://demo.comcms.cn/AdminCP

**账号密码都是 admin**
>>>>>>> df478d351626c40c987712d66d991f327a698ff5


### 技术交流群

**1600800**

### 贡献者

- 漫遊者(hogenwang)
- 笑笑(xxred)
- 一只萌新(HTR)
- 兼哲（github: ferocknew）

### 捐赠

- 如果您觉得本源码对您有所帮助，可以给我捐赠一杯咖啡

![捐赠微信码](https://images.gitee.com/uploads/images/2018/1202/202616_4bcf10db_390643.jpeg "")       ![捐赠支付宝码](https://images.gitee.com/uploads/images/2018/1202/202707_fd6b1cb4_390643.jpeg "")