using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

    Light lightSwitch;
    public Color p1Color;
    public Color p2Color;
    public Color normalColor;

    // Use this for initialization
    void Start () {
        lightSwitch = GetComponent<Light>();
	}
	
	public void Update()
    {
        if ((GameManager.instance.p1Score - GameManager.instance.p2Score) > 5)
        {
            lightSwitch.color = p1Color;
        }
        else if ((GameManager.instance.p2Score - GameManager.instance.p1Score) > 5)
        {
            lightSwitch.color = p2Color;
        }
        else if ((GameManager.instance.p1Score - GameManager.instance.p2Score) < 5 || (GameManager.instance.p2Score - GameManager.instance.p1Score) < 5)
        {
            lightSwitch.color = normalColor;
        }
    }
}
