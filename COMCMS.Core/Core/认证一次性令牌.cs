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
    [Description("认证一次性令牌")]
    [BindTable("AuthOneTimeToken", Description = "认证一次性令牌", ConnName = "dbconn", DbType = DatabaseType.SqlServer, Migration = "Off")]
    public sealed class AuthOneTimeToken : Entity<AuthOneTimeToken>
    {
        private Int32 _Id;
        [DataObjectField(true, true, false, 0)]
        [BindColumn("Id", "编号", "int")]
        public Int32 Id { get => _Id; set { if (OnPropertyChanging(__.Id, value)) { _Id = value; OnPropertyChanged(__.Id); } } }

        private String _TokenHash;
        [DataObjectField(false, false, false, 64)]
        [BindColumn("TokenHash", "令牌哈希", "nvarchar(64)", Master = true)]
        public String TokenHash { get => _TokenHash; set { if (OnPropertyChanging(__.TokenHash, value)) { _TokenHash = value; OnPropertyChanged(__.TokenHash); } } }

        private String _SubjectType;
        [DataObjectField(false, false, false, 20)]
        [BindColumn("SubjectType", "主体类型", "nvarchar(20)")]
        public String SubjectType { get => _SubjectType; set { if (OnPropertyChanging(__.SubjectType, value)) { _SubjectType = value; OnPropertyChanged(__.SubjectType); } } }

        private Int32 _SubjectId;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("SubjectId", "主体编号", "int")]
        public Int32 SubjectId { get => _SubjectId; set { if (OnPropertyChanging(__.SubjectId, value)) { _SubjectId = value; OnPropertyChanged(__.SubjectId); } } }

        private String _Purpose;
        [DataObjectField(false, false, false, 30)]
        [BindColumn("Purpose", "用途", "nvarchar(30)")]
        public String Purpose { get => _Purpose; set { if (OnPropertyChanging(__.Purpose, value)) { _Purpose = value; OnPropertyChanged(__.Purpose); } } }

        private DateTime _CreatedUtc;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CreatedUtc", "创建时间", "datetime")]
        public DateTime CreatedUtc { get => _CreatedUtc; set { if (OnPropertyChanging(__.CreatedUtc, value)) { _CreatedUtc = value; OnPropertyChanged(__.CreatedUtc); } } }

        private DateTime _ExpiresUtc;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("ExpiresUtc", "过期时间", "datetime")]
        public DateTime ExpiresUtc { get => _ExpiresUtc; set { if (OnPropertyChanging(__.ExpiresUtc, value)) { _ExpiresUtc = value; OnPropertyChanged(__.ExpiresUtc); } } }

        private DateTime _UsedUtc;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("UsedUtc", "使用时间", "datetime")]
        public DateTime UsedUtc { get => _UsedUtc; set { if (OnPropertyChanging(__.UsedUtc, value)) { _UsedUtc = value; OnPropertyChanged(__.UsedUtc); } } }

        private Int32 _IsUsed;
        [DataObjectField(false, false, false, 0)]
        [BindColumn("IsUsed", "是否已使用", "int")]
        public Int32 IsUsed { get => _IsUsed; set { if (OnPropertyChanging(__.IsUsed, value)) { _IsUsed = value; OnPropertyChanged(__.IsUsed); } } }

        public override Object this[String name]
        {
            get => name switch
            {
                __.Id => _Id,
                __.TokenHash => _TokenHash,
                __.SubjectType => _SubjectType,
                __.SubjectId => _SubjectId,
                __.Purpose => _Purpose,
                __.CreatedUtc => _CreatedUtc,
                __.ExpiresUtc => _ExpiresUtc,
                __.UsedUtc => _UsedUtc,
                __.IsUsed => _IsUsed,
                _ => base[name]
            };
            set
            {
                switch (name)
                {
                    case __.Id: _Id = value.ToInt(); break;
                    case __.TokenHash: _TokenHash = Convert.ToString(value); break;
                    case __.SubjectType: _SubjectType = Convert.ToString(value); break;
                    case __.SubjectId: _SubjectId = value.ToInt(); break;
                    case __.Purpose: _Purpose = Convert.ToString(value); break;
                    case __.CreatedUtc: _CreatedUtc = value.ToDateTime(); break;
                    case __.ExpiresUtc: _ExpiresUtc = value.ToDateTime(); break;
                    case __.UsedUtc: _UsedUtc = value.ToDateTime(); break;
                    case __.IsUsed: _IsUsed = value.ToInt(); break;
                    default: base[name] = value; break;
                }
            }
        }

        public partial class _
        {
            public static readonly Field Id = FindByName(__.Id);
            public static readonly Field TokenHash = FindByName(__.TokenHash);
            public static readonly Field SubjectType = FindByName(__.SubjectType);
            public static readonly Field SubjectId = FindByName(__.SubjectId);
            public static readonly Field Purpose = FindByName(__.Purpose);
            public static readonly Field CreatedUtc = FindByName(__.CreatedUtc);
            public static readonly Field ExpiresUtc = FindByName(__.ExpiresUtc);
            public static readonly Field UsedUtc = FindByName(__.UsedUtc);
            public static readonly Field IsUsed = FindByName(__.IsUsed);
            private static Field FindByName(String name) => Meta.Table.FindByName(name);
        }

        public partial class __
        {
            public const String Id = "Id";
            public const String TokenHash = "TokenHash";
            public const String SubjectType = "SubjectType";
            public const String SubjectId = "SubjectId";
            public const String Purpose = "Purpose";
            public const String CreatedUtc = "CreatedUtc";
            public const String ExpiresUtc = "ExpiresUtc";
            public const String UsedUtc = "UsedUtc";
            public const String IsUsed = "IsUsed";
        }
    }
}
