using System;
using BlockBraker.ScriptableObjects.BlockScriptableObject;
using BlockBraker.Scripts.Enum;
using UnityEngine;

namespace BlockBraker.Scripts.Gameplay
{
    public class Block : MonoBehaviour
    {
        #region Event | Action

        public static event Action<int, BlockType, Vector3, GameObject> OnBlockDestroyed;

        #endregion
        
        #region SerializeFields

        [SerializeField] private BlockScriptableObject blockScriptableObject = null;

        #endregion

        #region Fields

        public int _blockHealth;

        #endregion

        #region Awake | Start | Update

        private void Start()
        {
            _blockHealth = blockScriptableObject.health;
        }

        #endregion
        
        #region OnCollisionEnter2D

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.TryGetComponent(out Ball ball))
            {
                return;
            }
            
            TakeDamage(ball.GetDamage());
        }

        #endregion

        #region TakeDamage

        public void TakeDamage(int damage)
        {
            _blockHealth -= damage;

            if (_blockHealth <= 0)
            {
                HandleDeath();
                
                if (OnBlockDestroyed != null)
                {
                    OnBlockDestroyed(blockScriptableObject.score, blockScriptableObject.blockType, transform.position, gameObject);
                }
            }
        }

        #endregion

        #region Event: HandleDeath

        private void HandleDeath()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}

