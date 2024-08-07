
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] string animName;
    [SerializeField] Button[] menuButtons;
    [SerializeField] private AudioClip carStart, buttonSound;
    [SerializeField] private AudioSource source;
    public void Play()
    {
        source.Stop();
        foreach (var buttons in menuButtons)
        {
            buttons.enabled = false;
        }
        anim.Play(animName);
        source.PlayOneShot(carStart);
        StartCoroutine(NextScene());
    }

    public void Quite()
    {
        Application.OpenURL("https://yandex.ru/games");
    }

    IEnumerator NextScene()
    {

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PlaySound ()
    {
        source.PlayOneShot(buttonSound);
    }
   
}
