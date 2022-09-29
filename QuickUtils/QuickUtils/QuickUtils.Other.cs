using System.Collections.Generic;
using System.Reflection;
using Config;
using HarmonyLib;
using UnityEngine;

namespace QuickUtils
{
    public partial class QuickUtils
    {
        private static readonly Dictionary<string, KeyCode> _keyCodes = new Dictionary<string, KeyCode>();
        private static readonly Dictionary<string, QuickBase> _quickBases = new Dictionary<string, QuickBase>();
        private static readonly Dictionary<string, sbyte> gridCostDict = new Dictionary<string, sbyte>();
        private static GameObject _gameObject;
        private static Harmony _harmony;
    }
}