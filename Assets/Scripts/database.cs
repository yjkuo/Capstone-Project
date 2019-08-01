using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class database : MonoBehaviour {
    public struct Item
    {
        public double x;
        public double y;
        public double z;
        public Item(double cordx,double cordy,double cordz)
        {
            x = cordx;
            y = cordy;
            z = cordz;
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
    public List<Body> bodyDatabase = new List<Body>();
    public string[] names;

    public List<string> getNames()
    {
        List<string> tmp = new List<string>();
        for(int i = 1; i < names.Length; i++)
        {
            tmp.Add(names[i]);
        }
        return tmp;
    }
    // Use this for initialization
    void Start () {
        getDatabase("Assets/Resources/data1.txt");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void getDatabase(string path)
    {
        StreamReader sr = new StreamReader(path);
        FileInfo fin = new FileInfo("Assets/Resources/Out.csv");
        StreamWriter sw = fin.CreateText();
        List<List<Item>> posDatas = new List<List<Item>>();
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
            sw.Write(names[i] + ",,,,");
        }
        posDatas.Add(new List<Item>());
        sw.WriteLine(names[names.Length-1]);
        sr.ReadLine();
        sr.ReadLine();
    AddItem:
        string line = sr.ReadLine();        
        string[] linedatas = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < names.Length-1; i++)
        {
            posDatas[i].Add(new Item(double.Parse(linedatas[i * 3 + 1]),
                              double.Parse(linedatas[i * 3 + 2]),
                              double.Parse(linedatas[i * 3 + 3])));
            sw.Write(linedatas[i * 3 + 1] + "," + linedatas[i * 3 + 2] + "," + linedatas[i * 3 + 3]);
            sw.Write(",,");
        }
        sw.WriteLine();        
        if (sr.Peek() == -1) sr.Close();
        else goto AddItem;
        sw.Close();
        for (int i = 0; i < names.Length - 1; i++)
        {
            bodyDatabase.Add(new Body(names[i+1], posDatas[i]));
        }

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
    public Body getDatabyName(string name)
    {
        foreach(var item in bodyDatabase)
        {
            if (item.name == name) return item;
        }
        return new Body("NotFound", new List<Item>());
    }
    
}
