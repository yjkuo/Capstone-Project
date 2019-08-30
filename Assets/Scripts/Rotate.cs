using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour {
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public Slider slider;
    private GameObject graphContainer;

    void Awake()
    {
        graphContainer = GameObject.Find("PlayUI/Canvas/DataGenerator/Window_graph/graphContainer").gameObject;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && !slider.GetComponent<Playbar>().slide && !graphContainer.GetComponent<graphClicked>().graphDrag)
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
        }
        mPrevPos = Input.mousePosition;
    }
    
}
