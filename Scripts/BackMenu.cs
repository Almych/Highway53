
using UnityEngine;
using UnityEngine.SceneManagement;


public class BackMenu : MonoBehaviour
{
   
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
       
    }
    public void PlayEnd ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void PlayAgain ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
