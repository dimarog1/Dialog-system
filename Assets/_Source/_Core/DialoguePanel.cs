using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;


public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text messageBody;
    [SerializeField] private TMP_Text messageAuthor;
    [SerializeField] private DialogueManager dialogueManager;

    public void CloseDialoguePanel()
    {
        StopCoroutine(nameof(LetterDelayer));
        StopCoroutine(nameof(ShowMessages));
        dialogueManager.inDialogue = false;
        messageBody.text = string.Empty;
        gameObject.SetActive(false);
    }

    public IEnumerator ShowMessages(IEnumerable<string> body, string companion, char playerDialogueSeparator)
    {
        foreach (var line in body)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var lineCopy = line;
            
            if (line.EndsWith(playerDialogueSeparator))
            {
                lineCopy = lineCopy[..^1];
                messageAuthor.text = "Вы";
            }
            else
            {
                messageAuthor.text = companion[..1].ToUpper() + companion[1..];
            }

            if (dialogueManager.inDialogue)
            {
                yield return StartCoroutine(LetterDelayer(lineCopy));
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return) || !dialogueManager.inDialogue);

            messageBody.text = string.Empty;
        }

        CloseDialoguePanel();
    }

    private IEnumerator LetterDelayer(string line)
    {
        messageBody.text = string.Empty;
        foreach (var letter in line)
        {
            messageBody.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }
}