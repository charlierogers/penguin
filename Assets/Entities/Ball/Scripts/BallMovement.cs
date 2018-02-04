using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour {

    public float movement_speed = 1.0f;
    public float bounce_force = 50.0f;
    public float anti_gravity = 3.0f;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = new Vector3(movement_speed, rb.velocity.y, rb.velocity.z);
        if (rb.velocity.y < 0.0f) {
            rb.AddForce(Vector3.up * anti_gravity);
        }

	}

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("collision: " + LayerMask.LayerToName(collision.gameObject.layer));
        if (collision.gameObject.layer == LayerMask.NameToLayer("FloorTile")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Penguin")) {
            rb.AddForce(Vector3.up * bounce_force);
            Debug.Log("bounce");
        }
    }
}
