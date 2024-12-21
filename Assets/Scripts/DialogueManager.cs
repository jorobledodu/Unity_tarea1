using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string characterName; // Name of the character speaking
        [TextArea(3, 5)]
        public string dialogueText; // The dialogue text
    }

    public TextMeshProUGUI characterNameText; // The UI element for character's name
    public TextMeshProUGUI dialogueText; // The dialogue text UI element
    public Button nextButton; // The next button UI element
    public GameObject dialogueBox; // The dialogue box container

    public DialogueLine[] dialogueLines; // Array of dialogue lines
    private int currentLineIndex = 0; // Tracks the current dialogue line

    private void Start()
    {
        // Pause the game and start the dialogue
        Time.timeScale = 0;
        ShowDialogue();
        //Disable locomotion player script

        // Set up the Next button listener
        nextButton.onClick.AddListener(DisplayNextLine);
    }

    private void ShowDialogue()
    {
        dialogueBox.SetActive(true); // Show the dialogue box
        UpdateDialogueUI(); // Show the first line
    }

    private void DisplayNextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            // Show the next line
            UpdateDialogueUI();
        }
        else
        {
            // End the dialogue
            EndDialogue();
        }
    }

    private void UpdateDialogueUI()
    {
        // Update the character's name and dialogue text
        characterNameText.text = dialogueLines[currentLineIndex].characterName;
        dialogueText.text = dialogueLines[currentLineIndex].dialogueText;
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box
        Time.timeScale = 1; // Resume the game
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tarea 1");
    }
    public void CloseDialogueImmediately()
    {
        // Immediately end the dialogue
        currentLineIndex = dialogueLines.Length; // Skip all remaining lines
        EndDialogue();
    }
}