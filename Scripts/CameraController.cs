
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private SliderController slider;
    private Touch initTouch;
    private float rotX = 0f;
    private float rotY = 0f;
    private float sensivity = 7;
    private float xRotation;

    private void Start()
    {
        slider.OnSliderChanged += ChangeSensitivity;
        sensivity = PlayerPrefs.GetFloat("Save");
    }

    private void ChangeSensitivity(float value)
    {
        sensivity = value;
    }

    void Update()
    {
        
        foreach(Touch touch in Input.touches)
        {
            
            if(touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2  )
            {
                initTouch = touch;
            }
            else if(touch.phase == TouchPhase.Moved && touch.position.x > Screen.width / 2 )
            {
                rotX = touch.deltaPosition.x;
                rotY = touch.deltaPosition.y;

                rotX *= sensivity;
                rotY *= sensivity;

                xRotation -= rotY * Time.deltaTime;
                xRotation = Mathf.Clamp(xRotation, -80, 80);
                transform.localRotation = Quaternion.Euler(-xRotation, 0f, 0f); 
                player.Rotate(Vector3.down * rotX * Time.deltaTime);
            }
            else if(touch.phase == TouchPhase.Ended && touch.position.x > Screen.width / 2 )
            {
                initTouch = touch;
            }
            else if(touch.phase == TouchPhase.Stationary && touch.position.x > Screen.width / 2 )
            {
                initTouch = touch;
            }
            
        }
    }
 
}
