using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER HIT: " + collision.name);
    }
}