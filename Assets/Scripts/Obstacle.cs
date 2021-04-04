using System;
using GameScripts.Interaction;
using UnityEngine;

namespace GameScripts
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private float m_Damage;

        private void OnCollisionEnter2D(Collision2D other)
        {
            IDamageable damageable = other.transform.root.GetComponent<IDamageable>();
            if (damageable == null)
            {
                return;
            }
                
            
            damageable.AddDamage(m_Damage);
            
            gameObject.SetActive(false);
        }
    }
}