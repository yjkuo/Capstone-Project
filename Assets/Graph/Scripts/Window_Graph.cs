
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_Graph : MonoBehaviour {

    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Slider slider;
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;
    private RectTransform markLine;
    private List<GameObject> gameObjectList;
    private GameObject tooltipGameObject;
    //cache parameters
    private List<float> valueList;
    private bool inSameGraph;
    private int maxVisibleValueAmount = -1;

    private void Awake() {
        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();
        markLine = graphContainer.Find("markLine").GetComponent<RectTransform>();
        //tooltipGameObject = graphContainer.Find("tootip").gameObject;
        gameObjectList = new List<GameObject>();


        List<float> valueList = new List<float>() { 5, 6, 8, 14, 19, 18, 17, 15, 13, 17, 25, 37, 40, 36, 33 };
        ShowGraph(valueList, false);
        transform.Find("decreaseBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            ShowGraph(this.valueList, this.inSameGraph, this.maxVisibleValueAmount + 1);
        };
        transform.Find("increaseBtn").GetComponent<Button_UI>().ClickFunc = () =>
        {
            ShowGraph(this.valueList, this.inSameGraph, this.maxVisibleValueAmount - 1);
        };
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void ShowGraph(List<float> valueList, bool inSameGraph, int maxVisibleValueAmount = -1) {
        this.valueList = valueList;
        this.inSameGraph = inSameGraph;

        if (!inSameGraph)
        {
            foreach (GameObject gameObject in gameObjectList)
            {
                Destroy(gameObject);
            }
            gameObjectList.Clear();
        }
        if(maxVisibleValueAmount <= 0)
        {
            maxVisibleValueAmount = valueList.Count;
        }
        if (maxVisibleValueAmount > valueList.Count)
        {
            maxVisibleValueAmount = valueList.Count;
        }
        this.maxVisibleValueAmount = maxVisibleValueAmount;
        float graphHeight = graphContainer.sizeDelta.y;

        float yMaximum = valueList[0];
        float yMinimum = valueList[0];

        for(int i = Mathf.Max(valueList.Count - maxVisibleValueAmount,0); i < valueList.Count; i++)
        {
            float value = valueList[i];
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if(value < yMinimum)
            {
                yMinimum = value;
            }
        }
        float yDif = yMaximum - yMinimum;
        if (yDif <= 0)
            yDif = 5f;
        yMaximum = yMaximum + yDif * 0.2f+1;
        yMinimum = yMinimum - yDif * 0.2f-1;
        //float xSize = 50f;
        float xSize = graphContainer.sizeDelta.x/maxVisibleValueAmount +1;
        GameObject lastCircleGameObject = null;

        int xIndex = 0;
        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++) {
            float xPosition = xSize + xIndex * xSize;
            float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            gameObjectList.Add(circleGameObject);
            if (lastCircleGameObject != null) {                
                GameObject dotConnection = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition,inSameGraph);
                gameObjectList.Add(dotConnection); 
            }
            lastCircleGameObject = circleGameObject;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer,false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -7f);
            labelX.GetComponent<Text>().text = i.ToString();
            gameObjectList.Add(labelX.gameObject);

            RectTransform dashX = Instantiate(dashTemplateY);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, -3f);
            gameObjectList.Add(dashX.gameObject);
            xIndex++;

        }
        int seperatorCount = 10;
        for(int i = 0; i <= seperatorCount; ++i)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float nomalizedValue = i*1f / seperatorCount;
            labelY.anchoredPosition = new Vector2(-7f, nomalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(yMinimum + (nomalizedValue * (yMaximum-yMinimum))).ToString() ;
            gameObjectList.Add(labelY.gameObject);

            RectTransform dashY = Instantiate(dashTemplateX);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, nomalizedValue * graphHeight);
            gameObjectList.Add(dashY.gameObject);
        }
        //showTooltip("this is a tooltip", new Vector2(100, 100));
    }
    /*
    private void showTooltip(string tooltipText, Vector2 anchoredPosition)
    {
        tooltipGameObject.SetActive(true);
        tooltipGameObject.transform.Find("text").GetComponent<Text>().text = tooltipText;
        float padding = 4f;
        Vector2 backSize = new Vector2(
            tooltipGameObject.transform.Find("text").GetComponent<Text>().preferredWidth + padding * 2f,
            tooltipGameObject.transform.Find("text").GetComponent<Text>().preferredHeight + padding * 2f
            );
        tooltipGameObject.transform.Find("background").GetComponent<RectTransform>().sizeDelta = backSize;
        tooltipGameObject.transform.SetAsLastSibling();
    }
    private void hideTooltip()
    {
        tooltipGameObject.SetActive(false);
    }*/
    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, bool sameGraph) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        if(sameGraph)
            gameObject.GetComponent<Image>().color =  new Color(1, 1, 0, .5f);
        else
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        return gameObject;
    }
    public void clearGraph()
    {
        foreach (GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();
    }
    void Update()
    {
        if (!transform.Find("graphContainer").GetComponent<graphClicked>().graphDrag)
        {
            float xPos = slider.normalizedValue * graphContainer.sizeDelta.x;
            markLine.anchoredPosition = new Vector2(xPos, markLine.anchoredPosition.y);
        }
    }

}
