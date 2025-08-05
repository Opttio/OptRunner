using _OprRunner.Scripts.Core.EventBus;
using _OprRunner.Scripts.UI.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _OprRunner.Scripts.Core.Managers
{
    public class OprGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _runTime;
        [SerializeField] private GameObject _spawners;
        [SerializeField] private Camera _menuCamera;

        private void OnEnable()
        {
            GameEventBus.OnStarted += EnableObjects;
            GameEventBus.OnGameStatus += SwitchGameStatus;
            GameEventBus.OnRestart += RestartGame;
            GameEventBus.OnExit += ExitGame;
        }

        private void OnDisable()
        {
            GameEventBus.OnStarted -= EnableObjects;
            GameEventBus.OnGameStatus -= SwitchGameStatus;
            GameEventBus.OnRestart -= RestartGame;
            GameEventBus.OnExit -= ExitGame;
        }

        private void Start()
        {
            GameModels.ViewId = 0;
            GameEventBus.ChangeView(GameModels.ViewId);
            GameModels.IsStarted = false;
            Time.timeScale = 1f;
        }

        private void EnableObjects(bool obj)
        {
            _menuCamera.gameObject.SetActive(!obj);
            _runTime.SetActive(obj);
            _spawners.SetActive(obj);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void SwitchGameStatus(bool isPlaying)
        {
            Time.timeScale = isPlaying ? 1f : 0f;
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}