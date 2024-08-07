using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyInteraction : MonoBehaviour
{
    [SerializeField] private Transform hand;
    [SerializeField] private TMP_Text nameItem;
    [SerializeField] private string itemName;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip unlockSound, tryOpen;

    

    // Update is called once per frame
    public void KeyOpen ()
    {
        if (hand.childCount > 0 && hand.GetChild(0).name == itemName)
        {
            transform.tag = "Interactable";
            source.PlayOneShot(unlockSound);
        }
        else
        {
            source.PlayOneShot(tryOpen);
            nameItem.SetText($"Need {itemName} ");
            nameItem.gameObject.SetActive(true);
        }
    }

  
}
