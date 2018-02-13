using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowCollisionDetector : MonoBehaviour {

    private bool has_collided;

	// Use this for initialization
	void Start () {
        has_collided = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool HasCollided() {
        return has_collided;
    }

    private void OnCollisionEnter(Collision collision) {
        has_collided = true;
    }
}
