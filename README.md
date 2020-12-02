# COMCMS_NETCORE
COMCMS NETCORE版本,一个简单的CMS后台管理系统，带前台演示。
主要是演示了.net core 一个系统后台如何搭建。前台如何使用。可以简单的完成一个企业站。通过二次开发，可以支持商城系统、小程序、app服务器端等...


### 更新日志
- 2020-12-02 升级到.net5;增加文章栏目、商品分类快速修改排序；增加jwt授权登录
- 2020-11-11 管理组管理增加控制普通管理组的文章和商品权限，文章、商品操作增加权限判断
- 2020-10-11 更新组件，兼容IE11
- 2020-08-06 增加数据字典管理
- 2020-04-27 增加程序初始化数据，支持Mysql（windows和linux）、Sqlserver 数据库
- 2020-03-06 更新新的demo地址

### 技术简要

- .net 5 (请注意升级本地SDK 或者runtime)
- 数据库：Mysql 5.7 /Mysql 8.0 / SqlServer 2008+
- ORM:数据库操作使用XCode，目前支持mysql、sqlserver。详细见：https://github.com/NewLifeX/X
- 据库驱动使用：MySQL官方驱动，8.0.22
- 后台模板是H+
- 编辑器采用CKeditor 支持截图粘贴进去，上传图片采用webuploader

### 文件夹介绍

- COMCMS.API 目前还没用到，预留api的第三对接类库

- COMCMS.Common 通用帮助函数类库

- COMCMS.Core 数据库操作核心业务逻辑和实体 采用XCode

- COMCMS.Web .net 5 的MVC网站。

- XCoder 代码生成器，跟上述的没任何关系，只是为了生成数据库操作业务逻辑和实体而已。

- Lib.Core 部分中间件

- NewLife.UserGroup.WebUploader 大文件上传

- WebTest 测试站点；暂时屏蔽

- data 目录是初始化sql ，目前是mysql，注意linux版本要还原comcms_for_linux.sql

### 演示地址

```
演示地址1（windows server 2016 + IIS）：前台：[前台演示地址](http://demo.comcms.com) 后台：[后台演示地址](http://demo.comcms.com/AdminCP)
演示地址2（CentOS + Nginx）：前台：[前台演示地址](http://demo.comcms.cn) 后台：[后台演示地址](http://demo.comcms.cn/AdminCP)

账号密码都是admin
```

### 技术交流群
 **1600800** 

### 贡献者
漫遊者(hogenwang)、笑笑(xxred)、一只萌新(HTR)

### 捐赠
- 如果您觉得本源码对您有所帮助，可以给我捐赠一杯咖啡

![捐赠微信码](https://images.gitee.com/uploads/images/2018/1202/202616_4bcf10db_390643.jpeg "")       ![捐赠支付宝码](https://images.gitee.com/uploads/images/2018/1202/202707_fd6b1cb4_390643.jpeg "")