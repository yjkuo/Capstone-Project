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
    public void createList(string dir, string type)
    {
        List<float> dataList = new List<float>();
        //foreach (var data in body.data)1
        for(int i = 0; i<body.data.Count; ++i)
        {
            if(i%10 == 0)
            {
                if(type == "Position")
                {
                    if(dir == "x")
                    {
                        dataList.Add((float)(body.data[i].pos.x * 100));
                    }
                    else if(dir == "y")
                    {
                        dataList.Add((float)(body.data[i].pos.y * 100));
                    }
                    else if(dir == "z")
                    {
                        dataList.Add((float)(body.data[i].pos.z * 100));
                    }
                }
                else if(type == "Speed")
                {
                    if (dir == "x")
                    {
                        dataList.Add((float)(body.data[i].speed.x * 100));
                    }
                    else if (dir == "y")
                    {
                        dataList.Add((float)(body.data[i].speed.y * 100));
                    }
                    else if (dir == "z")
                    {
                        dataList.Add((float)(body.data[i].speed.z * 100));
                    }
                }
                else if(type == "Acceleration")
                {
                    if (dir == "x")
                    {
                        dataList.Add((float)(body.data[i].acceleration.x * 100));
                    }
                    else if (dir == "y")
                    {
                        dataList.Add((float)(body.data[i].acceleration.y * 100));
                    }
                    else if (dir == "z")
                    {
                        dataList.Add((float)(body.data[i].acceleration.z * 100));
                    }
                }
            }
                
            /*
            GameObject newData = Instantiate(listItem);
            newData.transform.GetChild(0).GetComponent<Text>().text = String.Format("{0:0.0000}", data.x);
            newData.transform.GetChild(1).GetComponent<Text>().text = String.Format("{0:0.0000}", data.y);
            newData.transform.GetChild(2).GetComponent<Text>().text = String.Format("{0:0.0000}", data.z);
            newData.transform.SetParent(scrollPanel.transform);
            newData.GetComponent<RectTransform>().transform.localScale = Vector3.one;
            */
        }
        windowGraph.ShowGraph(dataList,false);
                 
    }
    public void enterClick()
    {                
        Dropdown dropdown = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/bodyOption").GetComponent<Dropdown>();
        body = data.getDatabyName(dropdown.options[dropdown.value].text);
        Dropdown dirOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/dirOption").GetComponent<Dropdown>();
        Dropdown dataTypeOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/dataTypeOption").GetComponent<Dropdown>();

        if (body.name != "NotFound") createList(dirOption.options[dirOption.value].text, dataTypeOption.options[dataTypeOption.value].text);
    }
}
