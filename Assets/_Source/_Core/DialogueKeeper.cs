using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogueKeeper : MonoBehaviour
{
    [HideInInspector]
    public GameObject localDialoguePopUp;
    [SerializeField] private float popUpVerticalOffset = 1.9f;

    [HideInInspector]
    public GameObject signs;
    
    private void Start()
    {
        localDialoguePopUp = Instantiate(localDialoguePopUp, Vector3.zero, 
            Quaternion.identity, parent: transform);
        localDialoguePopUp.transform.localPosition = new Vector3(0, popUpVerticalOffset);

        signs = localDialoguePopUp.transform.GetChild(0).gameObject;
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