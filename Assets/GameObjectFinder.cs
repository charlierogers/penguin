using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFinder : MonoBehaviour {

    public GameObject GetChildByName(string name) {
        Transform trans = gameObject.transform.Find(name);
        if (trans == null) {
            Debug.LogWarning("No child gameobject with name " + name + " found");
            return null;
        }
        return trans.gameObject;
    }

}
