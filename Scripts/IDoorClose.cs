using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IDoorClose : MonoBehaviour
{
    [SerializeField] private Transform Enemy;
    void Start()
    {
        StartCoroutine(CloseDoor());
    }

    // Update is called once per frame
    IEnumerator CloseDoor()
    {
        if (transform.GetComponent<AnimationPlaying>().doorOpen && Vector3.Distance(Enemy.transform.position, transform.position) >= 5f)
        {
            yield return new WaitForSeconds(5);
            transform.GetComponent<AnimationPlaying>().StartAnimation();
            transform.GetComponent<NavMeshObstacle>().carving = false;
        }
    }
}
