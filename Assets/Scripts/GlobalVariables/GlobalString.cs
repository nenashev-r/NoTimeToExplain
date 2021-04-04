using System;
using UnityEngine;

namespace GameScripts.GlobalVariables
{
    [CreateAssetMenu( menuName = "GlobalVariables/String")]
    public class GlobalString : ScriptableObject, ISerializationCallbackReceiver
    {
        [NonSerialized][HideInInspector]
        public string RuntimeValue;
        
        public string InitValue;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            RuntimeValue = InitValue;
        }
    }
}