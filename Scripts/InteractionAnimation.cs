
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using Button = UnityEngine.UI.Button;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractionAnimation : MonoBehaviour
{
    public  int MaxCount;
    
    [SerializeField] private Button interactButton, DropButton;
    [SerializeField] private GameObject ShowUI;
    [SerializeField] private TMP_Text nameItem;
    [SerializeField] private Sprite[] interactButtonStates;
    [SerializeField] private Transform hand;
    [SerializeField] private EnemyMovement enemy;
    [SerializeField] private Animator anim;
    [SerializeField] private Image dieMenu;
    [SerializeField] private AudioClip drop, takeCode, pickUp, chase, backRound;
    
    public Transform droppedTrigger { get; private set; }
    public string animName;
    internal bool dropped, isHided, triggeredSound;
    public int currentCount { get; private set; }
    private Vector3 startScale;
    private int coolDown = 1;
    private bool triggered, isEquip, isHiding, isInHidePlace;
    private AnimationPlaying currState;
    private float distance = 4f;
    private Transform hidePlayer = null;
    private RaycastHit info;
    private GameObject current, dropCurrent, currentPickable;
   [SerializeField] private AudioSource source;
    
  



    void Start()
    {
       
        hand.rotation = Quaternion.Euler(0f, -90f, 0f);
        isEquip = false;
        interactButton.gameObject.SetActive(false);
        DropButton.gameObject.SetActive(false);
        interactButton.onClick.AddListener(Interact);
        DropButton.onClick.AddListener(DropInteract);
        nameItem.gameObject.SetActive(false);
       
    }

  
    private void Interact()
    {
        if (currState != null)
        {
            currState.StartAnimation();
            source.PlayOneShot(currState.currentSound);
            StartCoroutine(HideButton());
        }
           
        if (info.collider.CompareTag("Hideplace"))
            HidePlayer();
        
        
        if (info.collider.CompareTag("Pickable"))
            AddCount(); 
        
        if (info.collider.CompareTag("Holdable") && !isEquip)
        {
            Pickup();
           
        }
        else if (info.collider.CompareTag("Holdable") && isEquip)
        {
            Drop();
            current = info.collider.gameObject;
            dropCurrent = current;
            Pickup();
        }

        if (info.collider.CompareTag("Needable") && info.collider.GetComponent<INeedable>() != null)
        {
           
            if (currentCount == MaxCount)
            {
                nameItem.gameObject.SetActive(true);
                info.collider.GetComponent<INeedable>().Interactions();
                nameItem.color = Color.green;
                nameItem.SetText("Door is open");
            }
            else
            {
                nameItem.gameObject.SetActive(true);
                nameItem.SetText($"Need {MaxCount - currentCount} codes");
            }
        }

        if (info.collider.CompareTag("Needable") && info.collider.GetComponent<ToolInteractable>() != null)
        {
           
            
            if (hand.childCount == 0 ||  hand.GetChild(0).name != "Hammer")
            {
                nameItem.SetText("Need the hammer");
                nameItem.gameObject.SetActive(true);
            }
            else if (hand.childCount > 0 &&  hand.GetChild(0).name == "Hammer")
            {
                info.collider.GetComponent<ToolInteractable>().FallOpen();
                info.collider.tag = "Physic";
               
            }
           
        }
        if (info.collider.CompareTag("Needable") && info.collider.GetComponent<KeyInteraction>()!= null)
            info.collider.GetComponent<KeyInteraction>().KeyOpen();
        if (info.collider.CompareTag("EndNeed") && info.collider.GetComponent<Endgame>() != null)
            info.collider.GetComponent<Endgame>().EndOpen();


        if (isEquip)
        {
            DropButton.gameObject.SetActive(true);
            dropCurrent.transform.position = hand.position;
            dropCurrent.transform.rotation = hand.rotation;
        }
        if (enemy.sawPlayer)
        {
            source.Pause();
            source.clip = chase;
            if (source.clip == chase) source.Play();
        }
        else if (!enemy.sawPlayer)
        {
            source.Pause();
            source.clip = backRound;
            if (source.clip == backRound) source.Play();
        }
    }

  

    private void Update()
    {
        
        Ray ray = new Ray(transform.position,  transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        
        if (Physics.Raycast(ray, out info, distance))
        {
            if (info.collider.CompareTag("Untagged")) Null();
            else if (info.collider.CompareTag("Interactable"))
                SeeInteractable();
            else if (info.collider.CompareTag("Hideplace"))
            {
                interactButton.gameObject.SetActive(true);
                hidePlayer = info.collider.transform;
            }
            else if (info.collider.CompareTag("Pickable"))
                SeePickable();
            else if (info.collider.CompareTag("Holdable") && !isEquip)
                SeeHoldableItem();
            else if (info.collider.CompareTag("Holdable") && isEquip)
            {
                SeeHoldableItemEquip();
            }
            else if (info.collider.CompareTag("Needable") || info.collider.CompareTag("EndNeed"))
                interactButton.gameObject.SetActive(true);
            else Null();

            ChangePlayerSprite();
        }
        else Null();

        
       
    }
    

    void HidePlayer()
    {
        if (isInHidePlace == false)
        {
            if ( hidePlayer != null)
            {
                hidePlayer.GetChild(0).gameObject.SetActive(true);
                transform.parent.gameObject.SetActive(false);
                ShowUI.SetActive(false);
                isInHidePlace = true;
            }

        }
        else if (isInHidePlace)
        {
            if ( hidePlayer != null)
            {
                hidePlayer.GetChild(0).gameObject.SetActive(false); 
                transform.parent.gameObject.SetActive(true);
                isInHidePlace = false;
                ShowUI.SetActive(true);
            }
           
        }
        
    }
    private void AddCount()
    {
        if (currentCount == MaxCount)
            return;

        currentCount++;
        source.PlayOneShot(takeCode);
        currentPickable.SetActive(false);
        interactButton.gameObject.SetActive(false);
        currentPickable = null;
    }
    void Pickup()
    {
        isEquip = true;
        source.PlayOneShot(pickUp);
            current.transform.parent = hand;
            if (current.transform.position != hand.position)
            {
                current.transform.position = hand.position;
                
            }
            current.GetComponent<Collider>().isTrigger = true;
           
           
    }

    void Drop()
    {
        if(dropCurrent.GetComponent<Rigidbody>()== null && dropCurrent.GetComponent<Collider>()== null)
                return;
        dropCurrent.GetComponent<Rigidbody>().AddForce(dropCurrent.transform.parent.forward);
        source.PlayOneShot(drop);
        dropped = true;
        droppedTrigger = dropCurrent.transform;
        dropCurrent.GetComponent<Collider>().isTrigger = false;
        dropCurrent.GetComponent<Rigidbody>().isKinematic = false;
        isEquip = false;
        dropCurrent.transform.parent = null;
       // dropCurrent.transform.localScale = startScale;
        dropCurrent = null;
        
    }

    void Null()
    {
        interactButton.gameObject.SetActive(false);
        triggered = false;
        currState = null;
        nameItem.gameObject.SetActive(false);
        DropButton.gameObject.SetActive(false);
        current = null;
        hidePlayer = null;
        currentPickable = null;
    }

    private void DropInteract()
    {
        DropButton.gameObject.SetActive(true);
        Drop();

    }

    private IEnumerator HideButton()
    {
        isHiding = true;
        interactButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(coolDown);
        isHiding = false;

        if (triggered)
            interactButton.gameObject.SetActive(true);
    }


    private void SeeHoldableItem ()
    {
        if (!isHiding)
        {
            
            interactButton.gameObject.SetActive(true);
            nameItem.SetText($"{info.collider.name}");
            nameItem.color = Color.white;
            nameItem.gameObject.SetActive(true);
            current = info.collider.gameObject;
            startScale = info.collider.transform.localScale;
            dropCurrent = current;
            currState = null;
        }
        
    }
    private void SeeHoldableItemEquip()
    {
        if (!isHiding)
        {
            
            interactButton.gameObject.SetActive(true);
            nameItem.SetText($"{info.collider.name}");
            nameItem.color = Color.white;
            nameItem.gameObject.SetActive(true);
            if (current == null && dropCurrent == null)
            {
                current = info.collider.gameObject;
                  dropCurrent = current;
            }
            startScale = info.collider.transform.localScale;
            currState = null;
        }

    }

    private void ChangePlayerSprite ()
    {
        if (info.collider.CompareTag("Holdable") && !isEquip || info.collider.CompareTag("Pickable"))
            interactButton.image.sprite = interactButtonStates[1];
        else interactButton.image.sprite = interactButtonStates[0];
    }

    private void SeePickable ()
    {
        interactButton.gameObject.SetActive(true);
        currentPickable = info.collider.gameObject;
        nameItem.SetText("Piece of code");
        nameItem.color = Color.white;
        nameItem.gameObject.SetActive(true);
    }

    private void SeeInteractable ()
    {
        if (!info.collider.GetComponent<AnimationPlaying>().oneTimeAnim)
        {
            nameItem.gameObject.SetActive(false);
            if (!isHiding)
            {
                interactButton.gameObject.SetActive(true);
                currState = info.collider.GetComponent<AnimationPlaying>();
                triggered = true;
            }
        }
        else
            Null();
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(2);

        dieMenu.gameObject.SetActive(true);
    }

    public void  DieAnim ()
    {
        if (dropCurrent != null) Drop();
        anim.Play(animName);
        StartCoroutine(CoolDown());
       
    }
   
}
