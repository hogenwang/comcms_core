-- COMCMS security/authentication migration for SQL Server 2019+
-- Apply before deploying the application. This script is safe to run repeatedly.

IF COL_LENGTH('dbo.Admin', 'PassWord') IS NOT NULL
    ALTER TABLE dbo.Admin ALTER COLUMN PassWord NVARCHAR(256) NULL;

IF COL_LENGTH('dbo.Member', 'PassWord') IS NOT NULL
    ALTER TABLE dbo.Member ALTER COLUMN PassWord NVARCHAR(256) NULL;

IF COL_LENGTH('dbo.AdminLog', 'PassWord') IS NOT NULL
    UPDATE dbo.AdminLog SET PassWord = '******' WHERE PassWord IS NOT NULL AND PassWord <> '******';

IF COL_LENGTH('dbo.MemberLog', 'PassWord') IS NOT NULL
    UPDATE dbo.MemberLog SET PassWord = '******' WHERE PassWord IS NOT NULL AND PassWord <> '******';

IF OBJECT_ID('dbo.AuthSession', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AuthSession (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AuthSession PRIMARY KEY,
        SessionId NVARCHAR(36) NOT NULL,
        SubjectType NVARCHAR(20) NOT NULL,
        SubjectId INT NOT NULL,
        TokenFamily NVARCHAR(36) NOT NULL,
        RefreshTokenHash NVARCHAR(64) NULL,
        PreviousRefreshTokenHash NVARCHAR(64) NULL,
        SecurityStamp NVARCHAR(64) NOT NULL,
        DeviceName NVARCHAR(100) NULL,
        CreatedUtc DATETIME2 NOT NULL,
        ExpiresUtc DATETIME2 NOT NULL,
        LastUsedUtc DATETIME2 NOT NULL,
        RevokedUtc DATETIME2 NOT NULL CONSTRAINT DF_AuthSession_RevokedUtc DEFAULT '1970-01-01T00:00:00',
        ReplacedBySessionId NVARCHAR(36) NULL,
        IsRevoked INT NOT NULL CONSTRAINT DF_AuthSession_IsRevoked DEFAULT 0
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_AuthSession_SessionId' AND object_id = OBJECT_ID('dbo.AuthSession'))
    CREATE UNIQUE INDEX UX_AuthSession_SessionId ON dbo.AuthSession(SessionId);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_AuthSession_RefreshTokenHash' AND object_id = OBJECT_ID('dbo.AuthSession'))
    CREATE UNIQUE INDEX UX_AuthSession_RefreshTokenHash ON dbo.AuthSession(RefreshTokenHash) WHERE RefreshTokenHash IS NOT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_AuthSession_PreviousRefreshTokenHash' AND object_id = OBJECT_ID('dbo.AuthSession'))
    CREATE INDEX IX_AuthSession_PreviousRefreshTokenHash ON dbo.AuthSession(PreviousRefreshTokenHash) WHERE PreviousRefreshTokenHash IS NOT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_AuthSession_Subject' AND object_id = OBJECT_ID('dbo.AuthSession'))
    CREATE INDEX IX_AuthSession_Subject ON dbo.AuthSession(SubjectType, SubjectId, IsRevoked);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_AuthSession_TokenFamily' AND object_id = OBJECT_ID('dbo.AuthSession'))
    CREATE INDEX IX_AuthSession_TokenFamily ON dbo.AuthSession(TokenFamily, IsRevoked);

IF OBJECT_ID('dbo.AuthOneTimeToken', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AuthOneTimeToken (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AuthOneTimeToken PRIMARY KEY,
        TokenHash NVARCHAR(64) NOT NULL,
        SubjectType NVARCHAR(20) NOT NULL,
        SubjectId INT NOT NULL,
        Purpose NVARCHAR(30) NOT NULL,
        CreatedUtc DATETIME2 NOT NULL,
        ExpiresUtc DATETIME2 NOT NULL,
        UsedUtc DATETIME2 NOT NULL CONSTRAINT DF_AuthOneTimeToken_UsedUtc DEFAULT '1970-01-01T00:00:00',
        IsUsed INT NOT NULL CONSTRAINT DF_AuthOneTimeToken_IsUsed DEFAULT 0
    );
END;

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UX_AuthOneTimeToken_TokenHash' AND object_id = OBJECT_ID('dbo.AuthOneTimeToken'))
    CREATE UNIQUE INDEX UX_AuthOneTimeToken_TokenHash ON dbo.AuthOneTimeToken(TokenHash);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_AuthOneTimeToken_Subject' AND object_id = OBJECT_ID('dbo.AuthOneTimeToken'))
    CREATE INDEX IX_AuthOneTimeToken_Subject ON dbo.AuthOneTimeToken(SubjectType, SubjectId, Purpose, IsUsed);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_AuthOneTimeToken_ExpiresUtc' AND object_id = OBJECT_ID('dbo.AuthOneTimeToken'))
    CREATE INDEX IX_AuthOneTimeToken_ExpiresUtc ON dbo.AuthOneTimeToken(ExpiresUtc);
