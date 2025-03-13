
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestProject.Core
{
    // This class could be changed to some DI container
    public static class Dispatcher
    {
        private static readonly Dictionary<Type, IController> _controllers;
        private static readonly Dictionary<Type, IModel> _models;
        private static readonly List<IView> _views;
        private static ILogger _logger;
        
        static Dispatcher()
        {
            _controllers = new Dictionary<Type, IController>();
            _models = new Dictionary<Type, IModel>();
            _views = new List<IView>();
        }

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;
        }

        public static void InitializeControllers()
        {
            foreach (IController controller in  _controllers.Values)
            {
                controller.Initialize();
            }
        }
        
        public static T AddController<T>() where T : IController
        {
            T obj = Activator.CreateInstance<T>();
            _controllers.Add(obj.GetType(), obj);
            return obj;
        }

        public static T GetController<T>() where T : IController
        {
            Type type = typeof(T);
            if (!_controllers.ContainsKey(type))
            {
                _logger.Log(LogType.Error, $"Controller of type {type} not found");
                return default(T);
            }

            return (T)_controllers[type];
        }

        public static void AddView<T>(T view) where T : IView
        {
            _views.Add(view);
        }

        public static T GetView<T>() where T : IView
        {
            Type type = typeof(T);
            foreach (IView view in _views)
            {
                if (view is T resultView)
                {
                    return resultView;
                }
            }

            return default(T);
        }

        public static T AddModel<T>() where T : IModel
        {
            T obj = Activator.CreateInstance<T>();
            _models.Add(obj.GetType(), obj);

            return obj;
        }

        public static T GetModel<T>() where T : IModel
        {
            Type type = typeof(T);
            if (!_models.ContainsKey(type))
            {
                _logger.Log(LogType.Error, $"Model of type {type} not found");
                return default(T);
            }

            return (T)_models[type];
        }

        public static void Cleanup()
        {
            _controllers.Clear();
            _views.Clear();
        }
    }
}

