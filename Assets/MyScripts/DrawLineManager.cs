using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using Valve.VR;

public class DrawLineManager : MonoBehaviour
{
    public SteamVR_Behaviour_Pose trackedObj;   
    public SteamVR_Action_Boolean trackPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Action_Vibration hapticAction;
    public Material lMat;

    private MeshLineRenderer currLine;
    private int numClicks = 0;

    void Update()
    {
        if (trackPadAction.GetStateDown(trackedObj.inputSource))
        {
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();

            currLine = go.AddComponent<MeshLineRenderer>();
            currLine.lmat = new Material(lMat);
            currLine.setWidth(.1f);                  
        }
        else if (trackPadAction.GetState(trackedObj.inputSource))
        {
            //currLine.positionCount = numClicks + 1;
            //currLine.SetPosition(numClicks, trackedObj.transform.position);
            currLine.AddPoint(trackedObj.transform.position);
            numClicks++;
        }
        else if (trackPadAction.GetStateUp(trackedObj.inputSource))
        {
            numClicks = 0;
            currLine = null;
        }

            if (currLine != null)
        {
            currLine.lmat.color = ColorManager.Instance.GetCurrentColor();
        }
    }

    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);
    }
}   

