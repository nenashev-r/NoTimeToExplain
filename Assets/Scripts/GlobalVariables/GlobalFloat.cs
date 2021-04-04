using System;
using UnityEngine;

namespace GameScripts.GlobalVariables
{
    [CreateAssetMenu( menuName = "GlobalVariables/Float")]
    public class GlobalFloat : ScriptableObject, ISerializationCallbackReceiver
    {
        [NonSerialized][HideInInspector]
        public float value;
        
        [SerializeField] private float InitValue;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            value = InitValue;
        }
    }
}