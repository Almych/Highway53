using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Endgame : MonoBehaviour
{
    [SerializeField] private Transform hand;
    [SerializeField] private TMP_Text nameItem;
    [SerializeField] private string itemName;
    [SerializeField] private Image endGameMenu;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip tryOpen, winSound;
    public void EndOpen()
    {
        if (hand.childCount > 0 && hand.GetChild(0).name == itemName)
        {
            source.PlayOneShot(winSound);
            endGameMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
            
        }
        else
        {
            source.PlayOneShot(tryOpen);
            nameItem.SetText($"Need {itemName}");
            nameItem.gameObject.SetActive(true);
        }

    }
}
