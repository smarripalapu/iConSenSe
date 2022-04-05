using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.MixedReality.Toolkit.UI;

public class objectHit : MonoBehaviour

{
    public static GameObject controlledunit = null;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var source in MixedRealityToolkit.InputSystem.DetectedInputSources)
        {
            // Ignore anything that is not a hand because we want articulated hands
            if (source.SourceType == Microsoft.MixedReality.Toolkit.Input.InputSourceType.Hand)
            {
                foreach (var p in source.Pointers)
                {
                    if (p is IMixedRealityNearPointer)
                    {
                        // Ignore near pointers, we only want the rays
                        continue;
                    }
                    if (p.Result != null)
                    {
                        var startPoint = p.Position;
                        var endPoint = p.Result.Details.Point;
                        var hitObject = p.Result.Details.Object;
                        if (hitObject)
                        {
                            controlledunit = hitObject.transform.gameObject;
                            var myMaterial = controlledunit.GetComponent<Renderer>().material;
                            XElement root = XElement.Load("Assets/wellness.xml");
                            Debug.Log("Object name: " + controlledunit.name);
                            Debug.Log("Material name: " + myMaterial);

                            string objname = controlledunit.name;
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
                                IEnumerable<XElement> objType = from el in ele.Descendants()
                                                                select el;
                                if (ele.Name == "IfcPropertySet")
                                {
                                    string props = (string)ele.Attribute(xlink + "href");
                                    foreach (XElement prop in properties)
                                    {
                                        IEnumerable<XElement> pr = from el in prop.Elements("IfcPropertySet")
                                                                  where (string)el.Attribute("id") == props.Substring(1)
                                                                  select el;
                                        XElement temp = pr.FirstOrDefault();
                                        Debug.Log("PropertySet " + (string)temp.Attribute("Name"));
                                        IEnumerable<XElement> proper = from el in temp.Elements("IfcPropertySingleValue")
                                                                       select el;
                                        Debug.Log("Name " + (string)proper.FirstOrDefault().Attribute("Name"));
                                        Debug.Log("NominalValue " + (string)proper.FirstOrDefault().Attribute("NominalValue"));
                                    }
                                }
                                foreach (XElement t in objType)
                                {
                                    if (t.Name == "IfcPropertySet")
                                    {
                                        string props = (string)t.Attribute(xlink + "href");
                                        foreach (XElement prop in properties)
                                        {
                                            IEnumerable<XElement> pr = from el in prop.Elements("IfcPropertySet")
                                                                      where (string)el.Attribute("id") == props.Substring(1)
                                                                      select el;
                                            XElement temp = pr.FirstOrDefault();
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
                                foreach (XElement material in MaterialLayer)
                                {
                                    string lyr = (string)material.Attribute(xlink + "href");
                                    Debug.Log("MaterialLayer " + lyr.Substring(1));
                                    foreach (XElement mat in materials)
                                    {
                                        IEnumerable<XElement> Material = from el in mat.Elements("IfcMaterialLayerSetUsage")
                                                                         where (string)el.Attribute("id") == lyr.Substring(1)
                                                                         select el;
                                        XElement m = Material.FirstOrDefault();
                                        IEnumerable<XElement> finalMat = from el in m.Elements("IfcMaterialLayer")
                                                                         select el;
                                        foreach (XElement matvar in finalMat)
                                        {
                                            Debug.Log("MaterialLayer " + (string)matvar.Attribute("Name"));
                                            Debug.Log("Layer Thinkness " + (string)matvar.Attribute("LayerThickness"));
                                        }

                                    }

                                }
                            }

                            /*
                            string line = controlledunit.name;
                            string[] prop = line.Split(':');
                             
                            
                            toolTip.ToolTipText = line;
                            var connector = toolTip.GetComponent<ToolTipConnector>();
                            connector.Target = (anchor != null) ? anchor.gameObject : gameObject;

                            toolTip.ShowBackground = showBackground;
                            toolTip.ShowHighlight = showOutline;
                            toolTip.ShowConnector = showConnector;

                            connector.PivotDirection = pivotDirection;
                            connector.PivotDistance = pivotDistance;
                            connector.PivotDirectionOrient = pivotDirectionOrient;
                            connector.ManualPivotLocalPosition = manualPivotLocalPosition;
                            connector.ManualPivotDirection = manualPivotDirection;
                            connector.ConnectorFollowingType = followType;
                            connector.PivotMode = pivotMode;

                            if (connector.PivotMode == ConnectorPivotMode.Manual)
                            {
                                toolTip.PivotPosition = transform.TransformPoint(manualPivotLocalPosition);
                            }
                            */

                            if (Input.GetKeyDown(KeyCode.U))
                            {
                                controlledunit = hitObject.transform.gameObject;
                                controlledunit.transform.Rotate(20.0f, 0.0f, 0.0f, Space.World);
                            }
                        }

                    }

                }
            }
        }
    }

}
