using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour {

    public float movement_speed = 4.0f;
    public float fast_movement_speed = 8.0f;
    public float bounce_force = 50.0f;
    public float anti_gravity = 3.0f;
    public float squish_amount = 0.75f;
    public float squish_duration = 0.5f;
    public float snow_attraction = 3.0f;

    private Rigidbody rb;
    private Coroutine squishCoroutine;

    private bool hit_snow;
    private float speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        hit_snow = false;
        speed = movement_speed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!hit_snow) {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
            if (rb.velocity.y < 0.0f) {
                rb.AddForce(Vector3.up * anti_gravity);
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PenguinHead")) {
            hit_snow = false;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayBallBounceSound();
            rb.AddForce(Vector3.up * GameObject.Find("Penguin").GetComponent<MovePenguin>().GetBounceForce());
            if (squishCoroutine != null) {
                StopCoroutine(squishCoroutine);
                transform.localScale = Vector3.one;
            }
            squishCoroutine = StartCoroutine(SquishBall());
        } else if (!hit_snow && (collision.gameObject.layer == LayerMask.NameToLayer("Snow") ||
                collision.gameObject.layer == LayerMask.NameToLayer("HoopEdge"))) {
            hit_snow = true;
        }
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("SpeedRamp")) {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
            speed = fast_movement_speed;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("SpeedRamp")) {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                RigidbodyConstraints.FreezeRotationZ;
            speed = movement_speed;
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
