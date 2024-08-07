
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DoorOpen : MonoBehaviour
{
  
    private NavMeshObstacle _obstacle;
    private AnimationPlaying anim;
    private Transform targets;
    private bool OpenedByEnemy;
    
    
    void Start()
    {
       
        anim = GetComponent<AnimationPlaying>();
        _obstacle = GetComponent<NavMeshObstacle>();
       
    }

    private void Update()
    {
         if (OpenedByEnemy && Vector3.Distance(transform.position, targets.position) >= 6f)
         {
            anim.StartAnimation();
            OpenedByEnemy = false;
            anim.doorOpen = false;
         }

        if (anim.doorOpen)
        {
            _obstacle.carving = true;
            transform.gameObject.layer = 1;
        }
        else
        {
            _obstacle.carving = false;
            transform.gameObject.layer = 12;
        }
       
    }


    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            targets = other.transform;
            if (anim != null && !anim.doorOpen && _obstacle != null)
            {
                anim.StartAnimation();
                OpenedByEnemy = true;
                anim.doorOpen = true;
            }
            else return;
        }
       
    }

   




   
   
  
}
