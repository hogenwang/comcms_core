/*
 Navicat Premium Data Transfer

 Source Server         : 本地数据库
 Source Server Type    : SQL Server
 Source Server Version : 13004001
 Source Host           : localhost:1433
 Source Catalog        : comcms
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 13004001
 File Encoding         : 65001

 Date: 26/04/2020 18:50:38
*/


-- ----------------------------
-- Table structure for Admin
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin]') AND type IN ('U'))
	DROP TABLE [dbo].[Admin]
GO

CREATE TABLE [dbo].[Admin] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UserName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [PassWord] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Salt] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [RealName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Tel] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Email] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserLevel] int DEFAULT ((0)) NOT NULL,
  [RoleId] int DEFAULT ((0)) NOT NULL,
  [GroupId] int DEFAULT ((0)) NOT NULL,
  [LastLoginTime] datetime  NULL,
  [LastLoginIP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [ThisLoginTime] datetime  NULL,
  [ThisLoginIP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsLock] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Admin] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'PassWord'
GO

EXEC sp_addextendedproperty
'MS_Description', N'盐值',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'Salt'
GO

EXEC sp_addextendedproperty
'MS_Description', N'姓名',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'RealName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'电话',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'Tel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮件',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'级别',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'UserLevel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理组',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'RoleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户组',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'GroupId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后登录时间',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'LastLoginTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'上次登录IP',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'LastLoginIP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'本次登录时间',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'ThisLoginTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'本次登录IP',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'ThisLoginIP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否是锁定',
'SCHEMA', N'dbo',
'TABLE', N'Admin',
'COLUMN', N'IsLock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理员',
'SCHEMA', N'dbo',
'TABLE', N'Admin'
GO


-- ----------------------------
-- Records of Admin
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Admin] ON
GO

INSERT INTO [dbo].[Admin] ([Id], [UserName], [PassWord], [Salt], [RealName], [Tel], [Email], [UserLevel], [RoleId], [GroupId], [LastLoginTime], [LastLoginIP], [ThisLoginTime], [ThisLoginIP], [IsLock]) VALUES (N'1', N'admin', N'6671BCF861E8B2FA78BA7786EBC6D14C', N'n9FYh5Pztsba', N'admin', N'', N'', N'100', N'1', N'0', N'2020-04-26 16:35:43.000', N'127.0.0.1', N'2020-04-26 16:35:43.000', N'127.0.0.1', N'0')
GO

SET IDENTITY_INSERT [dbo].[Admin] OFF
GO


-- ----------------------------
-- Table structure for AdminLog
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminLog]') AND type IN ('U'))
	DROP TABLE [dbo].[AdminLog]
GO

CREATE TABLE [dbo].[AdminLog] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [GUID] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [PassWord] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [LoginTime] datetime  NULL,
  [LoginIP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsLoginOK] int DEFAULT ((0)) NOT NULL,
  [Actions] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [LastUpdateTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[AdminLog] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'唯一ID',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'GUID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'PassWord'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录时间',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'LoginTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录IP',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'LoginIP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否登录成功',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'IsLoginOK'
GO

EXEC sp_addextendedproperty
'MS_Description', N'记录',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'Actions'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录时间',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog',
'COLUMN', N'LastUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理日志表',
'SCHEMA', N'dbo',
'TABLE', N'AdminLog'
GO


-- ----------------------------
-- Records of AdminLog
-- ----------------------------
SET IDENTITY_INSERT [dbo].[AdminLog] ON
GO

INSERT INTO [dbo].[AdminLog] ([Id], [UId], [GUID], [UserName], [PassWord], [LoginTime], [LoginIP], [IsLoginOK], [Actions], [LastUpdateTime]) VALUES (N'1', N'0', N'd0f2f8ef-ee33-496e-9599-47cfbb395a56', N'admin', N'******', N'2020-04-26 16:35:43.000', N'127.0.0.1', N'1', NULL, N'2020-04-26 16:35:43.000')
GO

SET IDENTITY_INSERT [dbo].[AdminLog] OFF
GO


-- ----------------------------
-- Table structure for AdminMenu
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminMenu]') AND type IN ('U'))
	DROP TABLE [dbo].[AdminMenu]
GO

CREATE TABLE [dbo].[AdminMenu] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [MenuKey] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [MenuName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PermissionKey] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Link] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [PId] int DEFAULT ((0)) NOT NULL,
  [Level] int DEFAULT ((0)) NOT NULL,
  [Location] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsHide] int DEFAULT ((0)) NOT NULL,
  [Rank] int DEFAULT ((0)) NOT NULL,
  [Icon] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClassName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AdminMenu] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'标识key',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'MenuKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'页面名称',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'MenuName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'页面名称',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'PermissionKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'介绍',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'页面连接地址',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'Link'
GO

EXEC sp_addextendedproperty
'MS_Description', N'上级ID',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'PId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'级别',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'Level'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路径',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'Location'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'样式名称',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu',
'COLUMN', N'ClassName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'后台菜单',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenu'
GO


-- ----------------------------
-- Records of AdminMenu
-- ----------------------------
SET IDENTITY_INSERT [dbo].[AdminMenu] ON
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'1', N'home', N'主页', N'', N'', N'Index/Main', N'0', N'0', N'0,', N'0', N'0', N'', N'fa-home')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'2', N'system', N'系统设置', N'', N'', N'#', N'0', N'0', N'0,', N'0', N'1', N'', N'fa-gears')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'3', N'baseconfig', N'基本配置', N'', N'', N'System/BaseConfig', N'2', N'1', N'0,2,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'4', N'smptconfig', N'SMTP设置', N'', N'', N'System/SmtpConfig', N'2', N'1', N'0,2,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'5', N'attachconfig', N'附件设置', N'', N'', N'System/AttachConfig', N'2', N'1', N'0,2,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'6', N'articlesys', N'文章系统', N'', N'', N'#', N'0', N'0', N'0,', N'0', N'2', N'', N'fa-book')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'7', N'articlecategory', N'文章栏目管理', N'', N'', N'Article/ArticleCategoryList', N'6', N'1', N'0,6,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'8', N'article', N'文章管理', N'', N'', N'Article/ArticleList', N'6', N'1', N'0,6,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'9', N'productsys', N'商品系统', N'', N'', N'#', N'0', N'0', N'0,', N'0', N'3', N'', N'fa-balance-scale')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'10', N'productcategory', N'商品分类管理', N'', N'', N'Product/CategoryList', N'9', N'1', N'0,9,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'11', N'product', N'商品管理', N'', N'', N'Product/ProductList', N'9', N'1', N'0,9,', N'0', N'1', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'12', N'ordersys', N'订单系统', N'', N'', N'#', N'0', N'0', N'0,', N'0', N'4', N'', N'fa-shopping-bag')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'13', N'order', N'商品订单管理', N'', N'', N'Order/OrderList', N'12', N'1', N'0,12,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'14', N'payonline', N'支付记录', N'', N'', N'Order/PayOnlineList', N'12', N'1', N'0,12,', N'0', N'1', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'15', N'user', N'用户系统', N'', N'', N'#', N'0', N'0', N'0,', N'0', N'5', N'', N'fa-user')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'16', N'memberrole', N'用户组管理', N'', N'', N'Member/MemberRole', N'15', N'1', N'0,15,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'17', N'members', N'用户管理', N'', N'', N'Member/Members', N'15', N'1', N'0,15,', N'0', N'1', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'18', N'permissionsys', N'后台权限', N'', N'', N'#', N'0', N'0', N'0,', N'0', N'6', N'', N'fa-users')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'19', N'adminrole', N'管理组管理', N'', N'', N'Member/AdminRole', N'18', N'1', N'0,18,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'20', N'admins', N'管理员管理', N'', N'', N'Member/Admins', N'18', N'1', N'0,18,', N'0', N'1', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'21', N'eventkey', N'事件权限管理', N'', N'', N'Permission/EventKey', N'18', N'1', N'0,18,', N'0', N'3', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'22', N'adminmenu', N'后台栏目管理', N'', N'', N'Permission/AdminMenuList', N'18', N'1', N'0,18,', N'0', N'4', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'23', N'editme', N' 修改密码', N'', N'', N'Member/EditMe', N'18', N'1', N'0,18,', N'0', N'5', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'24', N'guestbooksys', N'留言系统', N'', N'', N'#', N'0', N'0', N'0,', N'0', N'7', N'', N'fa-rss-square')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'25', N'guestbookkinds', N'留言分类管理', N'', N'', N'Guestbook/GuestbookCategorys', N'24', N'1', N'0,24,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'26', N'guestbook', N'留言管理', N'', N'', N'Guestbook/GuestbookList', N'24', N'1', N'0,24,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'27', N'other', N'其他', N'', N'', N'#', N'0', N'0', N'0,', N'0', N'99', N'', N'fa-square-o')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'28', N'adskinds', N'广告分类管理', N'', N'', N'Other/AdsCategoryList', N'27', N'1', N'0,27,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'29', N'ads', N'广告管理', N'', N'', N'Other/AdsList', N'27', N'1', N'0,27,', N'0', N'1', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'30', N'linkkinds', N'友情连接分类管理', N'', N'', N'Other/LinkCategoryList', N'27', N'1', N'0,27,', N'0', N'2', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'31', N'link', N'友情连接管理', N'', N'', N'Other/LinkList', N'27', N'1', N'0,27,', N'0', N'3', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'32', N'weixinsys', N'微信公众号管理', N'', N'', N'#', N'0', N'0', N'0,', N'0', N'8', N'', N'fa-file-word-o')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'33', N'wxautoreply', N'关注自动回复', N'', N'', N'Weixin/SubscribeReply', N'32', N'1', N'0,32,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'34', N'wxmenu', N'自定义菜单管理', N'', N'', N'Weixin/Menu', N'32', N'1', N'0,32,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'35', N'wxkeywordreply', N'关键字回复', N'', N'', N'Weixin/ReplyRule', N'32', N'1', N'0,32,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'36', N'clickreplyrule', N'点击事件自动回复', N'', N'', N'Weixin/ClickReplyRule', N'32', N'1', N'0,32,', N'0', N'0', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'37', N'robots', N'Robots文档设置', N'', N'', N'System/Robots', N'2', N'1', N'0,2,', N'0', N'3', N'', N'')
GO

INSERT INTO [dbo].[AdminMenu] ([Id], [MenuKey], [MenuName], [PermissionKey], [Description], [Link], [PId], [Level], [Location], [IsHide], [Rank], [Icon], [ClassName]) VALUES (N'38', N'admincplog', N'后台管理日志', N'', N'', N'Member/AdminCPLogList', N'18', N'1', N'0,18,', N'0', N'10', N'', N'')
GO

SET IDENTITY_INSERT [dbo].[AdminMenu] OFF
GO


-- ----------------------------
-- Table structure for AdminMenuEvent
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminMenuEvent]') AND type IN ('U'))
	DROP TABLE [dbo].[AdminMenuEvent]
GO

CREATE TABLE [dbo].[AdminMenuEvent] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [MenuId] int DEFAULT ((0)) NOT NULL,
  [MenuKey] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [EventId] int DEFAULT ((0)) NOT NULL,
  [EventKey] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [EventName] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AdminMenuEvent] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenuEvent',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单ID',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenuEvent',
'COLUMN', N'MenuId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'菜单key',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenuEvent',
'COLUMN', N'MenuKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'事件ID',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenuEvent',
'COLUMN', N'EventId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'事件key',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenuEvent',
'COLUMN', N'EventKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'事件名称',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenuEvent',
'COLUMN', N'EventName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'后台菜单对应的事件权限',
'SCHEMA', N'dbo',
'TABLE', N'AdminMenuEvent'
GO


-- ----------------------------
-- Records of AdminMenuEvent
-- ----------------------------
SET IDENTITY_INSERT [dbo].[AdminMenuEvent] ON
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'1', N'1', N'home', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'2', N'3', N'baseconfig', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'3', N'3', N'baseconfig', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'4', N'4', N'smptconfig', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'5', N'4', N'smptconfig', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'6', N'5', N'attachconfig', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'7', N'5', N'attachconfig', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'8', N'7', N'articlecategory', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'9', N'7', N'articlecategory', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'10', N'7', N'articlecategory', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'11', N'7', N'articlecategory', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'12', N'7', N'articlecategory', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'13', N'7', N'articlecategory', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'14', N'8', N'article', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'15', N'8', N'article', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'16', N'8', N'article', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'17', N'8', N'article', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'18', N'8', N'article', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'19', N'8', N'article', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'20', N'8', N'article', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'21', N'8', N'article', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'22', N'8', N'article', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'23', N'8', N'article', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'24', N'8', N'article', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'25', N'10', N'productcategory', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'26', N'10', N'productcategory', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'27', N'10', N'productcategory', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'28', N'10', N'productcategory', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'29', N'10', N'productcategory', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'30', N'10', N'productcategory', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'31', N'11', N'product', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'32', N'11', N'product', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'33', N'11', N'product', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'34', N'11', N'product', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'35', N'11', N'product', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'36', N'11', N'product', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'37', N'11', N'product', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'38', N'11', N'product', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'39', N'11', N'product', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'40', N'11', N'product', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'41', N'11', N'product', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'42', N'13', N'order', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'43', N'13', N'order', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'44', N'13', N'order', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'45', N'13', N'order', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'46', N'13', N'order', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'47', N'13', N'order', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'48', N'13', N'order', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'49', N'13', N'order', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'50', N'13', N'order', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'51', N'13', N'order', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'52', N'13', N'order', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'53', N'14', N'payonline', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'54', N'14', N'payonline', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'55', N'14', N'payonline', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'56', N'14', N'payonline', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'57', N'14', N'payonline', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'58', N'14', N'payonline', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'59', N'14', N'payonline', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'60', N'14', N'payonline', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'61', N'14', N'payonline', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'62', N'14', N'payonline', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'63', N'16', N'memberrole', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'64', N'16', N'memberrole', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'65', N'16', N'memberrole', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'66', N'16', N'memberrole', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'67', N'16', N'memberrole', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'68', N'16', N'memberrole', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'69', N'17', N'members', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'70', N'17', N'members', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'71', N'17', N'members', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'72', N'17', N'members', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'73', N'17', N'members', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'74', N'17', N'members', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'75', N'17', N'members', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'76', N'17', N'members', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'77', N'17', N'members', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'78', N'17', N'members', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'79', N'17', N'members', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'80', N'19', N'adminrole', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'81', N'19', N'adminrole', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'82', N'19', N'adminrole', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'83', N'19', N'adminrole', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'84', N'19', N'adminrole', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'85', N'19', N'adminrole', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'86', N'19', N'adminrole', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'87', N'20', N'admins', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'88', N'20', N'admins', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'89', N'20', N'admins', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'90', N'20', N'admins', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'91', N'20', N'admins', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'92', N'20', N'admins', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'93', N'20', N'admins', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'94', N'20', N'admins', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'95', N'20', N'admins', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'96', N'20', N'admins', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'97', N'20', N'admins', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'98', N'21', N'eventkey', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'99', N'21', N'eventkey', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'100', N'21', N'eventkey', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'101', N'21', N'eventkey', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'102', N'21', N'eventkey', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'103', N'21', N'eventkey', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'104', N'21', N'eventkey', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'105', N'22', N'adminmenu', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'106', N'22', N'adminmenu', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'107', N'22', N'adminmenu', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'108', N'22', N'adminmenu', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'109', N'22', N'adminmenu', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'110', N'22', N'adminmenu', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'111', N'22', N'adminmenu', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'112', N'22', N'adminmenu', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'113', N'22', N'adminmenu', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'114', N'22', N'adminmenu', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'115', N'22', N'adminmenu', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'116', N'23', N'editme', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'117', N'23', N'editme', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'118', N'25', N'guestbookkinds', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'119', N'25', N'guestbookkinds', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'120', N'25', N'guestbookkinds', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'121', N'25', N'guestbookkinds', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'122', N'25', N'guestbookkinds', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'123', N'25', N'guestbookkinds', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'124', N'25', N'guestbookkinds', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'125', N'25', N'guestbookkinds', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'126', N'26', N'guestbook', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'127', N'26', N'guestbook', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'128', N'26', N'guestbook', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'129', N'26', N'guestbook', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'130', N'26', N'guestbook', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'131', N'26', N'guestbook', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'132', N'26', N'guestbook', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'133', N'26', N'guestbook', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'134', N'26', N'guestbook', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'135', N'26', N'guestbook', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'136', N'28', N'adskinds', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'137', N'28', N'adskinds', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'138', N'28', N'adskinds', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'139', N'28', N'adskinds', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'140', N'28', N'adskinds', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'141', N'29', N'ads', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'142', N'29', N'ads', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'143', N'29', N'ads', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'144', N'29', N'ads', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'145', N'29', N'ads', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'146', N'30', N'linkkinds', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'147', N'30', N'linkkinds', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'148', N'30', N'linkkinds', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'149', N'30', N'linkkinds', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'150', N'30', N'linkkinds', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'151', N'31', N'link', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'152', N'31', N'link', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'153', N'31', N'link', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'154', N'31', N'link', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'155', N'31', N'link', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'156', N'31', N'link', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'157', N'31', N'link', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'158', N'31', N'link', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'159', N'31', N'link', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'160', N'31', N'link', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'161', N'31', N'link', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'162', N'33', N'wxautoreply', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'163', N'33', N'wxautoreply', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'164', N'34', N'wxmenu', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'165', N'34', N'wxmenu', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'166', N'35', N'wxkeywordreply', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'167', N'35', N'wxkeywordreply', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'168', N'35', N'wxkeywordreply', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'169', N'35', N'wxkeywordreply', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'170', N'35', N'wxkeywordreply', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'171', N'35', N'wxkeywordreply', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'172', N'35', N'wxkeywordreply', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'173', N'35', N'wxkeywordreply', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'174', N'35', N'wxkeywordreply', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'175', N'35', N'wxkeywordreply', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'176', N'35', N'wxkeywordreply', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'177', N'36', N'clickreplyrule', N'1', N'add', N'添加')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'178', N'36', N'clickreplyrule', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'179', N'36', N'clickreplyrule', N'3', N'del', N'删除')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'180', N'36', N'clickreplyrule', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'181', N'36', N'clickreplyrule', N'5', N'viewlist', N'查看列表')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'182', N'36', N'clickreplyrule', N'6', N'import', N'导入')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'183', N'36', N'clickreplyrule', N'7', N'export', N'导出')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'184', N'36', N'clickreplyrule', N'8', N'filter', N'搜索')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'185', N'36', N'clickreplyrule', N'9', N'batch', N'批量操作')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'186', N'36', N'clickreplyrule', N'10', N'recycle', N'回收站')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'187', N'36', N'clickreplyrule', N'11', N'confirm', N'确认')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'188', N'37', N'robots', N'2', N'edit', N'修改')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'189', N'37', N'robots', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'190', N'38', N'admincplog', N'4', N'view', N'查看')
GO

INSERT INTO [dbo].[AdminMenuEvent] ([Id], [MenuId], [MenuKey], [EventId], [EventKey], [EventName]) VALUES (N'191', N'38', N'admincplog', N'5', N'viewlist', N'查看列表')
GO

SET IDENTITY_INSERT [dbo].[AdminMenuEvent] OFF
GO


-- ----------------------------
-- Table structure for AdminRoles
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminRoles]') AND type IN ('U'))
	DROP TABLE [dbo].[AdminRoles]
GO

CREATE TABLE [dbo].[AdminRoles] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [RoleType] int DEFAULT ((0)) NOT NULL,
  [RoleName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [RoleDescription] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsSuperAdmin] int DEFAULT ((0)) NOT NULL,
  [Stars] int DEFAULT ((0)) NOT NULL,
  [NotAllowDel] int DEFAULT ((0)) NOT NULL,
  [Rank] int DEFAULT ((0)) NOT NULL,
  [Color] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Menus] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Powers] ntext COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[AdminRoles] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色类型',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'RoleType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色名称',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'RoleName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色简单介绍',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'RoleDescription'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否是超级管理员',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'IsSuperAdmin'
GO

EXEC sp_addextendedproperty
'MS_Description', N'星级',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'Stars'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否不允许删除',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'NotAllowDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'颜色',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'Color'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理菜单',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'Menus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles',
'COLUMN', N'Powers'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理角色',
'SCHEMA', N'dbo',
'TABLE', N'AdminRoles'
GO


-- ----------------------------
-- Records of AdminRoles
-- ----------------------------
SET IDENTITY_INSERT [dbo].[AdminRoles] ON
GO

INSERT INTO [dbo].[AdminRoles] ([Id], [RoleType], [RoleName], [RoleDescription], [IsSuperAdmin], [Stars], [NotAllowDel], [Rank], [Color], [Menus], [Powers]) VALUES (N'1', N'0', N'超级管理员', N'系统超级管理员', N'1', N'5', N'1', N'0', N'', N'', N'')
GO

SET IDENTITY_INSERT [dbo].[AdminRoles] OFF
GO


-- ----------------------------
-- Table structure for Ads
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Ads]') AND type IN ('U'))
	DROP TABLE [dbo].[Ads]
GO

CREATE TABLE [dbo].[Ads] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Title] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Content] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [KId] int DEFAULT ((0)) NOT NULL,
  [TId] int DEFAULT ((0)) NOT NULL,
  [StartTime] datetime  NULL,
  [EndTime] datetime  NULL,
  [IsDisabled] bit DEFAULT ((0)) NOT NULL,
  [Sequence] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Ads] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Ads',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告标题',
'SCHEMA', N'dbo',
'TABLE', N'Ads',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告详情JSON',
'SCHEMA', N'dbo',
'TABLE', N'Ads',
'COLUMN', N'Content'
GO

EXEC sp_addextendedproperty
'MS_Description', N'分类ID',
'SCHEMA', N'dbo',
'TABLE', N'Ads',
'COLUMN', N'KId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告',
'SCHEMA', N'dbo',
'TABLE', N'Ads',
'COLUMN', N'TId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'起始时间',
'SCHEMA', N'dbo',
'TABLE', N'Ads',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'结束时间',
'SCHEMA', N'dbo',
'TABLE', N'Ads',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否禁用广告',
'SCHEMA', N'dbo',
'TABLE', N'Ads',
'COLUMN', N'IsDisabled'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序，默认999',
'SCHEMA', N'dbo',
'TABLE', N'Ads',
'COLUMN', N'Sequence'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告详情',
'SCHEMA', N'dbo',
'TABLE', N'Ads'
GO


-- ----------------------------
-- Table structure for AdsKind
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AdsKind]') AND type IN ('U'))
	DROP TABLE [dbo].[AdsKind]
GO

CREATE TABLE [dbo].[AdsKind] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KindName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindInfo] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Rank] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[AdsKind] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'AdsKind',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告类别名称',
'SCHEMA', N'dbo',
'TABLE', N'AdsKind',
'COLUMN', N'KindName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'简单说明',
'SCHEMA', N'dbo',
'TABLE', N'AdsKind',
'COLUMN', N'KindInfo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'AdsKind',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告类别',
'SCHEMA', N'dbo',
'TABLE', N'AdsKind'
GO


-- ----------------------------
-- Table structure for Area
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Area]') AND type IN ('U'))
	DROP TABLE [dbo].[Area]
GO

CREATE TABLE [dbo].[Area] (
  [Id] int  NOT NULL,
  [Name] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ParentId] int DEFAULT ((0)) NOT NULL,
  [Level] int DEFAULT ((0)) NOT NULL,
  [Code] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PinYin] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [PY] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TelCode] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [ZipCode] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Latitude] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Longitude] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Area] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'地区名称',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'Name'
GO

EXEC sp_addextendedproperty
'MS_Description', N'上级ID',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'ParentId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'级别',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'Level'
GO

EXEC sp_addextendedproperty
'MS_Description', N'区域代码',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'Code'
GO

EXEC sp_addextendedproperty
'MS_Description', N'拼音',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'PinYin'
GO

EXEC sp_addextendedproperty
'MS_Description', N'简写拼音',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'PY'
GO

EXEC sp_addextendedproperty
'MS_Description', N'区号',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'TelCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮政编码',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'ZipCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'纬度',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'Latitude'
GO

EXEC sp_addextendedproperty
'MS_Description', N'经度',
'SCHEMA', N'dbo',
'TABLE', N'Area',
'COLUMN', N'Longitude'
GO

EXEC sp_addextendedproperty
'MS_Description', N'地区',
'SCHEMA', N'dbo',
'TABLE', N'Area'
GO


-- ----------------------------
-- Table structure for Article
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND type IN ('U'))
	DROP TABLE [dbo].[Article]
GO

CREATE TABLE [dbo].[Article] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KId] int DEFAULT ((0)) NOT NULL,
  [Title] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [SubTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Content] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Keyword] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [LinkURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TitleColor] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsRecommend] int DEFAULT ((0)) NOT NULL,
  [IsNew] int DEFAULT ((0)) NOT NULL,
  [IsBest] int DEFAULT ((0)) NOT NULL,
  [IsHide] int DEFAULT ((0)) NOT NULL,
  [IsLock] int DEFAULT ((0)) NOT NULL,
  [IsDel] int DEFAULT ((0)) NOT NULL,
  [IsTop] int DEFAULT ((0)) NOT NULL,
  [IsComment] int DEFAULT ((0)) NOT NULL,
  [IsMember] int DEFAULT ((0)) NOT NULL,
  [Hits] int DEFAULT ((0)) NOT NULL,
  [Sequence] int DEFAULT ((0)) NOT NULL,
  [CommentCount] int DEFAULT ((0)) NOT NULL,
  [Icon] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClassName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BannerImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AdsId] int DEFAULT ((0)) NOT NULL,
  [Tags] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Origin] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [OriginURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [ItemImg] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [AuthorId] int DEFAULT ((0)) NOT NULL,
  [AddTime] datetime  NULL,
  [UpdateTime] datetime  NULL,
  [Location] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [FilePath] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Article] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目ID',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'KId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'标题',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'副标题',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'SubTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'内容',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Content'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Keyword'
GO

EXEC sp_addextendedproperty
'MS_Description', N'介绍',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'跳转链接',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'LinkURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称颜色',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'TitleColor'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模板',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'TemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否推荐',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'IsRecommend'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否最新',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'IsNew'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否推荐',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'IsBest'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否锁定，不允许删除',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'IsLock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除,已经删除到回收站',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'IsDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否置顶',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'IsTop'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否允许评论',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'IsComment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否会员栏目',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'IsMember'
GO

EXEC sp_addextendedproperty
'MS_Description', N'点击数量',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Hits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Sequence'
GO

EXEC sp_addextendedproperty
'MS_Description', N'评论数量',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'CommentCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'样式名称',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'ClassName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'banner图片',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'BannerImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告ID',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'AdsId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'TAG',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Tags'
GO

EXEC sp_addextendedproperty
'MS_Description', N'来源',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Origin'
GO

EXEC sp_addextendedproperty
'MS_Description', N'来源地址',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'OriginURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更多图片',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'ItemImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'AuthorId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路径',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'Location'
GO

EXEC sp_addextendedproperty
'MS_Description', N'存放目录',
'SCHEMA', N'dbo',
'TABLE', N'Article',
'COLUMN', N'FilePath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文章',
'SCHEMA', N'dbo',
'TABLE', N'Article'
GO


-- ----------------------------
-- Table structure for ArticleCategory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ArticleCategory]') AND type IN ('U'))
	DROP TABLE [dbo].[ArticleCategory]
GO

CREATE TABLE [dbo].[ArticleCategory] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KindName] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [SubTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Keyword] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [LinkURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TitleColor] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [DetailTemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindDomain] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsList] int DEFAULT ((0)) NOT NULL,
  [PageSize] int DEFAULT ((0)) NOT NULL,
  [PId] int DEFAULT ((0)) NOT NULL,
  [Level] int DEFAULT ((0)) NOT NULL,
  [Location] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsHide] int DEFAULT ((0)) NOT NULL,
  [IsLock] int DEFAULT ((0)) NOT NULL,
  [IsDel] int DEFAULT ((0)) NOT NULL,
  [IsComment] int DEFAULT ((0)) NOT NULL,
  [IsMember] int DEFAULT ((0)) NOT NULL,
  [IsShowSubDetail] int DEFAULT ((0)) NOT NULL,
  [CatalogId] int DEFAULT ((0)) NOT NULL,
  [Counts] int DEFAULT ((0)) NOT NULL,
  [Rank] int DEFAULT ((0)) NOT NULL,
  [Icon] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClassName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BannerImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindInfo] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AdsId] int DEFAULT ((0)) NOT NULL,
  [FilePath] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[ArticleCategory] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目名称',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'KindName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目副标题',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'SubTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目标题，填写则在浏览器替换此标题',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'KindTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'Keyword'
GO

EXEC sp_addextendedproperty
'MS_Description', N'介绍',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'跳转链接',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'LinkURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称颜色',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'TitleColor'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模板',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'TemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详情模板',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'DetailTemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别域名（保留）',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'KindDomain'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为列表页面',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'IsList'
GO

EXEC sp_addextendedproperty
'MS_Description', N'每页显示数量',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'PageSize'
GO

EXEC sp_addextendedproperty
'MS_Description', N'上级ID',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'PId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'级别',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'Level'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路径',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'Location'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否锁定，不允许删除',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'IsLock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除,已经删除到回收站',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'IsDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否允许评论',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'IsComment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否会员栏目',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'IsMember'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否显示下级栏目内容',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'IsShowSubDetail'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模型ID',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'CatalogId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详情数量，缓存',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'Counts'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'样式名称',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'ClassName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'banner图片',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'BannerImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目详细介绍',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'KindInfo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告ID',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'AdsId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'目录路径',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory',
'COLUMN', N'FilePath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文章栏目',
'SCHEMA', N'dbo',
'TABLE', N'ArticleCategory'
GO


-- ----------------------------
-- Table structure for BalanceChangeLog
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[BalanceChangeLog]') AND type IN ('U'))
	DROP TABLE [dbo].[BalanceChangeLog]
GO

CREATE TABLE [dbo].[BalanceChangeLog] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [AdminId] int DEFAULT ((0)) NOT NULL,
  [UserName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [IP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Actions] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Reward] money DEFAULT ((0)) NOT NULL,
  [BeforChange] money DEFAULT ((0)) NOT NULL,
  [AfterChange] money DEFAULT ((0)) NOT NULL,
  [LogDetails] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TypeId] int DEFAULT ((0)) NOT NULL,
  [OrderId] int DEFAULT ((0)) NOT NULL,
  [OrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[BalanceChangeLog] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'AdminId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录IP',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'IP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'记录',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'Actions'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总积分',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'Reward'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总积分',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'BeforChange'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总积分',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'AfterChange'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详细记录',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'LogDetails'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'TypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单ID',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'OrderId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单号',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog',
'COLUMN', N'OrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户余额变化记录',
'SCHEMA', N'dbo',
'TABLE', N'BalanceChangeLog'
GO


-- ----------------------------
-- Table structure for Category
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Category]') AND type IN ('U'))
	DROP TABLE [dbo].[Category]
GO

CREATE TABLE [dbo].[Category] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KindName] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [SubTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Keyword] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [LinkURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TitleColor] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [DetailTemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindDomain] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsList] int DEFAULT ((0)) NOT NULL,
  [PageSize] int DEFAULT ((0)) NOT NULL,
  [PId] int DEFAULT ((0)) NOT NULL,
  [Level] int DEFAULT ((0)) NOT NULL,
  [Location] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsHide] int DEFAULT ((0)) NOT NULL,
  [IsLock] int DEFAULT ((0)) NOT NULL,
  [IsDel] int DEFAULT ((0)) NOT NULL,
  [IsComment] int DEFAULT ((0)) NOT NULL,
  [IsMember] int DEFAULT ((0)) NOT NULL,
  [IsShowSubDetail] int DEFAULT ((0)) NOT NULL,
  [CatalogId] int DEFAULT ((0)) NOT NULL,
  [Counts] int DEFAULT ((0)) NOT NULL,
  [Rank] int DEFAULT ((0)) NOT NULL,
  [Icon] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClassName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BannerImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindInfo] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AdsId] int DEFAULT ((0)) NOT NULL,
  [FilePath] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsIndexShow] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Category] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目名称',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'KindName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目副标题',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'SubTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目标题，填写则在浏览器替换此标题',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'KindTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'Keyword'
GO

EXEC sp_addextendedproperty
'MS_Description', N'介绍',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'跳转链接',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'LinkURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称颜色',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'TitleColor'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模板',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'TemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详情模板',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'DetailTemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别域名（保留）',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'KindDomain'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为列表页面',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'IsList'
GO

EXEC sp_addextendedproperty
'MS_Description', N'每页显示数量',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'PageSize'
GO

EXEC sp_addextendedproperty
'MS_Description', N'上级ID',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'PId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'级别',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'Level'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路径',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'Location'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否锁定，不允许删除',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'IsLock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除,已经删除到回收站',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'IsDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否允许评论',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'IsComment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否会员栏目',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'IsMember'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否显示下级栏目内容',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'IsShowSubDetail'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模型ID',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'CatalogId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详情数量，缓存',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'Counts'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'样式名称',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'ClassName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'banner图片',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'BannerImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目详细介绍',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'KindInfo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告ID',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'AdsId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'目录路径',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'FilePath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否首页显示',
'SCHEMA', N'dbo',
'TABLE', N'Category',
'COLUMN', N'IsIndexShow'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品栏目',
'SCHEMA', N'dbo',
'TABLE', N'Category'
GO


-- ----------------------------
-- Table structure for Config
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Config]') AND type IN ('U'))
	DROP TABLE [dbo].[Config]
GO

CREATE TABLE [dbo].[Config] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [SiteName] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [SiteUrl] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SiteLogo] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Icp] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SiteEmail] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [SiteTel] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Copyright] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [IsCloseSite] bit DEFAULT ((0)) NOT NULL,
  [CloseReason] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [CountScript] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [WeiboQRCode] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [WinxinQRCode] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Keyword] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IndexTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsRewrite] int DEFAULT ((0)) NOT NULL,
  [SearchMinTime] int DEFAULT ((0)) NOT NULL,
  [OnlineQQ] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [OnlineSkype] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [OnlineWangWang] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [SkinName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OfficialName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OfficialDecsription] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [OfficialOriginalId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [WexinAccount] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Token] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [AppId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [AppSecret] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [FllowTipPageURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [OfficialType] int DEFAULT ((0)) NOT NULL,
  [EncodingAESKey] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [DEType] int DEFAULT ((0)) NOT NULL,
  [OfficialQRCode] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [OfficialImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [LastUpdateTime] datetime  NULL,
  [LastCacheTime] datetime  NULL,
  [MCHId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [MCHKey] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [DefaultFare] money DEFAULT ((0)) NOT NULL,
  [MaxFreeFare] money DEFAULT ((0)) NOT NULL,
  [WXAppId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [WXAppSecret] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsResetData] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Config] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'站点名称',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'SiteName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'站点URL',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'SiteUrl'
GO

EXEC sp_addextendedproperty
'MS_Description', N'站点LOGO',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'SiteLogo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'ICP备案',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'Icp'
GO

EXEC sp_addextendedproperty
'MS_Description', N'联系我们Email',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'SiteEmail'
GO

EXEC sp_addextendedproperty
'MS_Description', N'网站电话',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'SiteTel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'版权所有',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'Copyright'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否关闭网站',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'IsCloseSite'
GO

EXEC sp_addextendedproperty
'MS_Description', N'关闭原因',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'CloseReason'
GO

EXEC sp_addextendedproperty
'MS_Description', N'统计代码',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'CountScript'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微博二维码',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'WeiboQRCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信二维码',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'WinxinQRCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'关键字',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'Keyword'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'首页标题',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'IndexTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否URL地址重写',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'IsRewrite'
GO

EXEC sp_addextendedproperty
'MS_Description', N'搜索最小时间间距 秒',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'SearchMinTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'在线QQ',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'OnlineQQ'
GO

EXEC sp_addextendedproperty
'MS_Description', N'在线Skype',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'OnlineSkype'
GO

EXEC sp_addextendedproperty
'MS_Description', N'在线阿里旺旺',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'OnlineWangWang'
GO

EXEC sp_addextendedproperty
'MS_Description', N'站点URL',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'SkinName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公众号名称',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'OfficialName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公众号介绍',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'OfficialDecsription'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公众号原始ID',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'OfficialOriginalId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信名称',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'WexinAccount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'Token',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'Token'
GO

EXEC sp_addextendedproperty
'MS_Description', N'AppId',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'AppId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'AppSecret',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'AppSecret'
GO

EXEC sp_addextendedproperty
'MS_Description', N'引导关注素材地址',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'FllowTipPageURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'OfficialType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'EncodingAESKey',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'EncodingAESKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'解密方式0,明文',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'DEType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公众号二维码地址',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'OfficialQRCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公众号头像',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'OfficialImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新时间',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'LastUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后缓存时间',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'LastCacheTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信商家MCHId',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'MCHId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信商家key',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'MCHKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'默认运费',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'DefaultFare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最大免运费金额',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'MaxFreeFare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信小程序AppId',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'WXAppId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信小程序AppSecret',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'WXAppSecret'
GO

EXEC sp_addextendedproperty
'MS_Description', N'小程序首页是否显示清除数据按钮',
'SCHEMA', N'dbo',
'TABLE', N'Config',
'COLUMN', N'IsResetData'
GO

EXEC sp_addextendedproperty
'MS_Description', N'系统配置',
'SCHEMA', N'dbo',
'TABLE', N'Config'
GO


-- ----------------------------
-- Records of Config
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Config] ON
GO

INSERT INTO [dbo].[Config] ([Id], [SiteName], [SiteUrl], [SiteLogo], [Icp], [SiteEmail], [SiteTel], [Copyright], [IsCloseSite], [CloseReason], [CountScript], [WeiboQRCode], [WinxinQRCode], [Keyword], [Description], [IndexTitle], [IsRewrite], [SearchMinTime], [OnlineQQ], [OnlineSkype], [OnlineWangWang], [SkinName], [OfficialName], [OfficialDecsription], [OfficialOriginalId], [WexinAccount], [Token], [AppId], [AppSecret], [FllowTipPageURL], [OfficialType], [EncodingAESKey], [DEType], [OfficialQRCode], [OfficialImg], [LastUpdateTime], [LastCacheTime], [MCHId], [MCHKey], [DefaultFare], [MaxFreeFare], [WXAppId], [WXAppSecret], [IsResetData]) VALUES (N'1', N'COMCMS', N'http://www.comcms.com', N'', N'', N'', N'', NULL, N'0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'0', N'0', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'0', NULL, N'0', NULL, NULL, N'2020-04-26 16:12:27.000', N'2020-04-26 16:12:27.000', NULL, NULL, N'.0000', N'.0000', NULL, NULL, N'0')
GO

SET IDENTITY_INSERT [dbo].[Config] OFF
GO


-- ----------------------------
-- Table structure for Coupon
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Coupon]') AND type IN ('U'))
	DROP TABLE [dbo].[Coupon]
GO

CREATE TABLE [dbo].[Coupon] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [ItemNO] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [KId] int DEFAULT ((0)) NOT NULL,
  [CouponType] int DEFAULT ((0)) NOT NULL,
  [DiscuountRates] money DEFAULT ((0)) NOT NULL,
  [IsLimit] int DEFAULT ((0)) NOT NULL,
  [Price] money DEFAULT ((0)) NOT NULL,
  [NeedPrice] money DEFAULT ((0)) NOT NULL,
  [AddTime] datetime  NULL,
  [StartTime] datetime  NULL,
  [EndTime] datetime  NULL,
  [TotalCount] int DEFAULT ((0)) NOT NULL,
  [TotalUseCount] int DEFAULT ((0)) NOT NULL,
  [SpreadUId] int DEFAULT ((0)) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [MyType] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Coupon] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'券号',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'ItemNO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别，0默认没限制',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'KId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'优惠券类型，0 现金用券，1打折券',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'CouponType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'打折率，只有是打折券才有用',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'DiscuountRates'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否有类别限制，0 无限制；1 是类别限制，2是商品限制',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'IsLimit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'面额',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'Price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'需要消费面额',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'NeedPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最大领取数量',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'TotalCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'已使用次数',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'TotalUseCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'推广员ID，可选',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'SpreadUId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户Id',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'可使用类型',
'SCHEMA', N'dbo',
'TABLE', N'Coupon',
'COLUMN', N'MyType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'优惠券',
'SCHEMA', N'dbo',
'TABLE', N'Coupon'
GO


-- ----------------------------
-- Table structure for CouponKind
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[CouponKind]') AND type IN ('U'))
	DROP TABLE [dbo].[CouponKind]
GO

CREATE TABLE [dbo].[CouponKind] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [IsLimit] int DEFAULT ((0)) NOT NULL,
  [KindName] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [CouponType] int DEFAULT ((0)) NOT NULL,
  [KIds] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [PIds] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [KindNote] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [MyType] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[CouponKind] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否有类别限制，0 无限制；1 是类别限制，2是商品限制',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind',
'COLUMN', N'IsLimit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind',
'COLUMN', N'KindName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'优惠券类型，0 现金用券，1打折券',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind',
'COLUMN', N'CouponType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别按逗号分开',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind',
'COLUMN', N'KIds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品ID，按逗号分开',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind',
'COLUMN', N'PIds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别说明',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind',
'COLUMN', N'KindNote'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'可使用类型',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind',
'COLUMN', N'MyType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'优惠券分类',
'SCHEMA', N'dbo',
'TABLE', N'CouponKind'
GO


-- ----------------------------
-- Table structure for CouponUseLog
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[CouponUseLog]') AND type IN ('U'))
	DROP TABLE [dbo].[CouponUseLog]
GO

CREATE TABLE [dbo].[CouponUseLog] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [CouponId] int DEFAULT ((0)) NOT NULL,
  [ItemNO] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [OrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OrderId] int DEFAULT ((0)) NOT NULL,
  [UserName] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Title] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [IP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Actions] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[CouponUseLog] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'优惠券ID',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'CouponId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'优惠券编号',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'ItemNO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单号',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'OrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单编号',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'OrderId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单名称',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录IP',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'IP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'记录详情',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog',
'COLUMN', N'Actions'
GO

EXEC sp_addextendedproperty
'MS_Description', N'优惠券使用记录',
'SCHEMA', N'dbo',
'TABLE', N'CouponUseLog'
GO


-- ----------------------------
-- Table structure for Favortie
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Favortie]') AND type IN ('U'))
	DROP TABLE [dbo].[Favortie]
GO

CREATE TABLE [dbo].[Favortie] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [TId] int DEFAULT ((0)) NOT NULL,
  [RId] int DEFAULT ((0)) NOT NULL,
  [Price] money DEFAULT ((0)) NOT NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [UpdateTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[Favortie] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Favortie',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'Favortie',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'Tpye ID，类别类型：0文章，1相册（图片），2视频，3下载，4商品',
'SCHEMA', N'dbo',
'TABLE', N'Favortie',
'COLUMN', N'TId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'目标ID',
'SCHEMA', N'dbo',
'TABLE', N'Favortie',
'COLUMN', N'RId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'价格，如果是商品',
'SCHEMA', N'dbo',
'TABLE', N'Favortie',
'COLUMN', N'Price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'Favortie',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'加入购物车时间',
'SCHEMA', N'dbo',
'TABLE', N'Favortie',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新时间',
'SCHEMA', N'dbo',
'TABLE', N'Favortie',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'收藏夹',
'SCHEMA', N'dbo',
'TABLE', N'Favortie'
GO


-- ----------------------------
-- Table structure for Food
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Food]') AND type IN ('U'))
	DROP TABLE [dbo].[Food]
GO

CREATE TABLE [dbo].[Food] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KId] int DEFAULT ((0)) NOT NULL,
  [BId] int DEFAULT ((0)) NOT NULL,
  [ShopId] int DEFAULT ((0)) NOT NULL,
  [CId] int DEFAULT ((0)) NOT NULL,
  [SupportId] int DEFAULT ((0)) NOT NULL,
  [Title] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [ItemNO] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SubTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Unit] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Spec] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Color] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Weight] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Price] money DEFAULT ((0)) NOT NULL,
  [MarketPrice] money DEFAULT ((0)) NOT NULL,
  [SpecialPrice] money DEFAULT ((0)) NOT NULL,
  [Fare] money DEFAULT ((0)) NOT NULL,
  [Discount] money DEFAULT ((0)) NOT NULL,
  [Material] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Front] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Credits] int DEFAULT ((0)) NOT NULL,
  [Stock] int DEFAULT ((0)) NOT NULL,
  [WarnStock] int DEFAULT ((0)) NOT NULL,
  [IsSubProduct] int DEFAULT ((0)) NOT NULL,
  [PPId] int DEFAULT ((0)) NOT NULL,
  [Content] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Parameters] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Keyword] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [LinkURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TitleColor] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsRecommend] int DEFAULT ((0)) NOT NULL,
  [IsHide] int DEFAULT ((0)) NOT NULL,
  [IsLock] int DEFAULT ((0)) NOT NULL,
  [IsDel] int DEFAULT ((0)) NOT NULL,
  [IsTop] int DEFAULT ((0)) NOT NULL,
  [IsBest] int DEFAULT ((0)) NOT NULL,
  [IsComment] int DEFAULT ((0)) NOT NULL,
  [IsMember] int DEFAULT ((0)) NOT NULL,
  [IsNew] int DEFAULT ((0)) NOT NULL,
  [IsSpecial] int DEFAULT ((0)) NOT NULL,
  [IsPromote] int DEFAULT ((0)) NOT NULL,
  [IsHotSales] int DEFAULT ((0)) NOT NULL,
  [IsBreakup] int DEFAULT ((0)) NOT NULL,
  [IsShelves] int DEFAULT ((0)) NOT NULL,
  [IsVerify] int DEFAULT ((0)) NOT NULL,
  [Hits] int DEFAULT ((0)) NOT NULL,
  [IsGift] int DEFAULT ((0)) NOT NULL,
  [IsPart] int DEFAULT ((0)) NOT NULL,
  [Sequence] int DEFAULT ((0)) NOT NULL,
  [CommentCount] int DEFAULT ((0)) NOT NULL,
  [Icon] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClassName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BannerImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AdsId] int DEFAULT ((0)) NOT NULL,
  [Tags] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [ItemImg] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Service] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [AuthorId] int DEFAULT ((0)) NOT NULL,
  [AddTime] datetime  NULL,
  [UpdateTime] datetime  NULL,
  [Location] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [FilePath] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Food] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目ID',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'KId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'品牌ID',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'BId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'店铺ID',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'ShopId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'颜色ID',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'CId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'供货商ID',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'SupportId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'标题',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'ItemNO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'副标题',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'SubTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品单位',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Unit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品规格尺寸',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Spec'
GO

EXEC sp_addextendedproperty
'MS_Description', N'颜色',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Color'
GO

EXEC sp_addextendedproperty
'MS_Description', N'重量',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Weight'
GO

EXEC sp_addextendedproperty
'MS_Description', N'价格',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'市场价格',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'MarketPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'特价，如有特价，以特价为准',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'SpecialPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运费',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Fare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'折扣',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Discount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'材料',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Material'
GO

EXEC sp_addextendedproperty
'MS_Description', N'封面',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Front'
GO

EXEC sp_addextendedproperty
'MS_Description', N'积分',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Credits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'库存',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Stock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'警告库存',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'WarnStock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为子商品',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsSubProduct'
GO

EXEC sp_addextendedproperty
'MS_Description', N'父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'PPId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'内容',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Content'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品参数',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Parameters'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Keyword'
GO

EXEC sp_addextendedproperty
'MS_Description', N'介绍',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'跳转链接',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'LinkURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称颜色',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'TitleColor'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模板',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'TemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否推荐',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsRecommend'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否锁定，不允许删除',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsLock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除,已经删除到回收站',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否置顶',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsTop'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否精华',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsBest'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否允许评论',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsComment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否会员栏目',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsMember'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否新品',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsNew'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否特价',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsSpecial'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否促销',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsPromote'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否热销',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsHotSales'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否缺货',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsBreakup'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否下架',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsShelves'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否审核，1为已经审核前台显示',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsVerify'
GO

EXEC sp_addextendedproperty
'MS_Description', N'点击数量',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Hits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为礼品商品',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsGift'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为配件',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'IsPart'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Sequence'
GO

EXEC sp_addextendedproperty
'MS_Description', N'评论数量',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'CommentCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'样式名称',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'ClassName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'banner图片',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'BannerImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告ID',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'AdsId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'TAG',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Tags'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更多图片',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'ItemImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'售后服务',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Service'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'AuthorId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路径',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'Location'
GO

EXEC sp_addextendedproperty
'MS_Description', N'存放目录',
'SCHEMA', N'dbo',
'TABLE', N'Food',
'COLUMN', N'FilePath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'快餐',
'SCHEMA', N'dbo',
'TABLE', N'Food'
GO


-- ----------------------------
-- Table structure for Guestbook
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Guestbook]') AND type IN ('U'))
	DROP TABLE [dbo].[Guestbook]
GO

CREATE TABLE [dbo].[Guestbook] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KId] int DEFAULT ((0)) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [Title] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Content] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [IsVerify] int DEFAULT ((0)) NOT NULL,
  [IsRead] int DEFAULT ((0)) NOT NULL,
  [IsDel] int DEFAULT ((0)) NOT NULL,
  [IP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Nickname] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Email] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Tel] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [QQ] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Skype] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [HomePage] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Address] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Company] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [ReplyTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [ReplyContent] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [ReplyAddTime] datetime  NULL,
  [ReplyIP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [ReplyAdminId] int DEFAULT ((0)) NOT NULL,
  [ReplyNickName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Guestbook] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'分类ID',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'KId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'留言标题',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详细介绍',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'Content'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否审核通过',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'IsVerify'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否阅读',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'IsRead'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除,已经删除到回收站',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'IsDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户IP',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'IP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'昵称',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'Nickname'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮箱',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'电话',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'Tel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'QQ',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'QQ'
GO

EXEC sp_addextendedproperty
'MS_Description', N'Skype',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'Skype'
GO

EXEC sp_addextendedproperty
'MS_Description', N'主页',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'HomePage'
GO

EXEC sp_addextendedproperty
'MS_Description', N'地址',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'Address'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公司',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'Company'
GO

EXEC sp_addextendedproperty
'MS_Description', N'回复标题',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'ReplyTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'回复的详情',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'ReplyContent'
GO

EXEC sp_addextendedproperty
'MS_Description', N'回复时间',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'ReplyAddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户回复IP',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'ReplyIP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户的管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'ReplyAdminId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'回复者昵称',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook',
'COLUMN', N'ReplyNickName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'留言详情',
'SCHEMA', N'dbo',
'TABLE', N'Guestbook'
GO


-- ----------------------------
-- Table structure for GuestbookCategory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[GuestbookCategory]') AND type IN ('U'))
	DROP TABLE [dbo].[GuestbookCategory]
GO

CREATE TABLE [dbo].[GuestbookCategory] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KindName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindInfo] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Rank] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[GuestbookCategory] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'GuestbookCategory',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称',
'SCHEMA', N'dbo',
'TABLE', N'GuestbookCategory',
'COLUMN', N'KindName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'简单说明',
'SCHEMA', N'dbo',
'TABLE', N'GuestbookCategory',
'COLUMN', N'KindInfo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'分类图片',
'SCHEMA', N'dbo',
'TABLE', N'GuestbookCategory',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'GuestbookCategory',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'留言分类',
'SCHEMA', N'dbo',
'TABLE', N'GuestbookCategory'
GO


-- ----------------------------
-- Table structure for HotelRoom
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[HotelRoom]') AND type IN ('U'))
	DROP TABLE [dbo].[HotelRoom]
GO

CREATE TABLE [dbo].[HotelRoom] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KId] int DEFAULT ((0)) NOT NULL,
  [BId] int DEFAULT ((0)) NOT NULL,
  [ShopId] int DEFAULT ((0)) NOT NULL,
  [CId] int DEFAULT ((0)) NOT NULL,
  [SupportId] int DEFAULT ((0)) NOT NULL,
  [Title] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [ItemNO] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SubTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Unit] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Spec] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Color] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Weight] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Price] money DEFAULT ((0)) NOT NULL,
  [MarketPrice] money DEFAULT ((0)) NOT NULL,
  [SpecialPrice] money DEFAULT ((0)) NOT NULL,
  [Fare] money DEFAULT ((0)) NOT NULL,
  [Discount] money DEFAULT ((0)) NOT NULL,
  [Material] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Front] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Credits] int DEFAULT ((0)) NOT NULL,
  [Stock] int DEFAULT ((0)) NOT NULL,
  [WarnStock] int DEFAULT ((0)) NOT NULL,
  [IsSubProduct] int DEFAULT ((0)) NOT NULL,
  [PPId] int DEFAULT ((0)) NOT NULL,
  [Content] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Parameters] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Keyword] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [LinkURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TitleColor] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsRecommend] int DEFAULT ((0)) NOT NULL,
  [IsHide] int DEFAULT ((0)) NOT NULL,
  [IsLock] int DEFAULT ((0)) NOT NULL,
  [IsDel] int DEFAULT ((0)) NOT NULL,
  [IsTop] int DEFAULT ((0)) NOT NULL,
  [IsBest] int DEFAULT ((0)) NOT NULL,
  [IsComment] int DEFAULT ((0)) NOT NULL,
  [IsMember] int DEFAULT ((0)) NOT NULL,
  [IsNew] int DEFAULT ((0)) NOT NULL,
  [IsSpecial] int DEFAULT ((0)) NOT NULL,
  [IsPromote] int DEFAULT ((0)) NOT NULL,
  [IsHotSales] int DEFAULT ((0)) NOT NULL,
  [IsBreakup] int DEFAULT ((0)) NOT NULL,
  [IsShelves] int DEFAULT ((0)) NOT NULL,
  [IsVerify] int DEFAULT ((0)) NOT NULL,
  [Hits] int DEFAULT ((0)) NOT NULL,
  [IsGift] int DEFAULT ((0)) NOT NULL,
  [IsPart] int DEFAULT ((0)) NOT NULL,
  [Sequence] int DEFAULT ((0)) NOT NULL,
  [CommentCount] int DEFAULT ((0)) NOT NULL,
  [Icon] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClassName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BannerImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AdsId] int DEFAULT ((0)) NOT NULL,
  [Tags] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [ItemImg] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Service] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [AuthorId] int DEFAULT ((0)) NOT NULL,
  [AddTime] datetime  NULL,
  [UpdateTime] datetime  NULL,
  [Location] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [FilePath] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [PriceList] ntext COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[HotelRoom] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目ID',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'KId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'品牌ID',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'BId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'店铺ID',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'ShopId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'颜色ID',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'CId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'供货商ID',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'SupportId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'标题',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'ItemNO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'副标题',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'SubTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品单位',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Unit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品规格尺寸',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Spec'
GO

EXEC sp_addextendedproperty
'MS_Description', N'颜色',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Color'
GO

EXEC sp_addextendedproperty
'MS_Description', N'重量',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Weight'
GO

EXEC sp_addextendedproperty
'MS_Description', N'价格',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'市场价格',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'MarketPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'特价，如有特价，以特价为准',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'SpecialPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运费',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Fare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'折扣',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Discount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'材料',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Material'
GO

EXEC sp_addextendedproperty
'MS_Description', N'封面',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Front'
GO

EXEC sp_addextendedproperty
'MS_Description', N'积分',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Credits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'库存',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Stock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'警告库存',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'WarnStock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为子商品',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsSubProduct'
GO

EXEC sp_addextendedproperty
'MS_Description', N'父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'PPId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'内容',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Content'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品参数',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Parameters'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Keyword'
GO

EXEC sp_addextendedproperty
'MS_Description', N'介绍',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'跳转链接',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'LinkURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称颜色',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'TitleColor'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模板',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'TemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否推荐',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsRecommend'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否锁定，不允许删除',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsLock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除,已经删除到回收站',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否置顶',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsTop'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否精华',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsBest'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否允许评论',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsComment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否会员栏目',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsMember'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否新品',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsNew'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否特价',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsSpecial'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否促销',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsPromote'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否热销',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsHotSales'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否缺货',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsBreakup'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否下架',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsShelves'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否审核，1为已经审核前台显示',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsVerify'
GO

EXEC sp_addextendedproperty
'MS_Description', N'点击数量',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Hits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为礼品商品',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsGift'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为配件',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'IsPart'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Sequence'
GO

EXEC sp_addextendedproperty
'MS_Description', N'评论数量',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'CommentCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'样式名称',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'ClassName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'banner图片',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'BannerImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告ID',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'AdsId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'TAG',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Tags'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更多图片',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'ItemImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'售后服务',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Service'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'AuthorId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路径',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'Location'
GO

EXEC sp_addextendedproperty
'MS_Description', N'存放目录',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'FilePath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'日期价格',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom',
'COLUMN', N'PriceList'
GO

EXEC sp_addextendedproperty
'MS_Description', N'酒店房间',
'SCHEMA', N'dbo',
'TABLE', N'HotelRoom'
GO


-- ----------------------------
-- Table structure for Link
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Link]') AND type IN ('U'))
	DROP TABLE [dbo].[Link]
GO

CREATE TABLE [dbo].[Link] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KId] int DEFAULT ((0)) NOT NULL,
  [Title] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [LinkURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Info] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Logo] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsHide] bit DEFAULT ((0)) NOT NULL,
  [Sequence] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Link] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Link',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'分类ID',
'SCHEMA', N'dbo',
'TABLE', N'Link',
'COLUMN', N'KId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'站点标题',
'SCHEMA', N'dbo',
'TABLE', N'Link',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'站点连接',
'SCHEMA', N'dbo',
'TABLE', N'Link',
'COLUMN', N'LinkURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'介绍',
'SCHEMA', N'dbo',
'TABLE', N'Link',
'COLUMN', N'Info'
GO

EXEC sp_addextendedproperty
'MS_Description', N'站点LOGO',
'SCHEMA', N'dbo',
'TABLE', N'Link',
'COLUMN', N'Logo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏友情链接',
'SCHEMA', N'dbo',
'TABLE', N'Link',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序，默认999',
'SCHEMA', N'dbo',
'TABLE', N'Link',
'COLUMN', N'Sequence'
GO

EXEC sp_addextendedproperty
'MS_Description', N'友情连接详情',
'SCHEMA', N'dbo',
'TABLE', N'Link'
GO


-- ----------------------------
-- Table structure for LinkKind
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[LinkKind]') AND type IN ('U'))
	DROP TABLE [dbo].[LinkKind]
GO

CREATE TABLE [dbo].[LinkKind] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KindName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindInfo] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Rank] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[LinkKind] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'LinkKind',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称',
'SCHEMA', N'dbo',
'TABLE', N'LinkKind',
'COLUMN', N'KindName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'简单说明',
'SCHEMA', N'dbo',
'TABLE', N'LinkKind',
'COLUMN', N'KindInfo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'分类图片',
'SCHEMA', N'dbo',
'TABLE', N'LinkKind',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'LinkKind',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'友情链接分类',
'SCHEMA', N'dbo',
'TABLE', N'LinkKind'
GO


-- ----------------------------
-- Table structure for Member
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Member]') AND type IN ('U'))
	DROP TABLE [dbo].[Member]
GO

CREATE TABLE [dbo].[Member] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UserName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [PassWord] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Salt] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [RealName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Tel] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Email] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [RoleId] int DEFAULT ((0)) NOT NULL,
  [LastLoginTime] datetime  NULL,
  [LastLoginIP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [ThisLoginTime] datetime  NULL,
  [ThisLoginIP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsLock] int DEFAULT ((0)) NOT NULL,
  [Nickname] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Sex] int DEFAULT ((0)) NOT NULL,
  [Birthday] datetime  NULL,
  [Phone] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Fax] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Qq] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Weixin] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Alipay] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Skype] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Homepage] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Company] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Idno] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Country] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Province] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [City] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [District] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Address] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Postcode] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [RegIP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [RegTime] datetime  NULL,
  [LoginCount] int DEFAULT ((0)) NOT NULL,
  [RndNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [RePassWordTime] datetime  NULL,
  [Question] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Answer] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Balance] money DEFAULT ((0)) NOT NULL,
  [GiftBalance] money DEFAULT ((0)) NOT NULL,
  [Rebate] money DEFAULT ((0)) NOT NULL,
  [Bank] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BankCardNO] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BankBranch] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BankRealname] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [YearsPerformance] money DEFAULT ((0)) NOT NULL,
  [ExtCredits1] money DEFAULT ((0)) NOT NULL,
  [ExtCredits2] money DEFAULT ((0)) NOT NULL,
  [ExtCredits3] money DEFAULT ((0)) NOT NULL,
  [ExtCredits4] money DEFAULT ((0)) NOT NULL,
  [ExtCredits5] money DEFAULT ((0)) NOT NULL,
  [TotalCredits] money DEFAULT ((0)) NOT NULL,
  [Parent] int DEFAULT ((0)) NOT NULL,
  [Grandfather] int DEFAULT ((0)) NOT NULL,
  [IsSellers] int DEFAULT ((0)) NOT NULL,
  [IsVerifySellers] int DEFAULT ((0)) NOT NULL,
  [WeixinOpenId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [WeixinAppOpenId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [QQOpenId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [WeiboOpenId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [TenUnionId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [AliAppOpenId] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsAdmin] int DEFAULT ((0)) NOT NULL,
  [RegType] int DEFAULT ((0)) NOT NULL,
  [TotalOrders] int DEFAULT ((0)) NOT NULL,
  [TotalPayPrice] money DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Member] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'PassWord'
GO

EXEC sp_addextendedproperty
'MS_Description', N'盐值',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Salt'
GO

EXEC sp_addextendedproperty
'MS_Description', N'姓名',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'RealName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'手机',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Tel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮件',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'会员组，代理级别',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'RoleId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后登录时间',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'LastLoginTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'上次登录IP',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'LastLoginIP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'本次登录时间',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'ThisLoginTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'本次登录IP',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'ThisLoginIP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否是锁定',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'IsLock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'昵称',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Nickname'
GO

EXEC sp_addextendedproperty
'MS_Description', N'头像',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'UserImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'性别 0 保密 1 男 2女',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Sex'
GO

EXEC sp_addextendedproperty
'MS_Description', N'生日',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Birthday'
GO

EXEC sp_addextendedproperty
'MS_Description', N'电话',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Phone'
GO

EXEC sp_addextendedproperty
'MS_Description', N'传真',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Fax'
GO

EXEC sp_addextendedproperty
'MS_Description', N'QQ',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Qq'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Weixin'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付宝',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Alipay'
GO

EXEC sp_addextendedproperty
'MS_Description', N'skype',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Skype'
GO

EXEC sp_addextendedproperty
'MS_Description', N'主页',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Homepage'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公司',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Company'
GO

EXEC sp_addextendedproperty
'MS_Description', N'身份证',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Idno'
GO

EXEC sp_addextendedproperty
'MS_Description', N'国家',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Country'
GO

EXEC sp_addextendedproperty
'MS_Description', N'省',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Province'
GO

EXEC sp_addextendedproperty
'MS_Description', N'市',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'City'
GO

EXEC sp_addextendedproperty
'MS_Description', N'区',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'District'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详细地址',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Address'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮政编码',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Postcode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'注册IP',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'RegIP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'注册时间',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'RegTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录次数',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'LoginCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'随机字符',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'RndNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'找回密码时间',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'RePassWordTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'保密问题',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Question'
GO

EXEC sp_addextendedproperty
'MS_Description', N'保密答案',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Answer'
GO

EXEC sp_addextendedproperty
'MS_Description', N'可用余额',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Balance'
GO

EXEC sp_addextendedproperty
'MS_Description', N'赠送余额',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'GiftBalance'
GO

EXEC sp_addextendedproperty
'MS_Description', N'返利，分销提成',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Rebate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'银行',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Bank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'银行卡号',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'BankCardNO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支行名称',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'BankBranch'
GO

EXEC sp_addextendedproperty
'MS_Description', N'银行卡姓名',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'BankRealname'
GO

EXEC sp_addextendedproperty
'MS_Description', N'年业务量',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'YearsPerformance'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备用积分1',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'ExtCredits1'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备用积分2',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'ExtCredits2'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备用积分3',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'ExtCredits3'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备用积分4',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'ExtCredits4'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备用积分5',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'ExtCredits5'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总积分',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'TotalCredits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'父级用户ID',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Parent'
GO

EXEC sp_addextendedproperty
'MS_Description', N'爷级用户ID',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'Grandfather'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否是分销商',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'IsSellers'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否已经认证的分销商，缴纳费用',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'IsVerifySellers'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信公众号OpenId',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'WeixinOpenId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信OpenId',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'WeixinAppOpenId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'QQ OpenId',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'QQOpenId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微博OpenId',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'WeiboOpenId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'腾讯UnionId',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'TenUnionId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付宝小程序OpenId',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'AliAppOpenId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否是管理员',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'IsAdmin'
GO

EXEC sp_addextendedproperty
'MS_Description', N'注册类型',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'RegType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总订单',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'TotalOrders'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总支付金额',
'SCHEMA', N'dbo',
'TABLE', N'Member',
'COLUMN', N'TotalPayPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户',
'SCHEMA', N'dbo',
'TABLE', N'Member'
GO


-- ----------------------------
-- Table structure for MemberAddress
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberAddress]') AND type IN ('U'))
	DROP TABLE [dbo].[MemberAddress]
GO

CREATE TABLE [dbo].[MemberAddress] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [RealName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Tel] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Email] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDefault] bit DEFAULT ((0)) NOT NULL,
  [Phone] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Company] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Country] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Province] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [City] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [District] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Address] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Postcode] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [UpdateTime] datetime  NULL,
  [Rank] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[MemberAddress] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'姓名',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'RealName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'手机',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Tel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮件',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否是默认',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'IsDefault'
GO

EXEC sp_addextendedproperty
'MS_Description', N'电话',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Phone'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公司',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Company'
GO

EXEC sp_addextendedproperty
'MS_Description', N'国家',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Country'
GO

EXEC sp_addextendedproperty
'MS_Description', N'省',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Province'
GO

EXEC sp_addextendedproperty
'MS_Description', N'市',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'City'
GO

EXEC sp_addextendedproperty
'MS_Description', N'区',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'District'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详细地址',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Address'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮政编码',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Postcode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新时间',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户地址',
'SCHEMA', N'dbo',
'TABLE', N'MemberAddress'
GO


-- ----------------------------
-- Table structure for MemberCoupon
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberCoupon]') AND type IN ('U'))
	DROP TABLE [dbo].[MemberCoupon]
GO

CREATE TABLE [dbo].[MemberCoupon] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [ItemNO] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [CouponId] int DEFAULT ((0)) NOT NULL,
  [CouponType] int DEFAULT ((0)) NOT NULL,
  [DiscuountRates] money DEFAULT ((0)) NOT NULL,
  [IsLimit] int DEFAULT ((0)) NOT NULL,
  [Price] money DEFAULT ((0)) NOT NULL,
  [NeedPrice] money DEFAULT ((0)) NOT NULL,
  [AddTime] datetime  NULL,
  [StartTime] datetime  NULL,
  [EndTime] datetime  NULL,
  [IsUse] int DEFAULT ((0)) NOT NULL,
  [UseTime] datetime  NULL,
  [OrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OrderId] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[MemberCoupon] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'券号',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'ItemNO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'优惠券ID',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'CouponId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'优惠券类型，0 现金用券，1打折券',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'CouponType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'打折率，只有是打折券才有用',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'DiscuountRates'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否有类别限制，0 无限制；1 是类别限制，2是商品限制',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'IsLimit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'面额',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'Price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'需要消费面额',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'NeedPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否使用',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'IsUse'
GO

EXEC sp_addextendedproperty
'MS_Description', N'使用时间',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'UseTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单号（使用后）',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'OrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单编号（使用后）',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon',
'COLUMN', N'OrderId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户优惠券',
'SCHEMA', N'dbo',
'TABLE', N'MemberCoupon'
GO


-- ----------------------------
-- Table structure for MemberLog
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberLog]') AND type IN ('U'))
	DROP TABLE [dbo].[MemberLog]
GO

CREATE TABLE [dbo].[MemberLog] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [GUID] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [PassWord] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [LoginTime] datetime  NULL,
  [LoginIP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsLoginOK] int DEFAULT ((0)) NOT NULL,
  [Actions] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [LastUpdateTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[MemberLog] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'唯一ID',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'GUID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'密码',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'PassWord'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录时间',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'LoginTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录IP',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'LoginIP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否登录成功',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'IsLoginOK'
GO

EXEC sp_addextendedproperty
'MS_Description', N'记录',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'Actions'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录时间',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog',
'COLUMN', N'LastUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户日志表',
'SCHEMA', N'dbo',
'TABLE', N'MemberLog'
GO


-- ----------------------------
-- Table structure for MemberRoles
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[MemberRoles]') AND type IN ('U'))
	DROP TABLE [dbo].[MemberRoles]
GO

CREATE TABLE [dbo].[MemberRoles] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [RoleName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [RoleDescription] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Stars] int DEFAULT ((0)) NOT NULL,
  [NotAllowDel] int DEFAULT ((0)) NOT NULL,
  [Rank] int DEFAULT ((0)) NOT NULL,
  [Color] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [CashBack] money DEFAULT ((0)) NOT NULL,
  [ParentCashBack] money DEFAULT ((0)) NOT NULL,
  [GrandfatherCashBack] money DEFAULT ((0)) NOT NULL,
  [IsDefault] int DEFAULT ((0)) NOT NULL,
  [IsHalved] int DEFAULT ((0)) NOT NULL,
  [HalvedParentCashBack] money DEFAULT ((0)) NOT NULL,
  [HalvedGrandfatherCashBack] money DEFAULT ((0)) NOT NULL,
  [YearsPerformance] money DEFAULT ((0)) NOT NULL,
  [IsSellers] int DEFAULT ((0)) NOT NULL,
  [JoinPrice] money DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[MemberRoles] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色名称',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'RoleName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'角色简单介绍',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'RoleDescription'
GO

EXEC sp_addextendedproperty
'MS_Description', N'星级',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'Stars'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否不允许删除',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'NotAllowDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'颜色',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'Color'
GO

EXEC sp_addextendedproperty
'MS_Description', N'返现百分比',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'CashBack'
GO

EXEC sp_addextendedproperty
'MS_Description', N'父级返现百分比',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'ParentCashBack'
GO

EXEC sp_addextendedproperty
'MS_Description', N'爷级返现百分比',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'GrandfatherCashBack'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否是默认角色',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'IsDefault'
GO

EXEC sp_addextendedproperty
'MS_Description', N'超过级别是否减半',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'IsHalved'
GO

EXEC sp_addextendedproperty
'MS_Description', N'超过级别父级返现百分比',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'HalvedParentCashBack'
GO

EXEC sp_addextendedproperty
'MS_Description', N'超过级别爷级返现百分比',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'HalvedGrandfatherCashBack'
GO

EXEC sp_addextendedproperty
'MS_Description', N'年业务量',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'YearsPerformance'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否是分销商',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'IsSellers'
GO

EXEC sp_addextendedproperty
'MS_Description', N'加入分销商价格',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles',
'COLUMN', N'JoinPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户角色',
'SCHEMA', N'dbo',
'TABLE', N'MemberRoles'
GO


-- ----------------------------
-- Table structure for OnlinePayOrder
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[OnlinePayOrder]') AND type IN ('U'))
	DROP TABLE [dbo].[OnlinePayOrder]
GO

CREATE TABLE [dbo].[OnlinePayOrder] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [PayOrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OrderId] int DEFAULT ((0)) NOT NULL,
  [OrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PayId] int DEFAULT ((0)) NOT NULL,
  [PayType] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PayTypeNotes] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Actions] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [UserName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TotalQty] int DEFAULT ((0)) NOT NULL,
  [TotalPrice] money DEFAULT ((0)) NOT NULL,
  [PaymentStatus] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PaymentNotes] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsOK] int DEFAULT ((0)) NOT NULL,
  [Ip] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [ReceiveTime] datetime  NULL,
  [TypeId] int DEFAULT ((0)) NOT NULL,
  [MyType] int DEFAULT ((0)) NOT NULL,
  [Title] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OutTradeNo] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[OnlinePayOrder] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'在线支付订单号，唯一',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'PayOrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单ID',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'OrderId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单号',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'OrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付方式ID',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'PayId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'付款方式',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'PayType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付备注',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'PayTypeNotes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'日志记录',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'Actions'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总数量',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'TotalQty'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总价格',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'TotalPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付状态：未支付、已支付、已退款',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'PaymentStatus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付备注',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'PaymentNotes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否完成',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'IsOK'
GO

EXEC sp_addextendedproperty
'MS_Description', N'下单IP',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'Ip'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'回传时间',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'ReceiveTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付的类型',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'TypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'系统类型',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'MyType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单标题',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付成功流水号',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder',
'COLUMN', N'OutTradeNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'在线支付订单',
'SCHEMA', N'dbo',
'TABLE', N'OnlinePayOrder'
GO


-- ----------------------------
-- Table structure for Order
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Order]') AND type IN ('U'))
	DROP TABLE [dbo].[Order]
GO

CREATE TABLE [dbo].[Order] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [OrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [UserName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ShopId] int DEFAULT ((0)) NOT NULL,
  [Credit] int DEFAULT ((0)) NOT NULL,
  [TotalQty] int DEFAULT ((0)) NOT NULL,
  [TotalPrice] money DEFAULT ((0)) NOT NULL,
  [Discount] money DEFAULT ((0)) NOT NULL,
  [Fare] money DEFAULT ((0)) NOT NULL,
  [TotalTax] money DEFAULT ((0)) NOT NULL,
  [TotalPay] money DEFAULT ((0)) NOT NULL,
  [BackCredits] int DEFAULT ((0)) NOT NULL,
  [RealName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Country] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Province] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [City] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [District] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Address] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [PostCode] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Tel] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Mobile] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Email] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Notes] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AdminNotes] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [DeliverId] int DEFAULT ((0)) NOT NULL,
  [DeliverType] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [DeliverNO] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [DeliverNotes] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [PayId] int DEFAULT ((0)) NOT NULL,
  [PayType] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PayTypeNotes] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [OrderStatus] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PaymentStatus] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [DeliverStatus] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [Ip] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsInvoice] int DEFAULT ((0)) NOT NULL,
  [InvoiceCompanyName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [InvoiceCompanyID] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [InvoiceType] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [InvoiceNote] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsRead] int DEFAULT ((0)) NOT NULL,
  [IsEnd] int DEFAULT ((0)) NOT NULL,
  [EndTime] datetime  NULL,
  [IsOk] int DEFAULT ((0)) NOT NULL,
  [IsComment] int DEFAULT ((0)) NOT NULL,
  [Flag1] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Flag2] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Flag3] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Title] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [LastModTime] datetime  NULL,
  [OrderType] int DEFAULT ((0)) NOT NULL,
  [MyType] int DEFAULT ((0)) NOT NULL,
  [OutTradeNo] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Order] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单号',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'OrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'下单用户名',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商户ID',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'ShopId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'积分',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Credit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总数量',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'TotalQty'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总价格',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'TotalPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'折扣',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Discount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运费',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Fare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'税',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'TotalTax'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总支付价格',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'TotalPay'
GO

EXEC sp_addextendedproperty
'MS_Description', N'返现积分',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'BackCredits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'姓名',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'RealName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'国家',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Country'
GO

EXEC sp_addextendedproperty
'MS_Description', N'省份',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Province'
GO

EXEC sp_addextendedproperty
'MS_Description', N'城市',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'City'
GO

EXEC sp_addextendedproperty
'MS_Description', N'区',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'District'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详细地址',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Address'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮编',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'PostCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'手机',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Tel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'手机',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Mobile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮箱',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Notes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理员备注',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'AdminNotes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'配送方式ID',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'DeliverId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'配送方式名称',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'DeliverType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运单号',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'DeliverNO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'配送备注',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'DeliverNotes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付方式ID',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'PayId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'付款方式',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'PayType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付备注',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'PayTypeNotes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单状态：未确认、已确认、已完成、已取消',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'OrderStatus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付状态：未支付、已支付、已退款',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'PaymentStatus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'配送状态：未配送、配货中、已配送、已收到、退货中、已退货',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'DeliverStatus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'下单IP',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Ip'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否需要发票',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'IsInvoice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'抬头',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'InvoiceCompanyName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'纳税人识别号',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'InvoiceCompanyID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'发票内容',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'InvoiceType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'发票备注',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'InvoiceNote'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否未读',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'IsRead'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否结束',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'IsEnd'
GO

EXEC sp_addextendedproperty
'MS_Description', N'结束时间',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单是否顺利完成',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'IsOk'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单已经评论',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'IsComment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'预留字段1',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Flag1'
GO

EXEC sp_addextendedproperty
'MS_Description', N'预留字段2',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Flag2'
GO

EXEC sp_addextendedproperty
'MS_Description', N'预留字段3',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Flag3'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单名称',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后操作时间',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'LastModTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单类型，0为商品订单',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'OrderType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'系统类型',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'MyType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支付成功流水号',
'SCHEMA', N'dbo',
'TABLE', N'Order',
'COLUMN', N'OutTradeNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单',
'SCHEMA', N'dbo',
'TABLE', N'Order'
GO


-- ----------------------------
-- Table structure for OrderDetail
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderDetail]') AND type IN ('U'))
	DROP TABLE [dbo].[OrderDetail]
GO

CREATE TABLE [dbo].[OrderDetail] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [OrderId] int DEFAULT ((0)) NOT NULL,
  [OrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ShopId] int DEFAULT ((0)) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [PId] int DEFAULT ((0)) NOT NULL,
  [TypeId] int DEFAULT ((0)) NOT NULL,
  [PriceId] int DEFAULT ((0)) NOT NULL,
  [Title] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Attr] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Color] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Spec] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ItemNO] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Qty] int DEFAULT ((0)) NOT NULL,
  [Price] money DEFAULT ((0)) NOT NULL,
  [MarketPrice] money DEFAULT ((0)) NOT NULL,
  [SpecialPrice] money DEFAULT ((0)) NOT NULL,
  [Discount] money DEFAULT ((0)) NOT NULL,
  [Tax] money DEFAULT ((0)) NOT NULL,
  [Credit] int DEFAULT ((0)) NOT NULL,
  [BackCredits] int DEFAULT ((0)) NOT NULL,
  [IsOK] int DEFAULT ((0)) NOT NULL,
  [IsComment] int DEFAULT ((0)) NOT NULL,
  [AddTime] datetime  NULL,
  [CheckInDate] datetime  NULL,
  [LeaveDate] datetime  NULL,
  [MyType] int DEFAULT ((0)) NOT NULL,
  [IsPay] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[OrderDetail] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单ID',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'OrderId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单号',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'OrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商户ID',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'ShopId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品ID',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'PId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类型ID',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'TypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'价格ID',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'PriceId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品名称',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品名称',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品属性',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Attr'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品颜色',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Color'
GO

EXEC sp_addextendedproperty
'MS_Description', N'规格',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Spec'
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'ItemNO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'数量',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Qty'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品价格',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品市场价格',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'MarketPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品特价',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'SpecialPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品优惠',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Discount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品税',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Tax'
GO

EXEC sp_addextendedproperty
'MS_Description', N'积分',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'Credit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'返现积分',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'BackCredits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否完成',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'IsOK'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否已经评论',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'IsComment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'入住日期',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'CheckInDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'离开日期',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'LeaveDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'系统类型',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'MyType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否已经支付',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail',
'COLUMN', N'IsPay'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单详情',
'SCHEMA', N'dbo',
'TABLE', N'OrderDetail'
GO


-- ----------------------------
-- Table structure for OrderLog
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[OrderLog]') AND type IN ('U'))
	DROP TABLE [dbo].[OrderLog]
GO

CREATE TABLE [dbo].[OrderLog] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [OrderId] int DEFAULT ((0)) NOT NULL,
  [OrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Actions] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [AddTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[OrderLog] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'OrderLog',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单ID',
'SCHEMA', N'dbo',
'TABLE', N'OrderLog',
'COLUMN', N'OrderId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单号',
'SCHEMA', N'dbo',
'TABLE', N'OrderLog',
'COLUMN', N'OrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'日志记录',
'SCHEMA', N'dbo',
'TABLE', N'OrderLog',
'COLUMN', N'Actions'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'OrderLog',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'OrderLog',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单日志',
'SCHEMA', N'dbo',
'TABLE', N'OrderLog'
GO


-- ----------------------------
-- Table structure for OtherConfig
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[OtherConfig]') AND type IN ('U'))
	DROP TABLE [dbo].[OtherConfig]
GO

CREATE TABLE [dbo].[OtherConfig] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [ConfigName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ConfigValue] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [LastUpdateTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[OtherConfig] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'OtherConfig',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'配置名称',
'SCHEMA', N'dbo',
'TABLE', N'OtherConfig',
'COLUMN', N'ConfigName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'配置值 JSON',
'SCHEMA', N'dbo',
'TABLE', N'OtherConfig',
'COLUMN', N'ConfigValue'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新时间',
'SCHEMA', N'dbo',
'TABLE', N'OtherConfig',
'COLUMN', N'LastUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'其他配置',
'SCHEMA', N'dbo',
'TABLE', N'OtherConfig'
GO


-- ----------------------------
-- Table structure for Product
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type IN ('U'))
	DROP TABLE [dbo].[Product]
GO

CREATE TABLE [dbo].[Product] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KId] int DEFAULT ((0)) NOT NULL,
  [BId] int DEFAULT ((0)) NOT NULL,
  [ShopId] int DEFAULT ((0)) NOT NULL,
  [CId] int DEFAULT ((0)) NOT NULL,
  [SupportId] int DEFAULT ((0)) NOT NULL,
  [Title] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [ItemNO] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SubTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Unit] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Spec] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Color] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Weight] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Price] money DEFAULT ((0)) NOT NULL,
  [MarketPrice] money DEFAULT ((0)) NOT NULL,
  [SpecialPrice] money DEFAULT ((0)) NOT NULL,
  [Fare] money DEFAULT ((0)) NOT NULL,
  [Discount] money DEFAULT ((0)) NOT NULL,
  [Material] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Front] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Credits] int DEFAULT ((0)) NOT NULL,
  [Stock] int DEFAULT ((0)) NOT NULL,
  [WarnStock] int DEFAULT ((0)) NOT NULL,
  [IsSubProduct] int DEFAULT ((0)) NOT NULL,
  [PPId] int DEFAULT ((0)) NOT NULL,
  [Content] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Parameters] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Keyword] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [LinkURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TitleColor] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsRecommend] int DEFAULT ((0)) NOT NULL,
  [IsHide] int DEFAULT ((0)) NOT NULL,
  [IsLock] int DEFAULT ((0)) NOT NULL,
  [IsDel] int DEFAULT ((0)) NOT NULL,
  [IsTop] int DEFAULT ((0)) NOT NULL,
  [IsBest] int DEFAULT ((0)) NOT NULL,
  [IsComment] int DEFAULT ((0)) NOT NULL,
  [IsMember] int DEFAULT ((0)) NOT NULL,
  [IsNew] int DEFAULT ((0)) NOT NULL,
  [IsSpecial] int DEFAULT ((0)) NOT NULL,
  [IsPromote] int DEFAULT ((0)) NOT NULL,
  [IsHotSales] int DEFAULT ((0)) NOT NULL,
  [IsBreakup] int DEFAULT ((0)) NOT NULL,
  [IsShelves] int DEFAULT ((0)) NOT NULL,
  [IsVerify] int DEFAULT ((0)) NOT NULL,
  [Hits] int DEFAULT ((0)) NOT NULL,
  [IsGift] int DEFAULT ((0)) NOT NULL,
  [IsPart] int DEFAULT ((0)) NOT NULL,
  [Sequence] int DEFAULT ((0)) NOT NULL,
  [CommentCount] int DEFAULT ((0)) NOT NULL,
  [Icon] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClassName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BannerImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AdsId] int DEFAULT ((0)) NOT NULL,
  [Tags] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [ItemImg] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Service] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [AuthorId] int DEFAULT ((0)) NOT NULL,
  [AddTime] datetime  NULL,
  [UpdateTime] datetime  NULL,
  [Location] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [FilePath] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [FileName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [WarningStock] int DEFAULT ((0)) NOT NULL,
  [Sales] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Product] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目ID',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'KId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'品牌ID',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'BId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'店铺ID',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'ShopId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'颜色ID',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'CId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'供货商ID',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'SupportId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'标题',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'ItemNO'
GO

EXEC sp_addextendedproperty
'MS_Description', N'副标题',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'SubTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品单位',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Unit'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品规格尺寸',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Spec'
GO

EXEC sp_addextendedproperty
'MS_Description', N'颜色',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Color'
GO

EXEC sp_addextendedproperty
'MS_Description', N'重量',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Weight'
GO

EXEC sp_addextendedproperty
'MS_Description', N'价格',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'市场价格',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'MarketPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'特价，如有特价，以特价为准',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'SpecialPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运费',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Fare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'折扣',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Discount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'材料',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Material'
GO

EXEC sp_addextendedproperty
'MS_Description', N'封面',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Front'
GO

EXEC sp_addextendedproperty
'MS_Description', N'积分',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Credits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'库存',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Stock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'警告库存',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'WarnStock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为子商品',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsSubProduct'
GO

EXEC sp_addextendedproperty
'MS_Description', N'父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'PPId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'内容',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Content'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品参数',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Parameters'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Keyword'
GO

EXEC sp_addextendedproperty
'MS_Description', N'介绍',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'跳转链接',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'LinkURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称颜色',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'TitleColor'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模板',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'TemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否推荐',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsRecommend'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否锁定，不允许删除',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsLock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除,已经删除到回收站',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否置顶',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsTop'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否精华',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsBest'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否允许评论',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsComment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否会员栏目',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsMember'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否新品',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsNew'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否特价',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsSpecial'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否促销',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsPromote'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否热销',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsHotSales'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否缺货',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsBreakup'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否下架',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsShelves'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否审核，1为已经审核前台显示',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsVerify'
GO

EXEC sp_addextendedproperty
'MS_Description', N'点击数量',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Hits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为礼品商品',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsGift'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为配件',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'IsPart'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Sequence'
GO

EXEC sp_addextendedproperty
'MS_Description', N'评论数量',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'CommentCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'样式名称',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'ClassName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'banner图片',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'BannerImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告ID',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'AdsId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'TAG',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Tags'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更多图片',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'ItemImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'售后服务',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Service'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'AuthorId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路径',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Location'
GO

EXEC sp_addextendedproperty
'MS_Description', N'存放目录',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'FilePath'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件名称',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'FileName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'警告库存',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'WarningStock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'销售量',
'SCHEMA', N'dbo',
'TABLE', N'Product',
'COLUMN', N'Sales'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品',
'SCHEMA', N'dbo',
'TABLE', N'Product'
GO


-- ----------------------------
-- Table structure for ProductAttr
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductAttr]') AND type IN ('U'))
	DROP TABLE [dbo].[ProductAttr]
GO

CREATE TABLE [dbo].[ProductAttr] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [PId] int DEFAULT ((0)) NOT NULL,
  [Title] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TitleColor] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [Color] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Size] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Spec] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Price] money DEFAULT ((0)) NOT NULL,
  [MarketPrice] money DEFAULT ((0)) NOT NULL,
  [Stock] int DEFAULT ((0)) NOT NULL,
  [Rank] int DEFAULT ((0)) NOT NULL,
  [MoreImg] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [WarningStock] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[ProductAttr] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品ID',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'PId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'属性名称',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'属性名称颜色',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'TitleColor'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'颜色',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'Color'
GO

EXEC sp_addextendedproperty
'MS_Description', N'尺码',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'Size'
GO

EXEC sp_addextendedproperty
'MS_Description', N'规格',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'Spec'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'价格',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'Price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'市场价格',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'MarketPrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'库存',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'Stock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更多图片',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'MoreImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'预警库存',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr',
'COLUMN', N'WarningStock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品属性',
'SCHEMA', N'dbo',
'TABLE', N'ProductAttr'
GO


-- ----------------------------
-- Table structure for RebateChangeLog
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[RebateChangeLog]') AND type IN ('U'))
	DROP TABLE [dbo].[RebateChangeLog]
GO

CREATE TABLE [dbo].[RebateChangeLog] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [AdminId] int DEFAULT ((0)) NOT NULL,
  [UserName] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [IP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Actions] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Reward] money DEFAULT ((0)) NOT NULL,
  [BeforChange] money DEFAULT ((0)) NOT NULL,
  [AfterChange] money DEFAULT ((0)) NOT NULL,
  [LogDetails] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TypeId] int DEFAULT ((0)) NOT NULL,
  [MyType] int DEFAULT ((0)) NOT NULL,
  [OrderId] int DEFAULT ((0)) NOT NULL,
  [OrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[RebateChangeLog] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'AdminId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录IP',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'IP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'记录',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'Actions'
GO

EXEC sp_addextendedproperty
'MS_Description', N'返现、扣除金额',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'Reward'
GO

EXEC sp_addextendedproperty
'MS_Description', N'变化前',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'BeforChange'
GO

EXEC sp_addextendedproperty
'MS_Description', N'变化后',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'AfterChange'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详细记录',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'LogDetails'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'TypeId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'消费类型，见MyType',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'MyType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单ID',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'OrderId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单号',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog',
'COLUMN', N'OrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户返现余额变化记录',
'SCHEMA', N'dbo',
'TABLE', N'RebateChangeLog'
GO


-- ----------------------------
-- Table structure for ShipFeeStrategy
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ShipFeeStrategy]') AND type IN ('U'))
	DROP TABLE [dbo].[ShipFeeStrategy]
GO

CREATE TABLE [dbo].[ShipFeeStrategy] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Title] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [DefaultFare] money DEFAULT ((0)) NOT NULL,
  [MaxFreeFare] money DEFAULT ((0)) NOT NULL,
  [MinWeight] money DEFAULT ((0)) NOT NULL,
  [AppendWeight] money DEFAULT ((0)) NOT NULL,
  [AppendFee] money DEFAULT ((0)) NOT NULL,
  [AddTime] datetime  NULL,
  [UpdateTime] datetime  NULL,
  [AreaType] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[ShipFeeStrategy] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'策略名称',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'策略说明',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'默认运费',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'DefaultFare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最大免运费金额',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'MaxFreeFare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最小重量',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'MinWeight'
GO

EXEC sp_addextendedproperty
'MS_Description', N'续重重量',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'AppendWeight'
GO

EXEC sp_addextendedproperty
'MS_Description', N'续重金额',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'AppendFee'
GO

EXEC sp_addextendedproperty
'MS_Description', N'加入时间',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新时间',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'策略地区类型。1省份策略；2城市策略',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy',
'COLUMN', N'AreaType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运费策略',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategy'
GO


-- ----------------------------
-- Table structure for ShipFeeStrategyArea
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ShipFeeStrategyArea]') AND type IN ('U'))
	DROP TABLE [dbo].[ShipFeeStrategyArea]
GO

CREATE TABLE [dbo].[ShipFeeStrategyArea] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [AreaId] int DEFAULT ((0)) NOT NULL,
  [ShipFeeStrategyId] int DEFAULT ((0)) NOT NULL,
  [Province] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [City] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Level] int DEFAULT ((0)) NOT NULL,
  [Code] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[ShipFeeStrategyArea] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategyArea',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'地区Id',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategyArea',
'COLUMN', N'AreaId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运费策略Id',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategyArea',
'COLUMN', N'ShipFeeStrategyId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'省份',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategyArea',
'COLUMN', N'Province'
GO

EXEC sp_addextendedproperty
'MS_Description', N'城市',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategyArea',
'COLUMN', N'City'
GO

EXEC sp_addextendedproperty
'MS_Description', N'级别',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategyArea',
'COLUMN', N'Level'
GO

EXEC sp_addextendedproperty
'MS_Description', N'区域代码',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategyArea',
'COLUMN', N'Code'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运费区域策略详情',
'SCHEMA', N'dbo',
'TABLE', N'ShipFeeStrategyArea'
GO


-- ----------------------------
-- Table structure for Shop
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Shop]') AND type IN ('U'))
	DROP TABLE [dbo].[Shop]
GO

CREATE TABLE [dbo].[Shop] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [ShopName] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [KId] int DEFAULT ((0)) NOT NULL,
  [AId] int DEFAULT ((0)) NOT NULL,
  [Sequence] int DEFAULT ((0)) NOT NULL,
  [Latitude] money DEFAULT ((0)) NOT NULL,
  [Longitude] money DEFAULT ((0)) NOT NULL,
  [Country] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Province] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [City] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [District] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Address] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Postcode] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDel] int DEFAULT ((0)) NOT NULL,
  [IsHide] int DEFAULT ((0)) NOT NULL,
  [IsDisabled] int DEFAULT ((0)) NOT NULL,
  [Content] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Keyword] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [BannerImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Balance] money DEFAULT ((0)) NOT NULL,
  [IsTop] int DEFAULT ((0)) NOT NULL,
  [IsVip] int DEFAULT ((0)) NOT NULL,
  [IsRecommend] int DEFAULT ((0)) NOT NULL,
  [Likes] int DEFAULT ((0)) NOT NULL,
  [AvgScore] money DEFAULT ((0)) NOT NULL,
  [ServiceScore] money DEFAULT ((0)) NOT NULL,
  [SpeedScore] money DEFAULT ((0)) NOT NULL,
  [EnvironmentScore] money DEFAULT ((0)) NOT NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [MorePics] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Email] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [Tel] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Phone] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [QQ] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Skype] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [HomePage] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Weixin] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsShip] int DEFAULT ((0)) NOT NULL,
  [OpenTime] datetime  NULL,
  [CloseTime] datetime  NULL,
  [ShippingStartTime] datetime  NULL,
  [ShippingEndTime] datetime  NULL,
  [AddTime] datetime  NULL,
  [Hits] int DEFAULT ((0)) NOT NULL,
  [MyType] int DEFAULT ((0)) NOT NULL,
  [DefaultFare] money DEFAULT ((0)) NOT NULL,
  [MaxFreeFare] money DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Shop] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'店铺名称',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'ShopName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商家分类ID',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'KId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'地区ID',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'AId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Sequence'
GO

EXEC sp_addextendedproperty
'MS_Description', N'纬度',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Latitude'
GO

EXEC sp_addextendedproperty
'MS_Description', N'经度',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Longitude'
GO

EXEC sp_addextendedproperty
'MS_Description', N'国家',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Country'
GO

EXEC sp_addextendedproperty
'MS_Description', N'省',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Province'
GO

EXEC sp_addextendedproperty
'MS_Description', N'市',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'City'
GO

EXEC sp_addextendedproperty
'MS_Description', N'区',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'District'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详细地址',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Address'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮政编码',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Postcode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'IsDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否禁用',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'IsDisabled'
GO

EXEC sp_addextendedproperty
'MS_Description', N'店铺介绍',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Content'
GO

EXEC sp_addextendedproperty
'MS_Description', N'关键字',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Keyword'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'banner图片',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'BannerImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'余额',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Balance'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否置顶',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'IsTop'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否vip',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'IsVip'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否推荐',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'IsRecommend'
GO

EXEC sp_addextendedproperty
'MS_Description', N'点赞数',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Likes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'平均分数',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'AvgScore'
GO

EXEC sp_addextendedproperty
'MS_Description', N'服务分数',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'ServiceScore'
GO

EXEC sp_addextendedproperty
'MS_Description', N'速度分数',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'SpeedScore'
GO

EXEC sp_addextendedproperty
'MS_Description', N'环境分数',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'EnvironmentScore'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'店铺所有图片',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'MorePics'
GO

EXEC sp_addextendedproperty
'MS_Description', N'邮箱',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Email'
GO

EXEC sp_addextendedproperty
'MS_Description', N'电话',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Tel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'固定电话',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Phone'
GO

EXEC sp_addextendedproperty
'MS_Description', N'QQ',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'QQ'
GO

EXEC sp_addextendedproperty
'MS_Description', N'Skype',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Skype'
GO

EXEC sp_addextendedproperty
'MS_Description', N'主页',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'HomePage'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Weixin'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否配送',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'IsShip'
GO

EXEC sp_addextendedproperty
'MS_Description', N'开店时间',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'OpenTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'关店时间',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'CloseTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'配送开始时间',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'ShippingStartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'配送结束时间',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'ShippingEndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'点击量',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'Hits'
GO

EXEC sp_addextendedproperty
'MS_Description', N'店铺类型',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'MyType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'默认运费',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'DefaultFare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最大免运费金额',
'SCHEMA', N'dbo',
'TABLE', N'Shop',
'COLUMN', N'MaxFreeFare'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商家',
'SCHEMA', N'dbo',
'TABLE', N'Shop'
GO


-- ----------------------------
-- Table structure for ShopCategory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ShopCategory]') AND type IN ('U'))
	DROP TABLE [dbo].[ShopCategory]
GO

CREATE TABLE [dbo].[ShopCategory] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [KindName] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [SubTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindTitle] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Keyword] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Description] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [LinkURL] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [TitleColor] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [TemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [DetailTemplateFile] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindDomain] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsList] int DEFAULT ((0)) NOT NULL,
  [PageSize] int DEFAULT ((0)) NOT NULL,
  [PId] int DEFAULT ((0)) NOT NULL,
  [Level] int DEFAULT ((0)) NOT NULL,
  [Location] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsHide] int DEFAULT ((0)) NOT NULL,
  [IsLock] int DEFAULT ((0)) NOT NULL,
  [IsDel] int DEFAULT ((0)) NOT NULL,
  [IsComment] int DEFAULT ((0)) NOT NULL,
  [IsMember] int DEFAULT ((0)) NOT NULL,
  [IsShowSubDetail] int DEFAULT ((0)) NOT NULL,
  [CatalogId] int DEFAULT ((0)) NOT NULL,
  [Counts] int DEFAULT ((0)) NOT NULL,
  [Rank] int DEFAULT ((0)) NOT NULL,
  [Icon] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ClassName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [BannerImg] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [KindInfo] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [Pic] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [AdsId] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[ShopCategory] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目名称',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'KindName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目副标题',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'SubTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目标题，填写则在浏览器替换此标题',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'KindTitle'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'Keyword'
GO

EXEC sp_addextendedproperty
'MS_Description', N'介绍',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'Description'
GO

EXEC sp_addextendedproperty
'MS_Description', N'跳转链接',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'LinkURL'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别名称颜色',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'TitleColor'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模板',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'TemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详情模板',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'DetailTemplateFile'
GO

EXEC sp_addextendedproperty
'MS_Description', N'类别域名（保留）',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'KindDomain'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否为列表页面',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'IsList'
GO

EXEC sp_addextendedproperty
'MS_Description', N'每页显示数量',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'PageSize'
GO

EXEC sp_addextendedproperty
'MS_Description', N'上级ID',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'PId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'级别',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'Level'
GO

EXEC sp_addextendedproperty
'MS_Description', N'路径',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'Location'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否隐藏',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'IsHide'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否锁定，不允许删除',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'IsLock'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否删除,已经删除到回收站',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'IsDel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否允许评论',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'IsComment'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否会员栏目',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'IsMember'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否显示下级栏目内容',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'IsShowSubDetail'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模型ID',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'CatalogId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'详情数量，缓存',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'Counts'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图标',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'Icon'
GO

EXEC sp_addextendedproperty
'MS_Description', N'样式名称',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'ClassName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'banner图片',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'BannerImg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'栏目详细介绍',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'KindInfo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图片',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'Pic'
GO

EXEC sp_addextendedproperty
'MS_Description', N'广告ID',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory',
'COLUMN', N'AdsId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商家分类',
'SCHEMA', N'dbo',
'TABLE', N'ShopCategory'
GO


-- ----------------------------
-- Table structure for ShoppingCart
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[ShoppingCart]') AND type IN ('U'))
	DROP TABLE [dbo].[ShoppingCart]
GO

CREATE TABLE [dbo].[ShoppingCart] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [GUID] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Details] ntext COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [UpdateTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[ShoppingCart] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'ShoppingCart',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'ShoppingCart',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'唯一GUID，没登录用户使用',
'SCHEMA', N'dbo',
'TABLE', N'ShoppingCart',
'COLUMN', N'GUID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'购物车内容，JOSN',
'SCHEMA', N'dbo',
'TABLE', N'ShoppingCart',
'COLUMN', N'Details'
GO

EXEC sp_addextendedproperty
'MS_Description', N'加入购物车时间',
'SCHEMA', N'dbo',
'TABLE', N'ShoppingCart',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新时间',
'SCHEMA', N'dbo',
'TABLE', N'ShoppingCart',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'购物车',
'SCHEMA', N'dbo',
'TABLE', N'ShoppingCart'
GO


-- ----------------------------
-- Table structure for TargetEvent
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[TargetEvent]') AND type IN ('U'))
	DROP TABLE [dbo].[TargetEvent]
GO

CREATE TABLE [dbo].[TargetEvent] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [EventKey] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [EventName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsDisable] int DEFAULT ((0)) NOT NULL,
  [Rank] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[TargetEvent] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'TargetEvent',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'事件key',
'SCHEMA', N'dbo',
'TABLE', N'TargetEvent',
'COLUMN', N'EventKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'事件名称',
'SCHEMA', N'dbo',
'TABLE', N'TargetEvent',
'COLUMN', N'EventName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否禁用',
'SCHEMA', N'dbo',
'TABLE', N'TargetEvent',
'COLUMN', N'IsDisable'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'TargetEvent',
'COLUMN', N'Rank'
GO

EXEC sp_addextendedproperty
'MS_Description', N'目标事件',
'SCHEMA', N'dbo',
'TABLE', N'TargetEvent'
GO


-- ----------------------------
-- Records of TargetEvent
-- ----------------------------
SET IDENTITY_INSERT [dbo].[TargetEvent] ON
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'1', N'add', N'添加', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'2', N'edit', N'修改', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'3', N'del', N'删除', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'4', N'view', N'查看', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'5', N'viewlist', N'查看列表', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'6', N'import', N'导入', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'7', N'export', N'导出', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'8', N'filter', N'搜索', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'9', N'batch', N'批量操作', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'10', N'recycle', N'回收站', N'0', N'0')
GO

INSERT INTO [dbo].[TargetEvent] ([Id], [EventKey], [EventName], [IsDisable], [Rank]) VALUES (N'11', N'confirm', N'确认', N'0', N'0')
GO

SET IDENTITY_INSERT [dbo].[TargetEvent] OFF
GO


-- ----------------------------
-- Table structure for WithdrawOrder
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[WithdrawOrder]') AND type IN ('U'))
	DROP TABLE [dbo].[WithdrawOrder]
GO

CREATE TABLE [dbo].[WithdrawOrder] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [OrderNum] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserName] nvarchar(200) COLLATE Chinese_PRC_CI_AS  NULL,
  [Title] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL,
  [IP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Actions] nvarchar(250) COLLATE Chinese_PRC_CI_AS  NULL,
  [Price] money DEFAULT ((0)) NOT NULL,
  [VerifyAdminId] int DEFAULT ((0)) NOT NULL,
  [IsVerify] int DEFAULT ((0)) NOT NULL,
  [VerifyTime] datetime  NULL,
  [FormId] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[WithdrawOrder] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单号',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'OrderNum'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单名称',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录IP',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'IP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'记录详情',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'Actions'
GO

EXEC sp_addextendedproperty
'MS_Description', N'提现金额',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'Price'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审核管理员ID',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'VerifyAdminId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否通过审核',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'IsVerify'
GO

EXEC sp_addextendedproperty
'MS_Description', N'审核时间',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'VerifyTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'小程序FormId',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder',
'COLUMN', N'FormId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户提现订单',
'SCHEMA', N'dbo',
'TABLE', N'WithdrawOrder'
GO


-- ----------------------------
-- Table structure for WXAppSession
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[WXAppSession]') AND type IN ('U'))
	DROP TABLE [dbo].[WXAppSession]
GO

CREATE TABLE [dbo].[WXAppSession] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [UId] int DEFAULT ((0)) NOT NULL,
  [IP] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Key] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [SessionKey] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [OpenId] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [AddTime] datetime  NULL
)
GO

ALTER TABLE [dbo].[WXAppSession] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'编号',
'SCHEMA', N'dbo',
'TABLE', N'WXAppSession',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户ID',
'SCHEMA', N'dbo',
'TABLE', N'WXAppSession',
'COLUMN', N'UId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'登录IP',
'SCHEMA', N'dbo',
'TABLE', N'WXAppSession',
'COLUMN', N'IP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'系统生成Key',
'SCHEMA', N'dbo',
'TABLE', N'WXAppSession',
'COLUMN', N'Key'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信SessionKey',
'SCHEMA', N'dbo',
'TABLE', N'WXAppSession',
'COLUMN', N'SessionKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'微信小程序OpenId',
'SCHEMA', N'dbo',
'TABLE', N'WXAppSession',
'COLUMN', N'OpenId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'添加时间',
'SCHEMA', N'dbo',
'TABLE', N'WXAppSession',
'COLUMN', N'AddTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'小程序Session',
'SCHEMA', N'dbo',
'TABLE', N'WXAppSession'
GO


-- ----------------------------
-- Primary Key structure for table Admin
-- ----------------------------
ALTER TABLE [dbo].[Admin] ADD CONSTRAINT [PK__Admin__3214EC070BE151E2] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table AdminLog
-- ----------------------------
ALTER TABLE [dbo].[AdminLog] ADD CONSTRAINT [PK__AdminLog__3214EC07B6C01400] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table AdminMenu
-- ----------------------------
ALTER TABLE [dbo].[AdminMenu] ADD CONSTRAINT [PK__AdminMen__3214EC0713FD6092] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table AdminMenuEvent
-- ----------------------------
ALTER TABLE [dbo].[AdminMenuEvent] ADD CONSTRAINT [PK__AdminMen__3214EC0766C1640F] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table AdminRoles
-- ----------------------------
ALTER TABLE [dbo].[AdminRoles] ADD CONSTRAINT [PK__AdminRol__3214EC07494ABBBC] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Ads
-- ----------------------------
ALTER TABLE [dbo].[Ads] ADD CONSTRAINT [PK__Ads__3214EC07F20950FC] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table AdsKind
-- ----------------------------
ALTER TABLE [dbo].[AdsKind] ADD CONSTRAINT [PK__AdsKind__3214EC07A3BEDD3C] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Area
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Area_Name]
ON [dbo].[Area] (
  [Name] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Area_ParentId]
ON [dbo].[Area] (
  [ParentId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Area_Level]
ON [dbo].[Area] (
  [Level] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Area_Code]
ON [dbo].[Area] (
  [Code] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Area_PinYin]
ON [dbo].[Area] (
  [PinYin] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Area_PY]
ON [dbo].[Area] (
  [PY] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Area
-- ----------------------------
ALTER TABLE [dbo].[Area] ADD CONSTRAINT [PK__Area__3214EC07641C70E0] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Article
-- ----------------------------
ALTER TABLE [dbo].[Article] ADD CONSTRAINT [PK__Article__3214EC0762B47DAB] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table ArticleCategory
-- ----------------------------
ALTER TABLE [dbo].[ArticleCategory] ADD CONSTRAINT [PK__ArticleC__3214EC07EEE28C9B] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table BalanceChangeLog
-- ----------------------------
ALTER TABLE [dbo].[BalanceChangeLog] ADD CONSTRAINT [PK__BalanceC__3214EC077D646F4C] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Category
-- ----------------------------
ALTER TABLE [dbo].[Category] ADD CONSTRAINT [PK__Category__3214EC074235E836] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Config
-- ----------------------------
ALTER TABLE [dbo].[Config] ADD CONSTRAINT [PK__Config__3214EC07D9ABFA2C] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Coupon
-- ----------------------------
ALTER TABLE [dbo].[Coupon] ADD CONSTRAINT [PK__Coupon__3214EC076FF20207] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table CouponKind
-- ----------------------------
ALTER TABLE [dbo].[CouponKind] ADD CONSTRAINT [PK__CouponKi__3214EC07E9F856CA] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table CouponUseLog
-- ----------------------------
ALTER TABLE [dbo].[CouponUseLog] ADD CONSTRAINT [PK__CouponUs__3214EC0774E2BA5F] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Favortie
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Favortie_UId]
ON [dbo].[Favortie] (
  [UId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Favortie_TId]
ON [dbo].[Favortie] (
  [TId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Favortie_RId]
ON [dbo].[Favortie] (
  [RId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Favortie
-- ----------------------------
ALTER TABLE [dbo].[Favortie] ADD CONSTRAINT [PK__Favortie__3214EC07FCC7A7E8] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Food
-- ----------------------------
ALTER TABLE [dbo].[Food] ADD CONSTRAINT [PK__Food__3214EC071E1B18CF] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Guestbook
-- ----------------------------
ALTER TABLE [dbo].[Guestbook] ADD CONSTRAINT [PK__Guestboo__3214EC07334AFBC4] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table GuestbookCategory
-- ----------------------------
ALTER TABLE [dbo].[GuestbookCategory] ADD CONSTRAINT [PK__Guestboo__3214EC078E45FE08] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table HotelRoom
-- ----------------------------
ALTER TABLE [dbo].[HotelRoom] ADD CONSTRAINT [PK__HotelRoo__3214EC071F0FCCEA] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Link
-- ----------------------------
ALTER TABLE [dbo].[Link] ADD CONSTRAINT [PK__Link__3214EC07ACB87964] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table LinkKind
-- ----------------------------
ALTER TABLE [dbo].[LinkKind] ADD CONSTRAINT [PK__LinkKind__3214EC07B20D8679] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Member
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Member_Rebate]
ON [dbo].[Member] (
  [Rebate] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Member_Balance]
ON [dbo].[Member] (
  [Balance] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Member_WeixinOpenId]
ON [dbo].[Member] (
  [WeixinOpenId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Member_WeixinAppOpenId]
ON [dbo].[Member] (
  [WeixinAppOpenId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Member_Tel]
ON [dbo].[Member] (
  [Tel] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Member_UserName]
ON [dbo].[Member] (
  [UserName] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Member_RoleId]
ON [dbo].[Member] (
  [RoleId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Member_AliAppOpenId]
ON [dbo].[Member] (
  [AliAppOpenId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Member_TenUnionId]
ON [dbo].[Member] (
  [TenUnionId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Member_RegType]
ON [dbo].[Member] (
  [RegType] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Member
-- ----------------------------
ALTER TABLE [dbo].[Member] ADD CONSTRAINT [PK__Member__3214EC0763DEC348] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table MemberAddress
-- ----------------------------
ALTER TABLE [dbo].[MemberAddress] ADD CONSTRAINT [PK__MemberAd__3214EC07FE64B32F] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table MemberCoupon
-- ----------------------------
ALTER TABLE [dbo].[MemberCoupon] ADD CONSTRAINT [PK__MemberCo__3214EC071891780F] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table MemberLog
-- ----------------------------
ALTER TABLE [dbo].[MemberLog] ADD CONSTRAINT [PK__MemberLo__3214EC07692F3D03] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table MemberRoles
-- ----------------------------
ALTER TABLE [dbo].[MemberRoles] ADD CONSTRAINT [PK__MemberRo__3214EC075C03E835] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table OnlinePayOrder
-- ----------------------------
ALTER TABLE [dbo].[OnlinePayOrder] ADD CONSTRAINT [PK__OnlinePa__3214EC078AFF6475] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table Order
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Order_OrderNum]
ON [dbo].[Order] (
  [OrderNum] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Order_OrderStatus]
ON [dbo].[Order] (
  [OrderStatus] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Order_PaymentStatus]
ON [dbo].[Order] (
  [PaymentStatus] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Order_AddTime]
ON [dbo].[Order] (
  [AddTime] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Order_OrderType]
ON [dbo].[Order] (
  [OrderType] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_Order_UId]
ON [dbo].[Order] (
  [UId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Order
-- ----------------------------
ALTER TABLE [dbo].[Order] ADD CONSTRAINT [PK__Order__3214EC07C496791F] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table OrderDetail
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_OrderDetail_OrderNum]
ON [dbo].[OrderDetail] (
  [OrderNum] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_OrderDetail_PId]
ON [dbo].[OrderDetail] (
  [PId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_OrderDetail_OrderId]
ON [dbo].[OrderDetail] (
  [OrderId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_OrderDetail_PriceId]
ON [dbo].[OrderDetail] (
  [PriceId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_OrderDetail_UId]
ON [dbo].[OrderDetail] (
  [UId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_OrderDetail_IsPay]
ON [dbo].[OrderDetail] (
  [IsPay] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_OrderDetail_IsOK]
ON [dbo].[OrderDetail] (
  [IsOK] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table OrderDetail
-- ----------------------------
ALTER TABLE [dbo].[OrderDetail] ADD CONSTRAINT [PK__OrderDet__3214EC073BAF54DF] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table OrderLog
-- ----------------------------
ALTER TABLE [dbo].[OrderLog] ADD CONSTRAINT [PK__OrderLog__3214EC07CA4BBF9F] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table OtherConfig
-- ----------------------------
ALTER TABLE [dbo].[OtherConfig] ADD CONSTRAINT [PK__OtherCon__3214EC07A62C0B84] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Product
-- ----------------------------
ALTER TABLE [dbo].[Product] ADD CONSTRAINT [PK__Product__3214EC0772DD3F76] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table ProductAttr
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_ProductAttr_PId]
ON [dbo].[ProductAttr] (
  [PId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ProductAttr_Spec]
ON [dbo].[ProductAttr] (
  [Spec] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ProductAttr_Stock]
ON [dbo].[ProductAttr] (
  [Stock] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ProductAttr_Rank]
ON [dbo].[ProductAttr] (
  [Rank] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table ProductAttr
-- ----------------------------
ALTER TABLE [dbo].[ProductAttr] ADD CONSTRAINT [PK__ProductA__3214EC0718092A75] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table RebateChangeLog
-- ----------------------------
ALTER TABLE [dbo].[RebateChangeLog] ADD CONSTRAINT [PK__RebateCh__3214EC0774BC006B] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table ShipFeeStrategy
-- ----------------------------
ALTER TABLE [dbo].[ShipFeeStrategy] ADD CONSTRAINT [PK__ShipFeeS__3214EC07304AEEC9] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table ShipFeeStrategyArea
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_ShipFeeStrategyArea_AreaId]
ON [dbo].[ShipFeeStrategyArea] (
  [AreaId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ShipFeeStrategyArea_ShipFeeStrategyId]
ON [dbo].[ShipFeeStrategyArea] (
  [ShipFeeStrategyId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ShipFeeStrategyArea_Province]
ON [dbo].[ShipFeeStrategyArea] (
  [Province] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ShipFeeStrategyArea_City]
ON [dbo].[ShipFeeStrategyArea] (
  [City] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_ShipFeeStrategyArea_Level]
ON [dbo].[ShipFeeStrategyArea] (
  [Level] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table ShipFeeStrategyArea
-- ----------------------------
ALTER TABLE [dbo].[ShipFeeStrategyArea] ADD CONSTRAINT [PK__ShipFeeS__3214EC07FD126DD9] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table Shop
-- ----------------------------
ALTER TABLE [dbo].[Shop] ADD CONSTRAINT [PK__Shop__3214EC07F42E4224] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table ShopCategory
-- ----------------------------
ALTER TABLE [dbo].[ShopCategory] ADD CONSTRAINT [PK__ShopCate__3214EC07AC3F60E8] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table ShoppingCart
-- ----------------------------
ALTER TABLE [dbo].[ShoppingCart] ADD CONSTRAINT [PK__Shopping__3214EC07434C850A] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table TargetEvent
-- ----------------------------
ALTER TABLE [dbo].[TargetEvent] ADD CONSTRAINT [PK__TargetEv__3214EC07D34F0E89] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table WithdrawOrder
-- ----------------------------
ALTER TABLE [dbo].[WithdrawOrder] ADD CONSTRAINT [PK__Withdraw__3214EC07A9077AD3] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table WXAppSession
-- ----------------------------
ALTER TABLE [dbo].[WXAppSession] ADD CONSTRAINT [PK__WXAppSes__3214EC0749B4AF48] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

