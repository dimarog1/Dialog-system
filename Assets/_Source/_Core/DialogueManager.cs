using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
internal struct DialogueInfo
{
    public Transform keeper;
    [TextArea(10, 20)] public string body;
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField] public DialoguePanel dialoguePanel;
    [SerializeField] private List<DialogueInfo> dialogueInfos;
    private readonly Dictionary<Transform, string> _dialogueKeepersDictionary = new();
    [SerializeField] private GameObject dialoguePopUp;
    private Camera _mainCamera;

    [SerializeField] private char playerDialogueSeparator;

    private void Awake()
    {
        _mainCamera = GetComponent<Camera>();

        foreach (var dInfo in dialogueInfos)
        {
            _dialogueKeepersDictionary.Add(dInfo.keeper, dInfo.body);
        }

        foreach (var keeperTransform in _dialogueKeepersDictionary.Keys)
        {
            var dialogueKeeper = keeperTransform.AddComponent<DialogueKeeper>();
            dialogueKeeper.localDialoguePopUp = dialoguePopUp;
        }
    }

    private void Update()
    {
        CheckForClick();
    }

    private void CheckForClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var mousePos2D = new Vector2(mousePos.x, mousePos.y);

            var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit && hit.transform.TryGetComponent<DialogueKeeper>(out var dialogueKeeper) &&
                dialogueKeeper.signs.activeSelf)
            {
                dialoguePanel.gameObject.SetActive(true);
                StartCoroutine(dialoguePanel.ShowMessages(
                    _dialogueKeepersDictionary[dialogueKeeper.transform].Split('\n'),
                    dialogueKeeper.name, playerDialogueSeparator));
            }
        }
    }
}