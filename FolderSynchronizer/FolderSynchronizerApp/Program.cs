namespace FolderSynchronizerApp
{
    internal class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        static async Task Main(string[] args)
        {
            if (args.Length != 3)
            {
                Logger.Error("Please provide the parameters: <source> <replica> <syncInterval in minutes>");
                return;
            }

            string sourceFolderPath = args[0];
            if (!Directory.Exists(sourceFolderPath))
            {
                Logger.Error("Source folder does not exist");
                return;
            }
            string replicaFolderPath = args[1];
            if (!Directory.Exists(replicaFolderPath))
            {
                Logger.Error("Replica folder does not exist");
                return;
            }
            if(!int.TryParse(args[2], out int syncInterval))
            {
                Logger.Error("Sync interval must be a number>0");
                return;
            }
            if (syncInterval <= 0)
            {
                Logger.Error("Sync interval should be greater than 0");
                return;
            }

            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(50));


            while (await timer.WaitForNextTickAsync())
            {
                var syncronize = new Synchronizer(sourceFolderPath, replicaFolderPath);
                syncronize.Synchronize();

            }




        }


    }
}

