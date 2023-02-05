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
    public Rigidbody rb;
    public GameObject throwingBagPrefab;
    public GameObject fakeBagPrefab;
    public Transform bagHoldPosition;
    public Transform initialBagPosition;
    public Transform throwBagPosition;

    private bool hasBag = false;
    private GameObject activeBag;
    private GameObject fakeBag;
    bool canMove = true;
    [HideInInspector] public bool nearRoot;
    public float pickUpTime;
    private bool canInteract = true;
    public SkinnedMeshRenderer playerRenderer;
    public AnimationCurve flashCurve;

    void Update()
    {
        if (!canMove)
        {
            isMoving = false; 
            SetVelocity(Vector3.zero);
            return;
        }
        
    }

    void SetVelocity(Vector3 vel) => rb.velocity = vel;

    public void OnMove(InputValue inp)
    {
        Vector3 movement = inp.Get<Vector2>();
        Vector3 m = new Vector3(movement.x, 0f, movement.y);
        SetVelocity(m * speed);
        if (m.magnitude > 0f)
        {
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
            if (canMove)
            {
                
                transform.rotation = Quaternion.LookRotation(m);
                
            }
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
            SetVelocity(Vector3.zero);
        }
    }


    public void OnInteract(InputValue inp)
    {
        if (!canInteract) return;
        HandleAction();
        if (!hasBag && nearRoot)
        {
            canMove = false;
            StartCoroutine(DelayMovement());
            animator.SetTrigger("Pickup");
            GameEvents.OnInteract?.Invoke(player);
        }
            
    }
    
    IEnumerator DelayMovement()
    {
        yield return new WaitForSeconds(pickUpTime);
        canMove = true;
    }
    
    public void GetBag()
    {
        fakeBag = Instantiate(fakeBagPrefab, initialBagPosition.position, initialBagPosition.rotation);
        fakeBag.transform.parent = initialBagPosition;
        hasBag = true;
    }

    
    void HandleAction()
    {
        if(hasBag)
            ThrowBag();
    }
    

    void ThrowBag()
    {
        canMove = false;
        animator.SetTrigger("Throw");
        fakeBag.transform.parent = bagHoldPosition;
        isMoving = false;

    }

    public void ThrowFromHand()
    {
        Destroy(fakeBag);
        activeBag = Instantiate(throwingBagPrefab, throwBagPosition.position, throwBagPosition.rotation);
        activeBag.transform.parent = null;
        activeBag.GetComponent<ThrowingBag>().Fly();
        hasBag = false;
        StartCoroutine(CanMoveAgain());
        
    }

    IEnumerator CanMoveAgain()
    {
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }

    public void GetDamage()
    {
        StartCoroutine(DamageCoroutine());
    }

    IEnumerator DamageCoroutine()
    {
        canMove = false;
        canInteract = false;
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            playerRenderer.material.SetFloat("_Flash", flashCurve.Evaluate(elapsedTime/1f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canMove = true;
        canInteract = true;
        playerRenderer.material.SetFloat("_Flash", 0f);
    }

}
