using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;
    public Button nextButton;

    private string[] dialogues = {
        "Father: Son, I'm giving you your first mini-excavator.",
        "Father: Use it wisely. Build something amazing.",
        "Son: Thank you, Dad. I won’t let you down.",
        "Your journey begins now!"
    };

    private int index = 0;

    void Start()
    {
        dialogueText.text = dialogues[index];
        nextButton.onClick.AddListener(NextDialogue);
    }

    void NextDialogue()
    {
        index++;

        if (index >= dialogues.Length)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
        else
        {
            dialogueText.text = dialogues[index];
        }
    }
}
