using System;
using System.ComponentModel;
using NewLife;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace COMCMS.Core
{
    [Serializable]
    [DataObject]
    [Description("认证会话")]
    [BindTable("AuthSession", Description = "认证会话", ConnName = "dbconn", DbType = DatabaseType.SqlServer, Migration = "Off")]
    public class AuthSession : Entity<AuthSession>
    {
        private Int32 _Id;
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _SessionId;
        [DataObjectField(false, false, false, 36)]
        [BindColumn("SessionId", "会话ID", "nvarchar(36)", Master = true)]
        public String SessionId { get => _SessionId; set { if (OnPropertyChanging(__.SessionId, value)) { _SessionId = value; OnPropertyChanged(__.SessionId); } } }

        private String _SubjectType;
        [DataObjectField(false, false, false, 20)]
        [BindColumn("SubjectType", "主体类型", "nvarchar(20)")]
        public String SubjectType { get => _SubjectType; set { if (OnPropertyChanging(__.SubjectType, value)) { _SubjectType = value; OnPropertyChanged(__.SubjectType); } } }

        private Int32 _SubjectId;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SubjectId", "主体ID", "int")]
        public Int32 SubjectId { get => _SubjectId; set { if (OnPropertyChanging(__.SubjectId, value)) { _SubjectId = value; OnPropertyChanged(__.SubjectId); } } }

        private String _TokenFamily;
        [DataObjectField(false, false, false, 36)]
        [BindColumn("TokenFamily", "令牌族", "nvarchar(36)")]
        public String TokenFamily { get => _TokenFamily; set { if (OnPropertyChanging(__.TokenFamily, value)) { _TokenFamily = value; OnPropertyChanged(__.TokenFamily); } } }

        private String _RefreshTokenHash;
        [DataObjectField(false, false, true, 64)]
        [BindColumn("RefreshTokenHash", "刷新令牌哈希", "nvarchar(64)")]
        public String RefreshTokenHash { get => _RefreshTokenHash; set { if (OnPropertyChanging(__.RefreshTokenHash, value)) { _RefreshTokenHash = value; OnPropertyChanged(__.RefreshTokenHash); } } }

        private String _PreviousRefreshTokenHash;
        [DataObjectField(false, false, true, 64)]
        [BindColumn("PreviousRefreshTokenHash", "前一刷新令牌哈希", "nvarchar(64)")]
        public String PreviousRefreshTokenHash { get => _PreviousRefreshTokenHash; set { if (OnPropertyChanging(__.PreviousRefreshTokenHash, value)) { _PreviousRefreshTokenHash = value; OnPropertyChanged(__.PreviousRefreshTokenHash); } } }

        private String _SecurityStamp;
        [DataObjectField(false, false, false, 64)]
        [BindColumn("SecurityStamp", "账号安全戳", "nvarchar(64)")]
        public String SecurityStamp { get => _SecurityStamp; set { if (OnPropertyChanging(__.SecurityStamp, value)) { _SecurityStamp = value; OnPropertyChanged(__.SecurityStamp); } } }

        private String _DeviceName;
        [DataObjectField(false, false, true, 100)]
        [BindColumn("DeviceName", "设备名称", "nvarchar(100)")]
        public String DeviceName { get => _DeviceName; set { if (OnPropertyChanging(__.DeviceName, value)) { _DeviceName = value; OnPropertyChanged(__.DeviceName); } } }

        private DateTime _CreatedUtc;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CreatedUtc", "创建时间", "datetime")]
        public DateTime CreatedUtc { get => _CreatedUtc; set { if (OnPropertyChanging(__.CreatedUtc, value)) { _CreatedUtc = value; OnPropertyChanged(__.CreatedUtc); } } }

        private DateTime _ExpiresUtc;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExpiresUtc", "过期时间", "datetime")]
        public DateTime ExpiresUtc { get => _ExpiresUtc; set { if (OnPropertyChanging(__.ExpiresUtc, value)) { _ExpiresUtc = value; OnPropertyChanged(__.ExpiresUtc); } } }

        private DateTime _LastUsedUtc;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("LastUsedUtc", "最后使用时间", "datetime")]
        public DateTime LastUsedUtc { get => _LastUsedUtc; set { if (OnPropertyChanging(__.LastUsedUtc, value)) { _LastUsedUtc = value; OnPropertyChanged(__.LastUsedUtc); } } }

        private DateTime _RevokedUtc;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("RevokedUtc", "撤销时间", "datetime")]
        public DateTime RevokedUtc { get => _RevokedUtc; set { if (OnPropertyChanging(__.RevokedUtc, value)) { _RevokedUtc = value; OnPropertyChanged(__.RevokedUtc); } } }

        private String _ReplacedBySessionId;
        [DataObjectField(false, false, true, 36)]
        [BindColumn("ReplacedBySessionId", "替换会话ID", "nvarchar(36)")]
        public String ReplacedBySessionId { get => _ReplacedBySessionId; set { if (OnPropertyChanging(__.ReplacedBySessionId, value)) { _ReplacedBySessionId = value; OnPropertyChanged(__.ReplacedBySessionId); } } }

        private Int32 _IsRevoked;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsRevoked", "是否撤销", "int")]
        public Int32 IsRevoked { get => _IsRevoked; set { if (OnPropertyChanging(__.IsRevoked, value)) { _IsRevoked = value; OnPropertyChanged(__.IsRevoked); } } }

        public override Object this[String name]
        {
            get => name switch
            {
                __.Id => _Id,
                __.SessionId => _SessionId,
                __.SubjectType => _SubjectType,
                __.SubjectId => _SubjectId,
                __.TokenFamily => _TokenFamily,
                __.RefreshTokenHash => _RefreshTokenHash,
                __.PreviousRefreshTokenHash => _PreviousRefreshTokenHash,
                __.SecurityStamp => _SecurityStamp,
                __.DeviceName => _DeviceName,
                __.CreatedUtc => _CreatedUtc,
                __.ExpiresUtc => _ExpiresUtc,
                __.LastUsedUtc => _LastUsedUtc,
                __.RevokedUtc => _RevokedUtc,
                __.ReplacedBySessionId => _ReplacedBySessionId,
                __.IsRevoked => _IsRevoked,
                _ => base[name]
            };
            set
            {
                switch (name)
                {
                    case __.Id: _Id = value.ToInt(); break;
                    case __.SessionId: _SessionId = Convert.ToString(value); break;
                    case __.SubjectType: _SubjectType = Convert.ToString(value); break;
                    case __.SubjectId: _SubjectId = value.ToInt(); break;
                    case __.TokenFamily: _TokenFamily = Convert.ToString(value); break;
                    case __.RefreshTokenHash: _RefreshTokenHash = Convert.ToString(value); break;
                    case __.PreviousRefreshTokenHash: _PreviousRefreshTokenHash = Convert.ToString(value); break;
                    case __.SecurityStamp: _SecurityStamp = Convert.ToString(value); break;
                    case __.DeviceName: _DeviceName = Convert.ToString(value); break;
                    case __.CreatedUtc: _CreatedUtc = value.ToDateTime(); break;
                    case __.ExpiresUtc: _ExpiresUtc = value.ToDateTime(); break;
                    case __.LastUsedUtc: _LastUsedUtc = value.ToDateTime(); break;
                    case __.RevokedUtc: _RevokedUtc = value.ToDateTime(); break;
                    case __.ReplacedBySessionId: _ReplacedBySessionId = Convert.ToString(value); break;
                    case __.IsRevoked: _IsRevoked = value.ToInt(); break;
                    default: base[name] = value; break;
                }
            }
        }

        public static AuthSession FindBySessionId(string sessionId) => Find(_.SessionId == sessionId);
        public static AuthSession FindByRefreshHash(string hash) => Find(_.RefreshTokenHash == hash | _.PreviousRefreshTokenHash == hash);

        public static void RevokeFamily(string tokenFamily)
        {
            var sessions = FindAll(_.TokenFamily == tokenFamily & _.IsRevoked == 0);
            foreach (var session in sessions)
            {
                session.IsRevoked = 1;
                session.RevokedUtc = DateTime.UtcNow;
                session.Update();
            }
        }

        public static bool IsActive(AuthSession session) => session != null && session.IsRevoked == 0 && session.ExpiresUtc > DateTime.UtcNow;

        public partial class _
        {
            public static readonly Field Id = FindByName(__.Id);
            public static readonly Field SessionId = FindByName(__.SessionId);
            public static readonly Field SubjectType = FindByName(__.SubjectType);
            public static readonly Field SubjectId = FindByName(__.SubjectId);
            public static readonly Field TokenFamily = FindByName(__.TokenFamily);
            public static readonly Field RefreshTokenHash = FindByName(__.RefreshTokenHash);
            public static readonly Field PreviousRefreshTokenHash = FindByName(__.PreviousRefreshTokenHash);
            public static readonly Field SecurityStamp = FindByName(__.SecurityStamp);
            public static readonly Field DeviceName = FindByName(__.DeviceName);
            public static readonly Field CreatedUtc = FindByName(__.CreatedUtc);
            public static readonly Field ExpiresUtc = FindByName(__.ExpiresUtc);
            public static readonly Field LastUsedUtc = FindByName(__.LastUsedUtc);
            public static readonly Field RevokedUtc = FindByName(__.RevokedUtc);
            public static readonly Field ReplacedBySessionId = FindByName(__.ReplacedBySessionId);
            public static readonly Field IsRevoked = FindByName(__.IsRevoked);
            private static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        public partial class __
        {
            public const String Id = "Id";
            public const String SessionId = "SessionId";
            public const String SubjectType = "SubjectType";
            public const String SubjectId = "SubjectId";
            public const String TokenFamily = "TokenFamily";
            public const String RefreshTokenHash = "RefreshTokenHash";
            public const String PreviousRefreshTokenHash = "PreviousRefreshTokenHash";
            public const String SecurityStamp = "SecurityStamp";
            public const String DeviceName = "DeviceName";
            public const String CreatedUtc = "CreatedUtc";
            public const String ExpiresUtc = "ExpiresUtc";
            public const String LastUsedUtc = "LastUsedUtc";
            public const String RevokedUtc = "RevokedUtc";
            public const String ReplacedBySessionId = "ReplacedBySessionId";
            public const String IsRevoked = "IsRevoked";
        }
    }
}
