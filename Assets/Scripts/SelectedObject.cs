using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObject : MonoBehaviour
{
    public GameObject parent;
    // Use this for initialization
    void Start()
    {
        parent = GameObject.FindGameObjectWithTag("Character");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseEnter()
    {
    }

    void OnMouseDown()
    {
        parent.GetComponent<Controller>().childObjClicked(this);
    }

    void OnMouseExit()
    {
        //GetComponent<Renderer>().material.color = Color.white;
    }
}
