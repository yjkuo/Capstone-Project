using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Playbar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    // Use this for initialization
    Slider slider;
    private Animator anim;
    public bool slide = false;
	void Start () {
        anim = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
        slider = GetComponent<Slider>();
        anim.speed = 0;
	}
    public void OnPointerDown(PointerEventData e)
    {    
        slide = true;
    }
    public void OnPointerUp(PointerEventData e)
    {
        slide = false;
        anim.Play("swing", -1, slider.normalizedValue);

    }
    // Update is called once per frame
    void Update () {
        if (!slide)
        {
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            slider.normalizedValue = state.normalizedTime;
            //anim.Play("swing", -1, slider.normalizedValue);
        }
	}
}
