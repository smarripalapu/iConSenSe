using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Examples.Demos.EyeTracking;
using Microsoft.MixedReality.Toolkit.Input;

public class EyeGazeHit : MonoBehaviour
{

    private EyeTrackingTarget myEyeTrackingTarget = null;

    private Color newColor,oldColor;
    private float randomChOne, randomChTwo, randomChThree;
    private Renderer objRenderer;
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        myEyeTrackingTarget = this.GetComponent<EyeTrackingTarget>();
        if (myEyeTrackingTarget != null)
        {
            myEyeTrackingTarget.OnLookAtStart.AddListener(changeTrgtColor);
            myEyeTrackingTarget.OnLookAway.AddListener(changeTrgtOrgnl);
           // myEyeTrackingTarget.OnSelected.AddListener(changeTrgtColor);
        }
        randomChOne = Random.Range(0f, 1f);
        randomChTwo = Random.Range(0f, 1f);
        randomChThree = Random.Range(0f, 1f);
        //oldColor = 
        newColor = new Color(0.5f, 0.2f, 0.4f, 1f);
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
        oldColor = objRenderer.material.GetColor("_Color");
        objRenderer.material.SetColor("_Color", newColor);
    }
    public void changeTrgtOrgnl()
    {
        objRenderer.material.SetColor("_Color", oldColor);
    }
}
