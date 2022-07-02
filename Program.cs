var connection = InfraConfig.GetConnectionString("Accounts");
var masterConnection = Regex.Replace(connection, @"database=.*", "");
foreach (var queryFile in args)
{
    Logger.LogInfo(queryFile);
    Database.Open(masterConnection).Run(File.ReadAllText(queryFile));
    Logger.LogSuccess(queryFile);
}