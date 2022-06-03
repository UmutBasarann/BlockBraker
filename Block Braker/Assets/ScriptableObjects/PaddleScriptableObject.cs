using UnityEngine;

namespace BlockBraker.ScriptableObjects.PaddleScriptableObject
{
    [CreateAssetMenu(fileName = "PaddleData", menuName = "ScriptableObjects/PaddleScriptableObject", order = 1)]
    public class PaddleScriptableObject : ScriptableObject
    {
        public Vector2 direction = Vector2.zero;
        public float speed = 30f;
        public bool canMoveVertically = false;
    }
}