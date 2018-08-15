using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;
    public GameObject gameOverText;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;
    public Text scoreText;
    public Text errorText;
    public Text forceText;
    public Text pval;
    public Text ival;
    public Text dval;
    PIDController pid;
    float ptemp;
    float itemp;
    float dtemp;

    public InputField kp;
    public InputField ki;
    public InputField kd;

    private int score = 0;
 
	void Awake ()
    {
        pid = GetComponent<PIDController>();
		if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	void FixedUpdate ()
    {
        errorText.text = "Error: " + pid.error.ToString("0.000");
        forceText.text = "Force: " + pid.value.ToString("0.000");

        float.TryParse(kp.text, out ptemp);
        float.TryParse(ki.text, out itemp);
        float.TryParse(kd.text, out dtemp);
        if ((ptemp != pid.p) && (kp.text != ""))
        {
            PlayerPrefs.SetFloat("p", ptemp);
        }
        if ((itemp != pid.i) && (ki.text != ""))
        {
            PlayerPrefs.SetFloat("i", itemp);
        }
        if ((dtemp != pid.d) && (kd.text != ""))
        {
            PlayerPrefs.SetFloat("d", dtemp);
        }
    }

    public void BirdScored()
    {
        if (gameOver)
        {
            return;
        }
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void BirdDied()
    {
        gameOverText.SetActive(true);
        gameOver = true;

        pval.text = pid.p.ToString();
        ival.text = pid.i.ToString();
        dval.text = pid.d.ToString();

        PlayerPrefs.SetFloat("p", pid.p);
        PlayerPrefs.SetFloat("i", pid.i);
        PlayerPrefs.SetFloat("d", pid.d);
        PlayerPrefs.Save();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetSceneByName("Main").buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
