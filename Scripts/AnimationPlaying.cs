
using System;
using System.Collections;
using UnityEngine;




    public class AnimationPlaying : MonoBehaviour
    {
        internal AudioClip currentSound;
         private Animator  _animator;
       [SerializeField] private AudioClip open, close;
        private Collider _collider;
        internal bool doorOpen, oneTimeAnim;
        [SerializeField] private string openAnim;
        [SerializeField] private string closeAnim;
        private int coolDown = 1;
        private bool pauseInteraction = false;
        

        
        private IEnumerator PauseInteraction()
        {
            pauseInteraction = true;
            _collider.enabled = false;
            yield return new WaitForSeconds(coolDown);
            pauseInteraction = false;
            _collider.enabled = true;
        }

        public void StartAnimation()
        {
            if (closeAnim == "") oneTimeAnim = true;
            
            if (_animator != null && openAnim != null)
            {
                if (!doorOpen && !pauseInteraction)
                {
                    _animator.Play(openAnim, 0, 0.0f);
                    currentSound = close;
                    doorOpen = true;
                    
                    StartCoroutine(PauseInteraction());
                    
                }
            }

            if (_animator != null && closeAnim != null)
            {
                if (doorOpen && !pauseInteraction)
                {
                    _animator.Play(closeAnim, 0, 0.0f);
                     currentSound = open;
                    doorOpen = false;
                    StartCoroutine(PauseInteraction());
                }
            }
            




        }

        

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
            oneTimeAnim = false;
        }

        
    }


