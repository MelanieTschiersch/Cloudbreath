using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    bool playerNearby = false;
    bool isInteracting = false;
    public GameObject dialogueBox;

    void Update()
    {
        // Check for E key here to start interaction
        if (
            playerNearby && isInteracting == false &&
            Keyboard.current.eKey.wasPressedThisFrame
        )
        {
            Debug.Log("Start dialogue!");
            dialogueBox.SetActive(true);
            isInteracting = true;

        }
        else if (
            playerNearby && isInteracting == true &&
            Keyboard.current.eKey.wasPressedThisFrame
        )
        {
            Debug.Log("End dialogue!");
            dialogueBox.SetActive(false);
            isInteracting = false;
        }
        else if (
            playerNearby == false
        )
        {
            Debug.Log("End dialogue!");
            dialogueBox.SetActive(false);
            isInteracting = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER ENTER: " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLAYER DETECTED");
            playerNearby = true;
            isInteracting = false;
       }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("TRIGGER EXIT: " + collision.name);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("PLAYER LEFT");
            playerNearby = false;
            dialogueBox.SetActive(false);
            isInteracting = false;
        }
    }
}