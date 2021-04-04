using UnityEngine;

namespace GameScripts.GlobalVariables
{
    [CreateAssetMenu( menuName = "GlobalVariables/String")]
    public class GlobalString : ScriptableObject
    {
        public string Value;
    }
}