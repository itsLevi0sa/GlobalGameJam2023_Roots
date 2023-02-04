using UnityEngine;

public class Table : MonoBehaviour
{
    public string playerTag;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            
        }
    }
}
