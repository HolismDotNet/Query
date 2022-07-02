public class DataDeleter
{
    public static string MasterConnection { get; set; }

    public void DeleteData()
    {
        var databases = Database.Open(MasterConnection).Get("show databases");
        foreach (DataRow database in databases.Rows)
        {
            var databaseName = database["Database"].ToString();
            if (Char.IsLower(databaseName[0])) 
            {
                continue;
            }
            DeleteDatabaseData(databaseName);
        }
    }

    public void DeleteDatabaseData(string databaseName)
    {
        var tables = Database.Open(MasterConnection).Get(@$"show tables from {databaseName}");
        foreach(DataRow table in tables.Rows)
        {
            var tableName = table[$"Tables_in_{databaseName}"].ToString();
            if (tableName.EndsWith("Views") && tableName != "Views")
            {
                continue;
            }
            else 
            {
                DeleteTableData(databaseName, tableName);
            }
        }
    }

    public void DeleteTableData(string databaseName, string tableName)
    {
        try
        {
            Database.Open(MasterConnection).Run($"delete from {databaseName}.{tableName}");
        }
        catch (Exception ex)
        {
            Logger.LogException(ex);
        }
    }
}