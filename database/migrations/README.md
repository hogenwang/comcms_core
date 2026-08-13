# Database migrations

Run the script for the production database engine before deploying the authentication changes:

- MySQL: `mysql/20260813_security_authentication.sql`（会按 `information_schema` 识别 Windows/Linux 的表名大小写）
- SQL Server: `sqlserver/20260813_security_authentication.sql`

Back up the database first. The scripts widen existing password columns and add the `AuthSession` and `AuthOneTimeToken` tables and indexes. Do not configure XCode to perform production schema migrations automatically.

Before the configured legacy-password deadline, count accounts still using 32-character MD5 values and require those users to reset their passwords:

```sql
SELECT COUNT(*) FROM Admin WHERE LENGTH(PassWord) = 32;
SELECT COUNT(*) FROM Member WHERE LENGTH(PassWord) = 32;
```

For SQL Server, replace `LENGTH` with `LEN`. Provision the first administrator through the explicit Development-only installer with a unique username and a 12-128 character password; production online installation is blocked. MySQL 新安装按安装向导选择 `comcms_mysql.sql`（Windows）或 `comcms_mysql_linux.sql`（Linux），两者结构相同但表名大小写符合 XCode 映射。
