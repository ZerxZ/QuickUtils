using System.Collections.Generic;
using System.Reflection;
using Config;
using HarmonyLib;
using UnityEngine;

namespace QuickUtils
{
    [HarmonyPatch(typeof(UI_Worldmap), "OnEnable")]
    class UI_Worldmap_OnEnable_Patch
    {
        public static void Postfix()
        {
            Debug.Log("开始绑定QuickBase");
            QuickUtils.BindComponetWorldMap();
            Debug.Log("绑定完毕QuickBase");
        }
    }
    [HarmonyPatch(typeof(UI_Worldmap), "OnDestroy")]
    class UI_Worldmap_OnDestroy_Patch
    {
        public static void Postfix()
        {
            QuickUtils.DestroyComponetWorldMap();
            Debug.Log("成功销毁QuickBase");
            
        }
    }
}