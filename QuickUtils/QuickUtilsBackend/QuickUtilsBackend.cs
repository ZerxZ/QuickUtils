using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Config;
using GameData;
using GameData.Common;
using GameData.Domains;
using GameData.Domains.Character;
using GameData.Domains.Character.Creation;
using GameData.Domains.Character.ParallelModifications;
using GameData.Domains.Taiwu;
using GameData.Domains.TaiwuEvent.EventHelper;
using TaiwuModdingLib.Core.Plugin;
using HarmonyLib;
using Newtonsoft.Json;
using Character = GameData.Domains.Character.Character;

namespace QuickUtilsBackend
{
    [PluginConfig("QuickUtilsBackend", "Zerxz", "1.0.0")]
    public class QuickUtilsBackend : TaiwuRemakeHarmonyPlugin
    {
        public override void Initialize()
        {
            HarmonyInstance.PatchAll(typeof(TaiwuDomain_CanTransferItemToWarehouse_Patch));
        }

        public override void Dispose()
        {
            HarmonyInstance.UnpatchSelf();
        }
    }

    [HarmonyPatch(typeof(TaiwuDomain), "CanTransferItemToWarehouse")]
    public class TaiwuDomain_CanTransferItemToWarehouse_Patch
    {
        public static void Postfix(ref bool __result)
        {
            if (!__result)
            {
                __result = true;
            }
        }
    }

}