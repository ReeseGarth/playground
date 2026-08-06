using UnityEngine;

public class ShiftProgress : MonoBehaviour
{
    [SerializeField]
    private CleanableMess requiredCleaning;

    [SerializeField]
    private CleanableMess optionalCleaning;

    public bool IsRequiredCleaningComplete
    {
        get;
        private set;
    }

    public bool IsOptionalCleaningComplete
    {
        get;
        private set;
    }

    private void OnEnable()
    {
        optionalCleaning.Cleaned +=
            HandleOptionalCleaningCompleted;

        requiredCleaning.Cleaned +=
            HandleRequiredCleaningCompleted;
    }

    private void OnDisable()
    {
        if (optionalCleaning != null)
        {
            optionalCleaning.Cleaned -=
                HandleOptionalCleaningCompleted;
        }

        if (requiredCleaning != null)
        {
            requiredCleaning.Cleaned -=
                HandleRequiredCleaningCompleted;
        }
    }

    private void HandleOptionalCleaningCompleted(
        CleanableMess cleanableMess
    )
    {
        IsOptionalCleaningComplete = true;

        Debug.Log(
            "Optional shift cleaning complete",
            this
        );
    }

    private void HandleRequiredCleaningCompleted(
        CleanableMess cleanableMess
    )
    {
        IsRequiredCleaningComplete = true;

        Debug.Log(
            "Required shift cleaning complete",
            this
        );
    }
}
