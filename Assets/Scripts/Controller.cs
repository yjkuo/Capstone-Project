using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using System.IO;

public class Controller : MonoBehaviour {

    // Use this for initialization
    private Animator anim;
    public Slider slider;
    public Dropdown mDropdown;
    [SerializeField]    private Window_Graph windowGraph;
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
    public void historyBtnClicked()
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "csv files (*.csv)|*.csv";  //过滤文件类型
        dialog.InitialDirectory = "C:\\Users\\User\\Documents\\Capstone-Project";  
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            StreamReader sr = new StreamReader(dialog.FileName);
            List<float> data = new List<float>();
            List<database.Body> input = new List<database.Body>();
            List<string> bodyNames = new List<string>();
            List < List < database.Item >> tempList = new List<List<database.Item>>();
            string line = sr.ReadLine();
            string[] names = line.Split(',');
            
            for(int i=0; i < names.Length; ++i)
            {
                if (names[i] != "")
                {
                    bodyNames.Add(names[i]);
                    tempList.Add(new List<database.Item>());
                }
            }
        AddItem:
            line = sr.ReadLine();
            string[] linedatas = line.Split(',');
            for(int i=0; i < bodyNames.Count; i++)
            {
                float num;
                bool canParse = float.TryParse(linedatas[i], out num);
                if (canParse)
                {
                    tempList[i].Add(new database.Item(float.Parse(linedatas[i*10]),
                                   float.Parse(linedatas[i * 10 + 1]),
                                   float.Parse(linedatas[i * 10 + 2]),
                                   float.Parse(linedatas[i * 10 + 3]),
                                   float.Parse(linedatas[i * 10 + 4]),
                                   float.Parse(linedatas[i * 10 + 5]),
                                   float.Parse(linedatas[i * 10 + 6]),
                                   float.Parse(linedatas[i * 10 + 7]),
                                   float.Parse(linedatas[i * 10 + 8])));
                }                
            }
            if (sr.Peek() == -1) sr.Close();
            else goto AddItem;
            for (int i = 0; i < bodyNames.Count; i++)
            {
                input.Add(new database.Body(bodyNames[i], tempList[i]));
            }
            Dropdown dropdown = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/bodyOption").GetComponent<Dropdown>();
            Dropdown dirOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/dirOption").GetComponent<Dropdown>();
            Dropdown dataTypeOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/dataTypeOption").GetComponent<Dropdown>();
            string dir = dirOption.options[dirOption.value].text;
            string type = dataTypeOption.options[dataTypeOption.value].text;
            for (int i = 0; i < bodyNames.Count; i++)
            {
                if (input[i].name == dropdown.options[dropdown.value].text)
                {

                    List<float> dataList = new List<float>();
                    for (int j = 0; j < input[i].data.Count; ++j)
                    {
                        if (j % 10 == 0)
                        {
                            if (type == "Position")
                            {
                                if (dir == "x")
                                { 
                                    dataList.Add((float)(input[i].data[j].pos.x * 100));
                                }
                                else if (dir == "y")
                                {
                                    dataList.Add((float)(input[i].data[j].pos.y * 100));
                                }
                                else if (dir == "z")
                                {
                                    dataList.Add((float)(input[i].data[j].pos.z * 100));
                                }
                            }
                            else if (type == "Speed")
                            {
                                if (dir == "x")
                                {
                                    dataList.Add((float)(input[i].data[j].speed.x * 100));
                                }
                                else if (dir == "y")
                                {
                                    dataList.Add((float)(input[i].data[j].speed.y * 100));
                                }
                                else if (dir == "z")
                                {
                                    dataList.Add((float)(input[i].data[j].speed.z * 100));
                                }
                            }
                            else if (type == "Acceleration")
                            {
                                if (dir == "x")
                                {
                                    dataList.Add((float)(input[i].data[j].acceleration.x * 100));
                                }
                                else if (dir == "y")
                                {
                                    dataList.Add((float)(input[i].data[j].acceleration.y * 100));
                                }
                                else if (dir == "z")
                                {
                                    dataList.Add((float)(input[i].data[j].acceleration.z * 100));
                                }
                            }
                        }                           
                    }
                    windowGraph.ShowGraph(dataList, true);
                    break;
                }
            }

        }
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
