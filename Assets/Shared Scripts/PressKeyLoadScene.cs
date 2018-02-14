using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressKeyLoadScene : MonoBehaviour {

    public string scene_to_load = "Level1";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
            SceneManager.LoadScene(scene_to_load);
        }
	}
}
