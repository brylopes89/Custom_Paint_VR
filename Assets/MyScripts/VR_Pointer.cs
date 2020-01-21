using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VR_Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;    
    public GameObject m_Dot;
    public VRRaycastInput m_InputModule;
    public bool PointAt = false;

    [HideInInspector] public float targetLength;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();        
    }

    private void UpdateLine()
    {
        //Use default or distance
        PointerEventData data = m_InputModule.GetData();
        targetLength = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLength : data.pointerCurrentRaycast.distance;
        
        Ray ray = new Ray(transform.position, transform.forward);
       // RaycastHit hit = CreateRaycast(targetLength);        
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, targetLength))
        {     
            if (hit.transform.GetComponent<GraphicRaycaster>() != null || hit.transform.GetComponentInParent<GraphicRaycaster>() != null || hit.transform.GetComponent<BoxCollider>() != null)
            {
                PointAt = true;               

                m_LineRenderer.enabled = true;                
                m_Dot.gameObject.SetActive(true);

            }
            else
            {
                PointAt = false;
                m_LineRenderer.enabled = false;
                m_Dot.gameObject.SetActive(false);
            }
        }
        else
        {         
            PointAt = false;
            m_LineRenderer.enabled = false;
            m_Dot.gameObject.SetActive(false);
        }
       
        //Default
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        //Or based on hit
        if (hit.collider != null)
            endPosition = hit.point;

        // Set position of the dot
        m_Dot.transform.position = endPosition;

        //Set linerenderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);

    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);             

        return hit;
    }
}
