
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using System;

public class KeywordRecognisor : MonoBehaviour, IMixedRealitySpeechHandler
{
    public static GameObject controlledunit = null;
    private Sprite iconSprite; // Used in example for viewing image on screen.
    public GameObject itemPrefab; // Used in example for viewing image on screen.
    public Texture rawIcon;
    public Texture2D icon;
    private Texture2D finalIcon;
    public Sprite[] overlays;
    public string iconName;
    public GameObject it;
    public Camera maincam;
    public LineRenderer lineRenderer;
    public List<Vector3> positions = new List<Vector3>();

    public List<Texture2D> overlayIcons = new List<Texture2D>();


    // Start is called before the first frame update
    void Start()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealitySpeechHandler>(this);
        InvokeRepeating("UpdatePos", 0.5f, 1f);
        lineRenderer = GetComponent<LineRenderer>();
        //.SetActive(false);

    }

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

                        }
                    }

                }

            }

        }

    }

    public void OnSpeechKeywordRecognized(SpeechEventData eventData)
    {
        
        switch (eventData.Command.Keyword.ToLower())

        {

            case "sketch":
                /*
                GetOverlayTextures();
                iconName = controlledunit.name;
                rawIcon = AssetPreview.GetAssetPreview(controlledunit);
                icon = rawIcon as Texture2D;
                if (overlayIcons.Count != 0)
                {
                    if (icon == null)
                    {
                        Debug.LogError("There was an error generating image from " + controlledunit.name + "! Are you sure this is an 3D object?");
                        //continue;
                    }

                    icon = GetFinalTexture(icon, 0);
                }
                else
                {
                    // Check the icon.
                    if (icon == null)
                    {
                        Debug.LogError("There was an error generating image from " + controlledunit.name + "! Are you sure this is an 3D object?");
                        //continue;
                    }
                }
                //Invoke("HideShowGameobject", 5);
                
                RectTransform rt = controlledunit.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
                rt.localPosition = controlledunit.transform.position;

                if (controlledunit.tag == "colorChange" || controlledunit.tag == "others"){
                    it = controlledunit.transform.GetChild(1).gameObject.GetComponent<IconGeneratorUIExample>().AddImage(icon, iconName);
                }

                controlledunit.transform.GetChild(1).gameObject.transform.position = controlledunit.transform.position;
                controlledunit.transform.GetChild(1).gameObject.SetActive(true);
                */
                Debug.Log("sketch");
                // Enable Text component.
                break;



            case "unsketch":

                Debug.Log("unsketch is called");
                /*
                if (controlledunit.tag == "colorChange" || controlledunit.tag == "others"){
                    controlledunit.transform.GetChild(1).gameObject.GetComponent<IconGeneratorUIExample>().RemoveImage(it);
                }
                controlledunit.transform.GetChild(1).gameObject.SetActive(false);
                */
                break;

            case "draw path":

                Debug.Log("path is called");
               // List<Vector3> pos = GetCamCordinates();
                DrawPath(positions);
                lineRenderer.enabled = true;
                break;

            case "drop":

                Debug.Log("drop path is called");
                // List<Vector3> pos = GetCamCordinates();
                lineRenderer.enabled = false;

                break;

            default:

                Debug.Log("Unknown option { eventData.Command.Keyword}");
                break;

        }
    }

    /*private void GetOverlayTextures()
    {
        for (int i = 0; i < overlays.Length; i++)
        {
            if (overlays[i] == null)
                continue;

            string overlayPath = AssetDatabase.GetAssetPath(overlays[i]);
            byte[] fileData = File.ReadAllBytes(overlayPath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            if (tex.height != 128)
                TextureScale.Point(tex, 128, 128);

            overlayIcons.Add(tex);

        }
    }*/

    void HideShowGameobject()
    {
        if (controlledunit.transform.GetChild(1).gameObject.active)
            controlledunit.transform.GetChild(1).gameObject.SetActive(false);
        else
            controlledunit.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void UpdatePos()
    {
        maincam = GameObject.Find("Main Camera").GetComponent<Camera>();
        //Debug.Log(maincam.transform.position);
        positions.Add(maincam.transform.position);
    }

    




    void DrawPath(List<Vector3> vertexPositions)
    {

        lineRenderer.positionCount = vertexPositions.Count;
        lineRenderer.SetPositions(vertexPositions.ToArray());
    }
/*
    private Texture2D GetFinalTexture(Texture2D texture, int id)
    {
        finalIcon = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);

        for (int i = 0; i < overlayIcons.Count; i++)
        {
            if (i == 0)
                CombineTextures(finalIcon, texture, overlayIcons[i]);
            else
                CombineTextures(finalIcon, finalIcon, overlayIcons[i]);
        }

        return finalIcon;
    }

    public void CombineTextures(Texture2D final, Texture2D image, Texture2D overlay)
    {
        var offset = new Vector2(((final.width - overlay.width) / 2), ((final.height - overlay.height) / 2));

        final.SetPixels(image.GetPixels());

        for (int y = 0; y < overlay.height; y++)
        {
            for (int x = 0; x < overlay.width; x++)
            {
                Color PixelColorFore = overlay.GetPixel(x, y) * overlay.GetPixel(x, y).a;
                Color PixelColorBack = final.GetPixel((int)x + (int)offset.x, y + (int)offset.y) * (1 - PixelColorFore.a);
                final.SetPixel((int)x + (int)offset.x, (int)y + (int)offset.y, PixelColorBack + PixelColorFore);
            }
        }

        final.Apply();
    }

    // This is the same as doing string.IsNullOrWhiteSpace in the .NET 4.x runtime.
    // By doing it as a separate custom function we can also support people who are using the old .NET 3.5 runtime
    public bool IsNullOrWhiteSpace(string value)
    {
        if (value != null)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
        }
        return true;
    } */

}


