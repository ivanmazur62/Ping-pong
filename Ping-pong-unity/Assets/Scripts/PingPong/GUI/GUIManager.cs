using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PingPong
{
    public class GUIManager : MonoBehaviour
    {
        public MainMenuGUI mainMenuGUI;
        public NextLevelPopup nextLevelPopup;
        public Button pauseBtn;
        public Text currentLvl;        

        public void SetMenu(MainMenuType menuType)
        {
            mainMenuGUI.SetMenu(menuType);
            pauseBtn.gameObject.SetActive(menuType == MainMenuType.None);
            currentLvl.transform.parent.gameObject.SetActive(menuType == MainMenuType.None);
        }

        public void UpdateLevel(int levelIndex)
        {
            currentLvl.text = levelIndex.ToString();
            StartCoroutine(ShowPopup(levelIndex));
        }

        private IEnumerator ShowPopup(int levelIndex)
        {
            nextLevelPopup.ShowLevel(levelIndex);

            yield return new WaitForSeconds(0.5f);
            nextLevelPopup.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            nextLevelPopup.gameObject.SetActive(false);
        }
    }
}