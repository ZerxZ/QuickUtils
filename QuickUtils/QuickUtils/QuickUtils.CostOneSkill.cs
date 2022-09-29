using System.Reflection;
using Config;
using UnityEngine;

namespace QuickUtils
{
    public partial class QuickUtils
    {
        private void SkillOneCost(bool reset = false)
        {
            var value = false;
            if (!reset)
            {
                ModManager.GetSetting(ModIdStr, $"Key_SkillOneCost", ref value);
                var msg = value ? "开启" : "关闭";
                Debug.Log($"SkillOneCost设置为{msg}");
            }
           

            CombatSkill.Instance.Iterate(item =>
            {
                Debug.Log($"{item.Name}:改前GridCost为{item.GridCost}格子消耗");
                var GridCost = item.GetType().GetField("GridCost", BindingFlags.Instance | BindingFlags.Public);
                if (value )
                {
                    var gridCost = (sbyte)GridCost?.GetValue(item);
                    if (gridCost != 0 && !GridCostDict.ContainsKey(item.Name))
                    {
                        GridCostDict.Add(item.Name, gridCost);
                    }

                    GridCost?.SetValue(item, (sbyte)1);
                    Debug.Log($"{item.Name}:改后GridCost为1格子消耗");
                }
                else
                {
                    if (GridCostDict.Count != 0 && GridCostDict.ContainsKey(item.Name))
                    {
                        var num = GridCostDict[item.Name];
                        GridCost?.SetValue(item, num);
                        Debug.Log($"{item.Name}:改后GridCost为{num}格子消耗");
                    }
                    else
                    {
                        return false;
                    }
                 
                }

                return true;
            });
        }
    }
}