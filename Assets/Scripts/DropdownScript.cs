using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour {
    database bodydb;
    Dropdown mDropdown;
    private database.Body body;
    // Use this for initialization
    void Start () {
        mDropdown = GetComponent<Dropdown>();
        bodydb = GameObject.FindGameObjectWithTag("Database").GetComponent<database>();
        List<string> tmp = bodydb.getNames();
        mDropdown.ClearOptions();
        mDropdown.AddOptions(tmp);
        mDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(mDropdown);
        });

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void DropdownValueChanged(Dropdown change)
    {
        body = bodydb.getDatabyName(change.options[change.value].text);

        //if (body.name != "NotFound") GameObject.FindGameObjectWithTag("Database").GetComponent<DataGenerator>().createList();
    }
}
