using UnityEngine;

public class ThrowingBag : MonoBehaviour
{
    public Rigidbody rb;
    public float flyStrength;
    public GameObject stainPArticles;

    public void Fly()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * flyStrength, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Instantiate(stainPArticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
