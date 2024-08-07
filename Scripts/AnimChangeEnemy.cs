using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimChangeEnemy : MonoBehaviour
{
    private string currState;
    internal Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void EnemyAnimationChange(string newState)
    {
        //Time.timeScale = 1;
        if (currState == newState) return;

        animator.Play(newState);
        currState = newState;
        
    }
}
