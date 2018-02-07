using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour {

    public float flash_on_duration = 1.0f;
    public float flash_off_duration = 0.5f;

    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        StartCoroutine(Flash());
	}

    private IEnumerator Flash() {
        while (true) {
            yield return new WaitForSeconds(flash_on_duration);
            text.enabled = false;
            yield return new WaitForSeconds(flash_off_duration);
            text.enabled = true;
        }
    }
}
