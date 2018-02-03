using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroudDetector : MonoBehaviour {

    private bool on_ground;

	// Use this for initialization
	void Start () {
        on_ground = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool OnGround() {
        return on_ground;
    }

}
