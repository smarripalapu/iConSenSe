using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Microsoft.MixedReality.Toolkit.Examples.Demos.EyeTracking;
using Microsoft.MixedReality.Toolkit.Input;

public class EyeGazeHit : MonoBehaviour
{

    private EyeTrackingTarget myEyeTrackingTarget = null;
    public static TimeSpan startTimespan = TimeSpan.Zero;
    public static TimeSpan periodTimespan = TimeSpan.FromMinutes(0.5);
    private Color newColor,oldColor;
    private float randomChOne, randomChTwo, randomChThree;
    private Renderer objRenderer;
    public static Dictionary<string, int> hash = new Dictionary<string, int>();
    public DateTime timestamp;
    public static string eyeDataDoc;
    public System.Threading.Timer timer;
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        timestamp = DateTime.Now;
        Directory.CreateDirectory(Application.persistentDataPath + "/EyeData_Logs/");
        Debug.Log(Application.persistentDataPath);
        CreateTextFile();
        myEyeTrackingTarget = this.GetComponent<EyeTrackingTarget>();
        if (myEyeTrackingTarget != null)
        {
            myEyeTrackingTarget.OnLookAtStart.AddListener(changeTrgtColor);
            myEyeTrackingTarget.OnLookAway.AddListener(changeTrgtOrgnl);
           myEyeTrackingTarget.OnSelected.AddListener(changeTrgtColor);
        }
        randomChOne = UnityEngine.Random.Range(0f, 1f);
        randomChTwo = UnityEngine.Random.Range(0f, 1f);
        randomChThree = UnityEngine.Random.Range(0f, 1f);
        //oldColor = 
        newColor = new Color(1f, 0.92f, 0.016f, 0.3f);
        timer = new System.Threading.Timer((e) =>
        {
            writeToFile();
        }, null, startTimespan, periodTimespan);
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (myEyeTrackingTarget != null)
        {
           // myEyeTrackingTarget.OnLookAtStart.AddListener(changeTrgtColor);
            //myEyeTrackingTarget.OnLookAway.AddListener(changeTrgtOrgnl);
            myEyeTrackingTarget.OnLookAway.AddListener(changeTrgtOrgnl);
        }*/
    }
    public void changeTrgtColor()
    {
        objRenderer = this.GetComponent<Renderer>();
        bool keyExists = hash.ContainsKey(this.gameObject.name);
        oldColor = objRenderer.material.GetColor("_Color");
        objRenderer.material.SetColor("_Color", newColor);
        if (keyExists)
        {
            hash[this.gameObject.name] = hash[this.gameObject.name] + 1;
        }
        else
        {
            hash.Add(this.gameObject.name, 1);
        }

        
    }
    public void changeTrgtOrgnl()
    {
        if(objRenderer != null)
        {
            objRenderer.material.SetColor("_Color", oldColor);
        }
        
    }
    public void CreateTextFile()
    {
        eyeDataDoc = Application.persistentDataPath + "/EyeData_Logs/eyeDataDoc.txt";
        if (File.Exists(eyeDataDoc))
        {
            File.Delete(eyeDataDoc);
        }

        if (!File.Exists(eyeDataDoc))
        {
            File.WriteAllText(eyeDataDoc, "NUMBER OF TIMES OBJECT WAS SEEN BY USER \n\n");
        }
    }
    public static void writeToFile()
    {
        foreach (KeyValuePair<string, int> pair in hash)
        {
            File.AppendAllText(eyeDataDoc, pair.Key + "       " + pair.Value.ToString()+"             "+ DateTime.Now + "\n");
        }
        hash.Clear();
    }
    void OnApplicationQuit()
    {/*
        foreach (KeyValuePair<string,int> pair in hash)
        {
            File.AppendAllText(eyeDataDoc, pair.Key + "       " + pair.Value.ToString() + "\n");
        }*/

    }
}
