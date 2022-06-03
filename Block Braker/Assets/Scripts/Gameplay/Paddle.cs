using BlockBraker.ScriptableObjects.PaddleScriptableObject;
using BlockBraker.Scripts.Managers;
using BlockBraker.Scripts.Managers.GameManager;
using UnityEngine;
using TouchPhase = UnityEngine.TouchPhase;

namespace BlockBraker.Scripts.Gameplay
{
    public class Paddle : MonoBehaviour
    {
        #region SerializeFields

        [SerializeField] private PaddleScriptableObject paddleScriptableObject = null;

        #endregion

        #region Fields

        private Rigidbody2D _rigidbody;

        #region Constants

        private const float MAXBounciness = 30f;
        private const float Divider = 2f;

        #endregion

        #endregion

        #region Awake | Start | Update

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            GameSceneManager.OnUpdate += OnUpdate;
            Ball.OnLivesEnded += HandleBallLivesEnded;
            GameManager.OnGameWon += HandleGameWon;
        }
        
        private void OnUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * (horizontal * paddleScriptableObject.speed * Time.deltaTime));

            // if (Input.touchCount <= 0)
            // {
            //     return;
            // }
            //
            // Touch touch = Input.GetTouch(0);
            // Vector3 touchPosition = _mainCamera.ScreenToWorldPoint(touch.position);
            // touchPosition.z = 0;
            // paddleScriptableObject.direction = (touchPosition.x - transform.position.x);
            //
            // _rigidbody.velocity = paddleScriptableObject.direction * paddleScriptableObject.speed;
            //
            // if (touch.phase != TouchPhase.Ended)
            // {
            //     return;
            // }
            //
            // _rigidbody.velocity = Vector2.zero;
        }

        #endregion

        #region OnDestroy

        private void OnDestroy()
        {
            GameSceneManager.OnUpdate -= OnUpdate;
        }

        #endregion

        #region OnCollisionEnter2D

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent(out Ball ball))
            {
                return;
            }

            Rigidbody2D ballRigidbody = ball.GetComponent<Rigidbody2D>();



            Vector3 paddlePosition = transform.position;
            Vector2 contactPosition = other.GetContact(0).point;

            float offset = paddlePosition.x - contactPosition.x;
            float width = other.otherCollider.bounds.size.x / Divider;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ballRigidbody.velocity);
            float bounceAngle = (offset / width) * 75f;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -MAXBounciness, MAXBounciness);
            
            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ballRigidbody.velocity = rotation * Vector2.up * ballRigidbody.velocity.magnitude;
        }

        #endregion

        #region Event: HandleGameWon

        private void HandleGameWon()
        {
            Destroy(gameObject);
            
            GameManager.OnGameWon -= HandleGameWon;
        }

        #endregion

        #region Event: HandleBallLivesEnded

        private void HandleBallLivesEnded()
        {
            Destroy(gameObject);
            
            Ball.OnLivesEnded -= HandleBallLivesEnded;
        }

        #endregion
    }
}

