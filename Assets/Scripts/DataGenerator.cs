using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DataGenerator : MonoBehaviour {
    public GameObject listItem;
    public Transform scrollPanel;
    public InputField input;
    public database data;
    private database.Body body;

	// Use this for initialization
	void Start () {
        data = GetComponent<database>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void createList()
    {
        foreach (var data in body.data)
        {
            GameObject newData = Instantiate(listItem);
            newData.transform.GetChild(0).GetComponent<Text>().text = String.Format("{0:0.0000}", data.x);
            newData.transform.GetChild(1).GetComponent<Text>().text = String.Format("{0:0.0000}", data.y);
            newData.transform.GetChild(2).GetComponent<Text>().text = String.Format("{0:0.0000}", data.z);
            newData.transform.SetParent(scrollPanel.transform);
            newData.GetComponent<RectTransform>().transform.localScale = Vector3.one;
        }          
    }
    public void enterClick()
    {                
        Dropdown dropdown = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/Dropdown").GetComponent<Dropdown>();
        body = data.getDatabyName(dropdown.options[dropdown.value].text);

        if (body.name != "NotFound") createList();
    }
}
