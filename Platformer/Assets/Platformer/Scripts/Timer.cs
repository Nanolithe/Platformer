using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float accumulatedTime = 0f;

    //private float totalTime = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;

        if (accumulatedTime > 1f)
        {
            //totalTime += 1f;
            accumulatedTime = 0f;

            //Debug.Log($"time is {totalTime}");
            Debug.Log($"time is {Mathf.Floor(Time.timeSinceLevelLoad)}");
        }
    }
}
