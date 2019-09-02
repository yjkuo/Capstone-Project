using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Windows.Forms;

public class database : MonoBehaviour {
    public struct Item
    {
        public Vector3 pos;
        public Vector3 speed;
        public Vector3 acceleration;
        public Item(float posx, float posy, float posz,
                    float spdx, float spdy, float spdz,
                    float accx, float accy, float accz)
        {
            pos.x = posx;
            pos.y = posy;
            pos.z = posz;
            speed.x = spdx;
            speed.y = spdy;
            speed.z = spdz;
            acceleration.x = accx;
            acceleration.y = accy;
            acceleration.z = accz;
        }
    }
    public struct Body
    {
        public string name;
        public List<Item> data;
        public Body(string s, List<Item> l)
        {
            name = s;
            data = l;
        }
    }
    public List<List<Body>> fileDatabase = new List<List<Body>>();
    public List<string[]> fileBodyNames = new List<string[]>();

    public List<string> getNames(int index)
    {
        List<string> tmp = new List<string>();
        for(int i = 1; i < fileBodyNames[index].Length; i++)
        {
            tmp.Add(fileBodyNames[index][i]);
        }
        return tmp;
    }
    // Use this for initialization
    void Start () {
        //getDatabase("Assets/Resources/嚴的投球2_export.txt");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void importBtnClicked()
    {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "txt files (*.txt)|*.txt";
        dialog.InitialDirectory = "C:\\Users\\User\\Documents\\Capstone-Project";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            getDatabase(dialog.FileName);
            Dropdown dropdown = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/inputFileOption").GetComponent<Dropdown>();
            string filename = Path.GetFileName(dialog.FileName);
            dropdown.options.Add(new Dropdown.OptionData() { text = filename.Remove(filename.Length-4) });
            DropdownScript bodyOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/bodyOption").GetComponent<DropdownScript>();
            bodyOption.putBodyNames();
            bodyOption.gameObject.SetActive(true);
            Dropdown dataTypeOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/dataTypeOption").GetComponent<Dropdown>();
            dataTypeOption.gameObject.SetActive(true);
            Dropdown dirOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/dirOption").GetComponent<Dropdown>();
            dirOption.gameObject.SetActive(true);
        }

    }
    void getDatabase(string path)
    {
        StreamReader sr = new StreamReader(path);
        FileInfo fin = new FileInfo("Assets/Resources/Out.csv");
        StreamWriter sw = fin.CreateText();
        List<List<Item>> posDatas = new List<List<Item>>();
        string[] names;
        while (true)
        {
            string s = sr.ReadLine();
            names = s.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            if (names[0] == "Frame")
            {     
                break;
            }
        }
        
        for(int i = 1; i < names.Length-1; i++)
        {
            posDatas.Add(new List<Item>());
            sw.Write(names[i] + ",,,,,,,,,,");
        }
        posDatas.Add(new List<Item>());
        fileBodyNames.Add(names);
        sw.WriteLine(names[names.Length-1]);
        for(int i = 1; i < names.Length; i++)
        {
            sw.Write("x-axis,y-axis,z-axis,x-speed,y-speed,z-speed,x-acc,y-acc,z-acc,,");
        }
        sw.WriteLine();
        sr.ReadLine();
        sr.ReadLine();
    AddItem:
        string line = sr.ReadLine();        
        string[] linedatas = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < names.Length-1; i++)
        {
            posDatas[i].Add(new Item(float.Parse(linedatas[i * 9 + 1]),
                                    float.Parse(linedatas[i * 9 + 2]),
                                    float.Parse(linedatas[i * 9 + 3]),
                                    float.Parse(linedatas[i * 9 + 4]),
                                    float.Parse(linedatas[i * 9 + 5]),
                                    float.Parse(linedatas[i * 9 + 6]),
                                    float.Parse(linedatas[i * 9 + 7]),
                                    float.Parse(linedatas[i * 9 + 8]),
                                    float.Parse(linedatas[i * 9 + 9])));
            sw.Write(linedatas[i * 9 + 1] + "," + linedatas[i * 9 + 2] + "," + linedatas[i * 9 + 3] + ","
                + linedatas[i * 9 + 4] + "," + linedatas[i * 9 + 5] + "," + linedatas[i * 9 + 6] + ","
                + linedatas[i * 9 + 7] + "," + linedatas[i * 9 + 8] + "," + linedatas[i * 9 + 9]);
            sw.Write(",,");
        }
        sw.WriteLine();        
        if (sr.Peek() == -1) sr.Close();
        else goto AddItem;
        sw.Close();
        List<Body> bodyDatabase = new List<Body>();
        for (int i = 0; i < names.Length - 1; i++)
        {
            bodyDatabase.Add(new Body(names[i+1], posDatas[i]));
        }
        fileDatabase.Add(bodyDatabase);

        /*AddItem:
        string tmp = sr.ReadLine().Replace("name: ", "");
        List<Item> temp = new List<Item>();
        do
        {
            string s = sr.ReadLine();
            if (s == ",") break;
            string[] pos = s.Split(' ');
            temp.Add(new Item(int.Parse(pos[0]),
                              int.Parse(pos[1]),
                              int.Parse(pos[2])));
        } while (true);
        bodyDatabase.Add(new Body(tmp, temp));
        if (sr.Peek() == -1) sr.Close();
        else goto AddItem;
        */
    }
    public void writeCsvFile(int fileIndex, string path)
    {
        FileInfo fin = new FileInfo(path);
        StreamWriter sw = fin.CreateText();
        string[] names = fileBodyNames[fileIndex];
        for (int i = 1; i < names.Length - 1; i++)
        {
            sw.Write(names[i] + ",,,,,,,,,,");
        }
        sw.WriteLine(names[names.Length - 1]);
        for (int i = 1; i < names.Length; i++)
        {
            sw.Write("x-axis,y-axis,z-axis,x-speed,y-speed,z-speed,x-acc,y-acc,z-acc,,");
        }
        sw.WriteLine();
        Dropdown inputFileOption = GameObject.Find("PlayUI/Canvas/Panel/bodyInput/inputFileOption").GetComponent<Dropdown>();
        List<Body> bodyDB = fileDatabase[fileIndex];
        for (int j = 0; j < bodyDB[0].data.Count; ++j)
        {
            for (int i = 0; i < bodyDB.Count; i++)
            {
                sw.Write(bodyDB[i].data[j].pos.x + "," + bodyDB[i].data[j].pos.y + "," + bodyDB[i].data[j].pos.z + ","
                    + bodyDB[i].data[j].speed.x + "," + bodyDB[i].data[j].speed.y + "," + bodyDB[i].data[j].speed.z + ","
                    + bodyDB[i].data[j].acceleration.x + "," + bodyDB[i].data[j].acceleration.y + "," + bodyDB[i].data[j].acceleration.z);
                sw.Write(",,");
            }
            sw.WriteLine();
        }
    }

    public Body getDatabyName(int fileIndex, string name)
    {
        List<Body> bodyDatabase = fileDatabase[fileIndex];
        foreach (var item in bodyDatabase)
        {
            if (item.name == name) return item;
        }
        return new Body("NotFound", new List<Item>());
    }
    
}
