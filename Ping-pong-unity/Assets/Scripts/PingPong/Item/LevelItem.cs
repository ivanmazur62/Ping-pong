using UnityEngine;

namespace PingPong
{
    [CreateAssetMenu(fileName = "Level Item", menuName = "ScriptableObjects/Level", order = 1)]
    public class LevelItem : ScriptableObject
    {
        public int horizontalCount = 1;
        public int VerticalCount = 1;
    }
}
