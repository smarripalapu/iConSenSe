using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;

// This script is only a visual demostration of the asset!
// If you want to, you can use this with your own scripts.

public class IconGeneratorUIExample : MonoBehaviour
{
	private Sprite iconSprite; // Used in example for viewing image on screen.
	public GameObject itemPrefab; // Used in example for viewing image on screen.

    void Start()
    {
        // Enable Text component.
      // this.transform.GetChild(1).gameObject.SetActive(true);
    }

    // Add image to the 
	public GameObject AddImage(string itemName)
	{
		var tex = Resources.Load<Texture2D>("2DImages/" + itemName);
		iconSprite = Sprite.Create (tex, new Rect (0, 0, 128, 128), new Vector2 (0.5f, 0.5f));
		GameObject item = Instantiate (itemPrefab);
		item.transform.SetParent (this.gameObject.transform.GetChild (0));
		item.GetComponent<Image> ().sprite = iconSprite;
		item.GetComponentInChildren<Text> ().text = itemName;

		return item;
	}
	// Add image to the 
	public void RemoveImage(GameObject it)
	{
		Destroy(it);
	}
}