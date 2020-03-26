using System.Collections.Generic;
using UnityEngine;

namespace PingPong
{
    public class LevelController : MonoBehaviour
    {
        public Block block;
        public List<Block> blocks;
        public List<LevelItem> levels;
        private LevelItem currentLevelItem;

        private Rect buildBorder = new Rect(-2.7f, 4.7f, 5.5f, 1.7f);

        public void BuildLevel()
        {
            int currentLevelIndex = GameController.Instance.CurrentLevel <= levels.Count ? GameController.Instance.CurrentLevel - 1 : levels.Count - 1;
            
            currentLevelItem = levels[currentLevelIndex];
            for (int i = 1; i <= currentLevelItem.VerticalCount; i++)
            {
                float zPos = buildBorder.yMin - (buildBorder.height / currentLevelItem.VerticalCount) * (i - 0.5f);
                for (int k = 1; k <= currentLevelItem.horizontalCount; k++)
                {
                    float xPos = buildBorder.xMin + (buildBorder.width / currentLevelItem.horizontalCount) * (k - 0.5f);

                    Vector3 blockPos = new Vector3(xPos, gameObject.transform.position.y, zPos);
                    blocks.Add(Instantiate(block, blockPos, Quaternion.identity, gameObject.transform));
                }
            }
        }

        public void DestroyBlock(Block item)
        {
            blocks.Remove(item);
            Destroy(item.gameObject);
            
            if (blocks.Count <= 0)            
                GameController.Instance.StartNewLevel();
        }
    }
}