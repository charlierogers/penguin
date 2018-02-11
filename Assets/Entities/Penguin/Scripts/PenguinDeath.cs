using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenguinDeath : MonoBehaviour {

    public float level_reset_delay = 2.0f;

    private InputController inputController;
    private Rigidbody rb;
    private bool killed;

	// Use this for initialization
	void Start () {
        inputController = GetComponent<InputController>();
        rb = GetComponent<Rigidbody>();
        killed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Kill() {
        if (!killed) {
            killed = true;
            inputController.DisableInput();
            StartCoroutine(DelayedReset());
        }
    }

    private IEnumerator DelayedReset() {
        yield return new WaitForSeconds(level_reset_delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("SpikeBall")) {
            //rb.velocity = Vector3.zero;
            //rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Kill();
            //FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            //joint.connectedBody = collision.rigidbody;
        }
    }
}
