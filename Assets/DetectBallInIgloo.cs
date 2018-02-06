using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectBallInIgloo : MonoBehaviour {

    public float restart_delay = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball")) {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = collision.rigidbody;
            GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayWonLevelSound();
            GameObject.Find("Penguin").GetComponent<InputController>().DisableInput();
            StartCoroutine(DelayThenRestart());
        }
    }

    private IEnumerator DelayThenRestart() {
        yield return new WaitForSeconds(restart_delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
