using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class WindowGraph : MonoBehaviour {
    Dropdown mDropdown;
    // Use this for initialization
    void Start () {
        mDropdown = GetComponent<Dropdown>();
        string[] input = { "swing", "swing2", "pitch", "pitch2" };
        List<string> aniNames = new List<string>(input);
        mDropdown.AddOptions(aniNames);
	}
	
	// Update is called once per frame
	public string getAnimationName()
    {
        return mDropdown.options[mDropdown.value].text;
    }
}
