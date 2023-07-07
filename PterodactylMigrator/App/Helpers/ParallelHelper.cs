using Serilog;

namespace PterodactylMigrator.App.Helpers;

public class ParallelHelper
{
    public static void ExecuteParallel<T>(T[] data, Action<T> action, int partSize)
    {
        Log.Debug("Starting parallel execution");
        Log.Debug("Preparing tasks");
        
        var tasks = new List<Task>();

        foreach (var part in data.Chunk(partSize))
        {
            var t = new Task(() =>
            {
                foreach (var p in part)
                {
                    try
                    {
                        action.Invoke(p);
                    }
                    catch (Exception e)
                    {
                        Log.Error("Error executing task");
                        Log.Error(e.Message);
                    }
                }
            });
            
            tasks.Add(t);
        }
        
        Log.Debug($"Running {tasks.Count} tasks");

        foreach (var task in tasks)
        {
            task.Start();
        }
        
        Log.Debug("Waiting for tasks to finish");

        foreach (var task in tasks)
        {
            task.Wait();
        }
        
        Log.Debug("Execution done");
    }
}