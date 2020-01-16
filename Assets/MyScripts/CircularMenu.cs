using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class CircularMenu : MonoBehaviour
{
    public List<MenuButton> buttons = new List<MenuButton>();
    public SteamVR_Behaviour_Pose trackedObj;
    public SteamVR_Action_Boolean trackPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

    public int menuItems;
    public int currMenuItem;
    private int oldMenuItem;
    
    private Vector2 mousePos;
    private Vector2 fromVector2M = new Vector2(0.5f, 1.0f);
    private Vector2 centerCircle = new Vector2(0.5f, 0.5f);
    private Vector2 toVector2M;

    // Start is called before the first frame update
    void Start()
    {
        menuItems = buttons.Count;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentMenuItem();

        if (trackPadAction.GetStateDown(trackedObj.inputSource))
            ButtonAction();
        
    }

    public void GetCurrentMenuItem()
    {
        mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        toVector2M = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);

        float angle = (Mathf.Atan2(fromVector2M.y - centerCircle.y, fromVector2M.x - centerCircle.x) - Mathf.Atan2(toVector2M.y - centerCircle.y, toVector2M.x - centerCircle.x)) * Mathf.Rad2Deg;

        if(angle < 0)
           angle += 360;

        currMenuItem = (int)(angle / (360 / menuItems));

        if(currMenuItem != oldMenuItem)
        {
            buttons[oldMenuItem].sceneImage.color = buttons[oldMenuItem].normalColor;
            oldMenuItem = currMenuItem;
            buttons[currMenuItem].sceneImage.color = buttons[currMenuItem].highLightColor;
        }        
    }

    public void ButtonAction()
    {
        buttons[currMenuItem].sceneImage.color = buttons[currMenuItem].pressedColor;
        if(currMenuItem == 0)
        {
            print("You have pressed first button");
        }
    }
}

[System.Serializable]
public class MenuButton
{
    public string name;
    public Image sceneImage;
    public Color normalColor = Color.white;
    public Color highLightColor = Color.grey;
    public Color pressedColor = Color.blue;
}
