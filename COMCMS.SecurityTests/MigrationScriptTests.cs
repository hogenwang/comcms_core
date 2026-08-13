using System;
using System.IO;
using Xunit;

namespace COMCMS.SecurityTests
{
    public sealed class MigrationScriptTests
    {
        [Fact]
        public void MySqlMigration_RequiresCoreAccountTables()
        {
            var script = ReadMigration("mysql", "20260813_security_authentication.sql");

            Assert.Contains("@admin_table IS NULL", script, StringComparison.Ordinal);
            Assert.Contains("COMCMS_MIGRATION_ERROR_REQUIRED_ADMIN_TABLE_NOT_FOUND", script, StringComparison.Ordinal);
            Assert.Contains("@member_table IS NULL", script, StringComparison.Ordinal);
            Assert.Contains("COMCMS_MIGRATION_ERROR_REQUIRED_MEMBER_TABLE_NOT_FOUND", script, StringComparison.Ordinal);
        }

        [Fact]
        public void MySqlMigration_SkipsMissingOptionalLogTables()
        {
            var script = ReadMigration("mysql", "20260813_security_authentication.sql");

            AssertOptionalTableUsesNoOp(script, "@admin_log_table");
            AssertOptionalTableUsesNoOp(script, "@member_log_table");
        }

        private static void AssertOptionalTableUsesNoOp(string script, string variable)
        {
            var condition = script.IndexOf(variable + " IS NULL", StringComparison.Ordinal);
            Assert.True(condition >= 0, $"Missing optional-table guard for {variable}.");

            var noOp = script.IndexOf("'DO 0'", condition, StringComparison.Ordinal);
            Assert.True(noOp > condition && noOp - condition < 100,
                $"Missing no-op branch for {variable}.");
        }

        private static string ReadMigration(string engine, string fileName)
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory != null && !File.Exists(Path.Combine(directory.FullName, "COMCMS_NETCORE.sln")))
                directory = directory.Parent;

            Assert.NotNull(directory);
            var path = Path.Combine(directory.FullName, "database", "migrations", engine, fileName);
            return File.ReadAllText(path);
        }
    }
}
