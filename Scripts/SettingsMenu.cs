using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] UnityEvent GamePaused, GameResumed;
    [SerializeField] GameObject pauseMenu, gameMenu;
    [SerializeField] AudioSource source;
    public bool isPaused;
    void Start()
    {
        
    }

   public void PlayPause ()
    {
            Time.timeScale = 0;
            GamePaused.Invoke();
            pauseMenu.SetActive(true);
            gameMenu.SetActive(false);
             source.Pause();
    }

    public void StopPause ()
    {
        Time.timeScale = 1;
        GameResumed.Invoke();
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
        source.UnPause();
    }
   
}
