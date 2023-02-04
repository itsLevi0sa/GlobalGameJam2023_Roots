using UnityEngine;
using System.Collections;

public enum Player
{
    P1, P2
}

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private bool isMoving = false;

    public Player player;
    public float speed;
    public string horizontalInputAxis, verticalInputAxis, actionButton;
    public Rigidbody rb;
    public GameObject throwingBagPrefab;
    public Transform bagHoldPosition;
    
    private bool hasBag = false;
    private GameObject activeBag;
    bool canMove = true;


    void Update()
    {
        if (!canMove)
        {
            isMoving = false;
            return;
        }
         
        
        Movement();

        if (Input.GetButtonDown(actionButton))
        {
            if (!hasBag)
            {
                canMove = false;
                StartCoroutine(DelayMovement());
                animator.SetTrigger("Pickup");
                GameEvents.OnInteract?.Invoke(player);
            }     
            HandleAction();
        }
    }

    IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(7f);
        canMove = true;

    }

    void Movement()
    {
        float horizontal = Input.GetAxis(horizontalInputAxis);
        float vertical = Input.GetAxis(verticalInputAxis);
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        rb.velocity = movement * speed;
        if (movement.magnitude > 0f)
        {
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            transform.rotation = Quaternion.LookRotation(movement);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
        }
       

    }
    
    
    public void GetBag()
    {
        activeBag = Instantiate(throwingBagPrefab, bagHoldPosition.position, bagHoldPosition.rotation);
        activeBag.transform.parent = bagHoldPosition;
        hasBag = true;
    }

    
    void HandleAction()
    {
        if(hasBag)
            ThrowBag();
    }
    

    void ThrowBag()
    {
        activeBag.transform.parent = null;
        activeBag.GetComponent<ThrowingBag>().Fly();
        hasBag = false;
    }
}
