using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDeath : MonoBehaviour {

    private Animator animator;
    private Rigidbody rb;
    private SphereCollider sphereCollider;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("FloorTile") ||
            collision.gameObject.layer == LayerMask.NameToLayer("SpikeBall")) {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayBallDieSound();
            GameObject.Find("Penguin").GetComponent<InputController>().DisableInput();
            rb.velocity = Vector3.zero;
            sphereCollider.enabled = false;
            animator.SetTrigger("BreakBall");
        }
    }

}
