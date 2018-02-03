using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePenguin : MonoBehaviour {

    public float left_movement_limit = -5.0f;
    public float movement_speed = 4.0f;
    public float jump_speed = 4.0f;
    public float jump_time = 1.0f;
    public float wall_detect_dist = 0.5f;

    private Rigidbody rb;
    private InputController inputController;

    private bool on_ground;
    private bool jumpingUp;
    private bool jump;
    private float horizontalMovement;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        inputController = GetComponent<InputController>();
        jumpingUp = false;
        jump = false;
        horizontalMovement = 0.0f;
        on_ground = true;
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMovement = inputController.GetHorizontalAxis();

        if (on_ground && !jumpingUp && Input.GetKeyDown(KeyCode.UpArrow)) {
            jump = true;
        }
	}

    private void FixedUpdate() {

        if (CanMove()) {
            rb.velocity = new Vector3(horizontalMovement * movement_speed, rb.velocity.y, rb.velocity.z);
        }

        if (jump && !jumpingUp) {
            rb.AddForce(Vector3.up * jump_speed);
            jump = false;
            StartCoroutine(Jump(jump_time));
        }

        if (rb.velocity.x < 0.0f && TooFarLeft()) {
            rb.velocity = Vector3.zero;
        }

    }

    private bool CanMove() {
        return horizontalMovement != 0.0f && (on_ground || jumpingUp);
    }

    private bool WallDetected() {
        Debug.DrawRay(transform.position, Vector3.right * wall_detect_dist, Color.red);
        Debug.DrawRay(transform.position, Vector3.left * wall_detect_dist, Color.red);

        return Physics.Raycast(new Ray(transform.position, Vector3.right), wall_detect_dist) || 
            Physics.Raycast(new Ray(transform.position, Vector3.left), wall_detect_dist);
    }

    private bool TooFarLeft() {
        return transform.position.x - Camera.main.transform.position.x < left_movement_limit;
    }

    private IEnumerator JumpMonitor() {
        jumpingUp = true;
        while (rb.velocity.y >= 0.0f) {
            yield return new WaitForFixedUpdate();
        }
        jumpingUp = false;
    }

    private IEnumerator Jump(float jump_time) {
        StartCoroutine(JumpMonitor());
        while (jump_time > 0) {
            rb.AddForce(Vector3.up * jump_speed);
            jump_time -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnCollisionStay(Collision collision) {
        on_ground = true;       
    }

    private void OnCollisionExit(Collision collision) {
        on_ground = false;
    }

}
