using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevelOnFall : MonoBehaviour {

    public float fall_threshold = -10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < fall_threshold) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
	}
}
