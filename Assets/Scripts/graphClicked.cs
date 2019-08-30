using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class graphClicked : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    
    public bool graphDrag = false;
    private RectTransform markLine;
    private Animator anim;

    public void OnPointerDown(PointerEventData e)
    {
        graphDrag = true;
        Vector2 localCursor;
        var rect1 = GetComponent<RectTransform>();
        var pos1 = e.position;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect1, pos1,
            null, out localCursor))
            return;

        float xpos = localCursor.x;
        float ypos = localCursor.y;

        if (xpos < 0) xpos = xpos + (int)rect1.rect.width / 2;
        else xpos += (int)rect1.rect.width / 2;

        if (ypos > 0) ypos = ypos + (int)rect1.rect.height / 2;
        else ypos += (int)rect1.rect.height / 2;

        if (xpos > 0 && xpos < rect1.rect.width)
        {
            markLine.anchoredPosition = new Vector2(xpos, markLine.anchoredPosition.y);
            /*
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            Slider slider = GameObject.Find("PlayUI/Canvas/Panel/Player/Slider").GetComponent<Slider>();
            slider.normalizedValue = xpos / rect1.rect.width;
            */
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        graphDrag = false;
    }

    public void OnDrag(PointerEventData e)
    {
        Vector2 localCursor;
        var rect1 = GetComponent<RectTransform>();
        var pos1 = e.position;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect1, pos1,
            null, out localCursor))
            return;

        float xpos = localCursor.x;
        float ypos = localCursor.y;

        if (xpos < 0) xpos = xpos + (int)rect1.rect.width / 2;
        else xpos += (int)rect1.rect.width / 2;

        if (ypos > 0) ypos = ypos + (int)rect1.rect.height / 2;
        else ypos += (int)rect1.rect.height / 2;
        if(xpos>0 && xpos < rect1.rect.width)
            markLine.anchoredPosition = new Vector2(xpos, markLine.anchoredPosition.y);

        float normalizedValue = xpos/rect1.rect.width;
        Dropdown dp = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/animOption").GetComponent<Dropdown>();
        anim.Play(dp.options[dp.value].text, -1, normalizedValue);
    }
    // Use this for initialization
    void Awake () {
        markLine = transform.Find("markLine").GetComponent<RectTransform>();
        anim = GameObject.FindGameObjectWithTag("Character").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
		
	}
}
