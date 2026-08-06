using System;
using UnityEngine;

public class ShiftResults : MonoBehaviour
{
    [SerializeField]
    private ExitZone exitZone;

    [SerializeField]
    private ShiftProgress shiftProgress;

    public event Action<ShiftResult> ResultCreated;

    private void OnEnable()
    {
        exitZone.PlayerEntered += HandlePlayerEntered;
    }

    private void OnDisable()
    {
        exitZone.PlayerEntered -= HandlePlayerEntered;
    }

    private void HandlePlayerEntered()
    {
        bool requiredComplete =
            shiftProgress.IsRequiredCleaningComplete;

        bool optionalComplete =
            shiftProgress.IsOptionalCleaningComplete;

        int pay =
            (requiredComplete ? 150 : 0) +
            (optionalComplete ? 100 : 0);

        int rating;

        if (!requiredComplete)
        {
            rating = 1;
        }
        else if (optionalComplete)
        {
            rating = 5;
        }
        else
        {
            rating = 4;
        }

        ShiftResult result = new ShiftResult(
            requiredComplete,
            optionalComplete,
            pay,
            rating
        );

        ResultCreated?.Invoke(result);

        Debug.Log(
            "Shift extracted\n" +
            $"Required cleaning: {result.IsRequiredCleaningComplete}\n" +
            $"Optional cleanup: {result.IsOptionalCleaningComplete}\n" +
            $"Pay: ${result.Pay}\n" +
            $"Contractor rating: {result.Rating}/5",
            this
        );
    }
}
