using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;


public class xmlparser : MonoBehaviour
{
  public Camera raycastCamera;
  private GameObject _selectedObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetMouseButtonDown(0))
            {
                Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                XElement root = XElement.Load("Assets/wellness.xml");
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    _selectedObject = hit.transform.gameObject;
                    Debug.Log("object " + _selectedObject.name);
                    string objname = _selectedObject.name;
                    XNamespace xlink = "http://www.w3.org/1999/xlink";
                    IEnumerable<XElement> properties = from el in root.Elements("properties") select el;
                    IEnumerable<XElement>  materials= from el in root.Elements("materials") select el;
                    IEnumerable<XElement> IfcBuildingStorey = from el in root.Elements("decomposition").Elements("IfcProject").Elements("IfcSite").Elements("IfcBuilding").Elements("IfcBuildingStorey").Descendants()
                    where (string)el.Attribute("id") == objname
                    select el;
                    foreach (XElement ele in IfcBuildingStorey){
                        string type = ele.Name + "Type";
                        Debug.Log(ele.Name);
                        Debug.Log("name: " + (string)ele.Attribute("Name"));
                        Debug.Log("Objecttype " + (string)ele.Attribute("ObjectType"));
                        Debug.Log("ObjectPlacement " + (string)ele.Attribute("ObjectPlacement"));
                        IEnumerable<XElement> objType = from el in ele.Descendants()
                        select el;
                        if(ele.Name == "IfcPropertySet"){
                          string props = (string)ele.Attribute(xlink +"href");
                          foreach (XElement prop in properties){
                            IEnumerable<XElement> p = from el in prop.Elements("IfcPropertySet") where (string)el.Attribute("id") == props.Substring(1)
                            select el;
                            XElement temp = p.FirstOrDefault();
                            Debug.Log("PropertySet " + (string)temp.Attribute("Name"));
                            IEnumerable<XElement> proper = from el in temp.Elements("IfcPropertySingleValue")
                            select el;
                            Debug.Log("Name " + (string)proper.FirstOrDefault().Attribute("Name"));
                            Debug.Log("NominalValue " + (string)proper.FirstOrDefault().Attribute("NominalValue"));
                          }
                        }
                        foreach (XElement t in objType){
                          if(t.Name == "IfcPropertySet"){
                            string props = (string)t.Attribute(xlink +"href");
                            foreach (XElement prop in properties){
                              IEnumerable<XElement> p = from el in prop.Elements("IfcPropertySet") where (string)el.Attribute("id") == props.Substring(1)
                              select el;
                              XElement temp = p.FirstOrDefault();
                              Debug.Log("PropertySet " + (string)temp.Attribute("Name"));
                              IEnumerable<XElement> proper = from el in temp.Elements("IfcPropertySingleValue")
                              select el;
                              Debug.Log("Name " + (string)proper.FirstOrDefault().Attribute("Name"));
                              Debug.Log("NominalValue " + (string)proper.FirstOrDefault().Attribute("NominalValue"));
                            }
                          }

                        }
                        IEnumerable<XElement> MaterialLayer = from el in ele.Elements("IfcMaterialLayerSetUsage")
                        select el;
                        foreach (XElement material in MaterialLayer){
                          string lyr = (string)material.Attribute(xlink +"href");
                          Debug.Log("MaterialLayer " + lyr.Substring(1));
                          foreach (XElement mat in materials){
                            IEnumerable<XElement> Material = from el in mat.Elements("IfcMaterialLayerSetUsage") where (string)el.Attribute("id") == lyr.Substring(1)
                            select el;
                            XElement m = Material.FirstOrDefault();
                            IEnumerable<XElement> finalMat = from el in m.Elements("IfcMaterialLayer")
                            select el;
                            foreach (XElement matvar in finalMat){
                              Debug.Log("MaterialLayer " + (string)matvar.Attribute("Name"));
                              Debug.Log("Layer Thinkness " + (string)matvar.Attribute("LayerThickness"));
                            }

                          }

                        }
                      }
              }

    }
}
}
