using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour {

    // Use this for initialization
    private Animator anim;
    public Slider slider;
    public Dropdown mDropdown;
    private bool isplaying = false;
    // Rotation attributes

    void Start () {
        anim = GetComponent<Animator>();
        anim.speed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        //anim.Play("swing", -1, slider.normalizedValue);
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit ray_cast_hit;

            if (Physics.Raycast(ray, out ray_cast_hit))
            {
                Transform trans = ray_cast_hit.transform;
                if (trans.CompareTag("Selectable"))
                {
                    Debug.Log(trans.name.ToString() + "我被點了一下");
                }
            }
        }
    }
    public void triggerPlay()
    {
        if (isplaying)
        {
            anim.speed = 0f;
            isplaying = false;
        }
        else
        {            
            anim.Play("swing", -1, slider.normalizedValue);
            anim.speed = 1f;
            isplaying = true;
        }
        
    }
    public void triggerStop()
    {
        anim.Play("swing", -1, 0f);
        slider.value = 0;
        anim.speed = 0f;
        transform.position = new Vector3(-0.54f, 0.4f, -8.12f);
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }
    public void childObjClicked(SelectedObject obj)
    {      
        if(obj.name == "Character1_RightLeg")
        {
            mDropdown.value = mDropdown.options.FindIndex(option => option.text == "RShin");
        }
        else if(obj.name == "Character1_LeftLeg")
        {
            mDropdown.value = mDropdown.options.FindIndex(option => option.text == "LShin");
        }
        else if (obj.name == "Character1_RightUpLeg")
        {
            mDropdown.value = mDropdown.options.FindIndex(option => option.text == "RThigh");
        }
        else if (obj.name == "Character1_LeftUpLeg")
        {
            mDropdown.value = mDropdown.options.FindIndex(option => option.text == "LThigh");
        }
        else if (obj.name == "Character1_RightArm")
        {
            mDropdown.value = mDropdown.options.FindIndex(option => option.text == "RShoulder");
        }
        else if (obj.name == "Character1_LeftArm")
        {
            mDropdown.value = mDropdown.options.FindIndex(option => option.text == "LShoulder");
        }
        else if (obj.name == "Character1_RightForeArm")
        {
            mDropdown.value = mDropdown.options.FindIndex(option => option.text == "RForearm");
        }
        else if (obj.name == "Character1_LeftForeArm")
        {
            mDropdown.value = mDropdown.options.FindIndex(option => option.text == "LForearm");
        }
    }
}
