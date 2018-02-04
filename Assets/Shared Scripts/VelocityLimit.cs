using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityLimit : MonoBehaviour {

    public float vertical_speed_limit = 7.0f;
    public float horizontal_speed_limit = 4.0f;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = new Vector3(Mathf.Min(rb.velocity.x, horizontal_speed_limit),
            Mathf.Min(rb.velocity.y, vertical_speed_limit), rb.velocity.z);
	}
}
