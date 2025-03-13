using DefaultNamespace;
using UnityEngine;

namespace TestProject.Core
{
    /// <summary>
    /// Initialize Controllers, Models, Views
    /// </summary>
    public static class DispatcherInit
    {
        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            Dispatcher.SetLogger(Debug.unityLogger);
            
            Dispatcher.AddModel<MatchModel>();
            Dispatcher.AddModel<UsedColorsModel>();
            
            Dispatcher.AddController<MatchController>();
            Dispatcher.InitializeControllers();
            
            Application.quitting += OnApplicationQuit;
            
            Debug.Log("Dispatcher initialized");
        }

        private static void OnApplicationQuit()
        {
            Dispatcher.Cleanup();
            Debug.Log("Dispatcher Cleaned");
            
            Application.quitting -= OnApplicationQuit;
        }
    }
}