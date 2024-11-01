using System.Collections.Generic;
using UnityEngine;

namespace BreakTheCycle
{
    [DefaultExecutionOrder(-20)]
    public class ServiceLocator : MonoBehaviour
    {
        private Dictionary<System.Type, Component> _services;

	    #region UNITY_CALLBACKS

        //Singleton
        private static ServiceLocator _instance;
        [HideInInspector] public static ServiceLocator Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            _services = new Dictionary<System.Type, Component>();
        }

	    #endregion

	    #region CONTROL

        public T GetService<T>() where T : Component
        {
            var type = typeof(T);

            if (_services.ContainsKey(type))
            {
                return _services[type] as T;
            }
            // Debug.LogWarning($"Service of type {type.Name} not found");
            return null;
        }

        public void RegisterService<T>(T service) where T : Component
        {
            var type = typeof(T);

            if (!_services.ContainsKey(type))
            {
                _services.Add(type, service);
            }
            else
            {
                // Debug.LogWarning($"Service of type {type.Name} is already registered");
            }
        }

        public void UnregisterService<T>() where T : Component
        {
            var type = typeof(T);

            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
        }

        public void UnregisterServices()
        {
            _services.Clear();
        }

	    #endregion
    }
}