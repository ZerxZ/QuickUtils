using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QuickUtils
{
    public partial class QuickUtils
    {
        public static void BindComponetWorldMap()
        {
            BindComponet<QuickBag>();
            BindComponet<QuickWarehourse>();
        }
        public static void DestroyComponetWorldMap()
        {
            DestroyComponet<QuickBag>();
            DestroyComponet<QuickWarehourse>();
        }
        public static void ComponetAbleWorldMap()
        {
            ComponetAble<QuickBag>();
            ComponetAble<QuickWarehourse>();
        }

        public static void ComponetAble<T>() where T : QuickBase
        {
            var name = typeof(T).Name;
            if (QuickBases.ContainsKey(name))
            {
               var component=QuickBases[name];
               component.enabled = !component.enabled;
            }
        }
       
        public static void BindComponet<T>() where T : QuickBase
        {
            if (!QuickBases.ContainsKey(typeof(T).Name))
            {
                var component = _gameObject.AddComponent<T>();
            
                component.QuickUtils = Instance;
                QuickBases.Add(component.GetType().Name,component);
            }
           
        }

        public  KeyCode? BindKeycode(string key)
        {
           
            if (_keyCodes.ContainsKey(key))
            {
                return _keyCodes[key];
            }
            var value = string.Empty;
            ModManager.GetSetting(ModIdStr, $"Key_{key}Key", ref value);
            
            if (value == null)
            {
                return null;
            }
            
            Debug.Log(value);
            KeyCode code;
            if (Enum.TryParse(value.ToUpper(), out code))
            {
                Debug.Log(code);
                _keyCodes.Add(key,code);
                return code;
            }

            return null;
        }
        public static void DestroyComponets(params Type[] types)
        {
            foreach (var type in types)
            {
                DestroyComponet(type.Name);
            }
        }
        public static void DestroyComponets(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                DestroyComponet(type.Name);
            }
        }
        public static void DestroyComponet<T>() => DestroyComponet(typeof(T).Name);
        public static void DestroyComponet(string name)
        {
            if (QuickBases.ContainsKey(name))
            {
                Object.Destroy( QuickBases[name]);
                QuickBases.Remove(name);
            }
        }
    }
}