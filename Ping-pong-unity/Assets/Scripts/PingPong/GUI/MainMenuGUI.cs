using UnityEngine;
using UnityEngine.UI;

namespace PingPong
{
    public enum MainMenuType { Main, Game, None }

    public class MainMenuGUI : MonoBehaviour
    {            
        public Button startBtn;
        public Button continueBtn;
        public Button exitBtn;

        public void SetMenu(MainMenuType menuType)
        {
            startBtn.gameObject.SetActive(menuType == MainMenuType.Main);
            continueBtn.gameObject.SetActive(menuType == MainMenuType.Game);
            exitBtn.gameObject.SetActive(menuType != MainMenuType.None);            
            gameObject.SetActive(menuType != MainMenuType.None);
        }
    }
}