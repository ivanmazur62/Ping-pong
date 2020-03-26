using UnityEngine;

namespace PingPong
{
    public enum GameState {Start, Game, Pause }

    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        public GUIManager guiManager;
        public InputContrloller InputContrloller;
        public LevelController levelController;

        public Enemy enemy;
        public Ball ball;
        public Transform StartBallPosition;
        private Ball currentBall;

        public GameState currentState { get; private set; } = GameState.Start;

        public int CurrentLevel { get; private set; }        

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance == this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            InputContrloller.Initialisation();                        
            guiManager.SetMenu(MainMenuType.Main);
        }

        private void FixedUpdate()
        {
            if (currentState == GameState.Game)
            {
                if(currentBall == null)
                    UpdateBall();

                InputContrloller.LocalUpdate();
                currentBall.LocalUpdate();
                enemy.LocalUpdate();
            }
        }

        public void StartNewLevel()
        {
            CurrentLevel++;
            UpdateBall();
            levelController.BuildLevel();
            guiManager.UpdateLevel(CurrentLevel);
            enemy.SetSpeed(CurrentLevel);
        }

        private void UpdateBall()
        {
            currentBall = Instantiate(ball, StartBallPosition.position, Quaternion.identity);
            InputContrloller.ball = currentBall.gameObject;
            enemy.targetBall = currentBall.gameObject;                        
        }

        //GUI Methods
        public void StartGame()
        {
            StartNewLevel();
            enemy.SetSpeed(CurrentLevel);
            guiManager.SetMenu(MainMenuType.None);
            guiManager.UpdateLevel(CurrentLevel);
            currentState = GameState.Game;
            Time.timeScale = 1;
        }

        public void PauseGame()
        {
            currentState = GameState.Pause;
            guiManager.SetMenu(MainMenuType.Game);
            Time.timeScale = 0;
        }

        public void ContinueGame()
        {
            currentState = GameState.Game;
            guiManager.SetMenu(MainMenuType.None);
            Time.timeScale = 1;
        }

        public void ExitGame()
        {
            Debug.Log("ExitGame");
            Application.Quit();
        }       
    }
}