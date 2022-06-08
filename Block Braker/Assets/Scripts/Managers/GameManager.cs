using System;
using System.Collections.Generic;
using BlockBraker.Scripts.Config.GameConfig;
using BlockBraker.Scripts.Enum;
using BlockBraker.Scripts.Gameplay;
using BlockBraker.Scripts.Scoreboards;
using UnityEngine;
using Grid = BlockBraker.Scripts.Gameplay.Grid;
using Random = UnityEngine.Random;

namespace BlockBraker.Scripts.Managers.GameManager
{
    public class GameManager : MonoBehaviour
    {
        #region Event | Action

        public static event Action OnGameWon;

        #endregion

        #region Singleton

        private static GameManager _instance;
        public static GameManager Instance => _instance;

        #endregion

        #region SerializeFields

        [SerializeField] private Ball ball = null;
        [SerializeField] private BlockSpawner blockSpawner = null;
        [SerializeField] private GameObject endGameUI = null;
        [SerializeField] private GameObject gameWonUI = null;

        #endregion

        #region Fields

        private Grid _grid;
        private GameObject _blockPrefab;

        private int _width;
        private int _height;
        private Vector3 _origin;

        private List<GameObject> _activeBlocks = new List<GameObject>();

        #region Constants

        private const float CellSize = 4f;
        private const float PositionOffset = 2f;

        #endregion

        #endregion

        #region Awake | Start | Update

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(_instance.gameObject);
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }

        public void Start()
        {
            Init();
        }

        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(Vector3.zero, 12f);
        }

        #region Init

        public void Init()
        {
            _width = Random.Range(4, 13);
            _height = Random.Range(4, 7);
            _origin = new Vector3(-_width * PositionOffset, -_height);
            
            _grid = new Grid(_width, _height, CellSize, _origin);
            
            blockSpawner.SpawnBlock(_grid, _activeBlocks);

            GameConfig.CURRENT_GAME_SCORE = 0;
            
            Block.OnBlockDestroyed += HandleBlockDestroyed;
            Ball.OnLivesEnded += HandleBallLivesEnded;
        }

        #endregion

        #region Event: HandleBallLivesEnded

        private void HandleBallLivesEnded()
        {
            endGameUI.SetActive(true);
            
            foreach (var activeBlock in _activeBlocks)
            {
                Destroy(activeBlock);
            }
            
            _activeBlocks.Clear();
            
            Ball.OnLivesEnded -= HandleBallLivesEnded;
        }

        #endregion

        #region Event: HandleBlockDestroyed

        private void HandleBlockDestroyed(int life, BlockType blockType, Vector3 blockPosition, GameObject currentBlock)
        {
            blockPosition.x -= PositionOffset;
            blockPosition.y -= PositionOffset;
            blockPosition.z = 0f;

            switch (blockType)
            {
                case BlockType.HorizontalBlock:
                    List<GameObject> horizontalBlocks = new List<GameObject>();
                    
                    foreach (GameObject activeBlock in _activeBlocks)
                    {
                        if (activeBlock is null)
                        {
                            _activeBlocks.Remove(currentBlock);
                            return;
                        }
                        
                        if (activeBlock.transform.position.y == currentBlock.transform.position.y)
                        {
                            horizontalBlocks.Add(activeBlock);
                        }
                    }
                    
                    Debug.Log($"HorizontalBlockCount: {horizontalBlocks.Count}");
                    
                    foreach (GameObject horizontalBlock in horizontalBlocks)
                    {
                        Block block = horizontalBlock.GetComponent<Block>();
                        block.TakeDamage(ball.GetDamage());
                    }
                    
                    horizontalBlocks.Clear();
                    _activeBlocks.Remove(currentBlock);
                    break;
                case BlockType.VerticalBlock:
                    List<GameObject> verticalBlocks = new List<GameObject>();
                    
                    foreach (GameObject activeBlock in _activeBlocks)
                    {
                        if (activeBlock is null)
                        {
                            _activeBlocks.Remove(currentBlock);
                            return;
                        }
                        
                        if (activeBlock.transform.position.x == currentBlock.transform.position.x)
                        {
                            verticalBlocks.Add(activeBlock);
                        }
                    }
                    
                    Debug.Log($"HorizontalBlockCount: {verticalBlocks.Count}");
                    
                    foreach (GameObject verticalBlock in verticalBlocks)
                    {
                        Block block = verticalBlock.GetComponent<Block>();
                        block.TakeDamage(ball.GetDamage());
                    }
                    
                    verticalBlocks.Clear();
                    _activeBlocks.Remove(currentBlock);
                    break;
                case BlockType.RadiusBlock:
                    int maxColliders = 8;
                    Collider[] hitColliders = new Collider[maxColliders];
                    int numOfColliders = Physics.OverlapSphereNonAlloc(currentBlock.transform.position, 12f, hitColliders);

                    if (numOfColliders == 0)
                    {
                        _activeBlocks.Remove(currentBlock);
                        return;
                    }
                    
                    for (int i = 0; i < numOfColliders; i++)
                    {
                        Block block = hitColliders[i].GetComponent<Block>();
                        block.TakeDamage(ball.GetDamage());
                    }
                    
                    Array.Clear(hitColliders, 0, hitColliders.Length);
                    _activeBlocks.Remove(currentBlock);
                    break;
                default:
                    _activeBlocks.Remove(currentBlock);
                    break;
            }

            if (_activeBlocks.Count == 0)
            {
                gameWonUI.SetActive(true);
                _activeBlocks.Clear();

                ScoreboardEntryData scoreboardEntryData = new ScoreboardEntryData
                {
                    entryScore = GameConfig.CURRENT_GAME_SCORE
                };
                
                Scoreboard.Instance.AddEntry(scoreboardEntryData);

                if (OnGameWon != null)
                {
                    OnGameWon();
                    
                    Block.OnBlockDestroyed -= HandleBlockDestroyed;
                }
            }
        }

        #endregion
    }
}

