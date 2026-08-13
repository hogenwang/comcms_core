-- COMCMS security/authentication migration for MySQL 5.7+
-- Apply before deploying the application. This script is safe to run repeatedly.

SET @admin_table = (SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND LOWER(TABLE_NAME) = 'admin' LIMIT 1);
SET @member_table = (SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND LOWER(TABLE_NAME) = 'member' LIMIT 1);
SET @admin_log_table = (SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND LOWER(TABLE_NAME) = 'adminlog' LIMIT 1);
SET @member_log_table = (SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_SCHEMA = DATABASE() AND LOWER(TABLE_NAME) = 'memberlog' LIMIT 1);

-- Admin and Member are required application tables. The deliberately named
-- fallback table makes PREPARE fail with an explicit migration error when a
-- required table is missing, without requiring CREATE ROUTINE privileges.
SET @statement = IF(
  @admin_table IS NULL,
  'SELECT * FROM `COMCMS_MIGRATION_ERROR_REQUIRED_ADMIN_TABLE_NOT_FOUND`',
  CONCAT('ALTER TABLE `', REPLACE(@admin_table, '`', '``'), '` MODIFY COLUMN `PassWord` VARCHAR(256) NULL'));
PREPARE migration_statement FROM @statement;
EXECUTE migration_statement;
DEALLOCATE PREPARE migration_statement;

SET @statement = IF(
  @member_table IS NULL,
  'SELECT * FROM `COMCMS_MIGRATION_ERROR_REQUIRED_MEMBER_TABLE_NOT_FOUND`',
  CONCAT('ALTER TABLE `', REPLACE(@member_table, '`', '``'), '` MODIFY COLUMN `PassWord` VARCHAR(256) NULL'));
PREPARE migration_statement FROM @statement;
EXECUTE migration_statement;
DEALLOCATE PREPARE migration_statement;

-- Log tables are optional in older installations. Skip cleanup when absent.
SET @statement = IF(
  @admin_log_table IS NULL,
  'DO 0',
  CONCAT('UPDATE `', REPLACE(@admin_log_table, '`', '``'), '` SET `PassWord` = ''******'' WHERE `PassWord` IS NOT NULL AND `PassWord` <> ''******'''));
PREPARE migration_statement FROM @statement;
EXECUTE migration_statement;
DEALLOCATE PREPARE migration_statement;

SET @statement = IF(
  @member_log_table IS NULL,
  'DO 0',
  CONCAT('UPDATE `', REPLACE(@member_log_table, '`', '``'), '` SET `PassWord` = ''******'' WHERE `PassWord` IS NOT NULL AND `PassWord` <> ''******'''));
PREPARE migration_statement FROM @statement;
EXECUTE migration_statement;
DEALLOCATE PREPARE migration_statement;

CREATE TABLE IF NOT EXISTS `AuthSession` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `SessionId` VARCHAR(36) NOT NULL,
  `SubjectType` VARCHAR(20) NOT NULL,
  `SubjectId` INT NOT NULL,
  `TokenFamily` VARCHAR(36) NOT NULL,
  `RefreshTokenHash` VARCHAR(64) NULL,
  `PreviousRefreshTokenHash` VARCHAR(64) NULL,
  `SecurityStamp` VARCHAR(64) NOT NULL,
  `DeviceName` VARCHAR(100) NULL,
  `CreatedUtc` DATETIME(6) NOT NULL,
  `ExpiresUtc` DATETIME(6) NOT NULL,
  `LastUsedUtc` DATETIME(6) NOT NULL,
  `RevokedUtc` DATETIME(6) NOT NULL DEFAULT '1970-01-01 00:00:00',
  `ReplacedBySessionId` VARCHAR(36) NULL,
  `IsRevoked` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UX_AuthSession_SessionId` (`SessionId`),
  UNIQUE KEY `UX_AuthSession_RefreshTokenHash` (`RefreshTokenHash`),
  KEY `IX_AuthSession_PreviousRefreshTokenHash` (`PreviousRefreshTokenHash`),
  KEY `IX_AuthSession_Subject` (`SubjectType`, `SubjectId`, `IsRevoked`),
  KEY `IX_AuthSession_TokenFamily` (`TokenFamily`, `IsRevoked`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `AuthOneTimeToken` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `TokenHash` VARCHAR(64) NOT NULL,
  `SubjectType` VARCHAR(20) NOT NULL,
  `SubjectId` INT NOT NULL,
  `Purpose` VARCHAR(30) NOT NULL,
  `CreatedUtc` DATETIME(6) NOT NULL,
  `ExpiresUtc` DATETIME(6) NOT NULL,
  `UsedUtc` DATETIME(6) NOT NULL DEFAULT '1970-01-01 00:00:00',
  `IsUsed` INT NOT NULL DEFAULT 0,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UX_AuthOneTimeToken_TokenHash` (`TokenHash`),
  KEY `IX_AuthOneTimeToken_Subject` (`SubjectType`, `SubjectId`, `Purpose`, `IsUsed`),
  KEY `IX_AuthOneTimeToken_ExpiresUtc` (`ExpiresUtc`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
