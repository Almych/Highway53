
using UnityEngine;
using UnityEngine.UI;

public class HideController : MonoBehaviour
{
    [SerializeField] private Button interactButton;
   [SerializeField] private PlayerController _playerController;
   [SerializeField] private GameObject hidePlayer;
   [SerializeField] private RawImage image;
   private bool isInHidePlace;
    void Start()
    {
        interactButton.onClick.AddListener(HidePlayer);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit info;

        if (Physics.Raycast(ray, out info, 2.8f))
        {
            if (info.collider.CompareTag("Hideplace"))
            {
                interactButton.gameObject.SetActive(true);
                
            }
        }
    }
    
    void HidePlayer()
    {
        if (isInHidePlace == false)
        {
           
            image.enabled = false;
            hidePlayer.SetActive(true); 
            _playerController.gameObject.SetActive(false);
            isInHidePlace = true;
        }
        else if (isInHidePlace)
        {
            image.enabled = true;
            hidePlayer.SetActive(false); 
            _playerController.gameObject.SetActive(true);
            isInHidePlace = false;
        }
        
    }
}
