using Microsoft.EntityFrameworkCore;

namespace SportZoneServer.Data.Helpers;

public static class DatabaseUtils
{
    public static async Task TruncateAllTablesAsync(DbContext context)
    {
        const string sql = @"
DO
$$
DECLARE
    tabname text;
BEGIN
    EXECUTE 'SET session_replication_role = replica';

    FOR tabname IN
        SELECT tablename FROM pg_tables
        WHERE schemaname = 'public'
    LOOP
        EXECUTE 'DROP TABLE public.' || quote_ident(tabname) || ' CASCADE';
    END LOOP;

    EXECUTE 'SET session_replication_role = origin';
END;
$$;
";

        await context.Database.ExecuteSqlRawAsync(sql);
    }
}
