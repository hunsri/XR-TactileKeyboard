using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace KeyInputVR.KeyMaps
{
    public interface IKeyMap
    {
        public Dictionary<Key, KeyDefinition> GetKeyMap();
    }
}
