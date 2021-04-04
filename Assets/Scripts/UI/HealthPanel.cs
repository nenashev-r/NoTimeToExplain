using System;
using GameScripts.GlobalVariables;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_Text;
        [SerializeField] private GlobalFloat m_PlayerHP;

        private void Start()
        {
            UpdateValue();
        }

        public void UpdateValue()
        {
            m_Text.text = "HP " + m_PlayerHP.value;
        }
    }
}
