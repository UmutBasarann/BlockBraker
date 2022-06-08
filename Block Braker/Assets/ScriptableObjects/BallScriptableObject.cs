using UnityEngine;

namespace BlockBraker.ScriptableObjects.BallScriptableObject
{
    [CreateAssetMenu(fileName = "BallData", menuName = "ScriptableObjects/BallScriptableObject", order = 1)]
    public class BallScriptableObject : ScriptableObject
    {
        public Vector2 force = Vector2.zero;
        public float forceValue = 1f;
        public float speed = 500f;
        public int damage = 1500;
        public int life = 3;
    }
}

