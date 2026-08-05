using TMPro;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour
{
    [SerializeField]
    private TMP_Text objectiveText;

    [SerializeField]
    private PlayerInventory playerInventory;

    [SerializeField]
    private string requiredItemId = "key";

    [Header("Objective Text")]

    [SerializeField]
    private string findItemText =
        "Objective: Find the key";

    [SerializeField]
    private string exitText =
        "Objective: Exit through the door";

    [SerializeField]
    private string completeText =
        "Objective complete";

    private bool isExitComplete;

    private void OnEnable()
    {
        playerInventory.ItemAdded += OnItemAdded;
    }

    private void Start()
    {
        RefreshObjectiveText();
    }

    private void OnDisable()
    {
        playerInventory.ItemAdded -= OnItemAdded;
    }

    private void OnItemAdded(string itemId)
    {
        if (itemId == requiredItemId)
        {
            RefreshObjectiveText();
        }
    }

    public void CompleteExitObjective()
    {
        if (isExitComplete)
        {
            return;
        }

        isExitComplete = true;

        RefreshObjectiveText();

        Debug.Log("Objective complete: Exit the room");
    }

    private void RefreshObjectiveText()
    {
        if (isExitComplete)
        {
            objectiveText.text = completeText;
        }
        else if (playerInventory.Contains(requiredItemId))
        {
            objectiveText.text = exitText;
        }
        else
        {
            objectiveText.text = findItemText;
        }
    }
}
