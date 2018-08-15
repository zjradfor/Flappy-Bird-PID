using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorArrow : MonoBehaviour {

    public PIDController force;
    public float vectorScale = 0.1f;

	void Update ()
    {
        float size = force.value * vectorScale;
        transform.localScale = new Vector2(1f, size);
	}

}
