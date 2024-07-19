using RepeatUtil.DesignPattern.SingletonPattern;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Manager 
{
    public class GameManager : Singleton<GameManager>
    {
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public event Action OnGamePaused;
        public event Action OnGameUnpaused;

        public class OnStateChangedEventArgs : EventArgs {
            public GameState NewGameState;
        }

        public enum GameState
        {
            GamePlaying,
            GameOver,
            GamePaused
        }

        [SerializeField]
        private LevelContextSO levelContextSO;

        private GameState state;

        protected override void Awake()
        {
            base.Awake();
            state = GameState.GamePlaying;
        }

        public void TogglePauseGame()
        {
            if (state != GameState.GamePaused) {
                ChangeState(GameState.GamePaused);
                Time.timeScale = 0;
                OnGamePaused?.Invoke();
            } else {
                ChangeState(GameState.GamePlaying);
                Time.timeScale = 1;
                OnGameUnpaused?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            switch (state)
            {
                case GameState.GamePlaying:
                    break;
                case GameState.GameOver:
                    break;
            }
        }

        public void ChangeState(GameState newState)
        {
            state = newState;
            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
            {
                NewGameState = newState
            });
        }
     
        public bool IsGamePlaying() => state == GameState.GamePlaying;
    
        public void PlayAgain() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}