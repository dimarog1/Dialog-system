using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class DialoguePanel : MonoBehaviour
{
    [SerializeField] private Text messageBody;
    [SerializeField] private Text messageAuthor;

    private void CloseDialoguePanel()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator ShowMessages(IEnumerable<string> body, string companion, char playerDialogueSeparator)
    {
        foreach (var line in body)
        {
            var lineCopy = line;
            if (line.EndsWith(playerDialogueSeparator))
            {
                lineCopy = lineCopy[..^1];
                messageAuthor.text = companion[..1].ToUpper() + companion[1..];
            }
            else
            {
                messageAuthor.text = "Вы";
            }

            yield return StartCoroutine(LetterDelayer(lineCopy));

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

            messageBody.text = string.Empty;
        }

        CloseDialoguePanel();
    }

    private IEnumerator LetterDelayer(string line)
    {
        foreach (var letter in line)
        {
            messageBody.text += letter;

            yield return new WaitForSeconds(0.1f);
        }
    }
}