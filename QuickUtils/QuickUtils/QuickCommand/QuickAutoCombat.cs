using System;
using System.Net.NetworkInformation;
using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using Spine.Unity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace QuickUtils
{
    internal enum EAutoCombat
    {
        Default,
        Animal,
        Boss,
    }

    internal enum ESpeed
    {
        Up,
        Normal,
        Down
    }

    public class QuickAutoCombat : QuickBase
    {
        protected override UIElement Element => null;
        protected override KeyCode DefaultKeyCode => KeyCode.Z;


        private void Update()
        {
            if (Input.GetKeyDown(Key))
            {
                UI_Combat_Update_Patch.SetAutoFight();
            }
        }
    }


    [HarmonyPatch(typeof(UI_Combat), "Update")]
    class UI_Combat_Update_Patch
    {
        public static bool isAutoFight = false;
        public static ESpeed SpeedState = ESpeed.Normal;
        internal static Traverse<bool> autoCombat;
        internal static Traverse onClickAutoFight;
        internal static Traverse onClickSpeedUp;
        internal static Traverse onClickSpeedDown;
        internal static Traverse<float> displayTimeScale;
        private static UI_Combat _uiCombat;


        public static void SetAutoFight()
        {
            SetAutoFight(!isAutoFight);
        }

        public static void SetAutoFight(bool _isAutoCombat)
        {
            isAutoFight = _isAutoCombat;
            var isAutoCombat = autoCombat.Value;
            if (isAutoFight != isAutoCombat)
            {
                onClickAutoFight?.GetValue();
            }
        }

        public static void Destroy()
        {
            autoCombat = null;
            displayTimeScale = null;
            onClickAutoFight = null;
            onClickSpeedUp = null;
            onClickSpeedDown = null;
            InitSetting();
        }

        [CanBeNull]
        private static Traverse GetMethodInfo(string methodName, object[] agruments)
        {
            return Traverse.Create(_uiCombat).Method(methodName, agruments);
        }

        private static void InitSetting(EAutoCombat combat = EAutoCombat.Default)
        {
            var modIdStr = QuickUtils.Instance.ModIdStr;
            string key = "Key_DefalutAutoCombat";
            string speedKey = "Key_DefalutSpeed";
            switch (combat)
            {
                case EAutoCombat.Default:
                    break;
                case EAutoCombat.Boss:
                case EAutoCombat.Animal:
                    var name = Enum.GetName(typeof(EAutoCombat), combat);
                    key += name;
                    speedKey += name;
                    break;
            }

            string speedValue = string.Empty;
            ModManager.GetSetting(modIdStr, key, ref isAutoFight);
            ModManager.GetSetting(modIdStr, speedKey, ref speedValue);
            switch (speedValue)
            {
                case "默认":
                    speedValue = "Normal";
                    break;
                case "半倍":
                    speedValue = "Down";

                    break;
                case "二倍":
                    speedValue = "Up";
                    break;
            }

            if (!Enum.TryParse(speedValue, out SpeedState))
            {
                Debug.Log(SpeedState);
                //SpeedState = ESpeed.Normal;
            }
        }

        private static void SetSpeed()
        {
            var displayTime = CheckSpeed();
            Debug.Log($"displayTime:{displayTime} SpeedState :{SpeedState}");
            switch (displayTime)
            {
                case ESpeed.Down:
                    switch (SpeedState)
                    {
                        case ESpeed.Down:
                            break;
                        case ESpeed.Normal:
                            SetSpeedUp();
                            break;
                        case ESpeed.Up:
                            SetSpeedUp();
                            SetSpeedUp();
                            break;
                    }

                    break;
                case ESpeed.Normal:
                    switch (SpeedState)
                    {
                        case ESpeed.Down:
                            SetSpeedDown();
                            break;
                        case ESpeed.Normal:
                            break;
                        case ESpeed.Up:
                            SetSpeedUp();
                            break;
                    }

                    break;
                case ESpeed.Up:
                    switch (SpeedState)
                    {
                        case ESpeed.Down:
                            SetSpeedDown();
                            SetSpeedDown();
                            break;
                        case ESpeed.Normal:
                            SetSpeedDown();
                            break;
                        case ESpeed.Up:
                            break;
                    }

                    break;
            }
        }

        public static void SetSpeedUp()
        {
            switch (SpeedState)
            {
                case ESpeed.Down:
                    onClickSpeedUp.GetValue();
                    SpeedState = ESpeed.Normal;
                    break;
                case ESpeed.Normal:
                    onClickSpeedUp.GetValue();
                    SpeedState = ESpeed.Up;
                    break;
                case ESpeed.Up:
                    if (CheckSpeed() != ESpeed.Up)
                    {
                        onClickSpeedUp.GetValue();
                    }

                    break;
            }
        }

        public static void SetSpeedDown()
        {
            switch (SpeedState)
            {
                case ESpeed.Down:
                    if (CheckSpeed() != ESpeed.Down)
                    {
                        onClickSpeedDown.GetValue();
                    }

                    break;
                case ESpeed.Normal:
                    onClickSpeedDown.GetValue();
                    SpeedState = ESpeed.Down;
                    break;
                case ESpeed.Up:
                    onClickSpeedDown.GetValue();
                    SpeedState = ESpeed.Normal;
                    break;
            }
        }

        private static ESpeed CheckSpeed()
        {
            ESpeed eSpeed = ESpeed.Normal;
            switch (displayTimeScale.Value)
            {
                case 0.5f:
                    eSpeed = ESpeed.Down;
                    break;
                case 1f:
                    eSpeed = ESpeed.Normal;
                    break;
                case 2f:
                    eSpeed = ESpeed.Up;
                    break;
            }

            return eSpeed;
        }

   

        public static void Postfix(UI_Combat __instance)
        {
            if (!QuickUtils.QuickBases.ContainsKey(nameof(QuickAutoCombat)))
            {
                Debug.Log("QuickAutoCombat初始化");
                _uiCombat = __instance;
                QuickUtils.ComponetAbleWorldMap();
                QuickUtils.BindComponet<QuickAutoCombat>();
                autoCombat = Traverse.Create(__instance).Field<bool>("_autoCombat");
                displayTimeScale = Traverse.Create(__instance).Field<float>("_displayTimeScale");
                // ReSharper disable once PossibleNullReferenceException
                var isBoss = GetMethodInfo("IsBoss", new object[] { false }).GetValue<bool>();
                // ReSharper disable once PossibleNullReferenceException
                var isAnimal = GetMethodInfo("isAnimal", new object[] { false }).GetValue<bool>();
                var allAutoCombat = true;
                var modIdStr = QuickUtils.Instance.ModIdStr;
                ModManager.GetSetting(modIdStr, "Key_DefalutAutoCombat", ref allAutoCombat);
                if (allAutoCombat)
                {
                    if (isBoss)
                    {
                        InitSetting(EAutoCombat.Boss);
                    }
                    else if (isAnimal)
                    {
                        InitSetting(EAutoCombat.Animal);
                    }
                    else
                    {
                        InitSetting();
                    }
                }


                var arr = __instance.GetComponentsInChildren<CButton>();

                foreach (var btn in arr)
                {
                    switch (btn.name)
                    {
                        case "AutoFight":
                            onClickAutoFight = GetMethodInfo("OnClick", new[] { btn });
                            break;
                        case "SpeedUp":
                            onClickSpeedUp = GetMethodInfo("OnClick", new[] { btn });
                            break;
                        case "SpeedDown":
                            onClickSpeedDown = GetMethodInfo("OnClick", new[] { btn });
                            break;
                    }
                }

                bool isAutoCombat = autoCombat.Value;
                if (isAutoFight != isAutoCombat)
                {
                    SetAutoFight(isAutoFight);
                    SetSpeed();
                }
            }
        }
    }

    [HarmonyPatch(typeof(UI_CombatResult), "QuickHide")]
    class UI_CombatResult_QuickHide_Patch
    {
        private static void DestroyComponetUICombat()
        {
            QuickUtils.DestroyComponet<QuickAutoCombat>();
        }

        public static void Postfix()
        {
            UI_Combat_Update_Patch.Destroy();
            DestroyComponetUICombat();
            QuickUtils.ComponetAbleWorldMap();
            Debug.Log("销毁状态");
        }
    }
}