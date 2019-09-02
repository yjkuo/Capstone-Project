using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour {
    database bodydb;
    Dropdown mDropdown;

    // Use this for initialization
    void Start () {
        mDropdown = GetComponent<Dropdown>();        
        bodydb = GameObject.FindGameObjectWithTag("Database").GetComponent<database>();
        transform.gameObject.SetActive(false);

        
        mDropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(mDropdown);
        });

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void putBodyNames()
    {
        Dropdown inputFileOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/inputFileOption").GetComponent<Dropdown>();
        List<string> tmp = bodydb.getNames(inputFileOption.value);
        mDropdown.ClearOptions();
        mDropdown.AddOptions(tmp);
    }
    void DropdownValueChanged(Dropdown change)
    {
        database.Body body;
        Dropdown inputFileOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/inputFileOption").GetComponent<Dropdown>();
        body = bodydb.getDatabyName(inputFileOption.value,change.options[change.value].text);
        Dropdown dirOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/dirOption").GetComponent<Dropdown>();
        Dropdown dataTypeOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/dataTypeOption").GetComponent<Dropdown>();

        if (body.name != "NotFound") GameObject.FindGameObjectWithTag("Database").GetComponent<DataGenerator>().createList(body,dirOption.options[dirOption.value].text, dataTypeOption.options[dataTypeOption.value].text);
    }
}
