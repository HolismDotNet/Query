var connection = InfraConfig.GetConnectionString("Accounts");
var masterConnection = Regex.Replace(connection, @"database=.*", "");
masterConnection = masterConnection + "; Allow User Variables=true;";

if (args.Length == 1 && args[0] == "DeleteData")
{
    Logger.LogInfo($"Deleting all the data ...");
    for (var i = 0; i < 3; i++)
    {
        DataDeleter.MasterConnection = masterConnection;
        new DataDeleter().DeleteData();
    }
    Logger.LogInfo($"Deleted all the data");
}
else 
{
    for (var i = 0; i < 3; i++)
    {
        foreach (var queryFile in args)
        {
            try
            {
                Logger.LogInfo($"Running {queryFile} ...");
                Database.Open(masterConnection).Run(File.ReadAllText(queryFile));
                Logger.LogSuccess($"Ran {queryFile}");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }
    }
}