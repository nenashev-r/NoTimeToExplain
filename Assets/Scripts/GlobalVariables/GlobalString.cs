using System;
using UnityEngine;

namespace GameScripts.GlobalVariables
{
    [CreateAssetMenu( menuName = "GlobalVariables/String")]
    public class GlobalString : ScriptableObject, ISerializationCallbackReceiver
    {
        [NonSerialized][HideInInspector]
        public string value;
        
        [SerializeField] private string InitValue;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            value = InitValue;
        }
    }
}