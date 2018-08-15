using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PIDController : MonoBehaviour
{

    public float p;
    public float i;
    public float d;
    public float value = 0;
    public float error;
    public float min = 0f;
    public float max = 100f;
    public Transform birdpos;
    public float waitTime = 1f;
    public Sensor target;

    private float prevError = 0;
    private float integral = 0;
    private float derivative;
    private float currentPos = -1;
    private float timer;
    private bool startTimer = false;
    private float position;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("p")) PlayerPrefs.SetFloat("p", 0);
        if (!PlayerPrefs.HasKey("i")) PlayerPrefs.SetFloat("i", 0);
        if (!PlayerPrefs.HasKey("d")) PlayerPrefs.SetFloat("d", 0);


        p = PlayerPrefs.GetFloat("p");
        i = PlayerPrefs.GetFloat("i");
        d = PlayerPrefs.GetFloat("d");
    }

    void FixedUpdate()
    {
        error = position - birdpos.position.y;
        PID(error);
    }

    void PID(float PassedError)
    {
        float dt = Time.deltaTime;

        integral += PassedError * dt;
        derivative = (PassedError - prevError) / dt;
        prevError = PassedError;

        value = Mathf.Clamp(p * PassedError + i * integral + d * derivative, min, max);
    }

    void changed(float position)
    {
        if (GetComponent<ColumnPool>().pos != currentPos)
        {
            startTimer = true;
        }

    }

    private void Update()
    {
        changed(position);
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                position = GetComponent<ColumnPool>().pos + 2.4f;
                timer = 0f;
                startTimer = false;
            }
        }
    }



}
