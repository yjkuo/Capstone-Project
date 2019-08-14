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
    private Window_Graph windowGraph;

	// Use this for initialization
	void Start () {
        data = GetComponent<database>();
        windowGraph = transform.Find("Window_graph").GetComponent<Window_Graph>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void createList()
    {
        List<float> dataList = new List<float>();
        //foreach (var data in body.data)
        for(int i = 0; i<body.data.Count; ++i)
        {
            if(i%10 == 0)
                dataList.Add((float)(body.data[i].x*100));
            /*
            GameObject newData = Instantiate(listItem);
            newData.transform.GetChild(0).GetComponent<Text>().text = String.Format("{0:0.0000}", data.x);
            newData.transform.GetChild(1).GetComponent<Text>().text = String.Format("{0:0.0000}", data.y);
            newData.transform.GetChild(2).GetComponent<Text>().text = String.Format("{0:0.0000}", data.z);
            newData.transform.SetParent(scrollPanel.transform);
            newData.GetComponent<RectTransform>().transform.localScale = Vector3.one;
            */
        }
        windowGraph.ShowGraph(dataList);
                 
    }
    public void enterClick()
    {                
        Dropdown dropdown = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/Dropdown").GetComponent<Dropdown>();
        body = data.getDatabyName(dropdown.options[dropdown.value].text);

        if (body.name != "NotFound") createList();
    }
}
