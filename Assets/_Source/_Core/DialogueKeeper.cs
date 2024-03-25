using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueKeeper : MonoBehaviour
{
    [SerializeField]
    private GameObject localDialoguePopUp;
    
    [SerializeField]
    public GameObject signs;

    [HideInInspector]
    public string dialogueBody;
    
    private void Start()
    {
        dialogueBody = LoadDialogue($"Dialogues/{name}");
    }

    private static string LoadDialogue(string path)
    {
        var body = Resources.Load<TextAsset>(path).text;
        body = Regex.Replace(body, @"\t|\r", "");
        
        return body;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        signs.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        signs.SetActive(false);
    }
}