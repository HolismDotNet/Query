public class DatabaseDropper
{
    public static string MasterConnection { get; set; }
    
    public void DropDatabases()
    {
        var databases = Database.Open(MasterConnection).Get("show databases");
        foreach (DataRow database in databases.Rows)
        {
            var databaseName = database["Database"].ToString();
            if (Char.IsLower(databaseName[0])) 
            {
                continue;
            }
            DropDatabase(databaseName);
        }
    }

    public void DropDatabase(string databaseName)
    {
        try
        {
            Database.Open(MasterConnection).Run($"drop database `{databaseName}`");
            Logger.LogSuccess($"Dropped database {databaseName}");
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
        }
    }
}