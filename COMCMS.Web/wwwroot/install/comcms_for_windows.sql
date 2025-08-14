/*
 Navicat Premium Data Transfer

 Source Server         : MySQL
 Source Server Type    : MySQL
 Source Server Version : 50718
 Source Host           : localhost:3306
 Source Schema         : comcms2

 Target Server Type    : MySQL
 Target Server Version : 50718
 File Encoding         : 65001

 Date: 20/03/2020 12:00:29
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for activities
-- ----------------------------
DROP TABLE IF EXISTS `activities`;
CREATE TABLE `activities`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目名称',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目副标题',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `Counts` int(11) NOT NULL COMMENT '参加次数',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `KindInfo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '栏目详细介绍',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `NeedCredits` decimal(19, 4) NOT NULL COMMENT '参加积分',
  `ActivityType` int(11) NOT NULL COMMENT '活动类型',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '活动' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of activities
-- ----------------------------
INSERT INTO `activities` VALUES (1, '抽奖活动', '抽奖大放送', NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 999, NULL, NULL, NULL, '/userfiles/images/2018/5cd802d771012c7a.jpg', 10.0000, 0, '2018-10-29 10:27:25');

-- ----------------------------
-- Table structure for activitieslotterylog
-- ----------------------------
DROP TABLE IF EXISTS `activitieslotterylog`;
CREATE TABLE `activitieslotterylog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `UseCredits` int(11) NOT NULL COMMENT '使用积分',
  `RewardCredits` decimal(19, 4) NOT NULL COMMENT '兑换积分',
  `RndNumber` int(11) NOT NULL COMMENT '随机产生数字',
  `PrizeList` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '总随商品列表',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '活动抽奖记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for activity
-- ----------------------------
DROP TABLE IF EXISTS `activity`;
CREATE TABLE `activity`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目名称',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目副标题',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `Counts` int(11) NOT NULL COMMENT '参加次数',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `KindInfo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '栏目详细介绍',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `NeedCredits` decimal(19, 4) NOT NULL COMMENT '参加积分',
  `ActivityType` int(11) NOT NULL COMMENT '活动类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '活动' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for activityproduct
-- ----------------------------
DROP TABLE IF EXISTS `activityproduct`;
CREATE TABLE `activityproduct`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '栏目ID',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标题',
  `ItemNO` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编号',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '副标题',
  `Unit` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品单位',
  `Spec` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品规格尺寸',
  `Color` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `Weight` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '重量',
  `Price` decimal(19, 4) NOT NULL COMMENT '价格',
  `MarketPrice` decimal(19, 4) NOT NULL COMMENT '市场价格',
  `Material` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '材料',
  `Front` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '封面',
  `Credits` int(11) NOT NULL COMMENT '积分',
  `MaxCredits` int(11) NOT NULL COMMENT '积分',
  `Stock` int(11) NOT NULL COMMENT '库存',
  `WarnStock` int(11) NOT NULL COMMENT '警告库存',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '内容',
  `Parameters` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '商品参数',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模板',
  `IsRecommend` int(11) NOT NULL COMMENT '是否推荐',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsTop` int(11) NOT NULL COMMENT '是否置顶',
  `Sequence` int(11) NOT NULL COMMENT '排序',
  `CommentCount` int(11) NOT NULL COMMENT '评论数量',
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `Tags` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'TAG',
  `ItemImg` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '更多图片',
  `AuthorId` int(11) NOT NULL COMMENT '添加管理员ID',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '时间',
  `Probability` decimal(19, 4) NOT NULL COMMENT '概率：0.2为0.2%',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '活动商品' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of activityproduct
-- ----------------------------
INSERT INTO `activityproduct` VALUES (1, 1, 'iPhone Xs Max 256', NULL, NULL, NULL, NULL, NULL, NULL, 0.0000, 0.0000, NULL, NULL, 99999, 99999, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 999, 0, NULL, NULL, NULL, '/userfiles/images/2018/4882ef7b9d62b5d5.jpg', 0, NULL, NULL, 0, '2018-10-29 11:51:49', '2018-10-29 11:52:29', 1.0000);
INSERT INTO `activityproduct` VALUES (2, 1, '小天鹅洗衣机', NULL, NULL, NULL, NULL, NULL, NULL, 0.0000, 0.0000, NULL, NULL, 9999, 9999, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 999, 0, NULL, NULL, NULL, '/userfiles/images/2018/21bdecc83b3d6894.jpg', 0, NULL, NULL, 0, '2018-10-29 22:12:41', '2018-10-29 22:12:46', 5.0000);
INSERT INTO `activityproduct` VALUES (3, 1, 'iPhone Xs Max 128', NULL, NULL, NULL, NULL, NULL, NULL, 0.0000, 0.0000, NULL, NULL, 59999, 59999, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 999, 0, NULL, NULL, NULL, '/userfiles/images/2018/21bdecc83b3d6894.jpg', 0, NULL, NULL, 0, '2018-10-29 14:54:13', '2018-10-29 14:54:16', 2.0000);
INSERT INTO `activityproduct` VALUES (4, 1, '20积分', NULL, NULL, NULL, NULL, NULL, NULL, 0.0000, 0.0000, NULL, NULL, 20, 20, 99999999, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 999, 0, NULL, NULL, NULL, '/userfiles/images/2018/757732fb78bf7b74.jpg', 0, NULL, NULL, 0, '2018-10-29 21:37:45', '2018-10-29 21:38:11', 30.0000);
INSERT INTO `activityproduct` VALUES (5, 1, '1-10随机', NULL, NULL, NULL, NULL, NULL, NULL, 0.0000, 0.0000, NULL, NULL, 1, 10, 99999999, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 999, 0, NULL, NULL, NULL, '/userfiles/images/2018/206037f458454943.jpg', 0, NULL, NULL, 0, '2018-10-29 21:40:00', '2018-10-29 21:40:17', 60.0000);

-- ----------------------------
-- Table structure for admin
-- ----------------------------
DROP TABLE IF EXISTS `admin`;
CREATE TABLE `admin`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UserName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `PassWord` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码',
  `Salt` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '盐值',
  `RealName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '姓名',
  `Tel` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话',
  `Email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮件',
  `UserLevel` int(11) NOT NULL COMMENT '级别',
  `RoleId` int(11) NOT NULL COMMENT '管理组',
  `GroupId` int(11) NOT NULL COMMENT '用户组',
  `LastLoginTime` datetime(0) NULL DEFAULT NULL COMMENT '最后登录时间',
  `LastLoginIP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上次登录IP',
  `ThisLoginTime` datetime(0) NULL DEFAULT NULL COMMENT '本次登录时间',
  `ThisLoginIP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '本次登录IP',
  `IsLock` int(11) NOT NULL COMMENT '是否是锁定',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '管理员' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of admin
-- ----------------------------
INSERT INTO `admin` VALUES (1, 'admin', '8788C77C8FA36CF9D3718026355AB571', 'fOtOFYYpLO90', 'admin', '', '', 100, 1, 0, '2020-03-06 15:40:43', '127.0.0.1', '2018-04-16 00:17:43', '127.0.0.1', 0);

-- ----------------------------
-- Table structure for adminlog
-- ----------------------------
DROP TABLE IF EXISTS `adminlog`;
CREATE TABLE `adminlog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '管理员ID',
  `GUID` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '唯一ID',
  `UserName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `PassWord` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码',
  `LoginTime` datetime(0) NULL DEFAULT NULL COMMENT '登录时间',
  `LoginIP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '登录IP',
  `IsLoginOK` int(11) NOT NULL COMMENT '是否登录成功',
  `Actions` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '记录',
  `LastUpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '登录时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 50 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '管理日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of adminlog
-- ----------------------------
INSERT INTO `adminlog` VALUES (1, 0, '5d8b265b-342d-45f1-ab92-425791cd0b10', 'admin', '******', '2018-04-16 00:30:06', '127.0.0.1', 1, '查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；修改站点基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；查看基本配置；', '2018-04-16 01:04:56');
INSERT INTO `adminlog` VALUES (2, 0, '49ebe38a-3cfb-4e16-afa8-718d0c0758a1', 'admin', '******', '2018-04-16 01:09:35', '123.1.189.126', 1, '查看基本配置；', '2018-04-16 01:14:19');
INSERT INTO `adminlog` VALUES (3, 0, '592f0755-0b3b-4871-a33a-32b137bff2fd', 'admin', '******', '2018-04-16 08:43:30', '60.166.72.54', 1, '查看基本配置；', '2018-04-16 08:43:36');
INSERT INTO `adminlog` VALUES (4, 0, '363eb952-0d60-4eb7-82b6-419c710bb89c', 'admin', '******', '2018-04-16 10:19:15', '61.140.236.194', 1, NULL, '2018-04-16 10:19:15');
INSERT INTO `adminlog` VALUES (5, 0, 'e1699742-94c3-4eb7-8bda-0fe815533e57', 'admin', '******', '2018-04-16 10:19:52', '116.24.101.123', 1, '查看基本配置；查看基本配置；', '2018-04-16 10:26:05');
INSERT INTO `adminlog` VALUES (6, 0, 'f1b1264d-6b65-4573-b3d1-ae6135aa18f4', 'admin', '******', '2018-04-16 10:24:57', '116.226.70.126', 1, '查看基本配置；查看基本配置；', '2018-04-16 10:25:15');
INSERT INTO `adminlog` VALUES (7, 0, '453c6021-7bf7-4b38-9913-27536d827bfc', 'admin', '******', '2018-04-16 10:25:10', '112.64.119.42', 1, '查看基本配置；', '2018-04-16 10:25:41');
INSERT INTO `adminlog` VALUES (8, 0, '5796a8c3-4eaf-4bba-8742-7378135dd93b', 'admin', '******', '2018-04-16 10:32:07', '113.118.198.128', 1, '查看基本配置；', '2018-04-16 10:32:25');
INSERT INTO `adminlog` VALUES (9, 0, '46f181aa-4747-4633-8fad-099f06799fe6', 'admin', '******', '2018-04-16 10:35:19', '59.172.251.218', 1, NULL, '2018-04-16 10:35:19');
INSERT INTO `adminlog` VALUES (10, 0, 'b2c7c5d3-9e9f-4b35-825c-041c71bb3e68', 'admin', '******', '2018-04-17 22:53:58', '59.42.239.56', 1, '查看后台事件列表（keyword:;page:0;limit:20）;查看后台事件列表（keyword:;page:0;limit:20）;查看后台事件列表（keyword:;page:0;limit:20）;查看基本配置；查看SMTP配置；查看附件配置；查看附件配置；查看后台事件列表（keyword:;page:0;limit:20）;查看Robots.txt;', '2018-04-18 00:20:16');
INSERT INTO `adminlog` VALUES (11, 0, '9283b94f-d4ac-42d8-8ae8-ffb462877401', 'admin', '******', '2018-04-18 09:50:49', '61.140.238.15', 1, '查看Robots.txt;查看附件配置；查看附件配置；查看附件配置；修改附件配置;查看附件配置；', '2018-04-18 11:27:05');
INSERT INTO `adminlog` VALUES (12, 0, '1a19389a-b14d-453a-9970-1f154dd5331b', 'admin', '******', '2018-04-18 09:52:15', '116.226.70.126', 1, '查看后台事件列表（keyword:;page:0;limit:20）;', '2018-04-18 09:52:26');
INSERT INTO `adminlog` VALUES (13, 0, '659402f8-b25a-42d8-9761-bbbbd349096f', 'admin', '******', '2018-04-18 09:53:41', '171.221.146.125', 1, '查看基本配置；查看后台事件列表（keyword:;page:0;limit:20）;查看后台事件列表（keyword:;page:0;limit:25）;', '2018-04-18 10:15:50');
INSERT INTO `adminlog` VALUES (14, 0, 'e724d952-5c46-4c70-ac25-35b98c0dd121', 'admin', '******', '2018-04-18 09:54:47', '202.106.86.136', 1, '查看SMTP配置；', '2018-04-18 09:55:00');
INSERT INTO `adminlog` VALUES (15, 0, '99d7a774-b6ec-42f9-96ef-1525ff6eae79', 'admin', '******', '2018-04-18 09:55:11', '182.242.249.54', 1, '查看基本配置；', '2018-04-18 09:55:27');
INSERT INTO `adminlog` VALUES (16, 0, '596709a4-fe24-4141-8a4b-8c761d0a7e48', 'admin', '******', '2018-04-18 09:55:23', '113.57.27.145', 1, '查看基本配置；', '2018-04-18 09:55:29');
INSERT INTO `adminlog` VALUES (17, 0, 'ba87f31b-075c-4de7-afa8-f99272b00a12', 'admin', '******', '2018-04-18 09:58:16', '121.33.75.50', 1, '查看基本配置；查看基本配置；查看SMTP配置；查看附件配置；查看Robots.txt;', '2018-04-18 09:58:53');
INSERT INTO `adminlog` VALUES (18, 0, 'f68c98ad-4fb1-441c-bbfb-e6734d46d07f', 'Admin', '******', '2018-04-18 09:58:23', '36.98.143.127', 1, '查看附件配置；', '2018-04-18 10:03:38');
INSERT INTO `adminlog` VALUES (19, 0, 'c6b26c99-e361-44db-9969-df2799caca60', 'admin', '******', '2018-04-18 10:01:41', '106.120.233.218', 1, NULL, '2018-04-18 10:01:41');
INSERT INTO `adminlog` VALUES (20, 0, '161763c2-d840-4949-a672-4ac3e6339ffb', 'admin', '******', '2018-04-18 10:08:42', '223.104.108.31', 1, NULL, '2018-04-18 10:08:42');
INSERT INTO `adminlog` VALUES (21, 0, '0ed86444-f812-4ac7-840f-b0809a1be406', 'admin', '******', '2018-04-18 10:17:16', '171.221.146.125', 1, '查看后台事件列表（keyword:;page:0;limit:20）;查看SMTP配置；查看附件配置；查看Robots.txt;查看后台事件列表（keyword:;page:0;limit:20）;查看附件配置；查看SMTP配置；查看Robots.txt;查看后台事件列表（keyword:;page:0;limit:20）;查看后台事件列表（keyword:;page:0;limit:10）;查看后台事件列表（keyword:;page:0;limit:25）;查看附件配置；修改附件配置;查看附件配置；', '2018-04-18 11:29:19');
INSERT INTO `adminlog` VALUES (22, 0, '7aa7d5b4-4420-4918-b631-63c0a45ec141', 'admin', '******', '2018-04-18 10:21:01', '115.195.129.93', 1, '查看基本配置；查看SMTP配置；查看附件配置；查看Robots.txt;查看基本配置；查看SMTP配置；查看基本配置；查看SMTP配置；查看附件配置；', '2018-04-18 10:22:40');
INSERT INTO `adminlog` VALUES (23, 0, 'ae5c1441-bedd-49fb-bb39-96ddbfd20dc0', 'admin', '******', '2018-04-18 10:24:51', '222.195.191.87', 1, '查看基本配置；查看SMTP配置；查看附件配置；查看Robots.txt;查看基本配置；查看基本配置；查看基本配置；', '2018-04-18 10:31:21');
INSERT INTO `adminlog` VALUES (24, 0, 'eb783940-71cb-4681-940c-7faf15b2aeb0', 'admin', '******', '2018-04-18 10:27:43', '59.174.113.164', 1, '查看基本配置；查看SMTP配置；查看附件配置；查看Robots.txt;查看后台事件列表（keyword:;page:0;limit:20）;查看基本配置；', '2018-04-18 10:32:29');
INSERT INTO `adminlog` VALUES (25, 0, '1a7dbe72-653c-4f3e-a11f-07f3e384e374', 'admin', '******', '2018-04-18 11:13:45', '125.113.143.165', 1, NULL, '2018-04-18 11:13:45');
INSERT INTO `adminlog` VALUES (26, 0, '1efdb7b8-674b-4490-9466-8cf56adedb90', 'admin', '******', '2018-04-18 13:45:03', '171.221.146.125', 1, NULL, '2018-04-18 13:45:03');
INSERT INTO `adminlog` VALUES (27, 0, '8d7e7655-93d1-4e1c-9276-0c00ab5e0558', 'admin', '******', '2018-04-18 18:06:51', '113.109.108.136', 1, '查看文章栏目列表页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看文章栏目列表页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；查看文章栏目列表页面；查看添加文章栏目页面；查看添加文章栏目页面；查看添加文章栏目页面；添加文章栏目(id:1);查看文章栏目列表页面；查看/编辑文章栏目（id:1）页面；修改文章栏目(id:1);查看文章栏目列表页面；查看/编辑文章栏目（id:1）页面；查看文章栏目列表页面；查看/编辑文章栏目（id:1）页面；查看文章列表;查看添加文章页面;查看文章列表;查看添加文章页面;添加文章(id:1);查看文章(1);编辑文章(id:1);查看附件配置；修改附件配置;查看附件配置；查看文章(1);查看文章栏目列表页面；', '2018-04-18 20:37:31');
INSERT INTO `adminlog` VALUES (28, 0, 'e3217152-574c-4b7d-b8a6-6579333587fd', 'admin', '******', '2018-11-15 00:44:56', '127.0.0.1', 1, '|||2018-11-15 00:45: 查看文章栏目列表页面；|||2018-11-15 00:45: 查看文章列表;|||2018-11-15 00:45: 查看文章(1);|||2018-11-15 00:45: 查看文章(1);|||2018-11-15 00:45: 查看文章(1);|||2018-11-15 00:45: 查看文章(1);', '2018-11-15 00:45:59');
INSERT INTO `adminlog` VALUES (29, 0, '6b4d37d3-eb88-4c32-acdf-894632b031b6', 'admin', '******', '2018-11-15 23:43:02', '127.0.0.1', 1, '|||2018-11-15 23:43: 查看基本配置；|||2018-11-15 23:43: 查看文章栏目列表页面；|||2018-11-15 23:44: 查看添加文章栏目页面；|||2018-11-15 23:44: 添加文章栏目(id:2);|||2018-11-15 23:44: 查看文章栏目列表页面；|||2018-11-15 23:44: 查看添加文章栏目页面；|||2018-11-15 23:47: 添加文章栏目(id:3);|||2018-11-15 23:47: 查看文章栏目列表页面；|||2018-11-15 23:47: 查看添加文章栏目页面；|||2018-11-15 23:47: 添加文章栏目(id:4);|||2018-11-15 23:47: 查看文章栏目列表页面；|||2018-11-15 23:47: 查看添加文章栏目页面；|||2018-11-15 23:48: 添加文章栏目(id:5);|||2018-11-15 23:48: 查看文章栏目列表页面；|||2018-11-15 23:48: 查看商品栏目列表页面；|||2018-11-15 23:48: 查看添加商品栏目页面；|||2018-11-15 23:48: 查看添加商品栏目页面；|||2018-11-15 23:48: 添加商品栏目(id:1);|||2018-11-15 23:48: 查看商品栏目列表页面；|||2018-11-15 23:48: 查看添加商品栏目页面；|||2018-11-15 23:49: 添加商品栏目(id:2);|||2018-11-15 23:49: 查看商品栏目列表页面；|||2018-11-15 23:49: 查看添加商品栏目页面；|||2018-11-15 23:49: 添加商品栏目(id:3);|||2018-11-15 23:49: 查看商品栏目列表页面；|||2018-11-15 23:58: 查看文章列表;|||2018-11-15 23:58: 查看添加文章页面;|||2018-11-15 23:58: 添加文章(id:2);|||2018-11-16 00:42: 修改站点基本配置；|||2018-11-16 00:42: 查看基本配置；', '2018-11-16 00:42:24');
INSERT INTO `adminlog` VALUES (30, 0, '109e74c6-76cb-4d01-8012-40f17d19c49e', 'admin', '******', '2018-11-18 23:30:44', '127.0.0.1', 1, '|||2018-11-18 23:30: 查看基本配置；|||2018-11-18 23:30: 查看文章栏目列表页面；|||2018-11-18 23:31: 查看/编辑文章栏目（id:2）页面；|||2018-11-18 23:31: 修改文章栏目(id:2);|||2018-11-18 23:31: 查看文章栏目列表页面；|||2018-11-18 23:31: 查看添加文章栏目页面；|||2018-11-18 23:31: 添加文章栏目(id:6);|||2018-11-18 23:31: 查看文章栏目列表页面；|||2018-11-18 23:31: 查看添加文章栏目页面；|||2018-11-18 23:31: 添加文章栏目(id:7);|||2018-11-18 23:31: 查看文章栏目列表页面；|||2018-11-18 23:32: 查看/编辑文章栏目（id:4）页面；|||2018-11-18 23:32: 修改文章栏目(id:4);|||2018-11-18 23:32: 查看文章栏目列表页面；|||2018-11-18 23:33: 查看添加文章栏目页面；|||2018-11-18 23:33: 添加文章栏目(id:8);|||2018-11-18 23:33: 查看文章栏目列表页面；|||2018-11-18 23:33: 查看添加文章栏目页面；|||2018-11-18 23:33: 添加文章栏目(id:9);|||2018-11-18 23:33: 查看文章栏目列表页面；|||2018-11-18 23:33: 查看添加文章栏目页面；|||2018-11-18 23:33: 添加文章栏目(id:10);|||2018-11-18 23:33: 查看文章栏目列表页面；|||2018-11-18 23:33: 查看添加文章栏目页面；|||2018-11-18 23:33: 添加文章栏目(id:11);|||2018-11-18 23:33: 查看文章栏目列表页面；|||2018-11-18 23:33: 查看文章列表;|||2018-11-18 23:34: 查看文章(1);|||2018-11-18 23:35: 编辑文章(id:1);|||2018-11-18 23:50: 查看商品栏目列表页面；|||2018-11-19 00:08: 查看添加文章页面;|||2018-11-19 00:08: 添加文章(id:3);|||2018-11-19 00:08: 查看添加文章页面;|||2018-11-19 00:08: 添加文章(id:4);|||2018-11-19 00:08: 查看添加文章页面;|||2018-11-19 00:08: 添加文章(id:5);|||2018-11-19 00:08: 查看添加文章页面;|||2018-11-19 00:08: 添加文章(id:6);|||2018-11-19 00:16: 查看添加文章栏目页面；|||2018-11-19 00:16: 添加文章栏目(id:12);|||2018-11-19 00:16: 查看文章栏目列表页面；', '2018-11-19 00:16:58');
INSERT INTO `adminlog` VALUES (31, 0, '4bb5af57-640c-43a7-b228-d48041e56775', 'admin', '******', '2018-12-01 21:36:28', '127.0.0.1', 1, '|||2018-12-01 21:36: 查看文章栏目列表页面；|||2018-12-01 21:36: 查看/编辑文章栏目（id:1）页面；|||2018-12-01 21:37: 查看文章栏目列表页面；|||2018-12-01 21:37: 查看/编辑文章栏目（id:1）页面；|||2018-12-01 21:37: 修改文章栏目(id:1);|||2018-12-01 21:37: 查看文章栏目列表页面；|||2018-12-01 21:37: 查看/编辑文章栏目（id:1）页面；|||2018-12-01 21:38: 查看/编辑文章栏目（id:1）页面；|||2018-12-01 21:44: 查看/编辑文章栏目（id:2）页面；|||2018-12-01 21:44: 修改文章栏目(id:2);|||2018-12-01 21:44: 查看文章栏目列表页面；|||2018-12-01 22:25: 查看/编辑文章栏目（id:6）页面；|||2018-12-01 22:25: 修改文章栏目(id:6);|||2018-12-01 22:25: 查看文章栏目列表页面；|||2018-12-01 22:27: 查看/编辑文章栏目（id:2）页面；|||2018-12-01 22:27: 查看/编辑文章栏目（id:4）页面；|||2018-12-01 22:27: 查看/编辑文章栏目（id:6）页面；|||2018-12-01 22:27: 修改文章栏目(id:6);|||2018-12-01 22:27: 查看文章栏目列表页面；|||2018-12-01 22:27: 查看/编辑文章栏目（id:2）页面；|||2018-12-01 22:27: 修改文章栏目(id:2);|||2018-12-01 22:27: 查看文章栏目列表页面；|||2018-12-01 22:27: 查看/编辑文章栏目（id:4）页面；|||2018-12-01 22:27: 修改文章栏目(id:4);|||2018-12-01 22:27: 查看文章栏目列表页面；|||2018-12-01 22:27: 查看/编辑文章栏目（id:5）页面；|||2018-12-01 22:27: 查看/编辑文章栏目（id:5）页面；|||2018-12-01 22:28: 修改文章栏目(id:5);|||2018-12-01 22:28: 查看文章栏目列表页面；|||2018-12-01 22:44: 查看文章列表;|||2018-12-01 22:44: 查看文章(2);|||2018-12-01 22:44: 编辑文章(id:2);|||2018-12-01 23:05: 查看/编辑文章栏目（id:10）页面；|||2018-12-01 23:05: 查看文章栏目列表页面；|||2018-12-01 23:05: 查看/编辑文章栏目（id:10）页面；|||2018-12-01 23:06: 查看/编辑文章栏目（id:1）页面；|||2018-12-01 23:06: 查看文章栏目列表页面；|||2018-12-01 23:06: 查看/编辑文章栏目（id:2）页面；|||2018-12-01 23:06: 查看添加文章栏目页面；|||2018-12-01 23:07: 查看文章栏目列表页面；|||2018-12-01 23:07: 查看/编辑文章栏目（id:10）页面；|||2018-12-01 23:07: 修改文章栏目(id:10);|||2018-12-01 23:07: 查看文章栏目列表页面；|||2018-12-01 23:07: 查看/编辑文章栏目（id:10）页面；|||2018-12-01 23:08: 查看文章栏目列表页面；|||2018-12-01 23:08: 查看/编辑文章栏目（id:10）页面；|||2018-12-01 23:08: 修改文章栏目(id:10);|||2018-12-01 23:08: 查看文章栏目列表页面；', '2018-12-01 23:08:28');
INSERT INTO `adminlog` VALUES (32, 0, '229bbc0f-65f1-413f-99e3-aaeda53ddd34', 'admin', '******', '2018-12-02 00:31:23', '127.0.0.1', 1, '|||2018-12-02 00:31: 查看文章栏目列表页面；|||2018-12-02 00:31: 查看/编辑文章栏目（id:11）页面；|||2018-12-02 00:31: 查看/编辑文章栏目（id:11）页面；|||2018-12-02 00:32: 修改文章栏目(id:11);|||2018-12-02 00:32: 查看文章栏目列表页面；|||2018-12-02 01:00: 查看留言分类列表页面；|||2018-12-02 01:00: 添加留言分类(1)；|||2018-12-02 01:00: 查看留言分类列表页面；', '2018-12-02 01:00:59');
INSERT INTO `adminlog` VALUES (33, 0, '1fde3cde-8f37-4a1f-b2fb-44dbb9215ca3', 'admin', '******', '2018-12-02 16:32:51', '127.0.0.1', 1, '|||2018-12-02 16:40: 查看友情链接列表;|||2018-12-02 16:40: 查看友情链接分类列表页面；|||2018-12-02 16:40: 添加友情链接分类(1)；|||2018-12-02 16:40: 查看友情链接分类列表页面；|||2018-12-02 16:43: 查看添加友情链接页面;|||2018-12-02 16:45: 查看添加友情链接页面;|||2018-12-02 16:45: 添加友情链接(1);|||2018-12-02 16:45: 查看友情链接列表;|||2018-12-02 16:46: 查看友情链接列表;|||2018-12-02 16:46: 查看/编辑友情链接(1);|||2018-12-02 16:46: 查看添加友情链接页面;|||2018-12-02 16:46: 添加友情链接(2);|||2018-12-02 16:46: 查看友情链接列表;|||2018-12-02 16:52: 查看文章列表;|||2018-12-02 16:52: 查看添加文章页面;|||2018-12-02 16:53: 添加文章(id:7);|||2018-12-02 16:54: 查看添加文章页面;|||2018-12-02 16:55: 添加文章(id:8);|||2018-12-02 16:55: 查看添加文章页面;|||2018-12-02 16:55: 添加文章(id:9);|||2018-12-02 16:56: 查看添加文章页面;|||2018-12-02 16:56: 添加文章(id:10);|||2018-12-02 16:56: 查看添加文章页面;|||2018-12-02 16:56: 添加文章(id:11);|||2018-12-02 16:58: 查看文章栏目列表页面；|||2018-12-02 17:05: 查看/编辑文章栏目（id:7）页面；|||2018-12-02 17:05: 查看/编辑文章栏目（id:6）页面；|||2018-12-02 17:09: 查看文章栏目列表页面；|||2018-12-02 17:10: 查看/编辑文章栏目（id:6）页面；|||2018-12-02 17:10: 修改文章栏目(id:6);|||2018-12-02 17:10: 查看文章栏目列表页面；|||2018-12-02 17:11: 查看/编辑文章栏目（id:7）页面；|||2018-12-02 17:11: 修改文章栏目(id:7);|||2018-12-02 17:11: 查看文章栏目列表页面；|||2018-12-02 17:11: 查看/编辑文章栏目（id:8）页面；|||2018-12-02 17:11: 修改文章栏目(id:8);|||2018-12-02 17:11: 查看文章栏目列表页面；|||2018-12-02 17:12: 查看/编辑文章栏目（id:9）页面；|||2018-12-02 17:12: 修改文章栏目(id:9);|||2018-12-02 17:12: 查看文章栏目列表页面；|||2018-12-02 17:12: 查看/编辑文章栏目（id:12）页面；|||2018-12-02 17:12: 修改文章栏目(id:12);|||2018-12-02 17:12: 查看文章栏目列表页面；|||2018-12-02 17:14: 查看文章(7);|||2018-12-02 17:14: 查看文章(1);|||2018-12-02 17:14: 编辑文章(id:1);|||2018-12-02 17:14: 查看商品栏目列表页面；|||2018-12-02 17:14: 查看商品列表;|||2018-12-02 17:14: 查看添加商品页面;|||2018-12-02 17:17: 查看添加商品页面;|||2018-12-02 17:20: 添加商品(id:1);|||2018-12-02 17:21: 查看添加商品页面;|||2018-12-02 17:22: 添加商品(id:2);|||2018-12-02 17:22: 查看添加商品页面;|||2018-12-02 17:23: 添加商品(id:3);|||2018-12-02 17:23: 查看添加商品页面;|||2018-12-02 17:23: 添加商品(id:4);|||2018-12-02 17:49: 查看添加商品页面;|||2018-12-02 17:50: 添加商品(id:5);|||2018-12-02 17:56: 查看广告分类列表页面；|||2018-12-02 17:56: 添加广告栏目(1)；|||2018-12-02 17:56: 查看广告分类列表页面；|||2018-12-02 17:56: 查看广告列表;|||2018-12-02 17:56: 查看添加广告页面;|||2018-12-02 18:02: 添加广告(1);|||2018-12-02 18:02: 查看添加广告页面;|||2018-12-02 18:06: 添加广告(2);|||2018-12-02 18:06: 查看/编辑广告(1);|||2018-12-02 18:42: 查看文章栏目列表页面；|||2018-12-02 18:42: 查看/编辑文章栏目（id:6）页面；|||2018-12-02 18:42: 修改文章栏目(id:6);|||2018-12-02 18:42: 查看文章栏目列表页面；', '2018-12-02 18:42:45');
INSERT INTO `adminlog` VALUES (34, 0, 'b2daaeb3-b094-48bd-978d-8e777713f048', 'admin', '******', '2018-12-05 11:49:54', '127.0.0.1', 1, '|||2018-12-05 11:49: 查看基本配置；|||2018-12-05 11:49: 修改站点基本配置；|||2018-12-05 11:50: 查看基本配置；', '2018-12-05 11:50:00');
INSERT INTO `adminlog` VALUES (35, 0, '0d7c88f0-873e-4fbc-a3bd-04ad6cecaace', 'admin', '******', '2019-03-19 18:58:02', '127.0.0.1', 1, '|||2019-03-19 18:58: 查看文章列表;|||2019-03-19 18:58: 查看添加文章页面;|||2019-03-19 19:17: 查看文章列表;|||2019-03-19 19:17: 查看文章(9);|||2019-03-19 19:18: 查看文章(1);|||2019-03-19 19:18: 查看文章(1);|||2019-03-19 19:18: 查看文章(1);|||2019-03-19 19:19: 查看文章(1);|||2019-03-19 19:19: 查看文章(1);|||2019-03-19 19:19: 查看文章(1);', '2019-03-19 19:19:51');
INSERT INTO `adminlog` VALUES (36, 0, 'fd00cebc-39f6-4513-a5bf-91b8e65dfc7a', 'admin', '******', '2019-03-29 15:33:54', '127.0.0.1', 1, '|||2019-03-29 15:33: 查看文章列表;|||2019-03-29 15:35: 查看文章列表;', '2019-03-29 15:35:52');
INSERT INTO `adminlog` VALUES (37, 0, 'bae22480-fb8c-4e37-9772-8196ce2fe971', 'admin', '******', '2019-09-11 14:42:08', '127.0.0.1', 1, NULL, '2019-09-11 14:42:08');
INSERT INTO `adminlog` VALUES (38, 0, '8040a28e-a44e-4f76-a499-e0a3891c4262', 'admin', '******', '2019-09-11 14:43:44', '127.0.0.1', 1, NULL, '2019-09-11 14:43:44');
INSERT INTO `adminlog` VALUES (39, 0, '3ea95a69-3d79-4a2b-ac61-10c91c30c67f', 'admin', '******', '2019-09-11 14:44:20', '127.0.0.1', 1, '|||2019-09-11 14:44: 查看管理组列表;|||2019-09-11 14:44: 查看后台事件列表（keyword:;page:0;limit:20）;|||2019-09-11 14:45: 查看管理员列表|||2019-09-11 14:45: 查看/编辑管理员(admin);|||2019-09-11 14:45: 查看管理组（1）详情;', '2019-09-11 14:45:13');
INSERT INTO `adminlog` VALUES (40, 0, '25df79b6-5b57-4570-982d-365bd0697d65', 'admin', '******', '2019-09-27 17:16:10', '127.0.0.1', 1, '|||2019-09-27 17:16: 查看文章列表;|||2019-09-27 17:16: 查看文章(11);', '2019-09-27 17:16:16');
INSERT INTO `adminlog` VALUES (41, 0, 'ba65e78c-1b54-4998-93a4-1b6a84fa0651', 'admin', '******', '2019-10-30 17:02:16', '127.0.0.1', 1, NULL, '2019-10-30 17:02:16');
INSERT INTO `adminlog` VALUES (42, 0, '9756c023-6019-4516-9f15-dc139d870c18', 'admin', '******', '2019-10-30 17:10:16', '127.0.0.1', 1, NULL, '2019-10-30 17:10:16');
INSERT INTO `adminlog` VALUES (43, 0, '34570af3-d95f-48ae-b1fb-9f1b33bcd5bd', 'admin', '******', '2019-10-30 17:46:24', '127.0.0.1', 1, NULL, '2019-10-30 17:46:24');
INSERT INTO `adminlog` VALUES (44, 0, '3dd4eb9e-60a6-47e3-a7d0-b7ed4d87c0a2', 'admin', '******', '2019-11-05 10:20:02', '127.0.0.1', 1, NULL, '2019-11-05 10:20:02');
INSERT INTO `adminlog` VALUES (45, 0, '1aa1b824-d522-4294-aae8-00a4826cfd3c', 'admin', '******', '2019-12-06 16:10:01', '127.0.0.1', 1, NULL, '2019-12-06 16:10:01');
INSERT INTO `adminlog` VALUES (46, 0, '05efd4b7-0455-4a18-b2ad-19864033f5ff', 'admin', '******', '2020-02-13 11:31:01', '127.0.0.1', 1, NULL, '2020-02-13 11:31:01');
INSERT INTO `adminlog` VALUES (47, 0, '135246f9-891c-407c-ba04-6b27c29ae3d9', 'admin', '******', '2020-02-19 13:48:53', '127.0.0.1', 1, NULL, '2020-02-19 13:48:53');
INSERT INTO `adminlog` VALUES (48, 0, 'bd411eb9-8ae0-40eb-8447-dd5b1d5ba25d', 'admin', '******', '2020-02-19 14:41:21', '127.0.0.1', 1, NULL, '2020-02-19 14:41:21');
INSERT INTO `adminlog` VALUES (49, 0, '0755c829-aa1e-4919-b608-93c92599d30b', 'admin', '******', '2020-03-06 15:40:43', '127.0.0.1', 1, NULL, '2020-03-06 15:40:43');

-- ----------------------------
-- Table structure for adminmenu
-- ----------------------------
DROP TABLE IF EXISTS `adminmenu`;
CREATE TABLE `adminmenu`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `MenuKey` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标识key',
  `MenuName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '页面名称',
  `PermissionKey` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '页面名称',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `Link` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '页面连接地址',
  `PId` int(11) NOT NULL COMMENT '上级ID',
  `Level` int(11) NOT NULL COMMENT '级别',
  `Location` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路径',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式名称',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 39 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '后台菜单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of adminmenu
-- ----------------------------
INSERT INTO `adminmenu` VALUES (1, 'home', '主页', NULL, NULL, 'Index/Main', 0, 0, '0,', 0, 0, NULL, 'fa-home');
INSERT INTO `adminmenu` VALUES (2, 'system', '系统设置', NULL, NULL, '#', 0, 0, '0,', 0, 1, NULL, 'fa-gears');
INSERT INTO `adminmenu` VALUES (3, 'baseconfig', '基本配置', NULL, NULL, 'System/BaseConfig', 2, 1, '0,2,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (4, 'smptconfig', 'SMTP设置', NULL, NULL, 'System/SmtpConfig', 2, 1, '0,2,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (5, 'attachconfig', '附件设置', NULL, NULL, 'System/AttachConfig', 2, 1, '0,2,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (6, 'articlesys', '文章系统', NULL, NULL, '#', 0, 0, '0,', 0, 2, NULL, 'fa-book');
INSERT INTO `adminmenu` VALUES (7, 'articlecategory', '文章栏目管理', NULL, NULL, 'Article/ArticleCategoryList', 6, 1, '0,6,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (8, 'article', '文章管理', NULL, NULL, 'Article/ArticleList', 6, 1, '0,6,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (9, 'productsys', '商品系统', NULL, NULL, '#', 0, 0, '0,', 0, 3, NULL, 'fa-balance-scale');
INSERT INTO `adminmenu` VALUES (10, 'productcategory', '商品分类管理', NULL, NULL, 'Product/CategoryList', 9, 1, '0,9,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (11, 'product', '商品管理', NULL, NULL, 'Product/ProductList', 9, 1, '0,9,', 0, 1, NULL, NULL);
INSERT INTO `adminmenu` VALUES (12, 'ordersys', '订单系统', NULL, NULL, '#', 0, 0, '0,', 0, 4, NULL, 'fa-shopping-bag');
INSERT INTO `adminmenu` VALUES (13, 'order', '商品订单管理', NULL, NULL, 'Order/OrderList', 12, 1, '0,12,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (14, 'payonline', '支付记录', NULL, NULL, 'Order/PayOnlineList', 12, 1, '0,12,', 0, 1, NULL, NULL);
INSERT INTO `adminmenu` VALUES (15, 'user', '用户系统', NULL, NULL, '#', 0, 0, '0,', 0, 5, NULL, 'fa-user');
INSERT INTO `adminmenu` VALUES (16, 'memberrole', '用户组管理', NULL, NULL, 'Member/MemberRole', 15, 1, '0,15,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (17, 'members', '用户管理', NULL, NULL, 'Member/Members', 15, 1, '0,15,', 0, 1, NULL, NULL);
INSERT INTO `adminmenu` VALUES (18, 'permissionsys', '后台权限', NULL, NULL, '#', 0, 0, '0,', 0, 6, NULL, 'fa-users');
INSERT INTO `adminmenu` VALUES (19, 'adminrole', '管理组管理', NULL, NULL, 'Member/AdminRole', 18, 1, '0,18,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (20, 'admins', '管理员管理', NULL, NULL, 'Member/Admins', 18, 1, '0,18,', 0, 1, NULL, NULL);
INSERT INTO `adminmenu` VALUES (21, 'eventkey', '事件权限管理', NULL, NULL, 'Permission/EventKey', 18, 1, '0,18,', 0, 3, NULL, NULL);
INSERT INTO `adminmenu` VALUES (22, 'adminmenu', '后台栏目管理', NULL, NULL, 'Permission/AdminMenuList', 18, 1, '0,18,', 0, 4, NULL, NULL);
INSERT INTO `adminmenu` VALUES (23, 'editme', ' 修改密码', NULL, NULL, 'Member/EditMe', 18, 1, '0,18,', 0, 5, NULL, NULL);
INSERT INTO `adminmenu` VALUES (24, 'guestbooksys', '留言系统', NULL, NULL, '#', 0, 0, '0,', 0, 7, NULL, 'fa-rss-square');
INSERT INTO `adminmenu` VALUES (25, 'guestbookkinds', '留言分类管理', NULL, NULL, 'Guestbook/GuestbookCategorys', 24, 1, '0,24,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (26, 'guestbook', '留言管理', NULL, NULL, 'Guestbook/GuestbookList', 24, 1, '0,24,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (27, 'other', '其他', NULL, NULL, '#', 0, 0, '0,', 0, 99, NULL, 'fa-square-o');
INSERT INTO `adminmenu` VALUES (28, 'adskinds', '广告分类管理', NULL, NULL, 'Other/AdsCategoryList', 27, 1, '0,27,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (29, 'ads', '广告管理', NULL, NULL, 'Other/AdsList', 27, 1, '0,27,', 0, 1, NULL, NULL);
INSERT INTO `adminmenu` VALUES (30, 'linkkinds', '友情连接分类管理', NULL, NULL, 'Other/LinkCategoryList', 27, 1, '0,27,', 0, 2, NULL, NULL);
INSERT INTO `adminmenu` VALUES (31, 'link', '友情连接管理', NULL, NULL, 'Other/LinkList', 27, 1, '0,27,', 0, 3, NULL, NULL);
INSERT INTO `adminmenu` VALUES (32, 'weixinsys', '微信公众号管理', NULL, NULL, '#', 0, 0, '0,', 0, 8, NULL, 'fa-file-word-o');
INSERT INTO `adminmenu` VALUES (33, 'wxautoreply', '关注自动回复', NULL, NULL, 'Weixin/SubscribeReply', 32, 1, '0,32,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (34, 'wxmenu', '自定义菜单管理', NULL, NULL, 'Weixin/Menu', 32, 1, '0,32,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (35, 'wxkeywordreply', '关键字回复', NULL, NULL, 'Weixin/ReplyRule', 32, 1, '0,32,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (36, 'clickreplyrule', '点击事件自动回复', NULL, NULL, 'Weixin/ClickReplyRule', 32, 1, '0,32,', 0, 0, NULL, NULL);
INSERT INTO `adminmenu` VALUES (37, 'robots', 'Robots文档设置', NULL, NULL, 'System/Robots', 2, 1, '0,2,', 0, 3, NULL, NULL);
INSERT INTO `adminmenu` VALUES (38, 'admincplog', '后台管理日志', NULL, NULL, 'Member/AdminCPLogList', 18, 1, '0,18,', 0, 10, NULL, NULL);

-- ----------------------------
-- Table structure for adminmenuevent
-- ----------------------------
DROP TABLE IF EXISTS `adminmenuevent`;
CREATE TABLE `adminmenuevent`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `MenuId` int(11) NOT NULL COMMENT '菜单ID',
  `MenuKey` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单key',
  `EventId` int(11) NOT NULL COMMENT '事件ID',
  `EventKey` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '事件key',
  `EventName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '事件名称',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 192 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '后台菜单对应的事件权限' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of adminmenuevent
-- ----------------------------
INSERT INTO `adminmenuevent` VALUES (1, 1, 'home', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (2, 3, 'baseconfig', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (3, 3, 'baseconfig', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (4, 4, 'smptconfig', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (5, 4, 'smptconfig', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (6, 5, 'attachconfig', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (7, 5, 'attachconfig', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (8, 7, 'articlecategory', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (9, 7, 'articlecategory', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (10, 7, 'articlecategory', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (11, 7, 'articlecategory', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (12, 7, 'articlecategory', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (13, 7, 'articlecategory', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (14, 8, 'article', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (15, 8, 'article', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (16, 8, 'article', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (17, 8, 'article', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (18, 8, 'article', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (19, 8, 'article', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (20, 8, 'article', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (21, 8, 'article', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (22, 8, 'article', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (23, 8, 'article', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (24, 8, 'article', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (25, 10, 'productcategory', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (26, 10, 'productcategory', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (27, 10, 'productcategory', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (28, 10, 'productcategory', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (29, 10, 'productcategory', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (30, 10, 'productcategory', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (31, 11, 'product', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (32, 11, 'product', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (33, 11, 'product', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (34, 11, 'product', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (35, 11, 'product', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (36, 11, 'product', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (37, 11, 'product', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (38, 11, 'product', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (39, 11, 'product', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (40, 11, 'product', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (41, 11, 'product', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (42, 13, 'order', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (43, 13, 'order', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (44, 13, 'order', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (45, 13, 'order', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (46, 13, 'order', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (47, 13, 'order', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (48, 13, 'order', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (49, 13, 'order', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (50, 13, 'order', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (51, 13, 'order', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (52, 13, 'order', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (53, 14, 'payonline', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (54, 14, 'payonline', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (55, 14, 'payonline', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (56, 14, 'payonline', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (57, 14, 'payonline', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (58, 14, 'payonline', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (59, 14, 'payonline', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (60, 14, 'payonline', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (61, 14, 'payonline', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (62, 14, 'payonline', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (63, 16, 'memberrole', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (64, 16, 'memberrole', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (65, 16, 'memberrole', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (66, 16, 'memberrole', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (67, 16, 'memberrole', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (68, 16, 'memberrole', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (69, 17, 'members', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (70, 17, 'members', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (71, 17, 'members', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (72, 17, 'members', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (73, 17, 'members', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (74, 17, 'members', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (75, 17, 'members', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (76, 17, 'members', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (77, 17, 'members', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (78, 17, 'members', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (79, 17, 'members', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (80, 19, 'adminrole', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (81, 19, 'adminrole', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (82, 19, 'adminrole', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (83, 19, 'adminrole', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (84, 19, 'adminrole', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (85, 19, 'adminrole', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (86, 19, 'adminrole', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (87, 20, 'admins', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (88, 20, 'admins', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (89, 20, 'admins', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (90, 20, 'admins', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (91, 20, 'admins', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (92, 20, 'admins', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (93, 20, 'admins', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (94, 20, 'admins', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (95, 20, 'admins', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (96, 20, 'admins', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (97, 20, 'admins', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (98, 21, 'eventkey', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (99, 21, 'eventkey', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (100, 21, 'eventkey', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (101, 21, 'eventkey', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (102, 21, 'eventkey', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (103, 21, 'eventkey', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (104, 21, 'eventkey', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (105, 22, 'adminmenu', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (106, 22, 'adminmenu', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (107, 22, 'adminmenu', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (108, 22, 'adminmenu', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (109, 22, 'adminmenu', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (110, 22, 'adminmenu', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (111, 22, 'adminmenu', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (112, 22, 'adminmenu', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (113, 22, 'adminmenu', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (114, 22, 'adminmenu', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (115, 22, 'adminmenu', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (116, 23, 'editme', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (117, 23, 'editme', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (118, 25, 'guestbookkinds', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (119, 25, 'guestbookkinds', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (120, 25, 'guestbookkinds', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (121, 25, 'guestbookkinds', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (122, 25, 'guestbookkinds', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (123, 25, 'guestbookkinds', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (124, 25, 'guestbookkinds', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (125, 25, 'guestbookkinds', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (126, 26, 'guestbook', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (127, 26, 'guestbook', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (128, 26, 'guestbook', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (129, 26, 'guestbook', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (130, 26, 'guestbook', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (131, 26, 'guestbook', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (132, 26, 'guestbook', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (133, 26, 'guestbook', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (134, 26, 'guestbook', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (135, 26, 'guestbook', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (136, 28, 'adskinds', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (137, 28, 'adskinds', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (138, 28, 'adskinds', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (139, 28, 'adskinds', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (140, 28, 'adskinds', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (141, 29, 'ads', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (142, 29, 'ads', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (143, 29, 'ads', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (144, 29, 'ads', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (145, 29, 'ads', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (146, 30, 'linkkinds', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (147, 30, 'linkkinds', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (148, 30, 'linkkinds', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (149, 30, 'linkkinds', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (150, 30, 'linkkinds', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (151, 31, 'link', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (152, 31, 'link', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (153, 31, 'link', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (154, 31, 'link', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (155, 31, 'link', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (156, 31, 'link', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (157, 31, 'link', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (158, 31, 'link', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (159, 31, 'link', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (160, 31, 'link', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (161, 31, 'link', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (162, 33, 'wxautoreply', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (163, 33, 'wxautoreply', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (164, 34, 'wxmenu', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (165, 34, 'wxmenu', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (166, 35, 'wxkeywordreply', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (167, 35, 'wxkeywordreply', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (168, 35, 'wxkeywordreply', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (169, 35, 'wxkeywordreply', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (170, 35, 'wxkeywordreply', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (171, 35, 'wxkeywordreply', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (172, 35, 'wxkeywordreply', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (173, 35, 'wxkeywordreply', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (174, 35, 'wxkeywordreply', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (175, 35, 'wxkeywordreply', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (176, 35, 'wxkeywordreply', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (177, 36, 'clickreplyrule', 1, 'add', '添加');
INSERT INTO `adminmenuevent` VALUES (178, 36, 'clickreplyrule', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (179, 36, 'clickreplyrule', 3, 'del', '删除');
INSERT INTO `adminmenuevent` VALUES (180, 36, 'clickreplyrule', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (181, 36, 'clickreplyrule', 5, 'viewlist', '查看列表');
INSERT INTO `adminmenuevent` VALUES (182, 36, 'clickreplyrule', 6, 'import', '导入');
INSERT INTO `adminmenuevent` VALUES (183, 36, 'clickreplyrule', 7, 'export', '导出');
INSERT INTO `adminmenuevent` VALUES (184, 36, 'clickreplyrule', 8, 'filter', '搜索');
INSERT INTO `adminmenuevent` VALUES (185, 36, 'clickreplyrule', 9, 'batch', '批量操作');
INSERT INTO `adminmenuevent` VALUES (186, 36, 'clickreplyrule', 10, 'recycle', '回收站');
INSERT INTO `adminmenuevent` VALUES (187, 36, 'clickreplyrule', 11, 'confirm', '确认');
INSERT INTO `adminmenuevent` VALUES (188, 37, 'robots', 2, 'edit', '修改');
INSERT INTO `adminmenuevent` VALUES (189, 37, 'robots', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (190, 38, 'admincplog', 4, 'view', '查看');
INSERT INTO `adminmenuevent` VALUES (191, 38, 'admincplog', 5, 'viewlist', '查看列表');

-- ----------------------------
-- Table structure for adminroles
-- ----------------------------
DROP TABLE IF EXISTS `adminroles`;
CREATE TABLE `adminroles`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `RoleType` int(11) NOT NULL COMMENT '角色类型',
  `RoleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色名称',
  `RoleDescription` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色简单介绍',
  `IsSuperAdmin` int(11) NOT NULL COMMENT '是否是超级管理员',
  `Stars` int(11) NOT NULL COMMENT '星级',
  `NotAllowDel` int(11) NOT NULL COMMENT '是否不允许删除',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Color` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `Menus` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '管理菜单',
  `Powers` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '权限',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '管理角色' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of adminroles
-- ----------------------------
INSERT INTO `adminroles` VALUES (1, 0, '超级管理员', '系统超级管理员', 1, 5, 1, 0, '', '', '');

-- ----------------------------
-- Table structure for ads
-- ----------------------------
DROP TABLE IF EXISTS `ads`;
CREATE TABLE `ads`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `Title` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '广告标题',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '广告详情JSON',
  `KId` int(11) NOT NULL COMMENT '分类ID',
  `TId` int(11) NOT NULL COMMENT '广告代码类型：0代码、1文字广告、2图片广告、3Flash广告、4幻灯片广告、5漂浮广告、6对联浮动图片广告',
  `StartTime` datetime(0) NULL DEFAULT NULL COMMENT '起始时间',
  `EndTime` datetime(0) NULL DEFAULT NULL COMMENT '结束时间',
  `IsDisabled` enum('N','Y') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '是否禁用广告',
  `Sequence` int(11) NOT NULL COMMENT '排序，默认999',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '广告详情' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of ads
-- ----------------------------
INSERT INTO `ads` VALUES (1, '首页banner广告', '[{\"img\":\"/images/default/slide-1.jpg\",\"link\":\"/\",\"width\":1920,\"height\":686,\"alt\":\"\"},{\"img\":\"/images/default/slide-2.jpg\",\"link\":\"/\",\"width\":1920,\"height\":686,\"alt\":\"\"},{\"img\":\"/images/default/slide-3.jpg\",\"link\":\"/\",\"width\":1920,\"height\":686,\"alt\":\"\"},{\"img\":\"/images/default/slide-4.jpg\",\"link\":\"/\",\"width\":1920,\"height\":686,\"alt\":\"\"},{\"img\":\"/images/default/slide-5.jpg\",\"link\":\"/\",\"width\":1920,\"height\":686,\"alt\":\"\"}]', 1, 4, '2000-01-01 00:00:00', '2999-01-01 00:00:00', 'N', 999);
INSERT INTO `ads` VALUES (2, '移动端首页Banner', '[{\"img\":\"/images/default/slide-1.jpg\",\"link\":\"/\",\"width\":375,\"height\":185,\"alt\":\"\"},{\"img\":\"/images/default/slide-2.jpg\",\"link\":\"/\",\"width\":375,\"height\":185,\"alt\":\"\"},{\"img\":\"/images/default/slide-3.jpg\",\"link\":\"/\",\"width\":375,\"height\":185,\"alt\":\"\"},{\"img\":\"/images/default/slide-4.jpg\",\"link\":\"/\",\"width\":375,\"height\":185,\"alt\":\"\"},{\"img\":\"/images/default/slide-5.jpg\",\"link\":\"/\",\"width\":375,\"height\":185,\"alt\":\"\"}]', 1, 4, '2000-01-01 00:00:00', '2999-01-01 00:00:00', 'N', 999);

-- ----------------------------
-- Table structure for adskind
-- ----------------------------
DROP TABLE IF EXISTS `adskind`;
CREATE TABLE `adskind`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '广告类别名称',
  `KindInfo` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简单说明',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '广告类别' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of adskind
-- ----------------------------
INSERT INTO `adskind` VALUES (1, '默认广告', NULL, 0);

-- ----------------------------
-- Table structure for article
-- ----------------------------
DROP TABLE IF EXISTS `article`;
CREATE TABLE `article`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '栏目ID',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标题',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '副标题',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '内容',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模板',
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
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `Tags` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'TAG',
  `Origin` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '来源',
  `OriginURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '来源地址',
  `ItemImg` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '更多图片',
  `AuthorId` int(11) NOT NULL COMMENT '添加管理员ID',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '时间',
  `Location` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路径',
  `FilePath` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '存放目录',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '文章' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of article
-- ----------------------------
INSERT INTO `article` VALUES (1, 1, '关于我们', 'About Us', '<p>内容管理系统（英语：content management system，缩写为 CMS）是指在一个合作模式下，用于管理工作流程的一套制度。该系统可应用于手工操作中，也可以应用到计算机或网络里。作为一种中央储存器（central repository），内容管理系统可将相关内容集中储存并具有群组管理、版本控制等功能。版本控制是内容管理系统的一个主要优势。</p>\n\n<p>内容是任何类型的数字信息的结合体，可以是文本、图形图像、Web页面、业务文档、数据库表单、视频、声音、XML文件等。应该说，内容是一个比数据、文档和信息更广的概念，是对各种结构化数据、非结构化文档、信息的聚合。管理就是施加在&ldquo;内容&rdquo;对象上的一系列处理过程，包括收集、存储、审批、整理、定位、转换、分发、搜索、分析等，目的是为了使&ldquo;内容&rdquo;能够在正确的时间、以正确的形式传递到正确的地点和人。</p>\n\n<p>内容管理可以定义为：协助组织和个人，借助信息技术，实现内容的创建、储存、分享、应用、检索，并在企业个人、组织、业务、战略等诸方面产生价值的过程。而内容管理系统就是能够支撑内容管理的一种工具或一套工具的软件系统。</p>\n\n<p>内容管理系统的定义可以很狭窄，通常是指门户或商业网站的发布和管理系统；定义也可以很宽泛，个人网站系统也可归入其中。Wiki也是一种内容管理系统，Blog也算是一种内容管理系统。</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 53, 999, 0, NULL, NULL, NULL, '/images/default/slide-3.jpg', 0, NULL, NULL, NULL, '/userfiles/images/2018/20180413145904(1).png|||', 1, '2018-04-18 20:35:54', '2018-12-02 17:14:13', NULL, NULL);
INSERT INTO `article` VALUES (2, 10, '联系我们', NULL, '<p>联系我们</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 999, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, '', 1, '2018-11-15 23:58:24', '2018-12-01 22:44:45', NULL, NULL);
INSERT INTO `article` VALUES (3, 1, '企业文化', NULL, '<p>企业文化</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 999, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, '', 1, '2018-11-19 00:08:19', NULL, NULL, NULL);
INSERT INTO `article` VALUES (4, 1, '发展历程', NULL, '<p>发展历程</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14, 999, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, '', 1, '2018-11-19 00:08:28', NULL, NULL, NULL);
INSERT INTO `article` VALUES (5, 1, '组织架构', NULL, '<p>组织架构</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 13, 999, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, '', 1, '2018-11-19 00:08:38', NULL, NULL, NULL);
INSERT INTO `article` VALUES (6, 1, '荣誉证书', NULL, '<p>荣誉证书</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 999, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, '', 1, '2018-11-19 00:08:49', NULL, NULL, NULL);
INSERT INTO `article` VALUES (7, 6, 'COMCMS v0.9 正式发布，PC版前端演示', 'COMCMS v0.9 正式发布，PC版前端演示', '<p>COMCMS v0.9 正式发布，PC版前端演示</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 999, 0, NULL, NULL, NULL, '/images/default/slide-2.jpg', 0, NULL, NULL, NULL, '', 1, '2018-12-02 16:52:15', NULL, NULL, NULL);
INSERT INTO `article` VALUES (8, 7, '行业资讯行业资讯行业资讯行业资讯', NULL, '<p>行业资讯行业资讯行业资讯行业资讯</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 999, 0, NULL, NULL, NULL, '/images/default/slide-2.jpg', 0, NULL, NULL, NULL, '', 1, '2018-12-02 16:54:48', NULL, NULL, NULL);
INSERT INTO `article` VALUES (9, 6, 'COMCMS v0.9 正式发布，PC版前端演示2', NULL, '<p>COMCMS v0.9 正式发布，PC版前端演示2</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 999, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, '', 1, '2018-12-02 16:55:39', NULL, NULL, NULL);
INSERT INTO `article` VALUES (10, 6, 'COMCMS v0.9 正式发布，PC版前端演示3', NULL, '<p>COMCMS v0.9 正式发布，PC版前端演示3</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 999, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, '', 1, '2018-12-02 16:56:18', NULL, NULL, NULL);
INSERT INTO `article` VALUES (11, 6, 'COMCMS v0.9 正式发布，PC版前端演示4', NULL, '<p>COMCMS v0.9 正式发布，PC版前端演示4</p>\n', NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 999, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL, '', 1, '2018-12-02 16:56:27', '2019-10-30 17:10:39', NULL, NULL);

-- ----------------------------
-- Table structure for articlecategory
-- ----------------------------
DROP TABLE IF EXISTS `articlecategory`;
CREATE TABLE `articlecategory`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目名称',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目副标题',
  `KindTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目标题，填写则在浏览器替换此标题',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模板',
  `DetailTemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详情模板',
  `KindDomain` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别域名（保留）',
  `IsList` int(11) NOT NULL COMMENT '是否为列表页面',
  `PageSize` int(11) NOT NULL COMMENT '每页显示数量',
  `PId` int(11) NOT NULL COMMENT '上级ID',
  `Level` int(11) NOT NULL COMMENT '级别',
  `Location` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路径',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsShowSubDetail` int(11) NOT NULL COMMENT '是否显示下级栏目内容',
  `CatalogId` int(11) NOT NULL COMMENT '模型ID',
  `Counts` int(11) NOT NULL COMMENT '详情数量，缓存',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `KindInfo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '栏目详细介绍',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `FilePath` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '目录路径',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '文章栏目' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of articlecategory
-- ----------------------------
INSERT INTO `articlecategory` VALUES (1, '关于我们', NULL, NULL, NULL, NULL, '1', NULL, 'Index.cshtml', 'Detail_About.cshtml', NULL, 1, 15, 0, 0, '0,', 0, 0, 0, 0, 0, 1, 0, 5, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (2, '新闻资讯', NULL, NULL, NULL, NULL, '/Article/Index/6', NULL, 'Index.cshtml', 'Detail.cshtml', NULL, 1, 15, 0, 0, '0,', 0, 0, 0, 0, 0, 1, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (3, '项目案例', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 15, 0, 0, '0,', 0, 0, 0, 0, 0, 1, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (4, '用户服务', NULL, NULL, NULL, NULL, '/Article/Index/8', NULL, 'Index.cshtml', 'Detail.cshtml', NULL, 1, 15, 0, 0, '0,', 0, 0, 0, 0, 0, 1, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (5, '联系我们', NULL, NULL, NULL, NULL, '/Article/Index/10', NULL, 'Index.cshtml', 'Detail.cshtml', NULL, 1, 15, 0, 0, '0,', 0, 0, 0, 0, 0, 1, 0, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (6, '公司新闻', NULL, NULL, NULL, NULL, NULL, NULL, 'Index.cshtml', 'Detail_News.cshtml', NULL, 1, 2, 2, 1, '0,2,', 0, 0, 0, 0, 0, 1, 0, 4, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (7, '行业资讯', NULL, NULL, NULL, NULL, NULL, NULL, 'Index.cshtml', 'Detail_News.cshtml', NULL, 1, 15, 2, 1, '0,2,', 0, 0, 0, 0, 0, 1, 0, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (8, '产品使用', NULL, NULL, NULL, NULL, NULL, NULL, 'Index.cshtml', 'Detail_News.cshtml', NULL, 1, 15, 4, 1, '0,4,', 0, 0, 0, 0, 0, 1, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (9, '产品下载', NULL, NULL, NULL, NULL, NULL, NULL, 'Index.cshtml', 'Detail_News.cshtml', NULL, 1, 15, 4, 1, '0,4,', 0, 0, 0, 0, 0, 1, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (10, '联系方式', NULL, NULL, NULL, NULL, '2', NULL, 'Index.cshtml', 'Detail_Contact.cshtml', NULL, 1, 15, 5, 1, '0,5,', 0, 0, 0, 0, 0, 1, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (11, '在线留言', NULL, NULL, NULL, NULL, NULL, NULL, 'Index_Guestbook.cshtml', 'Detail.cshtml', NULL, 0, 15, 5, 1, '0,5,', 0, 0, 0, 0, 0, 1, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `articlecategory` VALUES (12, '常见问题', NULL, NULL, NULL, NULL, NULL, NULL, 'Index.cshtml', 'Detail_News.cshtml', NULL, 1, 15, 4, 1, '0,4,', 0, 0, 0, 0, 0, 1, 0, 0, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);

-- ----------------------------
-- Table structure for balancechangelog
-- ----------------------------
DROP TABLE IF EXISTS `balancechangelog`;
CREATE TABLE `balancechangelog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `AdminId` int(11) NOT NULL COMMENT '管理员ID',
  `UserName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '时间',
  `IP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '登录IP',
  `Actions` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '记录',
  `Reward` decimal(10, 0) NOT NULL COMMENT '总积分',
  `BeforChange` decimal(10, 0) NOT NULL COMMENT '总积分',
  `AfterChange` decimal(10, 0) NOT NULL COMMENT '总积分',
  `LogDetails` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详细记录',
  `TypeId` int(11) NOT NULL COMMENT '类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户余额变化记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for category
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目名称',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目副标题',
  `KindTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目标题，填写则在浏览器替换此标题',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模板',
  `DetailTemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详情模板',
  `KindDomain` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别域名（保留）',
  `IsList` int(11) NOT NULL COMMENT '是否为列表页面',
  `PageSize` int(11) NOT NULL COMMENT '每页显示数量',
  `PId` int(11) NOT NULL COMMENT '上级ID',
  `Level` int(11) NOT NULL COMMENT '级别',
  `Location` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路径',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsShowSubDetail` int(11) NOT NULL COMMENT '是否显示下级栏目内容',
  `CatalogId` int(11) NOT NULL COMMENT '模型ID',
  `Counts` int(11) NOT NULL COMMENT '详情数量，缓存',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `KindInfo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '栏目详细介绍',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `FilePath` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '目录路径',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '商品栏目' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of category
-- ----------------------------
INSERT INTO `category` VALUES (1, '网站系统', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 15, 0, 0, '0,', 0, 0, 0, 0, 0, 1, 0, 2, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `category` VALUES (2, '微商城系统', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 15, 0, 0, '0,', 0, 0, 0, 0, 0, 1, 0, 2, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);
INSERT INTO `category` VALUES (3, '小程序系统', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 15, 0, 0, '0,', 0, 0, 0, 0, 0, 1, 0, 1, 0, NULL, NULL, NULL, NULL, NULL, 0, NULL);

-- ----------------------------
-- Table structure for challenge
-- ----------------------------
DROP TABLE IF EXISTS `challenge`;
CREATE TABLE `challenge`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '名称',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '副标题',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsEnd` int(11) NOT NULL COMMENT '是否会员栏目',
  `JoinUserCounts` int(11) NOT NULL COMMENT '参加人数',
  `CompleteUserCounts` int(11) NOT NULL COMMENT '完成人数',
  `CompleteCounts` int(11) NOT NULL COMMENT '需要完成次数',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `DaySteps` int(11) NOT NULL COMMENT '日完成步数',
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '栏目详细介绍',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `NeedCredits` decimal(19, 4) NOT NULL COMMENT '参加积分',
  `RewardCredits` decimal(19, 4) NOT NULL COMMENT '奖励积分',
  `ActivityType` int(11) NOT NULL COMMENT '活动类型',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `StartTime` datetime(0) NULL DEFAULT NULL COMMENT '开始时间',
  `EndTime` datetime(0) NULL DEFAULT NULL COMMENT '结束时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '挑战' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of challenge
-- ----------------------------
INSERT INTO `challenge` VALUES (1, '7天挑战', NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 7, 999, 20000, NULL, NULL, NULL, '/userfiles/images/2018/2746846066a50e13.jpg', 10.0000, 50.0000, 0, '2018-10-31 20:41:42', '2018-11-01 00:00:00', '2018-11-07 00:00:00');

-- ----------------------------
-- Table structure for challengeapply
-- ----------------------------
DROP TABLE IF EXISTS `challengeapply`;
CREATE TABLE `challengeapply`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `NeedCredits` int(11) NOT NULL COMMENT '使用积分',
  `ChallengeId` int(11) NOT NULL COMMENT '挑战Id',
  `TargetCounts` int(11) NOT NULL COMMENT '目标总次数',
  `CompleteCounts` int(11) NOT NULL COMMENT '完成总次数',
  `DaySteps` int(11) NOT NULL COMMENT '我的日完成步数',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `StartTime` datetime(0) NULL DEFAULT NULL COMMENT '开始时间',
  `EndTime` datetime(0) NULL DEFAULT NULL COMMENT '结束时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '挑战报名' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for challengesuccesslog
-- ----------------------------
DROP TABLE IF EXISTS `challengesuccesslog`;
CREATE TABLE `challengesuccesslog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `ChallengeId` int(11) NOT NULL COMMENT '挑战Id',
  `DaySteps` int(11) NOT NULL COMMENT '我的日完成步数',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `CompleteDay` datetime(0) NULL DEFAULT NULL COMMENT '完成日期，只记录日期',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '挑战成功记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for config
-- ----------------------------
DROP TABLE IF EXISTS `config`;
CREATE TABLE `config`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `SiteName` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '站点名称',
  `SiteUrl` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '站点URL',
  `SiteLogo` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '站点LOGO',
  `ICP` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'ICP备案',
  `SiteEmail` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系我们Email',
  `SiteTel` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '网站电话',
  `Copyright` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '版权所有',
  `IsCloseSite` enum('N','Y') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '是否关闭网站',
  `CloseReason` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '关闭原因',
  `CountScript` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '统计代码',
  `WeiboQRCode` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微博二维码',
  `WinxinQRCode` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信二维码',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '关键字',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `IndexTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '首页标题',
  `IsRewrite` int(11) NOT NULL COMMENT '是否URL地址重写',
  `SearchMinTime` int(11) NOT NULL COMMENT '搜索最小时间间距 秒',
  `OnlineQQ` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '在线QQ',
  `OnlineSkype` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '在线Skype',
  `OnlineWangWang` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '在线阿里旺旺',
  `SkinName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '站点URL',
  `OfficialName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '公众号名称',
  `OfficialDecsription` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '公众号介绍',
  `OfficialOriginalId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '公众号原始ID',
  `WexinAccount` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信名称',
  `Token` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'Token',
  `AppId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'AppId',
  `AppSecret` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'AppSecret',
  `FllowTipPageURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '引导关注素材地址',
  `OfficialType` int(11) NOT NULL COMMENT '公众号类型：0普通订阅号  1普通服务号  2认证订阅号  3认证服务号 4企业号 5认证企业号',
  `EncodingAESKey` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'EncodingAESKey',
  `DEType` int(11) NOT NULL COMMENT '解密方式0,明文',
  `OfficialQRCode` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '公众号二维码地址',
  `OfficialImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '公众号头像',
  `LastUpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '最后更新时间',
  `LastCacheTime` datetime(0) NULL DEFAULT NULL COMMENT '最后缓存时间',
  `MCHId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信商家MCHId',
  `MCHKey` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信商家key',
  `DefaultFare` decimal(10, 0) NOT NULL COMMENT '默认运费',
  `MaxFreeFare` decimal(10, 0) NOT NULL COMMENT '最大免运费金额',
  `WXAppId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信小程序AppId',
  `WXAppSecret` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信小程序AppSecret',
  `IsResetData` int(11) NOT NULL COMMENT '小程序首页是否显示清除数据按钮',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '系统配置' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of config
-- ----------------------------
INSERT INTO `config` VALUES (1, 'COMCMS', 'http://www.comcms.com', '/images/logo.png', '', NULL, NULL, NULL, 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, '2018-12-05 11:49:59', '2018-12-05 11:49:59', NULL, NULL, 0, 0, NULL, NULL, 0);

-- ----------------------------
-- Table structure for coupon
-- ----------------------------
DROP TABLE IF EXISTS `coupon`;
CREATE TABLE `coupon`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `ItemNO` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '券号',
  `KId` int(11) NOT NULL COMMENT '类别，0默认没限制',
  `CouponType` int(11) NOT NULL COMMENT '优惠券类型，0 现金用券，1打折券',
  `DiscuountRates` decimal(10, 0) NOT NULL COMMENT '打折率，只有是打折券才有用',
  `IsLimit` int(11) NOT NULL COMMENT '是否有类别限制，0 无限制；1 是类别限制，2是商品限制',
  `Price` decimal(10, 0) NOT NULL COMMENT '面额',
  `NeedPrice` decimal(10, 0) NOT NULL COMMENT '需要消费面额',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `StartTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `EndTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `TotalCount` int(11) NOT NULL COMMENT '最大领取数量',
  `TotalUseCount` int(11) NOT NULL COMMENT '已使用次数',
  `SpreadUId` int(11) NOT NULL COMMENT '推广员ID，可选',
  `UId` int(11) NOT NULL COMMENT '用户Id',
  `MyType` int(11) NOT NULL COMMENT '可使用类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '优惠券' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for couponkind
-- ----------------------------
DROP TABLE IF EXISTS `couponkind`;
CREATE TABLE `couponkind`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `IsLimit` int(11) NOT NULL COMMENT '是否有类别限制，0 无限制；1 是类别限制，2是商品限制',
  `KindName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称',
  `CouponType` int(11) NOT NULL COMMENT '优惠券类型，0 现金用券，1打折券',
  `KIds` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '类别按逗号分开',
  `PIds` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '商品ID，按逗号分开',
  `KindNote` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别说明',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `MyType` int(11) NOT NULL COMMENT '可使用类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '优惠券分类' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for couponuselog
-- ----------------------------
DROP TABLE IF EXISTS `couponuselog`;
CREATE TABLE `couponuselog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `CouponId` int(11) NOT NULL COMMENT '优惠券ID',
  `ItemNO` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '优惠券编号',
  `OrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单号',
  `OrderId` int(11) NOT NULL COMMENT '订单编号',
  `UserName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `Title` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单名称',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '时间',
  `IP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '登录IP',
  `Actions` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '记录详情',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '优惠券使用记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for food
-- ----------------------------
DROP TABLE IF EXISTS `food`;
CREATE TABLE `food`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '栏目ID',
  `BId` int(11) NOT NULL COMMENT '品牌ID',
  `ShopId` int(11) NOT NULL COMMENT '店铺ID',
  `CId` int(11) NOT NULL COMMENT '颜色ID',
  `SupportId` int(11) NOT NULL COMMENT '供货商ID',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标题',
  `ItemNO` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编号',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '副标题',
  `Unit` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品单位',
  `Spec` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品规格尺寸',
  `Color` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `Weight` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '重量',
  `Price` decimal(10, 0) NOT NULL COMMENT '价格',
  `MarketPrice` decimal(10, 0) NOT NULL COMMENT '市场价格',
  `SpecialPrice` decimal(10, 0) NOT NULL COMMENT '特价，如有特价，以特价为准',
  `Fare` decimal(10, 0) NOT NULL COMMENT '运费',
  `Discount` decimal(10, 0) NOT NULL COMMENT '折扣',
  `Material` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '材料',
  `Front` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '封面',
  `Credits` int(11) NOT NULL COMMENT '积分',
  `Stock` int(11) NOT NULL COMMENT '库存',
  `WarnStock` int(11) NOT NULL COMMENT '警告库存',
  `IsSubProduct` int(11) NOT NULL COMMENT '是否为子商品',
  `PPId` int(11) NOT NULL COMMENT '父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '内容',
  `Parameters` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '商品参数',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模板',
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
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `Tags` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'TAG',
  `ItemImg` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '更多图片',
  `Service` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '售后服务',
  `AuthorId` int(11) NOT NULL COMMENT '添加管理员ID',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '时间',
  `Location` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路径',
  `FilePath` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '存放目录',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '快餐' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for guestbook
-- ----------------------------
DROP TABLE IF EXISTS `guestbook`;
CREATE TABLE `guestbook`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '分类ID',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '留言标题',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '详细介绍',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `IsVerify` int(11) NOT NULL COMMENT '是否审核通过',
  `IsRead` int(11) NOT NULL COMMENT '是否阅读',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户IP',
  `Nickname` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '昵称',
  `Email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `Tel` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话',
  `QQ` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'QQ',
  `Skype` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'Skype',
  `HomePage` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '主页',
  `Address` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址',
  `Company` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '公司',
  `ReplyTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '回复标题',
  `ReplyContent` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '回复的详情',
  `ReplyAddTime` datetime(0) NULL DEFAULT NULL COMMENT '回复时间',
  `ReplyIP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户回复IP',
  `ReplyAdminId` int(11) NOT NULL COMMENT '用户的管理员ID',
  `ReplyNickName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '回复者昵称',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '留言详情' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for guestbookcategory
-- ----------------------------
DROP TABLE IF EXISTS `guestbookcategory`;
CREATE TABLE `guestbookcategory`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称',
  `KindInfo` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简单说明',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '分类图片',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '留言分类' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of guestbookcategory
-- ----------------------------
INSERT INTO `guestbookcategory` VALUES (1, '默认类别', NULL, NULL, 0);

-- ----------------------------
-- Table structure for hotelroom
-- ----------------------------
DROP TABLE IF EXISTS `hotelroom`;
CREATE TABLE `hotelroom`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '栏目ID',
  `BId` int(11) NOT NULL COMMENT '品牌ID',
  `ShopId` int(11) NOT NULL COMMENT '店铺ID',
  `CId` int(11) NOT NULL COMMENT '颜色ID',
  `SupportId` int(11) NOT NULL COMMENT '供货商ID',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标题',
  `ItemNO` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编号',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '副标题',
  `Unit` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品单位',
  `Spec` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品规格尺寸',
  `Color` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `Weight` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '重量',
  `Price` decimal(10, 0) NOT NULL COMMENT '价格',
  `MarketPrice` decimal(10, 0) NOT NULL COMMENT '市场价格',
  `SpecialPrice` decimal(10, 0) NOT NULL COMMENT '特价，如有特价，以特价为准',
  `Fare` decimal(10, 0) NOT NULL COMMENT '运费',
  `Discount` decimal(10, 0) NOT NULL COMMENT '折扣',
  `Material` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '材料',
  `Front` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '封面',
  `Credits` int(11) NOT NULL COMMENT '积分',
  `Stock` int(11) NOT NULL COMMENT '库存',
  `WarnStock` int(11) NOT NULL COMMENT '警告库存',
  `IsSubProduct` int(11) NOT NULL COMMENT '是否为子商品',
  `PPId` int(11) NOT NULL COMMENT '父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '内容',
  `Parameters` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '商品参数',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模板',
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
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `Tags` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'TAG',
  `ItemImg` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '更多图片',
  `Service` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '售后服务',
  `AuthorId` int(11) NOT NULL COMMENT '添加管理员ID',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '时间',
  `Location` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路径',
  `FilePath` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '存放目录',
  `PriceList` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '日期价格',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '酒店房间' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for link
-- ----------------------------
DROP TABLE IF EXISTS `link`;
CREATE TABLE `link`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '分类ID',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '站点标题',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '站点连接',
  `Info` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `Logo` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '站点LOGO',
  `IsHide` enum('N','Y') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '是否隐藏友情链接',
  `Sequence` int(11) NOT NULL COMMENT '排序，默认999',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '友情连接详情' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of link
-- ----------------------------
INSERT INTO `link` VALUES (1, 1, 'Google', 'https://www.google.com', NULL, '/userfiles/images/2018/46704eae13e45ead.png', 'N', 999);
INSERT INTO `link` VALUES (2, 1, '谷歌', 'https://www.google.com', NULL, '/userfiles/images/2018/46704eae13e45ead.png', 'N', 999);

-- ----------------------------
-- Table structure for linkkind
-- ----------------------------
DROP TABLE IF EXISTS `linkkind`;
CREATE TABLE `linkkind`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称',
  `KindInfo` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '简单说明',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '分类图片',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '友情链接分类' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of linkkind
-- ----------------------------
INSERT INTO `linkkind` VALUES (1, '友情链接', NULL, NULL, 0);

-- ----------------------------
-- Table structure for member
-- ----------------------------
DROP TABLE IF EXISTS `member`;
CREATE TABLE `member`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UserName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `PassWord` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码',
  `Salt` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '盐值',
  `RealName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '姓名',
  `Tel` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机',
  `Email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮件',
  `RoleId` int(11) NOT NULL COMMENT '会员组，代理级别',
  `LastLoginTime` datetime(0) NULL DEFAULT NULL COMMENT '最后登录时间',
  `LastLoginIP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上次登录IP',
  `ThisLoginTime` datetime(0) NULL DEFAULT NULL COMMENT '本次登录时间',
  `ThisLoginIP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '本次登录IP',
  `IsLock` int(11) NOT NULL COMMENT '是否是锁定',
  `Nickname` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '昵称',
  `UserImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '头像',
  `Sex` int(11) NOT NULL COMMENT '性别 0 保密 1 男 2女',
  `Birthday` datetime(0) NULL DEFAULT NULL COMMENT '生日',
  `Phone` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话',
  `Fax` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '传真',
  `QQ` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'QQ',
  `Weixin` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信',
  `Alipay` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '支付宝',
  `Skype` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'skype',
  `Homepage` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '主页',
  `Company` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '公司',
  `IDNO` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '身份证',
  `Country` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '国家',
  `Province` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '省',
  `City` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '市',
  `District` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '区',
  `Address` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详细地址',
  `Postcode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮政编码',
  `RegIP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '注册IP',
  `RegTime` datetime(0) NULL DEFAULT NULL COMMENT '注册时间',
  `LoginCount` int(11) NOT NULL COMMENT '登录次数',
  `RndNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '随机字符',
  `RePassWordTime` datetime(0) NULL DEFAULT NULL COMMENT '找回密码时间',
  `Question` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '保密问题',
  `Answer` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '保密答案',
  `Balance` decimal(10, 0) NOT NULL COMMENT '可用余额',
  `GiftBalance` decimal(10, 0) NOT NULL COMMENT '赠送余额',
  `Rebate` decimal(10, 0) NOT NULL COMMENT '返利，分销提成',
  `Bank` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '银行',
  `BankCardNO` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '银行卡号',
  `BankBranch` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '支行名称',
  `BankRealname` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '银行卡姓名',
  `YearsPerformance` decimal(10, 0) NOT NULL COMMENT '年业务量',
  `ExtCredits1` decimal(10, 0) NOT NULL COMMENT '备用积分1',
  `ExtCredits2` decimal(10, 0) NOT NULL COMMENT '备用积分2',
  `ExtCredits3` decimal(10, 0) NOT NULL COMMENT '备用积分3',
  `ExtCredits4` decimal(10, 0) NOT NULL COMMENT '备用积分4',
  `ExtCredits5` decimal(10, 0) NOT NULL COMMENT '备用积分5',
  `TotalCredits` decimal(10, 0) NOT NULL COMMENT '总积分',
  `Parent` int(11) NOT NULL COMMENT '父级用户ID',
  `Grandfather` int(11) NOT NULL COMMENT '爷级用户ID',
  `IsSellers` int(11) NOT NULL COMMENT '是否是分销商',
  `IsVerifySellers` int(11) NOT NULL COMMENT '是否已经认证的分销商，缴纳费用',
  `WeixinOpenId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信公众号OpenId',
  `WeixinAppOpenId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信OpenId',
  `QQOpenId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'QQ OpenId',
  `WeiboOpenId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微博OpenId',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for memberaddress
-- ----------------------------
DROP TABLE IF EXISTS `memberaddress`;
CREATE TABLE `memberaddress`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `RealName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '姓名',
  `Tel` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机',
  `Email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮件',
  `IsDefault` enum('N','Y') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '是否是默认',
  `Phone` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话',
  `Company` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '公司',
  `Country` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '国家',
  `Province` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '省',
  `City` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '市',
  `District` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '区',
  `Address` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详细地址',
  `Postcode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮政编码',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户地址' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for membercoupon
-- ----------------------------
DROP TABLE IF EXISTS `membercoupon`;
CREATE TABLE `membercoupon`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `ItemNO` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '券号',
  `CouponId` int(11) NOT NULL COMMENT '优惠券ID',
  `CouponType` int(11) NOT NULL COMMENT '优惠券类型，0 现金用券，1打折券',
  `DiscuountRates` decimal(10, 0) NOT NULL COMMENT '打折率，只有是打折券才有用',
  `IsLimit` int(11) NOT NULL COMMENT '是否有类别限制，0 无限制；1 是类别限制，2是商品限制',
  `Price` decimal(10, 0) NOT NULL COMMENT '面额',
  `NeedPrice` decimal(10, 0) NOT NULL COMMENT '需要消费面额',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `StartTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `EndTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `IsUse` int(11) NOT NULL COMMENT '是否使用',
  `UseTime` datetime(0) NULL DEFAULT NULL COMMENT '使用时间',
  `OrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单号（使用后）',
  `OrderId` int(11) NOT NULL COMMENT '订单编号（使用后）',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户优惠券' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for memberlog
-- ----------------------------
DROP TABLE IF EXISTS `memberlog`;
CREATE TABLE `memberlog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '管理员ID',
  `GUID` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '唯一ID',
  `UserName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `PassWord` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码',
  `LoginTime` datetime(0) NULL DEFAULT NULL COMMENT '登录时间',
  `LoginIP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '登录IP',
  `IsLoginOK` int(11) NOT NULL COMMENT '是否登录成功',
  `Actions` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '记录',
  `LastUpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '登录时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户日志表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for memberroles
-- ----------------------------
DROP TABLE IF EXISTS `memberroles`;
CREATE TABLE `memberroles`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `RoleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色名称',
  `RoleDescription` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色简单介绍',
  `Stars` int(11) NOT NULL COMMENT '星级',
  `NotAllowDel` int(11) NOT NULL COMMENT '是否不允许删除',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Color` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `CashBack` decimal(10, 0) NOT NULL COMMENT '返现百分比',
  `ParentCashBack` decimal(10, 0) NOT NULL COMMENT '父级返现百分比',
  `GrandfatherCashBack` decimal(10, 0) NOT NULL COMMENT '爷级返现百分比',
  `IsDefault` int(11) NOT NULL COMMENT '是否是默认角色',
  `IsHalved` int(11) NOT NULL COMMENT '超过级别是否减半',
  `HalvedParentCashBack` decimal(10, 0) NOT NULL COMMENT '超过级别父级返现百分比',
  `HalvedGrandfatherCashBack` decimal(10, 0) NOT NULL COMMENT '超过级别爷级返现百分比',
  `YearsPerformance` decimal(10, 0) NOT NULL COMMENT '年业务量',
  `IsSellers` int(11) NOT NULL COMMENT '是否是分销商',
  `JoinPrice` decimal(10, 0) NOT NULL COMMENT '加入分销商价格',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户角色' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for onlinepayorder
-- ----------------------------
DROP TABLE IF EXISTS `onlinepayorder`;
CREATE TABLE `onlinepayorder`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `PayOrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '在线支付订单号，唯一',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单号',
  `PayId` int(11) NOT NULL COMMENT '支付方式ID',
  `PayType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '付款方式',
  `PayTypeNotes` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '支付备注',
  `Actions` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '日志记录',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `TotalQty` int(11) NOT NULL COMMENT '总数量',
  `TotalPrice` decimal(10, 0) NOT NULL COMMENT '总价格',
  `PaymentStatus` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '支付状态：未支付、已支付、已退款',
  `PaymentNotes` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '支付备注',
  `IsOK` int(11) NOT NULL COMMENT '是否完成',
  `IP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '下单IP',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `ReceiveTime` datetime(0) NULL DEFAULT NULL COMMENT '回传时间',
  `TypeId` int(11) NOT NULL COMMENT '支付的类型',
  `MyType` int(11) NOT NULL COMMENT '系统类型',
  `Title` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单标题',
  `OutTradeNo` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '支付成功流水号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '在线支付订单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for order
-- ----------------------------
DROP TABLE IF EXISTS `order`;
CREATE TABLE `order`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `OrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '下单用户名',
  `ShopId` int(11) NOT NULL COMMENT '商户ID',
  `Credit` int(11) NOT NULL COMMENT '积分',
  `TotalQty` int(11) NOT NULL COMMENT '总数量',
  `TotalPrice` decimal(10, 0) NOT NULL COMMENT '总价格',
  `Discount` decimal(10, 0) NOT NULL COMMENT '折扣',
  `Fare` decimal(10, 0) NOT NULL COMMENT '运费',
  `TotalTax` decimal(10, 0) NOT NULL COMMENT '税',
  `TotalPay` decimal(10, 0) NOT NULL COMMENT '总支付价格',
  `BackCredits` int(11) NOT NULL COMMENT '返现积分',
  `RealName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '姓名',
  `Country` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '国家',
  `Province` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '省份',
  `City` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '城市',
  `District` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '区',
  `Address` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详细地址',
  `PostCode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮编',
  `Tel` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机',
  `Mobile` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机',
  `Email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `Notes` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `AdminNotes` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '管理员备注',
  `Pic` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '图片',
  `DeliverId` int(11) NOT NULL COMMENT '配送方式ID',
  `DeliverType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '配送方式名称',
  `DeliverNO` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '运单号',
  `DeliverNotes` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '配送备注',
  `PayId` int(11) NOT NULL COMMENT '支付方式ID',
  `PayType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '付款方式',
  `PayTypeNotes` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '支付备注',
  `OrderStatus` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单状态：未确认、已确认、已完成、已取消',
  `PaymentStatus` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '支付状态：未支付、已支付、已退款',
  `DeliverStatus` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '配送状态：未配送、配货中、已配送、已收到、退货中、已退货',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `IP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '下单IP',
  `IsInvoice` int(11) NOT NULL COMMENT '是否需要发票',
  `InvoiceCompanyName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '抬头',
  `InvoiceCompanyID` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '纳税人识别号',
  `InvoiceType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '发票内容',
  `InvoiceNote` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '发票备注',
  `IsRead` int(11) NOT NULL COMMENT '是否未读',
  `IsEnd` int(11) NOT NULL COMMENT '是否结束',
  `EndTime` datetime(0) NULL DEFAULT NULL COMMENT '结束时间',
  `IsOk` int(11) NOT NULL COMMENT '订单是否顺利完成',
  `IsComment` int(11) NOT NULL COMMENT '订单已经评论',
  `Flag1` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '预留字段1',
  `Flag2` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '预留字段2',
  `Flag3` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '预留字段3',
  `Title` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单名称',
  `LastModTime` datetime(0) NULL DEFAULT NULL COMMENT '最后操作时间',
  `OrderType` int(11) NOT NULL COMMENT '订单类型，0为商品订单',
  `MyType` int(11) NOT NULL COMMENT '系统类型',
  `OutTradeNo` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '支付成功流水号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '订单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for orderdetail
-- ----------------------------
DROP TABLE IF EXISTS `orderdetail`;
CREATE TABLE `orderdetail`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单号',
  `ShopId` int(11) NOT NULL COMMENT '商户ID',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `PId` int(11) NOT NULL COMMENT '商品ID',
  `TypeId` int(11) NOT NULL COMMENT '类型ID',
  `PriceId` int(11) NOT NULL COMMENT '价格ID',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品名称',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品名称',
  `Attr` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品属性',
  `Color` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品颜色',
  `Spec` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '规格',
  `ItemNO` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编号',
  `Qty` int(11) NOT NULL COMMENT '数量',
  `Price` decimal(10, 0) NOT NULL COMMENT '商品价格',
  `MarketPrice` decimal(10, 0) NOT NULL COMMENT '商品市场价格',
  `SpecialPrice` decimal(10, 0) NOT NULL COMMENT '商品特价',
  `Discount` decimal(10, 0) NOT NULL COMMENT '商品优惠',
  `Tax` decimal(10, 0) NOT NULL COMMENT '商品税',
  `Credit` int(11) NOT NULL COMMENT '积分',
  `BackCredits` int(11) NOT NULL COMMENT '返现积分',
  `IsOK` int(11) NOT NULL COMMENT '是否完成',
  `IsComment` int(11) NOT NULL COMMENT '是否已经评论',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `CheckInDate` datetime(0) NULL DEFAULT NULL COMMENT '入住日期',
  `LeaveDate` datetime(0) NULL DEFAULT NULL COMMENT '离开日期',
  `MyType` int(11) NOT NULL COMMENT '系统类型',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '订单详情' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for orderlog
-- ----------------------------
DROP TABLE IF EXISTS `orderlog`;
CREATE TABLE `orderlog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单号',
  `Actions` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '日志记录',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '订单日志' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for otherconfig
-- ----------------------------
DROP TABLE IF EXISTS `otherconfig`;
CREATE TABLE `otherconfig`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `ConfigName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '配置名称',
  `ConfigValue` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '配置值 JSON',
  `LastUpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '其他配置' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of otherconfig
-- ----------------------------
INSERT INTO `otherconfig` VALUES (1, 'attach', '{\"SiteId\":0,\"AttachPatch\":\"userfiles\",\"SaveType\":0,\"IsCreateThum\":0,\"ThumQty\":80,\"ThumMaxWidth\":200,\"ThumMaxHeight\":200,\"IsWaterMark\":0,\"WaterMarkType\":0,\"WaterMarkMinWidth\":400,\"WaterMarkMinHeight\":400,\"WaterMarkImg\":null,\"WaterMarkText\":\"\",\"WaterMarkTextColor\":\"#0FF\",\"WaterMarkPlace\":9,\"WaterMarkQty\":80,\"WaterMarkDiaphaneity\":80,\"IsRandomFileName\":1,\"ImgMaxWidth\":1920,\"ImgMaxHeight\":2000}', '2018-04-18 20:36:55');
INSERT INTO `otherconfig` VALUES (2, 'smtp', '{\"SiteId\":0,\"SmtpEmail\":\"\",\"SmtpHost\":\"\",\"SmtpProt\":\"25\",\"SmtpEmailPwd\":\"\",\"PostUserName\":\"\"}', '2018-04-16 00:55:57');

-- ----------------------------
-- Table structure for product
-- ----------------------------
DROP TABLE IF EXISTS `product`;
CREATE TABLE `product`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KId` int(11) NOT NULL COMMENT '栏目ID',
  `BId` int(11) NOT NULL COMMENT '品牌ID',
  `ShopId` int(11) NOT NULL COMMENT '店铺ID',
  `CId` int(11) NOT NULL COMMENT '颜色ID',
  `SupportId` int(11) NOT NULL COMMENT '供货商ID',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标题',
  `ItemNO` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编号',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '副标题',
  `Unit` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品单位',
  `Spec` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '商品规格尺寸',
  `Color` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '颜色',
  `Weight` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '重量',
  `Price` decimal(10, 0) NOT NULL COMMENT '价格',
  `MarketPrice` decimal(10, 0) NOT NULL COMMENT '市场价格',
  `SpecialPrice` decimal(10, 0) NOT NULL COMMENT '特价，如有特价，以特价为准',
  `Fare` decimal(10, 0) NOT NULL COMMENT '运费',
  `Discount` decimal(10, 0) NOT NULL COMMENT '折扣',
  `Material` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '材料',
  `Front` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '封面',
  `Credits` int(11) NOT NULL COMMENT '积分',
  `Stock` int(11) NOT NULL COMMENT '库存',
  `WarnStock` int(11) NOT NULL COMMENT '警告库存',
  `IsSubProduct` int(11) NOT NULL COMMENT '是否为子商品',
  `PPId` int(11) NOT NULL COMMENT '父商品ID。如果为子商品，则需要填写父商品ID。实现多颜色功能',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '内容',
  `Parameters` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '商品参数',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模板',
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
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  `Tags` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'TAG',
  `ItemImg` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '更多图片',
  `Service` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '售后服务',
  `AuthorId` int(11) NOT NULL COMMENT '添加管理员ID',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '时间',
  `Location` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路径',
  `FilePath` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '存放目录',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '商品' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of product
-- ----------------------------
INSERT INTO `product` VALUES (1, 1, 0, 0, 0, 0, 'CMS系统', NULL, NULL, NULL, NULL, NULL, NULL, 5000, 6000, 0, 0, 0, NULL, NULL, 0, 0, 0, 0, 0, '<p>CMS系统</p>\n\n<p>CMS系统</p>\n\n<p>CMS系统</p>\n\n<p>CMS系统</p>\n', NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 999, 0, NULL, NULL, NULL, '/images/default/slide-4.jpg', 0, NULL, '', NULL, 1, '2018-12-02 17:17:53', NULL, NULL, NULL);
INSERT INTO `product` VALUES (2, 2, 0, 0, 0, 0, 'PC+手机商城系统', NULL, NULL, NULL, NULL, NULL, NULL, 18000, 0, 0, 0, 0, NULL, NULL, 0, 0, 0, 0, 0, '<p>PC+手机商城系统</p>\n', NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 999, 0, NULL, NULL, NULL, '/images/default/slide-5.jpg', 0, NULL, '', NULL, 1, '2018-12-02 17:21:43', NULL, NULL, NULL);
INSERT INTO `product` VALUES (3, 2, 0, 0, 0, 0, '微商城Shop系统', NULL, NULL, NULL, NULL, NULL, NULL, 25000, 0, 0, 0, 0, NULL, NULL, 0, 0, 0, 0, 0, '<p>微商城Shop系统</p>\n', NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 999, 0, NULL, NULL, NULL, '/images/default/slide-3.jpg', 0, NULL, '', NULL, 1, '2018-12-02 17:22:19', NULL, NULL, NULL);
INSERT INTO `product` VALUES (4, 3, 0, 0, 0, 0, '小程序商城系统', NULL, NULL, NULL, NULL, NULL, NULL, 18000, 0, 0, 0, 0, NULL, NULL, 0, 0, 0, 0, 0, '<p>小程序商城系统</p>\n', NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 999, 0, NULL, NULL, NULL, '/images/default/slide-2.jpg', 0, NULL, '', NULL, 1, '2018-12-02 17:23:02', NULL, NULL, NULL);
INSERT INTO `product` VALUES (5, 1, 0, 0, 0, 0, '博客系统', NULL, NULL, NULL, NULL, NULL, NULL, 10000, 0, 0, 0, 0, NULL, NULL, 0, 0, 0, 0, 0, '<p>博客系统</p>\n', NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 999, 0, NULL, NULL, NULL, '/images/default/slide-1.jpg', 0, NULL, '', NULL, 1, '2018-12-02 17:49:43', NULL, NULL, NULL);

-- ----------------------------
-- Table structure for rebatechangelog
-- ----------------------------
DROP TABLE IF EXISTS `rebatechangelog`;
CREATE TABLE `rebatechangelog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `AdminId` int(11) NOT NULL COMMENT '管理员ID',
  `UserName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '时间',
  `IP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '登录IP',
  `Actions` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '记录',
  `Reward` decimal(10, 0) NOT NULL COMMENT '返现、扣除金额',
  `BeforChange` decimal(10, 0) NOT NULL COMMENT '变化前',
  `AfterChange` decimal(10, 0) NOT NULL COMMENT '变化后',
  `LogDetails` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详细记录',
  `TypeId` int(11) NOT NULL COMMENT '类型 0 充值 1 购买 2 赠送 3 退款 4 分销提成',
  `MyType` int(11) NOT NULL COMMENT '消费类型，见MyType',
  `OrderId` int(11) NOT NULL COMMENT '订单ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单号',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户返现余额变化记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shop
-- ----------------------------
DROP TABLE IF EXISTS `shop`;
CREATE TABLE `shop`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `ShopName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '店铺名称',
  `KId` int(11) NOT NULL COMMENT '商家分类ID',
  `AId` int(11) NOT NULL COMMENT '地区ID',
  `Sequence` int(11) NOT NULL COMMENT '排序',
  `Latitude` decimal(10, 0) NOT NULL COMMENT '纬度',
  `Longitude` decimal(10, 0) NOT NULL COMMENT '经度',
  `Country` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '国家',
  `Province` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '省',
  `City` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '市',
  `District` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '区',
  `Address` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详细地址',
  `Postcode` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮政编码',
  `IsDel` int(11) NOT NULL COMMENT '是否删除',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsDisabled` int(11) NOT NULL COMMENT '是否禁用',
  `Content` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '店铺介绍',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '关键字',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `Balance` decimal(10, 0) NOT NULL COMMENT '余额',
  `IsTop` int(11) NOT NULL COMMENT '是否置顶',
  `IsVip` int(11) NOT NULL COMMENT '是否vip',
  `IsRecommend` int(11) NOT NULL COMMENT '是否推荐',
  `Likes` int(11) NOT NULL COMMENT '点赞数',
  `AvgScore` decimal(10, 0) NOT NULL COMMENT '平均分数',
  `ServiceScore` decimal(10, 0) NOT NULL COMMENT '服务分数',
  `SpeedScore` decimal(10, 0) NOT NULL COMMENT '速度分数',
  `EnvironmentScore` decimal(10, 0) NOT NULL COMMENT '环境分数',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `MorePics` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '店铺所有图片',
  `Email` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
  `Tel` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '电话',
  `Phone` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '固定电话',
  `QQ` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'QQ',
  `Skype` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'Skype',
  `HomePage` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '主页',
  `Weixin` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信',
  `IsShip` int(11) NOT NULL COMMENT '是否配送',
  `OpenTime` datetime(0) NULL DEFAULT NULL COMMENT '开店时间',
  `CloseTime` datetime(0) NULL DEFAULT NULL COMMENT '关店时间',
  `ShippingStartTime` datetime(0) NULL DEFAULT NULL COMMENT '配送开始时间',
  `ShippingEndTime` datetime(0) NULL DEFAULT NULL COMMENT '配送结束时间',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `Hits` int(11) NOT NULL COMMENT '点击量',
  `MyType` int(11) NOT NULL COMMENT '店铺类型',
  `DefaultFare` decimal(10, 0) NOT NULL COMMENT '默认运费',
  `MaxFreeFare` decimal(10, 0) NOT NULL COMMENT '最大免运费金额',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '商家' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shopcategory
-- ----------------------------
DROP TABLE IF EXISTS `shopcategory`;
CREATE TABLE `shopcategory`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `KindName` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目名称',
  `SubTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目副标题',
  `KindTitle` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '栏目标题，填写则在浏览器替换此标题',
  `Keyword` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `Description` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '介绍',
  `LinkURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '跳转链接',
  `TitleColor` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别名称颜色',
  `TemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '模板',
  `DetailTemplateFile` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详情模板',
  `KindDomain` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类别域名（保留）',
  `IsList` int(11) NOT NULL COMMENT '是否为列表页面',
  `PageSize` int(11) NOT NULL COMMENT '每页显示数量',
  `PId` int(11) NOT NULL COMMENT '上级ID',
  `Level` int(11) NOT NULL COMMENT '级别',
  `Location` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路径',
  `IsHide` int(11) NOT NULL COMMENT '是否隐藏',
  `IsLock` int(11) NOT NULL COMMENT '是否锁定，不允许删除',
  `IsDel` int(11) NOT NULL COMMENT '是否删除,已经删除到回收站',
  `IsComment` int(11) NOT NULL COMMENT '是否允许评论',
  `IsMember` int(11) NOT NULL COMMENT '是否会员栏目',
  `IsShowSubDetail` int(11) NOT NULL COMMENT '是否显示下级栏目内容',
  `CatalogId` int(11) NOT NULL COMMENT '模型ID',
  `Counts` int(11) NOT NULL COMMENT '详情数量，缓存',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `Icon` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图标',
  `ClassName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式名称',
  `BannerImg` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT 'banner图片',
  `KindInfo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '栏目详细介绍',
  `Pic` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片',
  `AdsId` int(11) NOT NULL COMMENT '广告ID',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '商家分类' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for shoppingcart
-- ----------------------------
DROP TABLE IF EXISTS `shoppingcart`;
CREATE TABLE `shoppingcart`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `GUID` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '唯一GUID，没登录用户使用',
  `Details` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '购物车内容，JOSN',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '加入购物车时间',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '购物车' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stepexchange
-- ----------------------------
DROP TABLE IF EXISTS `stepexchange`;
CREATE TABLE `stepexchange`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `StepDate` datetime(0) NULL DEFAULT NULL COMMENT '步数日期',
  `Timestamp` int(11) NOT NULL COMMENT '日期时间戳',
  `TotalStep` int(11) NOT NULL COMMENT '总步数',
  `ExchangeStep` int(11) NOT NULL COMMENT '兑换步数',
  `AbleStep` int(11) NOT NULL COMMENT '可兑换步数',
  `RewardStep` int(11) NOT NULL COMMENT '奖励步数',
  `OriginalStep` int(11) NOT NULL COMMENT '原始步数',
  `LastTime` datetime(0) NULL DEFAULT NULL COMMENT '最后更新时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '步数兑换' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for stepexchangelog
-- ----------------------------
DROP TABLE IF EXISTS `stepexchangelog`;
CREATE TABLE `stepexchangelog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `ExchangeStep` int(11) NOT NULL COMMENT '兑换步数',
  `ExchangeCredit` decimal(19, 4) NOT NULL COMMENT '兑换积分',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '步数兑换记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for targetevent
-- ----------------------------
DROP TABLE IF EXISTS `targetevent`;
CREATE TABLE `targetevent`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `EventKey` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '事件key',
  `EventName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '事件名称',
  `IsDisable` int(11) NOT NULL COMMENT '是否禁用',
  `Rank` int(11) NOT NULL COMMENT '排序',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '目标事件' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of targetevent
-- ----------------------------
INSERT INTO `targetevent` VALUES (1, 'add', '添加', 0, 0);
INSERT INTO `targetevent` VALUES (2, 'edit', '修改', 0, 0);
INSERT INTO `targetevent` VALUES (3, 'del', '删除', 0, 0);
INSERT INTO `targetevent` VALUES (4, 'view', '查看', 0, 0);
INSERT INTO `targetevent` VALUES (5, 'viewlist', '查看列表', 0, 0);
INSERT INTO `targetevent` VALUES (6, 'import', '导入', 0, 0);
INSERT INTO `targetevent` VALUES (7, 'export', '导出', 0, 0);
INSERT INTO `targetevent` VALUES (8, 'filter', '搜索', 0, 0);
INSERT INTO `targetevent` VALUES (9, 'batch', '批量操作', 0, 0);
INSERT INTO `targetevent` VALUES (10, 'recycle', '回收站', 0, 0);
INSERT INTO `targetevent` VALUES (11, 'confirm', '确认', 0, 0);

-- ----------------------------
-- Table structure for weixinrequestcontent
-- ----------------------------
DROP TABLE IF EXISTS `weixinrequestcontent`;
CREATE TABLE `weixinrequestcontent`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `RuleId` int(11) NOT NULL COMMENT '规则名称',
  `Title` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '回复标题',
  `Content` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '回复内容',
  `LinkUrl` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '详情链接地址',
  `ImgURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '图片地址',
  `MediaURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '语音或视频地址',
  `MeidaHdURL` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '高清语音或者视频地址',
  `MediaID` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '返回的素材ID',
  `WXAppAppId` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '小程序APPId',
  `WXAppPage` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '小程序页面',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '请求回复的内容' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of weixinrequestcontent
-- ----------------------------
INSERT INTO `weixinrequestcontent` VALUES (1, 1, NULL, '关注公众号自动回复文本', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, '2020-02-19 14:41:24');

-- ----------------------------
-- Table structure for weixinrequestrule
-- ----------------------------
DROP TABLE IF EXISTS `weixinrequestrule`;
CREATE TABLE `weixinrequestrule`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `RuleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '规则名称',
  `Keywords` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '请求关键词,逗号分隔',
  `RequestType` int(11) NOT NULL COMMENT '请求类型(0默认回复1文字2图片3语音4链接5地理位置6关注7取消关注8扫描带参数二维码事件9上报地理位置事件10自定义菜单事件）',
  `ResponseType` int(11) NOT NULL COMMENT '回复类型(1文本2图文3语音4视频5第三方接口)',
  `IsLikeQuery` int(11) NOT NULL COMMENT '是否模糊查询 0 是完全匹配；1是模糊匹配',
  `IsDefault` int(11) NOT NULL COMMENT '是否是默认回复',
  `Rank` int(11) NOT NULL COMMENT '排序',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '微信公众号回复规则' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of weixinrequestrule
-- ----------------------------
INSERT INTO `weixinrequestrule` VALUES (1, '关注自动回复', NULL, 6, 0, 0, 1, 0, NULL, '2020-02-19 14:41:24');

-- ----------------------------
-- Table structure for weixinresponsecontent
-- ----------------------------
DROP TABLE IF EXISTS `weixinresponsecontent`;
CREATE TABLE `weixinresponsecontent`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `OpenId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户OpenId',
  `RequestType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '请求类型',
  `RequestContent` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '内容',
  `ResponseType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '回复的类型 文本消息：text 图片消息:image 地理位置消息:location 链接消息:link',
  `ReponseContent` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT '系统回复的内容',
  `XmlContent` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL COMMENT 'xml原始内容',
  `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '公众平台回复信息' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for withdraworder
-- ----------------------------
DROP TABLE IF EXISTS `withdraworder`;
CREATE TABLE `withdraworder`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `OrderNum` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单号',
  `UserName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
  `Title` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '订单名称',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '时间',
  `IP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '登录IP',
  `Actions` varchar(250) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '记录详情',
  `Price` decimal(10, 0) NOT NULL COMMENT '提现金额',
  `VerifyAdminId` int(11) NOT NULL COMMENT '审核管理员ID',
  `IsVerify` int(11) NOT NULL COMMENT '是否通过审核',
  `VerifyTime` datetime(0) NULL DEFAULT NULL COMMENT '审核时间',
  `FormId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '小程序FormId',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '用户提现订单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for wxappsession
-- ----------------------------
DROP TABLE IF EXISTS `wxappsession`;
CREATE TABLE `wxappsession`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '编号',
  `UId` int(11) NOT NULL COMMENT '用户ID',
  `IP` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '登录IP',
  `Key` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '系统生成Key',
  `SessionKey` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信SessionKey',
  `OpenId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '微信小程序OpenId',
  `AddTime` datetime(0) NULL DEFAULT NULL COMMENT '添加时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci COMMENT = '小程序Session' ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
