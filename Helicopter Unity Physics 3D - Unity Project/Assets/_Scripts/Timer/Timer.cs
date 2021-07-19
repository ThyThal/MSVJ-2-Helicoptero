using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float maxTime;
    [SerializeField] private TimerModes mode;

    private float elapsedTime;

    public float ElapsedTime => elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        if (mode == TimerModes.Subtract)
        {
            elapsedTime = maxTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == TimerModes.Add)
        {
            elapsedTime += Time.deltaTime;
        }
        else if (mode == TimerModes.Subtract && elapsedTime > 0f)
        {
            elapsedTime -= maxTime;
            if (elapsedTime < 0f)
            {
                elapsedTime = 0f;
            }
        }

    }

    private enum TimerModes
    {
        Add,
        Subtract
    }

    public int GetSeconds()
    {
        return Mathf.FloorToInt(elapsedTime % 60);
    }

    public int GetMinutes()
    {
        return Mathf.FloorToInt(elapsedTime / 60);
    }
}
