using UnityEngine;
using UnityEngine.InputSystem;
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
            rb.velocity = Vector3.zero;
            return;
        }
         
        
        Movement();
    }

    public void OnMove(InputValue inp)
    {
        
        /*float horizontal = Input.GetAxis(horizontalInputAxis);
        float vertical = Input.GetAxis(verticalInputAxis);
        Vector3 movement = new Vector3(horizontal, 0f, vertical);*/
        Vector3 movement = inp.Get<Vector2>();
        Vector3 m = new Vector3(movement.x, 0f, movement.y);
        
        rb.velocity = m * speed;
        if (m.magnitude > 0f)
        {
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            transform.rotation = Quaternion.LookRotation(m);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
            rb.velocity = Vector3.zero;
        }
    }

    void Movement()
    {
    }

    public void OnInteract(InputValue inp)
    {
        HandleAction();
        if (!hasBag)
        {
            canMove = false;
            StartCoroutine(DelayMovement());
            animator.SetTrigger("Pickup");
            GameEvents.OnInteract?.Invoke(player);
        }
            
    }
    
    IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(7f);
        canMove = true;
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
