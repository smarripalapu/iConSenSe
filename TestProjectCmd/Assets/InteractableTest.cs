using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Linq;
using Microsoft.MixedReality.Toolkit.Input;
using System.Xml.Linq;
//using static IconGenerator;


public class InteractableTest : MonoBehaviour
{
    [SerializeField]
    public GameObject _tt;
    public Transform anchor = null;
    public string printproperties = "";
    public GameObject _selectedObject;
    //public MeshRenderer[] limbs;
    // public GameObject creatett;
    public BoundingBox bbox;
    public BoxCollider bc;
    public ObjectManipulator om;
    public NearInteractionGrabbable nig;
    //public IconGenerator ic;
    public GameObject GO;
    [SerializeField]
    public GameObject canv;
    public Transform[] li;

    void Start()
    {
        XElement root = XElement.Load("Assets/wellness.xml");
        //li = ;
        foreach(GameObject child in GameObject.FindGameObjectsWithTag("others"))
         {
            child.AddComponent<BoxCollider>();
            child.AddComponent<ObjectManipulator>();
            child.AddComponent<NearInteractionGrabbable>();
            bbox = child.AddComponent<BoundingBox>();
            Transform obj = child.transform;

            GO = Instantiate(canv);
            GO.transform.parent = child.transform;
            
            GO.SetActive(false);

            //ic.targets.Append(t);
            // Important: BoundingBox creates a scale handler on start if one does not exist
            // do not use AddComponent, as that will create a  duplicate handler that will not be used
            MinMaxScaleConstraint scaleConstraint = bbox.GetComponent<MinMaxScaleConstraint>();
            scaleConstraint.ScaleMinimum = 1f;
            scaleConstraint.ScaleMaximum = 2f;
            var displayer = child.gameObject.AddComponent<ToolTipSpawner>();
             _selectedObject = child;

              string objname = _selectedObject.name;

              XNamespace xlink = "http://www.w3.org/1999/xlink";
              IEnumerable<XElement> properties = from el in root.Elements("properties") select el;
              IEnumerable<XElement> materials = from el in root.Elements("materials") select el;
              IEnumerable<XElement> IfcBuildingStorey = from el in root.Elements("decomposition").Elements("IfcProject").Elements("IfcSite").Elements("IfcBuilding").Elements("IfcBuildingStorey").Descendants()
                                                        where (string)el.Attribute("id") == objname
                                                        select el;
              foreach (XElement ele in IfcBuildingStorey)
              {
                  string type = ele.Name + "Type";
                  Debug.Log(ele.Name);
                  Debug.Log("name: " + (string)ele.Attribute("Name"));
                  Debug.Log("Objecttype " + (string)ele.Attribute("ObjectType"));
                  Debug.Log("ObjectPlacement " + (string)ele.Attribute("ObjectPlacement"));
                  printproperties = "name: " + (string)ele.Attribute("Name") + "\n" + "Objecttype: " + (string)ele.Attribute("ObjectType") + "\n ObjectPlacement: " + (string)ele.Attribute("ObjectPlacement");

              }
                //printproperties = _selectedObject.name;
             // Debug.Log("susi: " + printproperties);
               displayer.toolTipText = printproperties;
               displayer.anchor = child.transform;
               displayer.SpawnableActivated(_tt);

           }
          foreach (GameObject child in GameObject.FindGameObjectsWithTag("colorChange"))
          {
              bc = child.AddComponent<BoxCollider>();
              om = child.AddComponent<ObjectManipulator>();
              nig = child.AddComponent<NearInteractionGrabbable>();
              bbox = child.AddComponent<BoundingBox>();
              Transform obj = child.GetComponent<Transform>();

              GO = Instantiate(canv);
              GO.transform.parent = child.transform;
              GO.SetActive(false);
               /*ic = child.AddComponent<IconGenerator>();
               ic.targets.Add(new Target("", child));
               ic.customFolder = "2DImages";*/

              //ic.targets.Append(t);
              // Important: BoundingBox creates a scale handler on start if one does not exist
              // do not use AddComponent, as that will create a  duplicate handler that will not be used
              MinMaxScaleConstraint scaleConstraint = bbox.GetComponent<MinMaxScaleConstraint>();
              scaleConstraint.ScaleMinimum = 1f;
              scaleConstraint.ScaleMaximum = 2f;
              var displayer = child.AddComponent<ToolTipSpawner>();
              _selectedObject = child;

              string objname = _selectedObject.name;

              XNamespace xlink = "http://www.w3.org/1999/xlink";
              IEnumerable<XElement> properties = from el in root.Elements("properties") select el;
              IEnumerable<XElement> materials = from el in root.Elements("materials") select el;
              IEnumerable<XElement> IfcBuildingStorey = from el in root.Elements("decomposition").Elements("IfcProject").Elements("IfcSite").Elements("IfcBuilding").Elements("IfcBuildingStorey").Descendants()
                                                        where (string)el.Attribute("id") == objname
                                                        select el;
              foreach (XElement ele in IfcBuildingStorey)
              {
                  string type = ele.Name + "Type";
                  Debug.Log(ele.Name);
                  Debug.Log("name: " + (string)ele.Attribute("Name"));
                  Debug.Log("Objecttype " + (string)ele.Attribute("ObjectType"));
                  Debug.Log("ObjectPlacement " + (string)ele.Attribute("ObjectPlacement"));
                  printproperties = "name: " + (string)ele.Attribute("Name") + "\n" + "Objecttype: " + (string)ele.Attribute("ObjectType") + "\n ObjectPlacement: " + (string)ele.Attribute("ObjectPlacement");

              }
              //printproperties = _selectedObject.name;
              // Debug.Log("susi: " + printproperties);
            
           // printproperties = child.gameObject.name;
            displayer.toolTipText = printproperties;
            displayer.anchor = child.transform;
            displayer.SpawnableActivated(_tt);

        }




    }
    

}
