using UnityEngine.InputSystem;

namespace KeyInputVR.KeyMaps
{
    public enum KeyType
    {
        UNSPECIFIED, REGULAR, SHIFT, CAPSLOCK, TAB, SPACE, BACKSPACE, ENTER, ARROW_UP, ARROW_DOWN, ARROW_LEFT, ARROW_RIGHT, DELETE
    };

    public static class KeyTypeExtensions
    {
        public static KeyType ToKeyType(this Key key) => key switch
        {
            Key.LeftShift => KeyType.SHIFT,
            Key.RightShift => KeyType.SHIFT,
            Key.CapsLock => KeyType.CAPSLOCK,
            Key.Tab => KeyType.TAB,
            Key.Space => KeyType.SPACE,
            Key.Backspace => KeyType.BACKSPACE,
            Key.Enter => KeyType.ENTER,
            Key.NumpadEnter => KeyType.ENTER,
            Key.UpArrow => KeyType.ARROW_UP,
            Key.DownArrow => KeyType.ARROW_DOWN,
            Key.LeftArrow => KeyType.ARROW_LEFT,
            Key.RightArrow => KeyType.ARROW_RIGHT,
            Key.Delete => KeyType.DELETE,
            _ => KeyType.UNSPECIFIED
        };
    }
}
