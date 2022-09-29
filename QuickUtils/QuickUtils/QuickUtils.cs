using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using TaiwuModdingLib.Core.Plugin;
using UnityEngine;
using Config;
using Config.ConfigCells.Character;
using GameData.Domains.Item;
using HarmonyLib;
using Object = UnityEngine.Object;

namespace QuickUtils
{
    [PluginConfig("QuickUtils", "Zerxz", "1.3.0")]
    public partial class QuickUtils : TaiwuRemakePlugin
    {
        public static QuickUtils Instance { get; private set; }
      
        public static GameObject GameObject => _gameObject;
        public static Harmony HarmonyInstance => _harmony;
        public static Dictionary<string, QuickBase> QuickBases => _quickBases;
        public static  Dictionary<string, KeyCode> KeyCodes => _keyCodes;
        public static Dictionary<string, sbyte> GridCostDict => gridCostDict;

        public override void Initialize()
        {
            Instance = this;
            _harmony = new Harmony("QuickUtils");
            _harmony.PatchAll();
            _gameObject = new GameObject($"QuickUtils{DateTime.Now}");
            SkillOneCost();
            Debug.Log("QuickUtils 初始化成功");
        }
        public override void Dispose()
        {
            Object.Destroy(_gameObject);
            SkillOneCost(true);
            Instance = null;
            _harmony.UnpatchAll();
            _keyCodes.Clear();
            Debug.Log("QuickUtils 销毁成功");
        }
    }
}