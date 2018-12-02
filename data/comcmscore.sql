/*
Navicat MySQL Data Transfer

Source Server         : MYSQL
Source Server Version : 50713
Source Host           : localhost:3306
Source Database       : comcms2

Target Server Type    : MYSQL
Target Server Version : 50713
File Encoding         : 65001

Date: 2018-11-16 01:09:40
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for admin
-- ----------------------------
DROP TABLE IF EXISTS `admin`;
CREATE TABLE `admin` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UserName` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户名',
  `PassWord` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '密码',
  `Salt` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '盐值',
  `RealName` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '姓名',
  `Tel` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '电话',
  `Email` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮件',
  `UserLevel` int(11) NOT NULL COMMENT '级别',
  `RoleId` int(11) NOT NULL COMMENT '管理组',
  `GroupId` int(11) NOT NULL COMMENT '用户组',
  `LastLoginTime` datetime DEFAULT NULL COMMENT '最后登录时间',
  `LastLoginIP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '上次登录IP',
  `ThisLoginTime` datetime DEFAULT NULL COMMENT '本次登录时间',
  `ThisLoginIP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '本次登录IP',
  `IsLock` int(11) NOT NULL COMMENT '是否是锁定',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='管理员';

-- ----------------------------
-- Records of admin
-- ----------------------------
INSERT INTO `admin` VALUES ('1', 'admin', '8788C77C8FA36CF9D3718026355AB571', 'fOtOFYYpLO90', 'admin', '', '', '100', '1', '0', '2018-11-15 23:43:02', '127.0.0.1', '2018-04-16 00:17:43', '127.0.0.1', '0');

-- ----------------------------
-- Table structure for adminlog
-- ----------------------------
DROP TABLE IF EXISTS `adminlog`;
CREATE TABLE `adminlog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '管理员ID',
  `GUID` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '唯一ID',
  `UserName` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户名',
  `PassWord` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '密码',
  `LoginTime` datetime DEFAULT NULL COMMENT '登录时间',
  `LoginIP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '登录IP',
  `IsLoginOK` int(11) NOT NULL COMMENT '是否登录成功',
  `Actions` text COMMENT '记录',
  `LastUpdateTime` datetime DEFAULT NULL COMMENT '登录时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='管理日志表';

-- ----------------------------
-- Records of adminlog
-- ----------------------------
INSERT INTO `adminlog` VALUES ('1', '0', '5d8b265b-342d-45f1-ab92-425791cd0b10', 'admin', '******', '2018-04-16 00:30:06', '127.0.0.1', '1', '查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；修改站点基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；', '2018-04-16 01:04:56');
INSERT INTO `adminlog` VALUES ('2', '0', '49ebe38a-3cfb-4e16-afa8-718d0c0758a1', 'admin', '******', '2018-04-16 01:09:35', '123.1.189.126', '1', '查看基本配置；', '2018-04-16 01:14:19');
INSERT INTO `adminlog` VALUES ('3', '0', '592f0755-0b3b-4871-a33a-32b137bff2fd', 'admin', '******', '2018-04-16 08:43:30', '60.166.72.54', '1', '查看基本配置；', '2018-04-16 08:43:36');
INSERT INTO `adminlog` VALUES ('4', '0', '363eb952-0d60-4eb7-82b6-419c710bb89c', 'admin', '******', '2018-04-16 10:19:15', '61.140.236.194', '1', null, '2018-04-16 10:19:15');
INSERT INTO `adminlog` VALUES ('5', '0', 'e1699742-94c3-4eb7-8bda-0fe815533e57', 'admin', '******', '2018-04-16 10:19:52', '116.24.101.123', '1', '查看基本配置；查看基本配置；', '2018-04-16 10:26:05');
INSERT INTO `adminlog` VALUES ('6', '0', 'f1b1264d-6b65-4573-b3d1-ae6135aa18f4', 'admin', '******', '2018-04-16 10:24:57', '116.226.70.126', '1', '查看基本配置；查看基本配置；', '2018-04-16 10:25:15');
INSERT INTO `adminlog` VALUES ('7', '0', '453c6021-7bf7-4b38-9913-27536d827bfc', 'admin', '******', '2018-04-16 10:25:10', '112.64.119.42', '1', '查看基本配置；', '2018-04-16 10:25:41');
INSERT INTO `adminlog` VALUES ('8', '0', '5796a8c3-4eaf-4bba-8742-7378135dd93b', 'admin', '******', '2018-04-16 10:32:07', '113.118.198.128', '1', '查看基本配置；', '2018-04-16 10:32:25');
INSERT INTO `adminlog` VALUES ('9', '0', '46f181aa-4747-4633-8fad-099f06799fe6', 'admin', '******', '2018-04-16 10:35:19', '59.172.251.218', '1', null, '2018-04-16 10:35:19');
INSERT INTO `adminlog` VALUES ('10', '0', 'b2c7c5d3-9e9f-4b35-825c-041c71bb3e68', 'admin', '******', '2018-04-17 22:53:58', '59.42.239.56', '1', '查看后台事件列表（keyword:;page:0;limit:20）;查看后台事件列表（keyword:;page:0;limit:20）;查看后台事件列表（keyword:;page:0;limit:20）;查看基本配置；查看SMTP配置；查看附件配置；查看附件配置；查看后台事件列表（keyword:;page:0;limit:20）;查看Robots.txt;', '2018-04-18 00:20:16');
INSERT INTO `adminlog` VALUES ('11', '0', '9283b94f-d4ac-42d8-8ae8-ffb462877401', 'admin', '******', '2018-04-18 09:50:49', '61.140.238.15', '1', '查看Robots.txt;查看附件配置；查看附件配置；查看附件配置；修改附件配置;查看附件配置；', '2018-04-18 11:27:05');
INSERT INTO `adminlog` VALUES ('12', '0', '1a19389a-b14d-453a-9970-1f154dd5331b', 'admin', '******', '2018-04-18 09:52:15', '116.226.70.126', '1', '查看后台事件列表（keyword:;page:0;limit:20）;', '2018-04-18 09:52:26');
INSERT INTO `adminlog` VALUES ('13', '0', '659402f8-b25a-42d8-9761-bbbbd349096f', 'admin', '******', '2018-04-18 09:53:41', '171.221.146.125', '1', '查看基本配置；查看后台事件列表（keyword:;page:0;limit:20）;查看后台事件列表（keyword:;page:0;limit:25）;', '2018-04-18 10:15:50');
INSERT INTO `adminlog` VALUES ('14', '0', 'e724d952-5c46-4c70-ac25-35b98c0dd121', 'admin', '******', '2018-04-18 09:54:47', '202.106.86.136', '1', '查看SMTP配置；', '2018-04-18 09:55:00');
INSERT INTO `adminlog` VALUES ('15', '0', '99d7a774-b6ec-42f9-96ef-1525ff6eae79', 'admin', '******', '2018-04-18 09:55:11', '182.242.249.54', '1', '查看基本配置；', '2018-04-18 09:55:27');
INSERT INTO `adminlog` VALUES ('16', '0', '596709a4-fe24-4141-8a4b-8c761d0a7e48', 'admin', '******', '2018-04-18 09:55:23', '113.57.27.145', '1', '查看基本配置；', '2018-04-18 09:55:29');
INSERT INTO `adminlog` VALUES ('17', '0', 'ba87f31b-075c-4de7-afa8-f99272b00a12', 'admin', '******', '2018-04-18 09:58:16', '121.33.75.50', '1', '查看基本配置；查看基本配置；查看SMTP配置；查看附件配置；查看Robots.txt;', '2018-04-18 09:58:53');
INSERT INTO `adminlog` VALUES ('18', '0', 'f68c98ad-4fb1-441c-bbfb-e6734d46d07f', 'Admin', '******', '2018-04-18 09:58:23', '36.98.143.127', '1', '查看附件配置；', '2018-04-18 10:03:38');
INSERT INTO `adminlog` VALUES ('19', '0', 'c6b26c99-e361-44db-9969-df2799caca60', 'admin', '******', '2018-04-18 10:01:41', '106.120.233.218', '1', null, '2018-04-18 10:01:41');
INSERT INTO `adminlog` VALUES ('20', '0', '161763c2-d840-4949-a672-4ac3e6339ffb', 'admin', '******', '2018-04-18 10:08:42', '223.104.108.31', '1', null, '2018-04-18 10:08:42');
INSERT INTO `adminlog` VALUES ('21', '0', '0ed86444-f812-4ac7-840f-b0809a1be406', 'admin', '******', '2018-04-18 10:17:16', '171.221.146.125', '1', '查看后台事件列表（keyword:;page:0;limit:20）;查看SMTP配置；查看附件配置；查看Robots.txt;查看后台事件列表（keyword:;page:0;limit:20）;查看附件配置；查看SMTP配置；查看Robots.txt;查看后台事件列表（keyword:;page:0;limit:20）;查看后台事件列表（keyword:;page:0;limit:10）;查看后台事件列表（keyword:;page:0;limit:25）;查看附件配置；修改附件配置;查看附件配置；', '2018-04-18 11:29:19');
INSERT INTO `adminlog` VALUES ('22', '0', '7aa7d5b4-4420-4918-b631-63c0a45ec141', 'admin', '******', '2018-04-18 10:21:01', '115.195.129.93', '1', '查看基本配置；查看SMTP配置；查看附件配置；查看Robots.txt;查看基本配置；查看SMTP配置；查看基本配置；查看SMTP配置；查看附件配置；', '2018-04-18 10:22:40');
INSERT INTO `adminlog` VALUES ('23', '0', 'ae5c1441-bedd-49fb-bb39-96ddbfd20dc0', 'admin', '******', '2018-04-18 10:24:51', '222.195.191.87', '1', '查看基本配置；查看SMTP配置；查看附件配置；查看Robots.txt;查看基本配置；查看基本配置；查看基本配置；', '2018-04-18 10:31:21');
INSERT INTO `adminlog` VALUES ('24', '0', 'eb783940-71cb-4681-940c-7faf15b2aeb0', 'admin', '******', '2018-04-18 10:27:43', '59.174.113.164', '1', '查看基本配置；查看SMTP配置；查看附件配置；查看Robots.txt;查看后台事件列表（keyword:;page:0;limit:20）;查看基本配置；', '2018-04-18 10:32:29');
INSERT INTO `adminlog` VALUES ('25', '0', '1a7dbe72-653c-4f3e-a11f-07f3e384e374', 'admin', '******', '2018-04-18 11:13:45', '125.113.143.165', '1', null, '2018-04-18 11:13:45');
INSERT INTO `adminlog` VALUES ('26', '0', '1efdb7b8-674b-4490-9466-8cf56adedb90', 'admin', '******', '2018-04-18 13:45:03', '171.221.146.125', '1', null, '2018-04-18 13:45:03');
INSERT INTO `adminlog` VALUES ('27', '0', '8d7e7655-93d1-4e1c-9276-0c00ab5e0558', 'admin', '******', '2018-04-18 18:06:51', '113.109.108.136', '1', '查看文章栏目列表页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看文章栏目列表页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看文章栏目列表页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；添加文章栏目(id:1);查看文章栏目列表页面；查看/编辑文章栏目（id:1）页面；修改文章栏目(id:1);查看文章栏目列表页面；查看/编辑文章栏目（id:1）页面；查看文章栏目列表页面；查看/编辑文章栏目（id:1）页面；查看文章列表;查看添加文章页面;查看文章列表;查看添加文章页面;添加文章(id:1);查看文章(1);编辑文章(id:1);查看附件配置；修改附件配置;查看附件配置；查看文章(1);查看文章栏目列表页面；', '2018-04-18 20:37:31');
INSERT INTO `adminlog` VALUES ('28', '0', 'e3217152-574c-4b7d-b8a6-6579333587fd', 'admin', '******', '2018-11-15 00:44:56', '127.0.0.1', '1', '|||2018-11-15 00:45: 查看文章栏目列表页面；|||2018-11-15 00:45: 查看文章列表;|||2018-11-15 00:45: 查看文章(1);|||2018-11-15 00:45: 查看文章(1);|||2018-11-15 00:45: 查看文章(1);|||2018-11-15 00:45: 查看文章(1);', '2018-11-15 00:45:59');
INSERT INTO `adminlog` VALUES ('29', '0', '6b4d37d3-eb88-4c32-acdf-894632b031b6', 'admin', '******', '2018-11-15 23:43:02', '127.0.0.1', '1', '|||2018-11-15 23:43: 查看基本配置；|||2018-11-15 23:43: 查看文章栏目列表页面；|||2018-11-15 23:44: 查看添加文章栏目页面；|||2018-11-15 23:44: 添加文章栏目(id:2);|||2018-11-15 23:44: 查看文章栏目列表页面；|||2018-11-15 23:44: 查看添加文章栏目页面；|||2018-11-15 23:47: 添加文章栏目(id:3);|||2018-11-15 23:47: 查看文章栏目列表页面；|||2018-11-15 23:47: 查看添加文章栏目页面；|||2018-11-15 23:47: 添加文章栏目(id:4);|||2018-11-15 23:47: 查看文章栏目列表页面；|||2018-11-15 23:47: 查看添加文章栏目页面；|||2018-11-15 23:48: 添加文章栏目(id:5);|||2018-11-15 23:48: 查看文章栏目列表页面；|||2018-11-15 23:48: 查看商品栏目列表页面；|||2018-11-15 23:48: 查看添加商品栏目页面；|||2018-11-15 23:48: 查看添加商品栏目页面；|||2018-11-15 23:48: 添加商品栏目(id:1);|||2018-11-15 23:48: 查看商品栏目列表页面；|||2018-11-15 23:48: 查看添加商品栏目页面；|||2018-11-15 23:49: 添加商品栏目(id:2);|||2018-11-15 23:49: 查看商品栏目列表页面；|||2018-11-15 23:49: 查看添加商品栏目页面；|||2018-11-15 23:49: 添加商品栏目(id:3);|||2018-11-15 23:49: 查看商品栏目列表页面；|||2018-11-15 23:58: 查看文章列表;|||2018-11-15 23:58: 查看添加文章页面;|||2018-11-15 23:58: 添加文章(id:2);|||2018-11-16 00:42: 修改站点基本配置；|||2018-11-16 00:42: 查看基本配置；', '2018-11-16 00:42:24');

-- ----------------------------
-- Table structure for adminmenu
-- ----------------------------
DROP TABLE IF EXISTS `adminmenu`;
CREATE TABLE `adminmenu` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `MenuKey` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '标识key',
  `MenuName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '页面名称',
  `PermissionKey` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '页面名称',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '介绍',
  `Link` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '页面连接地址',
  `PId` int(11) NOT NULL COMMENT '上级ID',
  `Level` int(11) NOT NULL COMMENT '级别',
  `Location` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '路径',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '样式名称',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=39 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='后台菜单';

-- ----------------------------
-- Records of adminmenu
-- ----------------------------
INSERT INTO `adminmenu` VALUES ('1', 'home', '主页', null, null, 'Index/Main', '0', '0', '0,', '0', '0', null, 'fa-home');
INSERT INTO `adminmenu` VALUES ('2', 'system', '系统设置', null, null, '#', '0', '0', '0,', '0', '1', null, 'fa-gears');
INSERT INTO `adminmenu` VALUES ('3', 'baseconfig', '基本配置', null, null, 'System/BaseConfig', '2', '1', '0,2,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('4', 'smptconfig', 'SMTP设置', null, null, 'System/SmtpConfig', '2', '1', '0,2,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('5', 'attachconfig', '附件设置', null, null, 'System/AttachConfig', '2', '1', '0,2,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('6', 'articlesys', '文章系统', null, null, '#', '0', '0', '0,', '0', '2', null, 'fa-book');
INSERT INTO `adminmenu` VALUES ('7', 'articlecategory', '文章栏目管理', null, null, 'Article/ArticleCategoryList', '6', '1', '0,6,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('8', 'article', '文章管理', null, null, 'Article/ArticleList', '6', '1', '0,6,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('9', 'productsys', '商品系统', null, null, '#', '0', '0', '0,', '0', '3', null, 'fa-balance-scale');
INSERT INTO `adminmenu` VALUES ('10', 'productcategory', '商品分类管理', null, null, 'Product/CategoryList', '9', '1', '0,9,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('11', 'product', '商品管理', null, null, 'Product/ProductList', '9', '1', '0,9,', '0', '1', null, null);
INSERT INTO `adminmenu` VALUES ('12', 'ordersys', '订单系统', null, null, '#', '0', '0', '0,', '0', '4', null, 'fa-shopping-bag');
INSERT INTO `adminmenu` VALUES ('13', 'order', '商品订单管理', null, null, 'Order/OrderList', '12', '1', '0,12,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('14', 'payonline', '支付记录', null, null, 'Order/PayOnlineList', '12', '1', '0,12,', '0', '1', null, null);
INSERT INTO `adminmenu` VALUES ('15', 'user', '用户系统', null, null, '#', '0', '0', '0,', '0', '5', null, 'fa-user');
INSERT INTO `adminmenu` VALUES ('16', 'memberrole', '用户组管理', null, null, 'Member/MemberRole', '15', '1', '0,15,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('17', 'members', '用户管理', null, null, 'Member/Members', '15', '1', '0,15,', '0', '1', null, null);
INSERT INTO `adminmenu` VALUES ('18', 'permissionsys', '后台权限', null, null, '#', '0', '0', '0,', '0', '6', null, 'fa-users');
INSERT INTO `adminmenu` VALUES ('19', 'adminrole', '管理组管理', null, null, 'Member/AdminRole', '18', '1', '0,18,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('20', 'admins', '管理员管理', null, null, 'Member/Admins', '18', '1', '0,18,', '0', '1', null, null);
INSERT INTO `adminmenu` VALUES ('21', 'eventkey', '事件权限管理', null, null, 'Permission/EventKey', '18', '1', '0,18,', '0', '3', null, null);
INSERT INTO `adminmenu` VALUES ('22', 'adminmenu', '后台栏目管理', null, null, 'Permission/AdminMenuList', '18', '1', '0,18,', '0', '4', null, null);
INSERT INTO `adminmenu` VALUES ('23', 'editme', ' 修改密码', null, null, 'Member/EditMe', '18', '1', '0,18,', '0', '5', null, null);
INSERT INTO `adminmenu` VALUES ('24', 'guestbooksys', '留言系统', null, null, '#', '0', '0', '0,', '0', '7', null, 'fa-rss-square');
INSERT INTO `adminmenu` VALUES ('25', 'guestbookkinds', '留言分类管理', null, null, 'Guestbook/GuestbookCategorys', '24', '1', '0,24,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('26', 'guestbook', '留言管理', null, null, 'Guestbook/GuestbookList', '24', '1', '0,24,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('27', 'other', '其他', null, null, '#', '0', '0', '0,', '0', '99', null, 'fa-square-o');
INSERT INTO `adminmenu` VALUES ('28', 'adskinds', '广告分类管理', null, null, 'Other/AdsCategoryList', '27', '1', '0,27,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('29', 'ads', '广告管理', null, null, 'Other/AdsList', '27', '1', '0,27,', '0', '1', null, null);
INSERT INTO `adminmenu` VALUES ('30', 'linkkinds', '友情连接分类管理', null, null, 'Other/LinkCategoryList', '27', '1', '0,27,', '0', '2', null, null);
INSERT INTO `adminmenu` VALUES ('31', 'link', '友情连接管理', null, null, 'Other/LinkList', '27', '1', '0,27,', '0', '3', null, null);
INSERT INTO `adminmenu` VALUES ('32', 'weixinsys', '微信公众号管理', null, null, '#', '0', '0', '0,', '0', '8', null, 'fa-file-word-o');
INSERT INTO `adminmenu` VALUES ('33', 'wxautoreply', '关注自动回复', null, null, 'Weixin/SubscribeReply', '32', '1', '0,32,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('34', 'wxmenu', '自定义菜单管理', null, null, 'Weixin/Menu', '32', '1', '0,32,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('35', 'wxkeywordreply', '关键字回复', null, null, 'Weixin/ReplyRule', '32', '1', '0,32,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('36', 'clickreplyrule', '点击事件自动回复', null, null, 'Weixin/ClickReplyRule', '32', '1', '0,32,', '0', '0', null, null);
INSERT INTO `adminmenu` VALUES ('37', 'robots', 'Robots文档设置', null, null, 'System/Robots', '2', '1', '0,2,', '0', '3', null, null);
INSERT INTO `adminmenu` VALUES ('38', 'admincplog', '后台管理日志', null, null, 'Member/AdminCPLogList', '18', '1', '0,18,', '0', '10', null, null);

-- ----------------------------
-- Table structure for adminmenuevent
-- ----------------------------
DROP TABLE IF EXISTS `adminmenuevent`;
CREATE TABLE `adminmenuevent` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `MenuId` int(11) NOT NULL COMMENT '菜单ID',
  `MenuKey` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '菜单key',
  `EventId` int(11) NOT NULL COMMENT '事件ID',
  `EventKey` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '事件key',
  `EventName` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '事件名称',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=192 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='后台菜单对应的事件权限';

-- ----------------------------
-- Records of adminmenuevent
-- ----------------------------
INSERT INTO `adminmenuevent` VALUES ('1', '1', 'home', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('2', '3', 'baseconfig', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('3', '3', 'baseconfig', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('4', '4', 'smptconfig', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('5', '4', 'smptconfig', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('6', '5', 'attachconfig', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('7', '5', 'attachconfig', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('8', '7', 'articlecategory', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('9', '7', 'articlecategory', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('10', '7', 'articlecategory', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('11', '7', 'articlecategory', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('12', '7', 'articlecategory', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('13', '7', 'articlecategory', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('14', '8', 'article', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('15', '8', 'article', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('16', '8', 'article', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('17', '8', 'article', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('18', '8', 'article', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('19', '8', 'article', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('20', '8', 'article', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('21', '8', 'article', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('22', '8', 'article', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('23', '8', 'article', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('24', '8', 'article', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('25', '10', 'productcategory', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('26', '10', 'productcategory', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('27', '10', 'productcategory', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('28', '10', 'productcategory', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('29', '10', 'productcategory', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('30', '10', 'productcategory', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('31', '11', 'product', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('32', '11', 'product', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('33', '11', 'product', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('34', '11', 'product', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('35', '11', 'product', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('36', '11', 'product', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('37', '11', 'product', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('38', '11', 'product', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('39', '11', 'product', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('40', '11', 'product', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('41', '11', 'product', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('42', '13', 'order', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('43', '13', 'order', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('44', '13', 'order', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('45', '13', 'order', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('46', '13', 'order', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('47', '13', 'order', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('48', '13', 'order', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('49', '13', 'order', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('50', '13', 'order', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('51', '13', 'order', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('52', '13', 'order', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('53', '14', 'payonline', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('54', '14', 'payonline', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('55', '14', 'payonline', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('56', '14', 'payonline', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('57', '14', 'payonline', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('58', '14', 'payonline', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('59', '14', 'payonline', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('60', '14', 'payonline', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('61', '14', 'payonline', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('62', '14', 'payonline', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('63', '16', 'memberrole', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('64', '16', 'memberrole', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('65', '16', 'memberrole', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('66', '16', 'memberrole', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('67', '16', 'memberrole', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('68', '16', 'memberrole', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('69', '17', 'members', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('70', '17', 'members', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('71', '17', 'members', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('72', '17', 'members', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('73', '17', 'members', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('74', '17', 'members', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('75', '17', 'members', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('76', '17', 'members', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('77', '17', 'members', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('78', '17', 'members', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('79', '17', 'members', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('80', '19', 'adminrole', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('81', '19', 'adminrole', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('82', '19', 'adminrole', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('83', '19', 'adminrole', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('84', '19', 'adminrole', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('85', '19', 'adminrole', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('86', '19', 'adminrole', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('87', '20', 'admins', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('88', '20', 'admins', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('89', '20', 'admins', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('90', '20', 'admins', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('91', '20', 'admins', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('92', '20', 'admins', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('93', '20', 'admins', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('94', '20', 'admins', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('95', '20', 'admins', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('96', '20', 'admins', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('97', '20', 'admins', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('98', '21', 'eventkey', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('99', '21', 'eventkey', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('100', '21', 'eventkey', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('101', '21', 'eventkey', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('102', '21', 'eventkey', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('103', '21', 'eventkey', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('104', '21', 'eventkey', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('105', '22', 'adminmenu', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('106', '22', 'adminmenu', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('107', '22', 'adminmenu', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('108', '22', 'adminmenu', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('109', '22', 'adminmenu', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('110', '22', 'adminmenu', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('111', '22', 'adminmenu', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('112', '22', 'adminmenu', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('113', '22', 'adminmenu', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('114', '22', 'adminmenu', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('115', '22', 'adminmenu', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('116', '23', 'editme', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('117', '23', 'editme', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('118', '25', 'guestbookkinds', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('119', '25', 'guestbookkinds', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('120', '25', 'guestbookkinds', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('121', '25', 'guestbookkinds', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('122', '25', 'guestbookkinds', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('123', '25', 'guestbookkinds', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('124', '25', 'guestbookkinds', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('125', '25', 'guestbookkinds', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('126', '26', 'guestbook', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('127', '26', 'guestbook', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('128', '26', 'guestbook', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('129', '26', 'guestbook', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('130', '26', 'guestbook', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('131', '26', 'guestbook', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('132', '26', 'guestbook', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('133', '26', 'guestbook', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('134', '26', 'guestbook', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('135', '26', 'guestbook', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('136', '28', 'adskinds', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('137', '28', 'adskinds', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('138', '28', 'adskinds', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('139', '28', 'adskinds', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('140', '28', 'adskinds', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('141', '29', 'ads', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('142', '29', 'ads', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('143', '29', 'ads', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('144', '29', 'ads', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('145', '29', 'ads', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('146', '30', 'linkkinds', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('147', '30', 'linkkinds', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('148', '30', 'linkkinds', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('149', '30', 'linkkinds', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('150', '30', 'linkkinds', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('151', '31', 'link', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('152', '31', 'link', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('153', '31', 'link', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('154', '31', 'link', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('155', '31', 'link', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('156', '31', 'link', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('157', '31', 'link', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('158', '31', 'link', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('159', '31', 'link', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('160', '31', 'link', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('161', '31', 'link', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('162', '33', 'wxautoreply', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('163', '33', 'wxautoreply', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('164', '34', 'wxmenu', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('165', '34', 'wxmenu', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('166', '35', 'wxkeywordreply', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('167', '35', 'wxkeywordreply', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('168', '35', 'wxkeywordreply', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('169', '35', 'wxkeywordreply', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('170', '35', 'wxkeywordreply', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('171', '35', 'wxkeywordreply', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('172', '35', 'wxkeywordreply', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('173', '35', 'wxkeywordreply', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('174', '35', 'wxkeywordreply', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('175', '35', 'wxkeywordreply', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('176', '35', 'wxkeywordreply', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('177', '36', 'clickreplyrule', '1', 'add', '添加');
INSERT INTO `adminmenuevent` VALUES ('178', '36', 'clickreplyrule', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('179', '36', 'clickreplyrule', '3', 'del', '删除');
INSERT INTO `adminmenuevent` VALUES ('180', '36', 'clickreplyrule', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('181', '36', 'clickreplyrule', '5', 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES ('182', '36', 'clickreplyrule', '6', 'import', '导入');
INSERT INTO `adminmenuevent` VALUES ('183', '36', 'clickreplyrule', '7', 'export', '导出');
INSERT INTO `adminmenuevent` VALUES ('184', '36', 'clickreplyrule', '8', 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES ('185', '36', 'clickreplyrule', '9', 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES ('186', '36', 'clickreplyrule', '10', 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES ('187', '36', 'clickreplyrule', '11', 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES ('188', '37', 'robots', '2', 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES ('189', '37', 'robots', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('190', '38', 'admincplog', '4', 'view', '查看');
INSERT INTO `adminmenuevent` VALUES ('191', '38', 'admincplog', '5', 'viewlist', '查看列表');

-- ----------------------------
-- Table structure for adminroles
-- ----------------------------
DROP TABLE IF EXISTS `adminroles`;
CREATE TABLE `adminroles` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `RoleType` int(11) NOT NULL COMMENT '角色类型',
  `RoleName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '角色名称',
  `RoleDescription` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '角色简单介绍',
  `IsSuperAdmin` int(11) NOT NULL COMMENT '是否是超级管理员',
  `Stars` int(11) NOT NULL COMMENT '星级',
  `NotAllowDel` int(11) NOT NULL COMMENT '是否不允许删除',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Color` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '颜色',
  `Menus` text COMMENT '管理菜单',
  `Powers` text COMMENT '权限',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='管理角色';

-- ----------------------------
-- Records of adminroles
-- ----------------------------
INSERT INTO `adminroles` VALUES ('1', '0', '超级管理员', '系统超级管理员', '1', '5', '1', '0', '', '', '');

-- ----------------------------
-- Table structure for ads
-- ----------------------------
DROP TABLE IF EXISTS `ads`;
CREATE TABLE `ads` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `Title` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '广告标题',
  `Content` text COMMENT '广告详情JSON',
  `KId` int(11) NOT NULL COMMENT '分类ID',
  `TId` int(11) NOT NULL COMMENT '广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告',
  `StartTime` datetime DEFAULT NULL COMMENT '起始时间',
  `EndTime` datetime DEFAULT NULL COMMENT '结束时间',
  `IsDisabled` enum('N','Y') NOT NULL COMMENT '是否禁用广告',
  `Sequence` int(11) NOT NULL COMMENT '排序，默认999',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='广告详情';

-- ----------------------------
-- Records of ads
-- ----------------------------

-- ----------------------------
-- Table structure for adskind
-- ----------------------------
DROP TABLE IF EXISTS `adskind`;
CREATE TABLE `adskind` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '广告类别名称',
  `KindInfo` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '简单说明',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='广告类别';

-- ----------------------------
-- Records of adskind
-- ----------------------------

-- ----------------------------
-- Table structure for article
-- ----------------------------
DROP TABLE IF EXISTS `article`;
CREATE TABLE `article` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '栏目ID',
  `Title` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '标题',
  `SubTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '副标题',
  `Content` text COMMENT '内容',
  `Keyword` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '模板',
  `IsRecommend` int(11) NOT NULL COMMENT '是否推荐',
  `IsNew` int(11) NOT NULL COMMENT '是否最新',
  `IsBest` int(11) NOT NULL COMMENT '是否推荐',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsTop` int(11) NOT NULL COMMENT '是否置顶',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `Hits` int(11) NOT NULL COMMENT '点击数量',
  `Sequence` int(11) NOT NULL COMMENT '排序',
  `CommentCount` int(11) NOT NULL COMMENT '评论数量',
  `Icon` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'banner图片',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `Tags` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'TAG',
  `Origin` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '来源',
  `OriginURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '来源地址',
  `ItemImg` text COMMENT '更多图片',
  `AuthorId` int(11) NOT NULL COMMENT '添加管理员ID',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '时间',
  `Location` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '路径',
  `FilePath` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '存放目录',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='文章';

-- ----------------------------
-- Records of article
-- ----------------------------
INSERT INTO `article` VALUES ('1', '1', '关于我们', null, '<p>关于我们</p>\n', null, null, null, null, null, '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '999', '0', null, null, null, '/userfiles/images/2018/20180413145904(1).png', '0', null, null, null, '/userfiles/images/2018/20180413145904(1).png|||', '1', '2018-04-18 20:35:54', '2018-04-18 20:36:42', null, null);
INSERT INTO `article` VALUES ('2', '5', '联系我们', null, '<p>联系我们</p>\n', null, null, null, null, null, '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '999', '0', null, null, null, null, '0', null, null, null, '', '1', '2018-11-15 23:58:24', null, null, null);

-- ----------------------------
-- Table structure for articlecategory
-- ----------------------------
DROP TABLE IF EXISTS `articlecategory`;
CREATE TABLE `articlecategory` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '栏目名称',
  `SubTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '栏目副标题',
  `KindTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '栏目标题，填写则在浏览器替换此标题',
  `Keyword` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '模板',
  `DetailTemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '详情模板',
  `KindDomain` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别域名（保留）',
  `IsList` int(11) NOT NULL COMMENT '是否为列表页面',
  `PageSize` int(11) NOT NULL COMMENT '每页显示数量',
  `PId` int(11) NOT NULL COMMENT '上级ID',
  `Level` int(11) NOT NULL COMMENT '级别',
  `Location` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '路径',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsShowSubDetail` int(11) NOT NULL COMMENT '是否显示下级栏目内容',
  `CatalogId` int(11) NOT NULL COMMENT '模型ID',
  `Counts` int(11) NOT NULL COMMENT '详情数量，缓存',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'banner图片',
  `KindInfo` text COMMENT '栏目详细介绍',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='文章栏目';

-- ----------------------------
-- Records of articlecategory
-- ----------------------------
INSERT INTO `articlecategory` VALUES ('1', '关于我们', null, null, null, null, null, null, null, null, null, '1', '15', '0', '0', '0,', '0', '0', '0', '0', '0', '1', '0', '1', '0', null, null, null, null, null, '0');
INSERT INTO `articlecategory` VALUES ('2', '新闻中心', null, null, null, null, null, null, null, null, null, '1', '15', '0', '0', '0,', '0', '0', '0', '0', '0', '1', '0', '0', '0', null, null, null, null, null, '0');
INSERT INTO `articlecategory` VALUES ('3', '项目案例', null, null, null, null, null, null, null, null, null, '1', '15', '0', '0', '0,', '0', '0', '0', '0', '0', '1', '0', '0', '0', null, null, null, null, null, '0');
INSERT INTO `articlecategory` VALUES ('4', '下载中心', null, null, null, null, null, null, null, null, null, '1', '15', '0', '0', '0,', '0', '0', '0', '0', '0', '1', '0', '0', '0', null, null, null, null, null, '0');
INSERT INTO `articlecategory` VALUES ('5', '联系我们', null, null, null, null, null, null, null, null, null, '1', '15', '0', '0', '0,', '0', '0', '0', '0', '0', '1', '0', '1', '0', null, null, null, null, null, '0');

-- ----------------------------
-- Table structure for balancechangelog
-- ----------------------------
DROP TABLE IF EXISTS `balancechangelog`;
CREATE TABLE `balancechangelog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `AdminId` int(11) NOT NULL COMMENT '管理员ID',
  `UserName` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户名',
  `AddTime` datetime DEFAULT NULL COMMENT '时间',
  `IP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '登录IP',
  `Actions` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '记录',
  `Reward` decimal(10,0) NOT NULL COMMENT '总积分',
  `BeforChange` decimal(10,0) NOT NULL COMMENT '总积分',
  `AfterChange` decimal(10,0) NOT NULL COMMENT '总积分',
  `LogDetails` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '详细记录',
  `TypeId` int(11) NOT NULL COMMENT '类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='用户余额变化记录';

-- ----------------------------
-- Records of balancechangelog
-- ----------------------------

-- ----------------------------
-- Table structure for category
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '栏目名称',
  `SubTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '栏目副标题',
  `KindTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '栏目标题，填写则在浏览器替换此标题',
  `Keyword` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '模板',
  `DetailTemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '详情模板',
  `KindDomain` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别域名（保留）',
  `IsList` int(11) NOT NULL COMMENT '是否为列表页面',
  `PageSize` int(11) NOT NULL COMMENT '每页显示数量',
  `PId` int(11) NOT NULL COMMENT '上级ID',
  `Level` int(11) NOT NULL COMMENT '级别',
  `Location` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '路径',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsShowSubDetail` int(11) NOT NULL COMMENT '是否显示下级栏目内容',
  `CatalogId` int(11) NOT NULL COMMENT '模型ID',
  `Counts` int(11) NOT NULL COMMENT '详情数量，缓存',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'banner图片',
  `KindInfo` text COMMENT '栏目详细介绍',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='商品栏目';

-- ----------------------------
-- Records of category
-- ----------------------------
INSERT INTO `category` VALUES ('1', '网站系统', null, null, null, null, null, null, null, null, null, '0', '15', '0', '0', '0,', '0', '0', '0', '0', '0', '1', '0', '0', '0', null, null, null, null, null, '0');
INSERT INTO `category` VALUES ('2', '微商城系统', null, null, null, null, null, null, null, null, null, '0', '15', '0', '0', '0,', '0', '0', '0', '0', '0', '1', '0', '0', '0', null, null, null, null, null, '0');
INSERT INTO `category` VALUES ('3', '小程序系统', null, null, null, null, null, null, null, null, null, '0', '15', '0', '0', '0,', '0', '0', '0', '0', '0', '1', '0', '0', '0', null, null, null, null, null, '0');

-- ----------------------------
-- Table structure for config
-- ----------------------------
DROP TABLE IF EXISTS `config`;
CREATE TABLE `config` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `SiteName` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '站点名称',
  `SiteUrl` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '站点URL',
  `SiteLogo` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '站点LOGO',
  `ICP` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT 'ICP备案',
  `SiteEmail` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '联系我们Email',
  `SiteTel` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '网站电话',
  `Copyright` text COMMENT '版权所有',
  `IsCloseSite` enum('N','Y') NOT NULL COMMENT '是否关闭网站',
  `CloseReason` text COMMENT '关闭原因',
  `CountScript` text COMMENT '统计代码',
  `WeiboQRCode` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '微博二维码',
  `WinxinQRCode` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信二维码',
  `Keyword` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '关键字',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `IndexTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '首页标题',
  `IsRewrite` int(11) NOT NULL COMMENT '是否URL地址重写',
  `SearchMinTime` int(11) NOT NULL COMMENT '搜索最小时间间距 秒',
  `OnlineQQ` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '在线QQ',
  `OnlineSkype` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '在线Skype',
  `OnlineWangWang` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '在线阿里旺旺',
  `SkinName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '站点URL',
  `OfficialName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '公众号名称',
  `OfficialDecsription` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '公众号介绍',
  `OfficialOriginalId` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '公众号原始ID',
  `WexinAccount` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信名称',
  `Token` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT 'Token',
  `AppId` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT 'AppId',
  `AppSecret` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT 'AppSecret',
  `FllowTipPageURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '引导关注素材地址',
  `OfficialType` int(11) NOT NULL COMMENT '公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号',
  `EncodingAESKey` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'EncodingAESKey',
  `DEType` int(11) NOT NULL COMMENT '解密方式0,明文',
  `OfficialQRCode` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '公众号二维码地址',
  `OfficialImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '公众号头像',
  `LastUpdateTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `LastCacheTime` datetime DEFAULT NULL COMMENT '最后缓存时间',
  `MCHId` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信商家MCHId',
  `MCHKey` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信商家key',
  `DefaultFare` decimal(10,0) NOT NULL COMMENT '默认运费',
  `MaxFreeFare` decimal(10,0) NOT NULL COMMENT '最大免运费金额',
  `WXAppId` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信小程序AppId',
  `WXAppSecret` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信小程序AppSecret',
  `IsResetData` int(11) NOT NULL COMMENT '小程序首页是否显示清除数据按钮',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='系统配置';

-- ----------------------------
-- Records of config
-- ----------------------------
INSERT INTO `config` VALUES ('1', 'COMCMS', 'http://www.comcms.com', '/images/logo.png', '', null, null, null, 'N', null, null, null, null, null, null, null, '0', '0', null, null, null, null, null, null, null, null, null, null, null, null, '0', null, '0', null, null, '2018-11-16 00:42:22', '2018-11-16 00:42:22', null, null, '0', '0', null, null, '0');

-- ----------------------------
-- Table structure for coupon
-- ----------------------------
DROP TABLE IF EXISTS `coupon`;
CREATE TABLE `coupon` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `ItemNO` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '券号',
  `KId` int(11) NOT NULL COMMENT '类别，0默认没限制',
  `CouponType` int(11) NOT NULL COMMENT '优惠券类型，0 现金用券，1打折券',
  `DiscuountRates` decimal(10,0) NOT NULL COMMENT '打折率，只有是打折券才有用',
  `IsLimit` int(11) NOT NULL COMMENT '是否有类别限制，0 无限制；1 是类别限制，2是商品限制',
  `Price` decimal(10,0) NOT NULL COMMENT '面额',
  `NeedPrice` decimal(10,0) NOT NULL COMMENT '需要消费面额',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `StartTime` datetime DEFAULT NULL COMMENT '添加时间',
  `EndTime` datetime DEFAULT NULL COMMENT '添加时间',
  `TotalCount` int(11) NOT NULL COMMENT '最大领取数量',
  `TotalUseCount` int(11) NOT NULL COMMENT '已使用次数',
  `SpreadUId` int(11) NOT NULL COMMENT '推广员ID，可选',
  `UId` int(11) NOT NULL COMMENT '用户Id',
  `MyType` int(11) NOT NULL COMMENT '可使用类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='优惠券';

-- ----------------------------
-- Records of coupon
-- ----------------------------

-- ----------------------------
-- Table structure for couponkind
-- ----------------------------
DROP TABLE IF EXISTS `couponkind`;
CREATE TABLE `couponkind` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `IsLimit` int(11) NOT NULL COMMENT '是否有类别限制，0 无限制；1 是类别限制，2是商品限制',
  `KindName` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称',
  `CouponType` int(11) NOT NULL COMMENT '优惠券类型，0 现金用券，1打折券',
  `KIds` text COMMENT '类别按逗号分开',
  `PIds` text COMMENT '商品ID，按逗号分开',
  `KindNote` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别说明',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `MyType` int(11) NOT NULL COMMENT '可使用类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='优惠券分类';

-- ----------------------------
-- Records of couponkind
-- ----------------------------

-- ----------------------------
-- Table structure for couponuselog
-- ----------------------------
DROP TABLE IF EXISTS `couponuselog`;
CREATE TABLE `couponuselog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `CouponId` int(11) NOT NULL COMMENT '优惠券ID',
  `ItemNO` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '优惠券编号',
  `OrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单号',
  `OrderId` int(11) NOT NULL COMMENT '订单编号',
  `UserName` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户名',
  `Title` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单名称',
  `AddTime` datetime DEFAULT NULL COMMENT '时间',
  `IP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '登录IP',
  `Actions` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '记录详情',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='优惠券使用记录';

-- ----------------------------
-- Records of couponuselog
-- ----------------------------

-- ----------------------------
-- Table structure for food
-- ----------------------------
DROP TABLE IF EXISTS `food`;
CREATE TABLE `food` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '栏目ID',
  `BId` int(11) NOT NULL COMMENT '品牌ID',
  `ShopId` int(11) NOT NULL COMMENT '店铺ID',
  `CId` int(11) NOT NULL COMMENT '颜色ID',
  `SupportId` int(11) NOT NULL COMMENT '供货商ID',
  `Title` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '标题',
  `ItemNO` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '编号',
  `SubTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '副标题',
  `Unit` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品单位',
  `Spec` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品规格尺寸',
  `Color` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '颜色',
  `Weight` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '重量',
  `Price` decimal(10,0) NOT NULL COMMENT '价格',
  `MarketPrice` decimal(10,0) NOT NULL COMMENT '市场价格',
  `SpecialPrice` decimal(10,0) NOT NULL COMMENT '特价，如有特价，以特价为准',
  `Fare` decimal(10,0) NOT NULL COMMENT '运费',
  `Discount` decimal(10,0) NOT NULL COMMENT '折扣',
  `Material` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '材料',
  `Front` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '封面',
  `Credits` int(11) NOT NULL COMMENT '积分',
  `Stock` int(11) NOT NULL COMMENT '库存',
  `WarnStock` int(11) NOT NULL COMMENT '警告库存',
  `IsSubProduct` int(11) NOT NULL COMMENT '是否为子商品',
  `PPId` int(11) NOT NULL COMMENT '父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能',
  `Content` text COMMENT '内容',
  `Parameters` text COMMENT '商品参数',
  `Keyword` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '模板',
  `IsRecommend` int(11) NOT NULL COMMENT '是否推荐',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsTop` int(11) NOT NULL COMMENT '是否置顶',
  `IsBest` int(11) NOT NULL COMMENT '是否精华',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsNew` int(11) NOT NULL COMMENT '是否新品',
  `IsSpecial` int(11) NOT NULL COMMENT '是否特价',
  `IsPromote` int(11) NOT NULL COMMENT '是否促销',
  `IsHotSales` int(11) NOT NULL COMMENT '是否热销',
  `IsBreakup` int(11) NOT NULL COMMENT '是否缺货',
  `IsShelves` int(11) NOT NULL COMMENT '是否下架',
  `IsVerify` int(11) NOT NULL COMMENT '是否审核，1为已经审核前台显示',
  `Hits` int(11) NOT NULL COMMENT '点击数量',
  `IsGift` int(11) NOT NULL COMMENT '是否为礼品商品',
  `IsPart` int(11) NOT NULL COMMENT '是否为配件',
  `Sequence` int(11) NOT NULL COMMENT '排序',
  `CommentCount` int(11) NOT NULL COMMENT '评论数量',
  `Icon` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'banner图片',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `Tags` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'TAG',
  `ItemImg` text COMMENT '更多图片',
  `Service` text COMMENT '售后服务',
  `AuthorId` int(11) NOT NULL COMMENT '添加管理员ID',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '时间',
  `Location` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '路径',
  `FilePath` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '存放目录',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='快餐';

-- ----------------------------
-- Records of food
-- ----------------------------

-- ----------------------------
-- Table structure for guestbook
-- ----------------------------
DROP TABLE IF EXISTS `guestbook`;
CREATE TABLE `guestbook` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '分类ID',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `Title` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '留言标题',
  `Content` text COMMENT '详细介绍',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `IsVerify` int(11) NOT NULL COMMENT '是否审核通过',
  `IsRead` int(11) NOT NULL COMMENT '是否阅读',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户IP',
  `Nickname` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '昵称',
  `Email` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮箱',
  `Tel` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '电话',
  `QQ` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT 'QQ',
  `Skype` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT 'Skype',
  `HomePage` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '主页',
  `Address` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '地址',
  `Company` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '公司',
  `ReplyTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '回复标题',
  `ReplyContent` text COMMENT '回复的详情',
  `ReplyAddTime` datetime DEFAULT NULL COMMENT '回复时间',
  `ReplyIP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户回复IP',
  `ReplyAdminId` int(11) NOT NULL COMMENT '用户的管理员ID',
  `ReplyNickName` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '回复者昵称',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='留言详情';

-- ----------------------------
-- Records of guestbook
-- ----------------------------

-- ----------------------------
-- Table structure for guestbookcategory
-- ----------------------------
DROP TABLE IF EXISTS `guestbookcategory`;
CREATE TABLE `guestbookcategory` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称',
  `KindInfo` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '简单说明',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '分类图片',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='留言分类';

-- ----------------------------
-- Records of guestbookcategory
-- ----------------------------

-- ----------------------------
-- Table structure for hotelroom
-- ----------------------------
DROP TABLE IF EXISTS `hotelroom`;
CREATE TABLE `hotelroom` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '栏目ID',
  `BId` int(11) NOT NULL COMMENT '品牌ID',
  `ShopId` int(11) NOT NULL COMMENT '店铺ID',
  `CId` int(11) NOT NULL COMMENT '颜色ID',
  `SupportId` int(11) NOT NULL COMMENT '供货商ID',
  `Title` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '标题',
  `ItemNO` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '编号',
  `SubTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '副标题',
  `Unit` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品单位',
  `Spec` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品规格尺寸',
  `Color` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '颜色',
  `Weight` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '重量',
  `Price` decimal(10,0) NOT NULL COMMENT '价格',
  `MarketPrice` decimal(10,0) NOT NULL COMMENT '市场价格',
  `SpecialPrice` decimal(10,0) NOT NULL COMMENT '特价，如有特价，以特价为准',
  `Fare` decimal(10,0) NOT NULL COMMENT '运费',
  `Discount` decimal(10,0) NOT NULL COMMENT '折扣',
  `Material` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '材料',
  `Front` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '封面',
  `Credits` int(11) NOT NULL COMMENT '积分',
  `Stock` int(11) NOT NULL COMMENT '库存',
  `WarnStock` int(11) NOT NULL COMMENT '警告库存',
  `IsSubProduct` int(11) NOT NULL COMMENT '是否为子商品',
  `PPId` int(11) NOT NULL COMMENT '父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能',
  `Content` text COMMENT '内容',
  `Parameters` text COMMENT '商品参数',
  `Keyword` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '模板',
  `IsRecommend` int(11) NOT NULL COMMENT '是否推荐',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsTop` int(11) NOT NULL COMMENT '是否置顶',
  `IsBest` int(11) NOT NULL COMMENT '是否精华',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsNew` int(11) NOT NULL COMMENT '是否新品',
  `IsSpecial` int(11) NOT NULL COMMENT '是否特价',
  `IsPromote` int(11) NOT NULL COMMENT '是否促销',
  `IsHotSales` int(11) NOT NULL COMMENT '是否热销',
  `IsBreakup` int(11) NOT NULL COMMENT '是否缺货',
  `IsShelves` int(11) NOT NULL COMMENT '是否下架',
  `IsVerify` int(11) NOT NULL COMMENT '是否审核，1为已经审核前台显示',
  `Hits` int(11) NOT NULL COMMENT '点击数量',
  `IsGift` int(11) NOT NULL COMMENT '是否为礼品商品',
  `IsPart` int(11) NOT NULL COMMENT '是否为配件',
  `Sequence` int(11) NOT NULL COMMENT '排序',
  `CommentCount` int(11) NOT NULL COMMENT '评论数量',
  `Icon` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'banner图片',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `Tags` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'TAG',
  `ItemImg` text COMMENT '更多图片',
  `Service` text COMMENT '售后服务',
  `AuthorId` int(11) NOT NULL COMMENT '添加管理员ID',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '时间',
  `Location` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '路径',
  `FilePath` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '存放目录',
  `PriceList` text COMMENT '日期价格',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='酒店房间';

-- ----------------------------
-- Records of hotelroom
-- ----------------------------

-- ----------------------------
-- Table structure for link
-- ----------------------------
DROP TABLE IF EXISTS `link`;
CREATE TABLE `link` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '分类ID',
  `Title` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '站点标题',
  `LinkURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '站点连接',
  `Info` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '介绍',
  `Logo` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '站点LOGO',
  `IsHide` enum('N','Y') NOT NULL COMMENT '是否隐藏友情链接',
  `Sequence` int(11) NOT NULL COMMENT '排序，默认999',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='友情连接详情';

-- ----------------------------
-- Records of link
-- ----------------------------

-- ----------------------------
-- Table structure for linkkind
-- ----------------------------
DROP TABLE IF EXISTS `linkkind`;
CREATE TABLE `linkkind` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称',
  `KindInfo` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '简单说明',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '分类图片',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='友情链接分类';

-- ----------------------------
-- Records of linkkind
-- ----------------------------

-- ----------------------------
-- Table structure for member
-- ----------------------------
DROP TABLE IF EXISTS `member`;
CREATE TABLE `member` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UserName` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户名',
  `PassWord` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '密码',
  `Salt` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '盐值',
  `RealName` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '姓名',
  `Tel` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '手机',
  `Email` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮件',
  `RoleId` int(11) NOT NULL COMMENT '会员组，代理级别',
  `LastLoginTime` datetime DEFAULT NULL COMMENT '最后登录时间',
  `LastLoginIP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '上次登录IP',
  `ThisLoginTime` datetime DEFAULT NULL COMMENT '本次登录时间',
  `ThisLoginIP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '本次登录IP',
  `IsLock` int(11) NOT NULL COMMENT '是否是锁定',
  `Nickname` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '昵称',
  `UserImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '头像',
  `Sex` int(11) NOT NULL COMMENT '性别 0 保密 1 男 2女',
  `Birthday` datetime DEFAULT NULL COMMENT '生日',
  `Phone` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '电话',
  `Fax` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '传真',
  `QQ` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT 'QQ',
  `Weixin` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信',
  `Alipay` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '支付宝',
  `Skype` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT 'skype',
  `Homepage` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '主页',
  `Company` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '公司',
  `IDNO` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '身份证',
  `Country` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '国家',
  `Province` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '省',
  `City` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '市',
  `District` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '区',
  `Address` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '详细地址',
  `Postcode` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮政编码',
  `RegIP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '注册IP',
  `RegTime` datetime DEFAULT NULL COMMENT '注册时间',
  `LoginCount` int(11) NOT NULL COMMENT '登录次数',
  `RndNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '随机字符',
  `RePassWordTime` datetime DEFAULT NULL COMMENT '找回密码时间',
  `Question` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '保密问题',
  `Answer` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '保密答案',
  `Balance` decimal(10,0) NOT NULL COMMENT '可用余额',
  `GiftBalance` decimal(10,0) NOT NULL COMMENT '赠送余额',
  `Rebate` decimal(10,0) NOT NULL COMMENT '返利，分销提成',
  `Bank` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '银行',
  `BankCardNO` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '银行卡号',
  `BankBranch` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '支行名称',
  `BankRealname` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '银行卡姓名',
  `YearsPerformance` decimal(10,0) NOT NULL COMMENT '年业务量',
  `ExtCredits1` decimal(10,0) NOT NULL COMMENT '备用积分1',
  `ExtCredits2` decimal(10,0) NOT NULL COMMENT '备用积分2',
  `ExtCredits3` decimal(10,0) NOT NULL COMMENT '备用积分3',
  `ExtCredits4` decimal(10,0) NOT NULL COMMENT '备用积分4',
  `ExtCredits5` decimal(10,0) NOT NULL COMMENT '备用积分5',
  `TotalCredits` decimal(10,0) NOT NULL COMMENT '总积分',
  `Parent` int(11) NOT NULL COMMENT '父级用户ID',
  `Grandfather` int(11) NOT NULL COMMENT '爷级用户ID',
  `IsSellers` int(11) NOT NULL COMMENT '是否是分销商',
  `IsVerifySellers` int(11) NOT NULL COMMENT '是否已经认证的分销商，缴纳费用',
  `WeixinOpenId` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信公众号OpenId',
  `WeixinAppOpenId` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信OpenId',
  `QQOpenId` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT 'QQ OpenId',
  `WeiboOpenId` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '微博OpenId',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='用户';

-- ----------------------------
-- Records of member
-- ----------------------------

-- ----------------------------
-- Table structure for memberaddress
-- ----------------------------
DROP TABLE IF EXISTS `memberaddress`;
CREATE TABLE `memberaddress` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `RealName` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '姓名',
  `Tel` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '手机',
  `Email` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮件',
  `IsDefault` enum('N','Y') NOT NULL COMMENT '是否是默认',
  `Phone` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '电话',
  `Company` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '公司',
  `Country` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '国家',
  `Province` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '省',
  `City` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '市',
  `District` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '区',
  `Address` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '详细地址',
  `Postcode` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮政编码',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '更新时间',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='用户地址';

-- ----------------------------
-- Records of memberaddress
-- ----------------------------

-- ----------------------------
-- Table structure for membercoupon
-- ----------------------------
DROP TABLE IF EXISTS `membercoupon`;
CREATE TABLE `membercoupon` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `ItemNO` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '券号',
  `CouponId` int(11) NOT NULL COMMENT '优惠券ID',
  `CouponType` int(11) NOT NULL COMMENT '优惠券类型，0 现金用券，1打折券',
  `DiscuountRates` decimal(10,0) NOT NULL COMMENT '打折率，只有是打折券才有用',
  `IsLimit` int(11) NOT NULL COMMENT '是否有类别限制，0 无限制；1 是类别限制，2是商品限制',
  `Price` decimal(10,0) NOT NULL COMMENT '面额',
  `NeedPrice` decimal(10,0) NOT NULL COMMENT '需要消费面额',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `StartTime` datetime DEFAULT NULL COMMENT '添加时间',
  `EndTime` datetime DEFAULT NULL COMMENT '添加时间',
  `IsUse` int(11) NOT NULL COMMENT '是否使用',
  `UseTime` datetime DEFAULT NULL COMMENT '使用时间',
  `OrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单号（使用后）',
  `OrderId` int(11) NOT NULL COMMENT '订单编号（使用后）',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='用户优惠券';

-- ----------------------------
-- Records of membercoupon
-- ----------------------------

-- ----------------------------
-- Table structure for memberlog
-- ----------------------------
DROP TABLE IF EXISTS `memberlog`;
CREATE TABLE `memberlog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '管理员ID',
  `GUID` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '唯一ID',
  `UserName` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户名',
  `PassWord` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '密码',
  `LoginTime` datetime DEFAULT NULL COMMENT '登录时间',
  `LoginIP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '登录IP',
  `IsLoginOK` int(11) NOT NULL COMMENT '是否登录成功',
  `Actions` text COMMENT '记录',
  `LastUpdateTime` datetime DEFAULT NULL COMMENT '登录时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='用户日志表';

-- ----------------------------
-- Records of memberlog
-- ----------------------------

-- ----------------------------
-- Table structure for memberroles
-- ----------------------------
DROP TABLE IF EXISTS `memberroles`;
CREATE TABLE `memberroles` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `RoleName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '角色名称',
  `RoleDescription` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '角色简单介绍',
  `Stars` int(11) NOT NULL COMMENT '星级',
  `NotAllowDel` int(11) NOT NULL COMMENT '是否不允许删除',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Color` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '颜色',
  `CashBack` decimal(10,0) NOT NULL COMMENT '返现百分比',
  `ParentCashBack` decimal(10,0) NOT NULL COMMENT '父级返现百分比',
  `GrandfatherCashBack` decimal(10,0) NOT NULL COMMENT '爷级返现百分比',
  `IsDefault` int(11) NOT NULL COMMENT '是否是默认角色',
  `IsHalved` int(11) NOT NULL COMMENT '超过级别是否减半',
  `HalvedParentCashBack` decimal(10,0) NOT NULL COMMENT '超过级别父级返现百分比',
  `HalvedGrandfatherCashBack` decimal(10,0) NOT NULL COMMENT '超过级别爷级返现百分比',
  `YearsPerformance` decimal(10,0) NOT NULL COMMENT '年业务量',
  `IsSellers` int(11) NOT NULL COMMENT '是否是分销商',
  `JoinPrice` decimal(10,0) NOT NULL COMMENT '加入分销商价格',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='用户角色';

-- ----------------------------
-- Records of memberroles
-- ----------------------------

-- ----------------------------
-- Table structure for onlinepayorder
-- ----------------------------
DROP TABLE IF EXISTS `onlinepayorder`;
CREATE TABLE `onlinepayorder` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `PayOrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '在线支付订单号，唯一',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单号',
  `PayId` int(11) NOT NULL COMMENT '支付方式ID',
  `PayType` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '付款方式',
  `PayTypeNotes` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '支付备注',
  `Actions` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '日志记录',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `UserName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户名',
  `TotalQty` int(11) NOT NULL COMMENT '总数量',
  `TotalPrice` decimal(10,0) NOT NULL COMMENT '总价格',
  `PaymentStatus` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '支付状态：未支付、已支付、已退款',
  `PaymentNotes` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '支付备注',
  `IsOK` int(11) NOT NULL COMMENT '是否完成',
  `IP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '下单IP',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `ReceiveTime` datetime DEFAULT NULL COMMENT '回传时间',
  `TypeId` int(11) NOT NULL COMMENT '支付的类型',
  `MyType` int(11) NOT NULL COMMENT '系统类型',
  `Title` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单标题',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='在线支付订单';

-- ----------------------------
-- Records of onlinepayorder
-- ----------------------------

-- ----------------------------
-- Table structure for order
-- ----------------------------
DROP TABLE IF EXISTS `order`;
CREATE TABLE `order` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `OrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `UserName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '下单用户名',
  `ShopId` int(11) NOT NULL COMMENT '商户ID',
  `Credit` int(11) NOT NULL COMMENT '积分',
  `TotalQty` int(11) NOT NULL COMMENT '总数量',
  `TotalPrice` decimal(10,0) NOT NULL COMMENT '总价格',
  `Discount` decimal(10,0) NOT NULL COMMENT '折扣',
  `Fare` decimal(10,0) NOT NULL COMMENT '运费',
  `TotalTax` decimal(10,0) NOT NULL COMMENT '税',
  `TotalPay` decimal(10,0) NOT NULL COMMENT '总支付价格',
  `BackCredits` int(11) NOT NULL COMMENT '返现积分',
  `RealName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '姓名',
  `Country` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '国家',
  `Province` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '省份',
  `City` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '城市',
  `District` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '区',
  `Address` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '详细地址',
  `PostCode` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮编',
  `Tel` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '手机',
  `Mobile` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '手机',
  `Email` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮箱',
  `Notes` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '备注',
  `AdminNotes` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '管理员备注',
  `Pic` text COMMENT '图片',
  `DeliverId` int(11) NOT NULL COMMENT '配送方式ID',
  `DeliverType` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '配送方式名称',
  `DeliverNO` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '运单号',
  `DeliverNotes` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '配送备注',
  `PayId` int(11) NOT NULL COMMENT '支付方式ID',
  `PayType` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '付款方式',
  `PayTypeNotes` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '支付备注',
  `OrderStatus` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单状态：未确认、已确认、已完成、已取消',
  `PaymentStatus` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '支付状态：未支付、已支付、已退款',
  `DeliverStatus` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '配送状态：未配送、配货中、已配送、已收到、退货中、已退货',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `IP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '下单IP',
  `IsInvoice` int(11) NOT NULL COMMENT '是否需要发票',
  `InvoiceCompanyName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '抬头',
  `InvoiceCompanyID` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '纳税人识别号',
  `InvoiceType` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '发票内容',
  `InvoiceNote` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '发票备注',
  `IsRead` int(11) NOT NULL COMMENT '是否未读',
  `IsEnd` int(11) NOT NULL COMMENT '是否结束',
  `EndTime` datetime DEFAULT NULL COMMENT '结束时间',
  `IsOk` int(11) NOT NULL COMMENT '订单是否顺利完成',
  `IsComment` int(11) NOT NULL COMMENT '订单已经评论',
  `Flag1` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '预留字段1',
  `Flag2` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '预留字段2',
  `Flag3` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '预留字段3',
  `Title` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单名称',
  `LastModTime` datetime DEFAULT NULL COMMENT '最后操作时间',
  `OrderType` int(11) NOT NULL COMMENT '订单类型，0为商品订单',
  `MyType` int(11) NOT NULL COMMENT '系统类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='订单';

-- ----------------------------
-- Records of order
-- ----------------------------

-- ----------------------------
-- Table structure for orderdetail
-- ----------------------------
DROP TABLE IF EXISTS `orderdetail`;
CREATE TABLE `orderdetail` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单号',
  `ShopId` int(11) NOT NULL COMMENT '商户ID',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `PId` int(11) NOT NULL COMMENT '商品ID',
  `TypeId` int(11) NOT NULL COMMENT '类型ID',
  `PriceId` int(11) NOT NULL COMMENT '价格ID',
  `Title` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品名称',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品名称',
  `Attr` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品属性',
  `Color` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品颜色',
  `Spec` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '规格',
  `ItemNO` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '编号',
  `Qty` int(11) NOT NULL COMMENT '数量',
  `Price` decimal(10,0) NOT NULL COMMENT '商品价格',
  `MarketPrice` decimal(10,0) NOT NULL COMMENT '商品市场价格',
  `SpecialPrice` decimal(10,0) NOT NULL COMMENT '商品特价',
  `Discount` decimal(10,0) NOT NULL COMMENT '商品优惠',
  `Tax` decimal(10,0) NOT NULL COMMENT '商品税',
  `Credit` int(11) NOT NULL COMMENT '积分',
  `BackCredits` int(11) NOT NULL COMMENT '返现积分',
  `IsOK` int(11) NOT NULL COMMENT '是否完成',
  `IsComment` int(11) NOT NULL COMMENT '是否已经评论',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `CheckInDate` datetime DEFAULT NULL COMMENT '入住日期',
  `LeaveDate` datetime DEFAULT NULL COMMENT '离开日期',
  `MyType` int(11) NOT NULL COMMENT '系统类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='订单详情';

-- ----------------------------
-- Records of orderdetail
-- ----------------------------

-- ----------------------------
-- Table structure for orderlog
-- ----------------------------
DROP TABLE IF EXISTS `orderlog`;
CREATE TABLE `orderlog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单号',
  `Actions` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '日志记录',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='订单日志';

-- ----------------------------
-- Records of orderlog
-- ----------------------------

-- ----------------------------
-- Table structure for otherconfig
-- ----------------------------
DROP TABLE IF EXISTS `otherconfig`;
CREATE TABLE `otherconfig` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `ConfigName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '配置名称',
  `ConfigValue` text COMMENT '配置值 JSON',
  `LastUpdateTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='其他配置';

-- ----------------------------
-- Records of otherconfig
-- ----------------------------
INSERT INTO `otherconfig` VALUES ('1', 'attach', '{\"SiteId\":0,\"AttachPatch\":\"userfiles\",\"SaveType\":0,\"IsCreateThum\":0,\"ThumQty\":80,\"ThumMaxWidth\":200,\"ThumMaxHeight\":200,\"IsWaterMark\":0,\"WaterMarkType\":0,\"WaterMarkMinWidth\":400,\"WaterMarkMinHeight\":400,\"WaterMarkImg\":null,\"WaterMarkText\":\"\",\"WaterMarkTextColor\":\"#0FF\",\"WaterMarkPlace\":9,\"WaterMarkQty\":80,\"WaterMarkDiaphaneity\":80,\"IsRandomFileName\":1,\"ImgMaxWidth\":1920,\"ImgMaxHeight\":2000}', '2018-04-18 20:36:55');
INSERT INTO `otherconfig` VALUES ('2', 'smtp', '{\"SiteId\":0,\"SmtpEmail\":\"\",\"SmtpHost\":\"\",\"SmtpProt\":\"25\",\"SmtpEmailPwd\":\"\",\"PostUserName\":\"\"}', '2018-04-16 00:55:57');

-- ----------------------------
-- Table structure for product
-- ----------------------------
DROP TABLE IF EXISTS `product`;
CREATE TABLE `product` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '栏目ID',
  `BId` int(11) NOT NULL COMMENT '品牌ID',
  `ShopId` int(11) NOT NULL COMMENT '店铺ID',
  `CId` int(11) NOT NULL COMMENT '颜色ID',
  `SupportId` int(11) NOT NULL COMMENT '供货商ID',
  `Title` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '标题',
  `ItemNO` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '编号',
  `SubTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '副标题',
  `Unit` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品单位',
  `Spec` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '商品规格尺寸',
  `Color` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '颜色',
  `Weight` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '重量',
  `Price` decimal(10,0) NOT NULL COMMENT '价格',
  `MarketPrice` decimal(10,0) NOT NULL COMMENT '市场价格',
  `SpecialPrice` decimal(10,0) NOT NULL COMMENT '特价，如有特价，以特价为准',
  `Fare` decimal(10,0) NOT NULL COMMENT '运费',
  `Discount` decimal(10,0) NOT NULL COMMENT '折扣',
  `Material` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '材料',
  `Front` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '封面',
  `Credits` int(11) NOT NULL COMMENT '积分',
  `Stock` int(11) NOT NULL COMMENT '库存',
  `WarnStock` int(11) NOT NULL COMMENT '警告库存',
  `IsSubProduct` int(11) NOT NULL COMMENT '是否为子商品',
  `PPId` int(11) NOT NULL COMMENT '父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能',
  `Content` text COMMENT '内容',
  `Parameters` text COMMENT '商品参数',
  `Keyword` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '模板',
  `IsRecommend` int(11) NOT NULL COMMENT '是否推荐',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsTop` int(11) NOT NULL COMMENT '是否置顶',
  `IsBest` int(11) NOT NULL COMMENT '是否精华',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsNew` int(11) NOT NULL COMMENT '是否新品',
  `IsSpecial` int(11) NOT NULL COMMENT '是否特价',
  `IsPromote` int(11) NOT NULL COMMENT '是否促销',
  `IsHotSales` int(11) NOT NULL COMMENT '是否热销',
  `IsBreakup` int(11) NOT NULL COMMENT '是否缺货',
  `IsShelves` int(11) NOT NULL COMMENT '是否下架',
  `IsVerify` int(11) NOT NULL COMMENT '是否审核，1为已经审核前台显示',
  `Hits` int(11) NOT NULL COMMENT '点击数量',
  `IsGift` int(11) NOT NULL COMMENT '是否为礼品商品',
  `IsPart` int(11) NOT NULL COMMENT '是否为配件',
  `Sequence` int(11) NOT NULL COMMENT '排序',
  `CommentCount` int(11) NOT NULL COMMENT '评论数量',
  `Icon` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'banner图片',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `Tags` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'TAG',
  `ItemImg` text COMMENT '更多图片',
  `Service` text COMMENT '售后服务',
  `AuthorId` int(11) NOT NULL COMMENT '添加管理员ID',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '时间',
  `Location` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '路径',
  `FilePath` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '存放目录',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='商品';

-- ----------------------------
-- Records of product
-- ----------------------------

-- ----------------------------
-- Table structure for rebatechangelog
-- ----------------------------
DROP TABLE IF EXISTS `rebatechangelog`;
CREATE TABLE `rebatechangelog` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `AdminId` int(11) NOT NULL COMMENT '管理员ID',
  `UserName` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户名',
  `AddTime` datetime DEFAULT NULL COMMENT '时间',
  `IP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '登录IP',
  `Actions` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '记录',
  `Reward` decimal(10,0) NOT NULL COMMENT '返现、扣除金额',
  `BeforChange` decimal(10,0) NOT NULL COMMENT '变化前',
  `AfterChange` decimal(10,0) NOT NULL COMMENT '变化后',
  `LogDetails` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '详细记录',
  `TypeId` int(11) NOT NULL COMMENT '类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成',
  `MyType` int(11) NOT NULL COMMENT '消费类型，见MyType',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='用户返现余额变化记录';

-- ----------------------------
-- Records of rebatechangelog
-- ----------------------------

-- ----------------------------
-- Table structure for shop
-- ----------------------------
DROP TABLE IF EXISTS `shop`;
CREATE TABLE `shop` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `ShopName` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '店铺名称',
  `KId` int(11) NOT NULL COMMENT '商家分类ID',
  `AId` int(11) NOT NULL COMMENT '地区ID',
  `Sequence` int(11) NOT NULL COMMENT '排序',
  `Latitude` decimal(10,0) NOT NULL COMMENT '纬度',
  `Longitude` decimal(10,0) NOT NULL COMMENT '经度',
  `Country` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '国家',
  `Province` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '省',
  `City` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '市',
  `District` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '区',
  `Address` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '详细地址',
  `Postcode` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮政编码',
  `IsDel` int(11) NOT NULL COMMENT '是否删除',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsDisabled` int(11) NOT NULL COMMENT '是否禁用',
  `Content` text COMMENT '店铺介绍',
  `Keyword` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '关键字',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `BannerImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'banner图片',
  `Balance` decimal(10,0) NOT NULL COMMENT '余额',
  `IsTop` int(11) NOT NULL COMMENT '是否置顶',
  `IsVip` int(11) NOT NULL COMMENT '是否vip',
  `IsRecommend` int(11) NOT NULL COMMENT '是否推荐',
  `Likes` int(11) NOT NULL COMMENT '点赞数',
  `AvgScore` decimal(10,0) NOT NULL COMMENT '平均分数',
  `ServiceScore` decimal(10,0) NOT NULL COMMENT '服务分数',
  `SpeedScore` decimal(10,0) NOT NULL COMMENT '速度分数',
  `EnvironmentScore` decimal(10,0) NOT NULL COMMENT '环境分数',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '图片',
  `MorePics` text COMMENT '店铺所有图片',
  `Email` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '邮箱',
  `Tel` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '电话',
  `Phone` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '固定电话',
  `QQ` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT 'QQ',
  `Skype` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT 'Skype',
  `HomePage` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '主页',
  `Weixin` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信',
  `IsShip` int(11) NOT NULL COMMENT '是否配送',
  `OpenTime` datetime DEFAULT NULL COMMENT '开店时间',
  `CloseTime` datetime DEFAULT NULL COMMENT '关店时间',
  `ShippingStartTime` datetime DEFAULT NULL COMMENT '配送开始时间',
  `ShippingEndTime` datetime DEFAULT NULL COMMENT '配送结束时间',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  `Hits` int(11) NOT NULL COMMENT '点击量',
  `MyType` int(11) NOT NULL COMMENT '店铺类型',
  `DefaultFare` decimal(10,0) NOT NULL COMMENT '默认运费',
  `MaxFreeFare` decimal(10,0) NOT NULL COMMENT '最大免运费金额',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='商家';

-- ----------------------------
-- Records of shop
-- ----------------------------

-- ----------------------------
-- Table structure for shopcategory
-- ----------------------------
DROP TABLE IF EXISTS `shopcategory`;
CREATE TABLE `shopcategory` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '栏目名称',
  `SubTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '栏目副标题',
  `KindTitle` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '栏目标题，填写则在浏览器替换此标题',
  `Keyword` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '模板',
  `DetailTemplateFile` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '详情模板',
  `KindDomain` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '类别域名（保留）',
  `IsList` int(11) NOT NULL COMMENT '是否为列表页面',
  `PageSize` int(11) NOT NULL COMMENT '每页显示数量',
  `PId` int(11) NOT NULL COMMENT '上级ID',
  `Level` int(11) NOT NULL COMMENT '级别',
  `Location` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '路径',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsShowSubDetail` int(11) NOT NULL COMMENT '是否显示下级栏目内容',
  `CatalogId` int(11) NOT NULL COMMENT '模型ID',
  `Counts` int(11) NOT NULL COMMENT '详情数量，缓存',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT 'banner图片',
  `KindInfo` text COMMENT '栏目详细介绍',
  `Pic` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='商家分类';

-- ----------------------------
-- Records of shopcategory
-- ----------------------------

-- ----------------------------
-- Table structure for shoppingcart
-- ----------------------------
DROP TABLE IF EXISTS `shoppingcart`;
CREATE TABLE `shoppingcart` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `GUID` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '唯一GUID，没登录用户使用',
  `Details` text COMMENT '购物车内容，JOSN',
  `AddTime` datetime DEFAULT NULL COMMENT '加入购物车时间',
  `UpdateTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='购物车';

-- ----------------------------
-- Records of shoppingcart
-- ----------------------------

-- ----------------------------
-- Table structure for targetevent
-- ----------------------------
DROP TABLE IF EXISTS `targetevent`;
CREATE TABLE `targetevent` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `EventKey` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '事件key',
  `EventName` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '事件名称',
  `IsDisable` int(11) NOT NULL COMMENT '是否禁用',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='目标事件';

-- ----------------------------
-- Records of targetevent
-- ----------------------------
INSERT INTO `targetevent` VALUES ('1', 'add', '添加', '0', '0');
INSERT INTO `targetevent` VALUES ('2', 'edit', '修改', '0', '0');
INSERT INTO `targetevent` VALUES ('3', 'del', '删除', '0', '0');
INSERT INTO `targetevent` VALUES ('4', 'view', '查看', '0', '0');
INSERT INTO `targetevent` VALUES ('5', 'viewlist', '查看列表', '0', '0');
INSERT INTO `targetevent` VALUES ('6', 'import', '导入', '0', '0');
INSERT INTO `targetevent` VALUES ('7', 'export', '导出', '0', '0');
INSERT INTO `targetevent` VALUES ('8', 'filter', '搜索', '0', '0');
INSERT INTO `targetevent` VALUES ('9', 'batch', '批量操作', '0', '0');
INSERT INTO `targetevent` VALUES ('10', 'recycle', '回收站', '0', '0');
INSERT INTO `targetevent` VALUES ('11', 'confirm', '确认', '0', '0');

-- ----------------------------
-- Table structure for withdraworder
-- ----------------------------
DROP TABLE IF EXISTS `withdraworder`;
CREATE TABLE `withdraworder` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单号',
  `UserName` varchar(200) CHARACTER SET utf8 DEFAULT NULL COMMENT '用户名',
  `Title` varchar(100) CHARACTER SET utf8 DEFAULT NULL COMMENT '订单名称',
  `AddTime` datetime DEFAULT NULL COMMENT '时间',
  `IP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '登录IP',
  `Actions` varchar(250) CHARACTER SET utf8 DEFAULT NULL COMMENT '记录详情',
  `Price` decimal(10,0) NOT NULL COMMENT '提现金额',
  `VerifyAdminId` int(11) NOT NULL COMMENT '审核管理员ID',
  `IsVerify` int(11) NOT NULL COMMENT '是否通过审核',
  `VerifyTime` datetime DEFAULT NULL COMMENT '审核时间',
  `FormId` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '小程序FormId',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='用户提现订单';

-- ----------------------------
-- Records of withdraworder
-- ----------------------------

-- ----------------------------
-- Table structure for wxappsession
-- ----------------------------
DROP TABLE IF EXISTS `wxappsession`;
CREATE TABLE `wxappsession` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `IP` varchar(20) CHARACTER SET utf8 DEFAULT NULL COMMENT '登录IP',
  `Key` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '系统生成Key',
  `SessionKey` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信SessionKey',
  `OpenId` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT '微信小程序OpenId',
  `AddTime` datetime DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 ROW_FORMAT=DYNAMIC COMMENT='小程序Session';

-- ----------------------------
-- Records of wxappsession
-- ----------------------------
