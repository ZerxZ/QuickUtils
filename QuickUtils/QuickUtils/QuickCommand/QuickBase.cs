using UnityEngine;

namespace QuickUtils
{
    abstract public class QuickBase:MonoBehaviour
    {
        protected static UIManager Manager => UIManager.Instance;
        abstract protected  UIElement Element { get; }
        abstract protected KeyCode DefaultKeyCode{ get; }

        protected internal QuickUtils QuickUtils{ get; set; }
        protected KeyCode Key=>QuickUtils.BindKeycode(GetType().Name) ?? DefaultKeyCode;
    }
}