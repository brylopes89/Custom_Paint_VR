using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class ButtonPress : MonoBehaviour
{
    public SteamVR_Behaviour_Pose trackedObj;
    public SteamVR_Action_Boolean trackPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    
    private MenuController menuController;
    private Collider col;
    // Start is called before the first frame update
    void Start()
    {
        menuController = FindObjectOfType<MenuController>();
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trackPadAction.GetStateDown(trackedObj.inputSource))
        {
            Ray ray = new Ray(trackedObj.transform.position, trackedObj.transform.forward);
            RaycastHit hit;

            if (col.Raycast(ray, out hit, Mathf.Infinity))
            {
                GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}
