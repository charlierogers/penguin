using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLineRendererSortingLayer : MonoBehaviour {

    private TrailRenderer trailRenderer;
    public string sorting_layer;
    public int order_in_layer;

    void Awake() {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Start() {
        trailRenderer.sortingLayerName = sorting_layer;
        trailRenderer.sortingOrder = order_in_layer;
    }

}
