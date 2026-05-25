using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER ENTER: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLAYER DETECTED");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("TRIGGER EXIT: " + collision.name);
    }
}