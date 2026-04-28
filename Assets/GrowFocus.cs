using UnityEngine;
using UnityEngine.InputSystem;

public class GrowFocus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public Transform CloudHole;
    public float minsize = 2f;
    public float maxsize = 8f;
    public float growSpeed = 0.5f; 
    public float shrinkSpeed = 0.2f;
    public float signal = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            // if Space bar is pushed: grow visibility
            signal += growSpeed*Time.deltaTime;
        }
        else
        {
            // otherwise shrink the visibility
            signal -= shrinkSpeed*Time.deltaTime;
        }
        // clamp the signal value between 0 and 1
        signal = Mathf.Clamp01(signal);
        // calculate the new size of the cloud hole based on the signal value
        float newSize = Mathf.Lerp(minsize, maxsize, signal);
        // apply the new size to the cloud hole
        CloudHole.localScale = new Vector2(newSize, newSize);
    }
}
