using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour {

    private float backgroundWidth, rightEdge;

	// Use this for initialization
	void Start () {
        backgroundWidth = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        float rightEdge = transform.position.x + backgroundWidth / 2.0f;
        if (rightEdge < GetLeftCameraEdgePosition()) {
            TranslateBackground();
        }
	}

    private void TranslateBackground() {
        transform.position = transform.position + Vector3.right * 2.0f * backgroundWidth;
    }

    private float GetLeftCameraEdgePosition() {
        float halfWidth = Camera.main.aspect * Camera.main.orthographicSize;
        return Camera.main.transform.position.x - halfWidth;
    }
}
