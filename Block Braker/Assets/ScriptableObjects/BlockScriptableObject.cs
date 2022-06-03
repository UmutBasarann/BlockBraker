using BlockBraker.Scripts.Enum;
using UnityEngine;

namespace BlockBraker.ScriptableObjects.BlockScriptableObject
{
    [CreateAssetMenu(fileName = "BlockData", menuName = "ScriptableObjects/BlockScriptableObject", order = 1)]
    public class BlockScriptableObject : ScriptableObject
    {
        public BlockType blockType;
        public int health = 1500;
        public int score = 150;
    }
}

