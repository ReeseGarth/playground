using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ShiftResultsUI : MonoBehaviour
{
    [SerializeField]
    private ShiftResults shiftResults;

    [SerializeField]
    private GameObject resultsPanel;

    [SerializeField]
    private TMP_Text resultsText;

    private void Awake()
    {
        resultsPanel.SetActive(false);
    }

    private void OnEnable()
    {
        shiftResults.ResultCreated += ShowResult;
    }

    private void OnDisable()
    {
        shiftResults.ResultCreated -= ShowResult;

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (!resultsPanel.activeSelf)
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

    private void ShowResult(ShiftResult result)
    {
        string outcome =
            result.IsRequiredCleaningComplete
                ? "SHIFT COMPLETE"
                : "SHIFT ABANDONED";

        string requiredStatus =
            result.IsRequiredCleaningComplete
                ? "Complete"
                : "Incomplete";

        string optionalStatus =
            result.IsOptionalCleaningComplete
                ? "Complete"
                : "Incomplete";

        resultsText.text =
            $"{outcome}\n\n" +
            $"Required cleaning: {requiredStatus}\n" +
            $"Optional cleanup: {optionalStatus}\n" +
            $"Pay: ${result.Pay}\n" +
            $"Contractor rating: {result.Rating}/5\n\n" +
            "Press R to work another shift";

        resultsPanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Restart()
    {
        Time.timeScale = 1f;

        Scene currentScene =
            SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.buildIndex);
    }
}