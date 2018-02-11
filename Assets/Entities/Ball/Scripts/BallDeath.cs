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

    public void Kill() {
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayBallDieSound();
            sphereCollider.enabled = false;
            animator.SetTrigger("BreakBall");
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("FloorTile")) {
            GameObject.Find("Penguin").GetComponent<PenguinDeath>().Kill();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Kill();
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("SpikeBall")) {
            GameObject.Find("Penguin").GetComponent<PenguinDeath>().Kill();
            Kill();
        }
    }

}
