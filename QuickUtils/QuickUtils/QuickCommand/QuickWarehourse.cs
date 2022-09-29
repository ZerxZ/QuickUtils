using System.Collections.Generic;
using UnityEngine;

namespace QuickUtils
{
    public class QuickWarehourse : QuickBase
    {
        protected override UIElement Element => UIElement.Warehouse;
        protected override KeyCode DefaultKeyCode => KeyCode.Z;

        private void Update()
        {
            if (Input.GetKeyDown(Key) )
            {
                if (Manager.IsElementActive(Element))
                {
                    Manager.HideUI(Element);
                }
                else
                {
                    Manager.ShowUI(Element);
                }
                
            }
        }
    }
}