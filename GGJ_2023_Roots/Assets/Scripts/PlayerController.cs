using UnityEngine;

public enum Player
{
    P1, P2
}

public class PlayerController : MonoBehaviour
{
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
         return;
        
        Movement();

        if (Input.GetButtonDown(actionButton))
        {
            if(!hasBag)
                GameEvents.OnInteract?.Invoke(player);
            HandleAction();
        }
    }

    void Movement()
    {
        float horizontal = Input.GetAxis(horizontalInputAxis);
        float vertical = Input.GetAxis(verticalInputAxis);
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        rb.velocity = movement * speed;
        if (movement.magnitude > 0f)
        {
            transform.rotation = Quaternion.LookRotation(movement);
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
