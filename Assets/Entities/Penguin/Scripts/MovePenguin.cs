using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePenguin : MonoBehaviour {

    public float left_movement_limit = -5.0f;
    public float movement_speed = 4.0f;
    public float jump_speed = 4.0f;
    public float extra_gravity = 5.0f;
    public float regular_bounce_force = 30.0f;
    public float jump_bounce_force = 60.0f;
    public float raycast_down_dist = 0.75f;

    private Rigidbody rb;
    private InputController inputController;
    private Animator animator;

    private bool on_ground;
    private bool jumpingUp;
    private bool jump;
    private float horizontalMovement;
    private Vector3 stablePosition;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        inputController = GetComponent<InputController>();
        animator = GetComponent<Animator>();
        jumpingUp = false;
        jump = false;
        horizontalMovement = 0.0f;
        on_ground = true;
	}
	
	// Update is called once per frame
	void Update () {
        horizontalMovement = inputController.GetHorizontalAxis();

        // should player jump?
        if (on_ground && !jumpingUp && inputController.GetJumpButton()) {
            jump = true;
        }

        // update the animator parameter for motion
        animator.SetFloat("horizontal_input", horizontalMovement);
	}

    public Vector3 GetStablePosition() {
        return stablePosition;
    }

    public float GetBounceForce() {
        return jumpingUp ? jump_bounce_force : regular_bounce_force;
    }

    private void FixedUpdate() {

        // check whether penguin should jump
        if (jump && !jumpingUp) {
            rb.AddForce(Vector3.up * jump_speed, ForceMode.Impulse);
            jumpingUp = true;
            jump = false;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayJumpSound();
            StartCoroutine(JumpMonitor());
        }

        // check whether penguin can move
        if (CanMove()) {
            rb.velocity = new Vector3(horizontalMovement * movement_speed, rb.velocity.y, rb.velocity.z);
        }

        // add additional gravity to penguin if not on ground
        if (!on_ground) {
            Vector3 newVel = rb.velocity;
            newVel.y -= extra_gravity * Time.fixedDeltaTime;
            rb.velocity = newVel;
        }

        // prevent penguin from going too far left
        if (rb.velocity.x < 0.0f && TooFarLeft()) {
            rb.velocity = Vector3.zero;
        }

        // update stable position 
        if (on_ground) {
            stablePosition = transform.position;
        } else {
            stablePosition.x = transform.position.x;
        }

        // check if penguin far enough off ground to flap wings
        animator.SetBool("on_ground", NearGround());
    }

    private bool CanMove() {
        return horizontalMovement != 0.0f && (on_ground || jumpingUp);
    }

    private bool TooFarLeft() {
        return transform.position.x - Camera.main.transform.position.x < left_movement_limit;
    }

    private IEnumerator JumpMonitor() {
        while (rb.velocity.y <= 0.0f) {
            yield return new WaitForFixedUpdate();
        }
        while (rb.velocity.y >= 0.0f) {
            yield return new WaitForFixedUpdate();
        }
        jumpingUp = false;
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("HorizontalWall")) {
            on_ground = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("HorizontalWall")) {
            on_ground = false;
        }
    }

    private bool NearGround() {
        Vector3 front = transform.position;
        front.x += 0.20f;
        Vector3 back = transform.position;
        back.x -= 0.25f;
        Debug.DrawRay(front, Vector3.down * raycast_down_dist, Color.red);
        Debug.DrawRay(back, Vector3.down * raycast_down_dist, Color.red);
        return Physics.Raycast(new Ray(front, Vector3.down), raycast_down_dist) ||
                Physics.Raycast(new Ray(back, Vector3.down), raycast_down_dist);
    }
}
