using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    public float upForce = 200f;
    public PIDController change;

    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;

   void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead == false)
        {
            if (Input.GetMouseButton(0))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
            }
            else
            {
                float PIDValue = change.value;
                rb2d.AddForce(new Vector2(0, PIDValue));
                anim.SetTrigger("Flap");
            }
        }
    }

    void OnCollisionEnter2D()
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger("Die");
        GameController.instance.BirdDied();
    }
}