using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveValues : MonoBehaviour {

    public float kp = 60f;
    public float ki = 0f;
    public float fd = 20f;

    private void Awake()
    {
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
    }

    //public void Restart()
    //{
    //    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    SceneManager.LoadSceneAsync(SceneManager.GetSceneByName("Main").buildIndex);
    //}


}
