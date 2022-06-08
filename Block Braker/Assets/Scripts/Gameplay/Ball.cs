using System;
using BlockBraker.ScriptableObjects.BallScriptableObject;
using BlockBraker.Scripts.Managers.GameManager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlockBraker.Scripts.Gameplay
{
    public class Ball : MonoBehaviour
    {
        #region Event | Action

        public static event Action<int> OnDeathZoneTriggered;
        public static event Action OnLivesEnded;

        #endregion
        
        #region SerializeFields

        [SerializeField] private BallScriptableObject ballScriptableObject = null;

        #endregion
        
        #region Fields

        private Rigidbody2D _rigidbody;

        #region Constants

        private const float InvokeTime = 1.5f;
        public int _life;

        #endregion

        #endregion

        #region Awake | Start | Update

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _life = ballScriptableObject.life;
        }

        private void Start()
        {
            Init();
            
            GameManager.OnGameWon += HandleGameWon;
        }

        #endregion

        #region Init

        private void Init()
        {
            transform.position = new Vector3(0f, -10f);
            Invoke(nameof(SetRandomTrajectory), InvokeTime);
        }

        #endregion

        #region Set: RandomTrajectory

        private void SetRandomTrajectory()
        {
            ballScriptableObject.force.x = Random.Range(-ballScriptableObject.forceValue, ballScriptableObject.forceValue);
            ballScriptableObject.force.y = ballScriptableObject.forceValue;
        
            _rigidbody.AddForce(ballScriptableObject.force * ballScriptableObject.speed);
        }

        #endregion
        
        #region Get: Damage

        public int GetDamage()
        {
            return ballScriptableObject.damage;
        }

        #endregion

        #region LoseLive

        private void LoseLife()
        {
            _life--;

            if (_life <= 0)
            {
                Destroy(gameObject);
                
                if (OnLivesEnded != null)
                {
                    OnLivesEnded();
                }
            }
        }

        #endregion

        #region OnTriggerEnter

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("BottomBorder"))
            {
                return;
            }

            HandleBallTrigger();
            
            Debug.Log($"Total Lives Left: {_life}");

            if (OnDeathZoneTriggered != null)
            {
                OnDeathZoneTriggered(_life);
            }
        }

        #endregion

        #region HandleBallTrigger

        private void HandleBallTrigger()
        {
            LoseLife();

            Init();
        }

        #endregion

        #region Event: HandleGameWon

        private void HandleGameWon()
        {
            Destroy(gameObject);
            
            GameManager.OnGameWon -= HandleGameWon;
        }

        #endregion
    }    
}

