
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
   
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Button sit;
    [SerializeField] private Sprite[] playerStates;
    [SerializeField] private float magnitude;
     private CharacterController _controller;
     private float horizontal;
     private float vertical;
     public float speed;
     private bool isSquating;
     private float gravity = -500f;
     
   
   

    private void Awake()
    {
        sit.onClick.AddListener(SitController);
        _controller = GetComponent<CharacterController>();
    }

   private void Update()
    {
        speed = isSquating ? 3f : 7f;
        HandleMovement();

    }

    


    private void HandleMovement()
    {
        if (_controller.enabled)
        {
            horizontal = _joystick.Horizontal;
            vertical = _joystick.Vertical;
            Vector3 move = transform.right * horizontal + transform.forward * vertical;
            if (_controller.isGrounded == false)
                move.y += gravity * Time.deltaTime;

            _controller.Move(move * speed * Time.deltaTime);
        }
        
    }

     void SitController()
     {
         if (isSquating == false)
         {
             transform.localScale = new Vector3(1,transform.localScale.y - 1,1);
             isSquating = true;
             sit.image.sprite = playerStates[1];
         }
         else
         {
             transform.localScale = new Vector3(1, transform.localScale.y + 1,1);
             isSquating = false;
             sit.image.sprite = playerStates[0];
         }
     }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        
        if (rb!= null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rb.AddForceAtPosition(forceDirection * magnitude, transform.position, ForceMode.Impulse);
        }
    }

}
    