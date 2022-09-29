using System;
using System.Collections.Generic;
using FrameWork;
using UnityEngine;

namespace QuickUtils
{
    public class QuickBag : QuickBase
    {
        protected  override UIElement Element => UIElement.CharacterMenu;
        protected override KeyCode DefaultKeyCode  => KeyCode.E;
        private static int TaiwuCharId => SingletonObject.getInstance<BasicGameData>().TaiwuCharId;
      

      
        private void Update()
        {
            if (Input.GetKeyDown(Key))
            {
                if (Manager.IsElementActive(Element))
                {
                    Manager.HideUI(Element);
                }
                else
                {
                    ArgumentBox argBox = EasyPool.Get<ArgumentBox>();
                    			argBox.Set("CharacterId", TaiwuCharId);
                    			argBox.Set("IsTaiwuTeam", true);
                    			argBox.Set("IsUIBottomEnter", true);
                                Element.SetOnInitArgs(argBox);
                                Manager.ShowUI(Element);
                }
             
               
            }
        }
    }
}