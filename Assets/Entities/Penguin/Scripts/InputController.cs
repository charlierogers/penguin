using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {


    private bool input_enabled;

	// Use this for initialization
	void Start () {
        input_enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float GetHorizontalAxis() {
        if (!input_enabled)
            return 0.0f;

        return Input.GetAxisRaw("Horizontal");
    }

    public bool GetJumpButton() {
        if (!input_enabled)
            return false;

        return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);
    }

    public void DisableInput() {
        input_enabled = false;
    }

    public void EnableInput() {
        input_enabled = true;
    }
}
