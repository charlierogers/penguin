using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour {

    public float movement_speed = 1.0f;
    public float bounce_force = 50.0f;
    public float anti_gravity = 3.0f;
    public float squish_amount = 0.75f;
    public float squish_duration = 0.5f;

    private Rigidbody rb;
    private Coroutine squishCoroutine;

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("PenguinHead")) {
            rb.AddForce(Vector3.up * bounce_force);
            if (squishCoroutine != null) {
                StopCoroutine(squishCoroutine);
                transform.localScale = Vector3.one;
            }
            squishCoroutine = StartCoroutine(SquishBall());
        }
    }

    private IEnumerator SquishBall() {
        Vector3 squishedScale = transform.localScale;
        squishedScale.y = squish_amount;
        transform.localScale = squishedScale;
        yield return new WaitForSeconds(squish_duration);
        transform.localScale = Vector3.one;
    }
}
