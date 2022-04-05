using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDrawings : MonoBehaviour
{
    public GameObject Floor1;
    public GameObject Floor2;

    // Start is called before the first frame update
    void Start()
    {
        //Floor1 = GameObject.Find("Floor1");
        Floor1.SetActive(false);
        Floor2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowFloor1()
    {
        //Floor1 = GameObject.Find("Floor1");
        Floor1.SetActive(true);
    }
    public void ShowFloor2()
    {
        //Floor1 = GameObject.Find("Floor1");
        Floor2.SetActive(true);
    }
}
