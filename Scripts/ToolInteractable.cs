using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolInteractable : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private AudioClip plankDrop;
    [SerializeField] private AudioSource source;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FallOpen()
    {
        source.PlayOneShot(plankDrop);
        rb.isKinematic = false;
        transform.parent = null;
        rb.AddForce(Vector3.down);
    }
}
