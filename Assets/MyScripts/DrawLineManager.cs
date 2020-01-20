using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class DrawLineManager : MonoBehaviour
{
    public static DrawLineManager Instance;
    public GameObject penPoint;
    public SteamVR_Behaviour_Pose trackedObj;   
    public SteamVR_Action_Boolean trackPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Action_Vibration hapticAction;
    public Material lMat;
    public Transform playerTrans;
    public float lineWidth = .1f;
    
    [HideInInspector] public List<GameObject> lineRendererList = new List<GameObject>();
    [HideInInspector] public MeshLineRenderer currLine;

    private int numClicks = 0;

    public void Awake()
    {
        Instance = this;
    }
    void Update()// to do, smooth out currposition with last position to create ribbon effect
    {
        if (trackPadAction.GetStateDown(trackedObj.inputSource))
        {
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();            

            currLine = go.AddComponent<MeshLineRenderer>();            
            currLine.lmat = new Material(lMat);            
            currLine.SetWidth(lineWidth);
            currLine.transform.position = go.transform.position;

            go.transform.position = penPoint.transform.position;

            lineRendererList.Add(go);
        }
        else if (trackPadAction.GetState(trackedObj.inputSource))
        {
            //currLine.positionCount = numClicks + 1;
            //currLine.SetPosition(numClicks, trackedObj.transform.position);
            currLine.AddPoint(trackedObj.transform.position, playerTrans.position);
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

