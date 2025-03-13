using UnityEngine;

namespace TestProject.Core
{
    /// <summary>
    /// Base class for MonoBehaviour views
    /// </summary>
    public class BaseView : MonoBehaviour, IView
    {
        public ILogger Logger => Debug.unityLogger;
        
        protected virtual void Awake()
        {
            Dispatcher.AddView(this);
        }
    }
}