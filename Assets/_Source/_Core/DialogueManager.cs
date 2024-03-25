using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] public DialoguePanel dialoguePanel;
    [SerializeField] private List<DialogueKeeper> dialogueKeepers;
    [SerializeField] private GameObject dialoguePopUp;
    private Camera _mainCamera;

    [SerializeField] private char playerDialogueSeparator;

    [HideInInspector] public bool inDialogue;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (!inDialogue && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(TryStartDialogue());
        }
    }

    private IEnumerator TryStartDialogue()
    {
        var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var mousePos2D = new Vector2(mousePos.x, mousePos.y);

        var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit && hit.transform.TryGetComponent<DialogueKeeper>(out var dialogueKeeper) &&
            dialogueKeeper.signs.activeSelf)
        {
            inDialogue = true;
            
            dialoguePanel.gameObject.SetActive(true);
            yield return StartCoroutine(dialoguePanel.ShowMessages(
                dialogueKeeper.dialogueBody.Split('\n'),
                dialogueKeeper.name, // Сюда в дальнейшем можно передавать, что-то сложнее, чем просто название объекта
                playerDialogueSeparator));
            
            inDialogue = false;
        }
    }
}