using UnityEngine;

namespace BlockBraker.Scripts.Gameplay
{
    public class Grid
    {
        #region Fields

        private int _width;
        private int _height;

        private int[,] _grid;
        private float _cellSize;

        private Vector3 _origin;

        #endregion

        #region Constructor

        public Grid(int width, int height, float cellSize, Vector3 origin)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _origin = origin;

            _grid = new int[width, height];

            for (int x = 0; x < _grid.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.GetLength(1); y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y +1), Color.red, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 100f);
                }
            }
            
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.red, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.red, 100f);
        }

        #endregion

        #region Get: XDimension

        public int GetXDimension()
        {
            return _grid.GetLength(0);
        }

        #endregion

        #region Get: YDimension

        public int GetYDimension()
        {
            return _grid.GetLength(1);
        }

        #endregion

        #region Get: WorldPosition

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _cellSize + _origin;
        }

        #endregion

        #region Get: GridPosition

        private void GetGridPosition(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPosition - _origin).x / _cellSize);
            y = Mathf.FloorToInt((worldPosition - _origin).y / _cellSize);
        }

        #endregion

        #region Get: Value

        public int GetValue(int x, int y)
        {
            if (x < 0 && y < 0 && x >= _width && y >= _height)
            {
                return 0;
            }

            return _grid[x, y];
        }

        public int GetValue(Vector3 worldPosition)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            return GetValue(x, y);
        }

        #endregion

        #region Set: Value

        private void SetValue(int x, int y, int value)
        {
            if (x < 0 && y < 0 && x >= _width && y >= _height)
            {
                return;
            }

            _grid[x, y] = value;
        }

        public void SetValue(Vector3 worldPosition, int value)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            SetValue(x, y, value);
        }

        #endregion

    }
}

