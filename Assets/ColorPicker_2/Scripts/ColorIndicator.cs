using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ColorIndicator : MonoBehaviour {

	HSBColor color;
	HSBColor colorImage;

	void Start() {
		//color = HSBColor.FromColor(GetComponent<Renderer>().sharedMaterial.GetColor("_Color"));
		colorImage = HSBColor.FromColor(GetComponent<Image>().material.GetColor("_Color"));
		transform.parent.BroadcastMessage("SetColor", colorImage);
	}

	void ApplyColor ()
	{
		//GetComponent<Renderer>().sharedMaterial.SetColor ("_Color", color.ToColor());
		GetComponent<Image>().material.SetColor("_Color", colorImage.ToColor());
		transform.parent.BroadcastMessage("OnColorChange", colorImage, SendMessageOptions.DontRequireReceiver);
	}

	void SetHue(float hue)
	{
		color.h = hue;
		ApplyColor();
    }	

	void SetSaturationBrightness(Vector2 sb) {
		color.s = sb.x;
		color.b = sb.y;
		ApplyColor();
	}
}
