using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Text dialogueText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialogue;
    public event Action OnCloseDialogue;

    PlayerController playercontrollerscript;


    public static DialogueManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    Dialogue dialogue;
    Action onDialogueFinished;
    int currentLine = 0;
    bool isTyping;

    public bool IsShowing { get; private set; }

    public void HandleUpdate()
    {
        playercontrollerscript = FindObjectOfType<PlayerController>();
        if (playercontrollerscript.isClicked == true && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
                playercontrollerscript.isClicked = false;
            }
            else 
            {
                currentLine = 0;
                IsShowing = false;
                dialogueBox.SetActive(false);
                onDialogueFinished?.Invoke();
                OnCloseDialogue?.Invoke();
                playercontrollerscript.isClicked = false;
            }
        }
    }

    public IEnumerator ShowDialogue(Dialogue dialogue, Action onFinished=null)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialogue?.Invoke();

        IsShowing = true;
        this.dialogue = dialogue;
        onDialogueFinished = onFinished;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }

    public IEnumerator TypeDialogue(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
        playercontrollerscript.isClicked = false;
    }
}
