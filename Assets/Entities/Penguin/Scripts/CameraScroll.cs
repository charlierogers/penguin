using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {

    public float right_limit = 5.0f;
    public float up_limit = 2.0f;
    public float down_limit = -2.0f;
    public float transition_duration = 1.0f;

    private Vector3 initialOffset;
    private Coroutine transitionCoroutine;
    private MovePenguin movePenguin;
    private Vector3 transition_velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        movePenguin = GetComponent<MovePenguin>();
        initialOffset = Camera.main.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //float cameraHorzDiff = transform.position.x - Camera.main.transform.position.x;
        //float cameraVertDiff = movePenguin.GetStablePosition().y - (Camera.main.transform.position.y - initialOffset.y);
        ////float camX = Camera.main.transform.position.x;
        //float camY = Camera.main.transform.position.y;
        //float camZ = Camera.main.transform.position.z;
        ////keep camera approximately centered on player vertically
        //if (cameraVertDiff > up_limit || cameraVertDiff < down_limit) {
        //    if (transitionCoroutine != null)
        //        StopCoroutine(transitionCoroutine);
        //    transitionCoroutine = StartCoroutine(CameraTransition(movePenguin.GetStablePosition() + initialOffset));
        //}
        ////move camera to the right with player
        //if (cameraHorzDiff > right_limit) {
        //    Camera.main.transform.position = new Vector3(transform.position.x - right_limit, camY, camZ);
        //}


        Vector3 targetPos = movePenguin.GetStablePosition() + initialOffset;
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPos, ref transition_velocity, transition_duration);
	}

    private IEnumerator CameraTransition(Vector3 targetPos) {
        float t = 0.0f;
        Vector3 startPos = Camera.main.transform.position;
        while (t < 1.0f) {
            t += Time.deltaTime * (Time.timeScale / transition_duration);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, 
                Mathf.Lerp(startPos.y, targetPos.y, t),
                Camera.main.transform.position.z);
            yield return 0;
        }
    }
}
