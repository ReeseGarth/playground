using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    private PlayerCapture playerCapture;

    [SerializeField]
    private GameObject gameOverPanel;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        playerCapture.Captured += ShowGameOver;
    }

    private void OnDisable()
    {
        playerCapture.Captured -= ShowGameOver;

        Time.timeScale = 1f;
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (!gameOverPanel.activeSelf)
        {
            return;
        }

        if (
            Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame
        )
        {
            Restart();
        }
    }

    private void Restart()
    {
        Time.timeScale = 1f;

        Scene currentScene =
            SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }
}