using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SerialUISummoner : MonoBehaviour
{
    public float minDistance = .2f;
    public float delay = 0.9f;
    public bool showing = false;   
    public Transform player;
    public SteamVR_Behaviour_Pose trackedObj;
    public SteamVR_Action_Boolean trackPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

    protected Animator[] children;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        children = GetComponentsInChildren<Animator>();

        anim.SetBool("Shown", showing);
        for (int a = 0; a < children.Length; a++)
        {
            children[a].SetBool("Shown", showing);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = Camera.main.transform.position - transform.position;
        float distanceSquare = minDistance * minDistance;
        /*if(delta.sqrMagnitude < distanceSquare)
        {
            if (showing) return;            
            StartCoroutine(ActivateInTurn());
            
        }
        else
        {
            if (!showing) return;
            StartCoroutine("DeactivateInTurn");
        }*/

        if (trackPadAction.GetStateDown(trackedObj.inputSource))
        {
            if (showing) return;
            StartCoroutine(ActivateInTurn());
        }        
        else if (trackPadAction.GetStateUp(trackedObj.inputSource))
        {
            if (!showing) return;
            StartCoroutine("DeactivateInTurn");
        }
    }
    public IEnumerator ActivateInTurn()
    {
        showing = true;

        yield return new WaitForSeconds(delay);

        anim.SetBool("Shown", true);

        yield return new WaitForSeconds(delay);

        for (int a = 0; a < children.Length; a++)
        {
            children[a].SetBool("Shown", true);
            yield return new WaitForSeconds(delay);
        }        
    }

    public IEnumerator DeactivateInTurn()
    {
        showing = false;
         
        yield return new WaitForSeconds(delay);

        anim.SetBool("Shown", true);

        yield return new WaitForSeconds(delay);

        for (int a = 0; a < children.Length; a++)
        {
            children[a].SetBool("Shown", false);
            yield return new WaitForSeconds(delay);
        }
    }
}
