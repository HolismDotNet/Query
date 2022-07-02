var connection = InfraConfig.GetConnectionString("Accounts");
var masterConnection = Regex.Replace(connection, @"database=.*", "");

if (args.Length == 1 && args[0] == "DeleteData")
{
    Logger.LogInfo($"Deleting all the data ...");
    for (var i = 0; i < 5; i++)
    {
        DataDeleter.MasterConnection = masterConnection;
        new DataDeleter().DeleteData();
    }
    Logger.LogInfo($"Deleted all the data");
}
else 
{
    foreach (var queryFile in args)
    {
        Logger.LogInfo($"Running {queryFile} ...");
        Database.Open(masterConnection).Run(File.ReadAllText(queryFile));
        Logger.LogSuccess($"Ran {queryFile}");
    }
}