using UnityEngine;
using UnityEngine.InputSystem;

public class BreathFocus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public Transform CloudHole;
    // define size and speed of the cloud hole
    public float minsize = 1.5f;
    public float maxsize = 10f;
    public float growSpeed = 0.03f; 
    public float shrinkSpeed = 0.03f;

    // define breathing signals
    public float breathThreshold = 3f; // threshold to determine if inhale increases visibility
    public float breathThresholdOut = 1f; // threshold to determine if exhale too short
    float signalIn = 0f;
    float signalOut = 0f;
    float targetVisibility = 0f;
    float visibility = 0f;

    bool prevBreath = true; // true if previous moment was breathing in, false if breathing out

    // Update is called once per frame
    void Update()
    {
        // define breathing in as space bar pushed, otherwise breathing out
        bool breathingIn =
            Keyboard.current != null &&
            Keyboard.current.spaceKey.isPressed;

        if (breathingIn)
        {
            // player JUST started inhaling
            if (!prevBreath)
            {
                // previous exhale was too short
                if (signalOut < breathThresholdOut)
                {
                    targetVisibility -= shrinkSpeed;
                }
                // reset exhale timer
                signalOut = 0f;
            }

            // increase inhale timer
            signalIn += Time.deltaTime;
            if (signalIn < breathThreshold)
            {
                targetVisibility += growSpeed * Time.deltaTime;
            }
        }
        else
        {
            // if Space bar is not pushed: increase breathing out signal
            signalOut += Time.deltaTime;
            // if breathing in and breathing out: increase visibility
            if (signalOut > breathThreshold)
            {
                targetVisibility -= shrinkSpeed * Time.deltaTime;
                signalIn = 0f; // reset breathing in signal after increasing visibility
            }
        }

        targetVisibility = Mathf.Clamp01(targetVisibility);

        visibility = Mathf.Lerp(
            visibility,
            targetVisibility,
            2f * Time.deltaTime
        );

        // calculate the new size of the cloud hole based on the signal value
        float newSize = Mathf.Lerp(minsize, maxsize, visibility);
        newSize += Mathf.Sin(Time.time) * 0.1f; // add some subtle pulsing effect
        // apply the new size to the cloud hole
        CloudHole.localScale = new Vector3(newSize, newSize, 1);

        // define if previous moment was breathing in
        prevBreath = breathingIn;
    }
}
