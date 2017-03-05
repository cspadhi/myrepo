using EFEndToEnd.DataAccess.Migrations;
using System.Data.Entity;

namespace EFEndToEnd.DataAccess
{
    public class Migrator : MigrateDatabaseToLatestVersion<Context, Configuration>
    {
    }
}
