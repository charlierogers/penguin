using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectBallInHoop : MonoBehaviour {

    public float next_level_delay = 2.0f;
    public string next_scene = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayWonLevelSound();
        GameObject.Find("Penguin").GetComponent<InputController>().DisableInput();
        StartCoroutine(DelayThenRestart());
    }

    private IEnumerator DelayThenRestart() {
        yield return new WaitForSeconds(next_level_delay);
        SceneManager.LoadScene(next_scene);
    }
}
