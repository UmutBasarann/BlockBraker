using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlockBraker.Scripts.Gameplay
{
    public class BlockSpawner: MonoBehaviour
    {
        #region Fields

        private GameObject _blockPrefab;

        #endregion

        #region SpawnBlock

        public void SpawnBlock(Grid grid, List<GameObject> activeBlocks)
        {
            for (int i = 0; i < grid.GetXDimension(); i++)
            {
                for (int j = 0; j < grid.GetYDimension(); j++)
                {
                    float spawnRate = Random.value;

                    switch (spawnRate)
                    {
                        case <= .3f:
                            _blockPrefab = Resources.Load("HorizontalBlock") as GameObject;
                            break;
                        case <= .4f:
                            _blockPrefab = Resources.Load("VerticalBlock") as GameObject;
                            break;
                        case <= .5f:
                            _blockPrefab = Resources.Load("RadiusBlock") as GameObject;
                            break;
                        default:
                            _blockPrefab = Resources.Load("DefaultBlock") as GameObject;
                            break;
                    }

                    Vector3 gridPosition = grid.GetWorldPosition(i, j);
                    gridPosition.x += 2f;
                    gridPosition.y += 2f;

                    GameObject blockInstance = Instantiate(_blockPrefab, gridPosition, Quaternion.identity);
                    activeBlocks.Add(blockInstance);
                }
            }
        }

        #endregion
    }    
}