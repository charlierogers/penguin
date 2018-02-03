using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {

    public float right_limit = 5.0f;
    public float up_limit = 2.0f;
    public float down_limit = -2.0f;

    private float initialVertOffset;

	// Use this for initialization
	void Start () {
        initialVertOffset = transform.position.y - Camera.main.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        float cameraHorzDiff = transform.position.x - Camera.main.transform.position.x;
        float cameraVertDiff = transform.position.y - (Camera.main.transform.position.y + initialVertOffset);
        float camX = Camera.main.transform.position.x;
        float camY = Camera.main.transform.position.y;
        float camZ = Camera.main.transform.position.z;
        //keep camera approximately centered on player vertically
        Camera.main.transform.position = new Vector3(camX, transform.position.y - initialVertOffset, camZ);
        //if (cameraVertDiff > up_limit) {
        //    Camera.main.transform.position = new Vector3(camX, Mathf.Lerp(camY, camY - initialVertOffset, Time.deltaTime), camZ);
        //} else if (cameraVertDiff < down_limit) {
        //    Camera.main.transform.position = new Vector3(camX, Mathf.Lerp(camY, camY + initialVertOffset, Time.deltaTime), camZ);
        //}
        //move camera to the right with player
        if (cameraHorzDiff > right_limit) {
            Camera.main.transform.position = new Vector3(transform.position.x - right_limit, camY, camZ);
            //camX = transform.position.x - right_limit;
        }

        //Camera.main.transform.position = new Vector3(camX, camY, camZ);
	}
}
