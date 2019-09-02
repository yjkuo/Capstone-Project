using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dirDropdown : MonoBehaviour {
    Dropdown mDropdown;
    database bodydb;
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
    void DropdownValueChanged(Dropdown change)
    {
        database.Body body;
        Dropdown bodyOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/bodyOption").GetComponent<Dropdown>();
        Dropdown inputFileOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/inputFileOption").GetComponent<Dropdown>();
        body = bodydb.getDatabyName(inputFileOption.value, bodyOption.options[bodyOption.value].text);
        Dropdown dataTypeOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/dataTypeOption").GetComponent<Dropdown>();


        if (body.name != "NotFound") GameObject.FindGameObjectWithTag("Database").GetComponent<DataGenerator>().createList(body, change.options[change.value].text, dataTypeOption.options[dataTypeOption.value].text);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
