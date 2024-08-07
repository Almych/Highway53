using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falldown : MonoBehaviour
{
   private Rigidbody rb;
    private Vector3 startPosition, startScale;
    private bool isFalled;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startScale = transform.localScale;
        StartCoroutine(ReturnPosition());
    }
    private void Update()
    {
        if (transform.localScale != startScale) transform.localScale = startScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touch2");
            rb.isKinematic = false;
            isFalled = true;
        }
    }

    IEnumerator ReturnPosition ()
    {
        yield return new WaitForSeconds(40);
        if (isFalled)
        {
            transform.position = startPosition;
        }
    }
}
