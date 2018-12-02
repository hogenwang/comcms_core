# COMCMS_NETCORE
COMCMS NETCORE版本,一个简单的CMS后台管理系统，暂时不带前台演示，后面有时间会加上。
主要是演示了.net core 一个系统后台如何搭建。前台如何使用。可以简单的完成一个企业站。通过二次开发，可以支持商城系统、小程序、app服务器端等...

### 技术简要

- .net core 2.1.6 (请注意升级本地SDK 或者runtime)
- 数据库：Mysql 5.7 /Mysql 8.0
- ORM:数据库操作使用XCode，目前支持mysql、sqlserver。详细见：https://github.com/NewLifeX/X
- 据库驱动使用：MySQL官方驱动，8.0.13
- 后台模板是H+
- 编辑器采用CKeditor 支持截图粘贴进去，上传图片采用webuploader

### 文件夹介绍

- COMCMS.API 目前还没用到，预留api的第三对接类库

- COMCMS.Common 通用帮助函数类库

- COMCMS.Core 数据库操作核心业务逻辑和实体 采用XCode

- COMCMS.Web .net core 2.1 的MVC网站。

- XCoder 代码生成器，跟上述的没任何关系，只是为了生成数据库操作业务逻辑和实体而已。

- Lib.Core 部分中间件

- NewLife.UserGroup.WebUploader 大文件上传

- WebTest 测试站点

- data 目录是初始化sql ，目前是mysql

### 演示地址

```
演示地址（windows server 2019 + IIS）：前台：http://123.207.59.192 后台：[http://123.207.59.192/AdminCP](http://123.207.59.192/AdminCP)
演示地址2（Centos + Nginx）：后台[http://47.106.32.133/AdminCP]

账号密码都是admin

特别说明：因为测试地址用的是ip。而且没有使用https。所以，经常会给运营商劫持加入广告。所以如果登录不进去，请刷新一次试试
```

### 技术交流群
 **1600800** 

### 贡献者
漫遊者(hogenwang)、笑笑(xxred)、一只萌新(HTR)

### 捐赠
- 如果您觉得本源码对您有用，可以给我捐赠一杯咖啡

![捐赠微信码](https://images.gitee.com/uploads/images/2018/1202/202616_4bcf10db_390643.jpeg "s_wx.jpg")  ![捐赠支付宝码](https://images.gitee.com/uploads/images/2018/1202/202707_fd6b1cb4_390643.jpeg "s_zfb.jpg")