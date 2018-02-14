using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenguinDeath : MonoBehaviour {

    public float level_reset_delay = 2.0f;
    public float spike_shake_radius = 0.25f;

    private InputController inputController;
    private Rigidbody rb;
    private bool killed;
    private bool getting_spiked;

	// Use this for initialization
	void Start () {
        inputController = GetComponent<InputController>();
        rb = GetComponent<Rigidbody>();
        killed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (getting_spiked) {
            transform.position = transform.position + (Vector3) Random.insideUnitCircle * spike_shake_radius;
        }
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
        getting_spiked = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter(Collision collision) {
        if (!killed && !getting_spiked && collision.gameObject.layer == LayerMask.NameToLayer("SpikeBall")) {
            getting_spiked = true;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlaySpikeSound();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Kill();
        }
    }
}
