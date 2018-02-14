using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour {

    public float transition_duration = 1.0f;

    private Vector3 initialOffset;
    private MovePenguin movePenguin;
    private Vector3 transition_velocity = Vector3.zero;

    // Use this for initialization
    void Start() {
        movePenguin = GetComponent<MovePenguin>();
        initialOffset = Camera.main.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.x > Camera.main.transform.position.x) {
            Vector3 targetPos = movePenguin.GetStablePosition() + initialOffset;
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetPos, ref transition_velocity, transition_duration);
        }
    }

}