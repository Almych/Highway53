
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInteractions : MonoBehaviour
{
    public LayerMask door;
    private bool isOpen;




    private void Update()
    {
        DoorInteraction();
    }

    void DoorInteraction()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f, door))
        {
          
            if (hit.collider.GetComponent<AnimationPlaying>()!= null)
            {
                if (hit.collider.GetComponent<AnimationPlaying>().doorOpen == false &&
                    hit.collider.GetComponent<NavMeshObstacle>() != null)
                {
                    hit.collider.GetComponent<AnimationPlaying>().StartAnimation();
                    hit.collider.GetComponent<NavMeshObstacle>().carving = true;
                    Debug.Log("Enemy open");
                }
                
            }
            else return;
        }
        
    }
}
