using UnityEngine;
using UnityEngine.UI;

namespace PingPong
{
    public class NextLevelPopup : MonoBehaviour
    {
        public Text levelNumber;

        public void ShowLevel(int levelIndex)
        {
            levelNumber.text = levelIndex.ToString();      
        }
    }
}
