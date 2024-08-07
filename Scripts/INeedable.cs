using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INeedable : MonoBehaviour
{
    [SerializeField] private InteractionAnimation interact;
    [SerializeField] private Transform door;
    [SerializeField] AudioClip detectorSound;
    [SerializeField] AudioSource source;
    private Material detector;

     
    void Start()
    {
        door.gameObject.tag = "Untagged";
        detector = transform.GetChild(0).GetComponent<Renderer>().material;
    }

    // Update is called once per frame
   internal void Interactions()
    {
        
        if (interact.currentCount == interact.MaxCount && detector != null)
        {
            source.PlayOneShot(detectorSound);
            door.gameObject.tag = "Interactable";
            detector.color = Color.green;
        }
    }
}
