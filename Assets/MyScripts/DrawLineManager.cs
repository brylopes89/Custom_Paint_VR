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

    private LineRenderer currLine;
    private int numClicks = 0;

    void Update()
    {
        if (trackPadAction.GetStateDown(trackedObj.inputSource))
        {
            GameObject go = new GameObject();
            currLine = go.AddComponent<LineRenderer>();
            currLine.SetWidth(.1f, .1f);

            numClicks = 0;
            //print(trackedObj.inputSource);
            //print(trackedObj.inputSource);
        }
        else if (trackPadAction.GetState(trackedObj.inputSource))
        {
            currLine.SetVertexCount(numClicks + 1);
            currLine.SetPosition(numClicks, trackedObj.transform.position);
            numClicks++;
        }
        /*if (trackPadAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            //Pulse(1, 150, 75, SteamVR_Input_Sources.LeftHand);
            GameObject go = new GameObject();
            currLine = go.AddComponent<LineRenderer>();
        }
        else if (trackPadAction.GetState(SteamVR_Input_Sources.LeftHand))
        {
            currLine.SetPosition(0, trackedObj);
        }
        if (trackPadAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            //Pulse(1, 150, 75, SteamVR_Input_Sources.RightHand);
        }
        else if (trackPadAction.GetState(SteamVR_Input_Sources.RightHand))
        {

        }*/

    }

    private void Pulse(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);

        //print("pulse" + " " + source.ToString());
    }
}   

