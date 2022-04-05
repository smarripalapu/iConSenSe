using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour
{

    [SerializeField]
    private GameObject[] walls;
    private Renderer wallRenderer;
    private Color newWallColor;
    private float randomChOne, randomChTwo, randomChThree;

    // Start is called before the first frame update
    void Start()
    {
        walls = GameObject.FindGameObjectsWithTag("colorChange");
        // gameObject.GetComponent<Button>().onClick.AddListener(ChangeWallColor);
    }

    // Update is called once per frame
    public void ChangeWallColor()
    {
        walls = GameObject.FindGameObjectsWithTag("colorChange");
        //gameObject.GetComponent<Button>().onClick.AddListener(ChangeWallColor);
        randomChOne = Random.Range(0f, 1f);
        randomChTwo = Random.Range(0f, 1f);
        randomChThree = Random.Range(0f, 1f);
        newWallColor = new Color(randomChOne, randomChTwo, randomChThree, 1f);

        foreach (GameObject go in walls)
        {
            wallRenderer = go.GetComponent<Renderer>();
            wallRenderer.material.SetColor("_Color", newWallColor);
        }

    }

}
