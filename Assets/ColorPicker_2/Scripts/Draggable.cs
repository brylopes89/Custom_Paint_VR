using UnityEngine;
using Valve.VR;

public class Draggable : MonoBehaviour
{
	public SteamVR_Behaviour_Pose trackedObj;
	public SteamVR_Action_Boolean trackPadAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

	public bool fixX;
	public bool fixY;
	public Transform thumb;

	private Collider col;
	bool dragging;

	private void Start()
	{
		col = GetComponent<Collider>();
	}

	void FixedUpdate()
	{
		if (trackPadAction.GetStateDown(trackedObj.inputSource))
		{
			dragging = false;
			Ray ray = new Ray(trackedObj.transform.position, trackedObj.transform.forward);
			//var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (col.Raycast(ray, out hit, Mathf.Infinity)) 
			{
				dragging = true;
			}
		}
		if (trackPadAction.GetStateUp(trackedObj.inputSource)) 
			dragging = false;
		if (dragging && trackPadAction.GetState(trackedObj.inputSource)) 
		{
			Ray ray = new Ray(trackedObj.transform.position, trackedObj.transform.forward);			
			RaycastHit hit;

			if (col.Raycast(ray, out hit, Mathf.Infinity))
			{
				var point = hit.point;
				point = col.ClosestPointOnBounds(point);
				SetThumbPosition(point);
				SendMessage("OnDrag", Vector3.one - (thumb.position - col.bounds.min) / col.bounds.size.x);
			}			
		}
	}

	void SetDragPoint(Vector3 point)
	{
		//point = ((Vector3.one - point) * col.bounds.size.x) + col.bounds.min;
		SetThumbPosition(point);
	}

	void SetThumbPosition(Vector3 point)
	{
		thumb.position = new Vector3(fixX ? thumb.position.x : point.x, fixY ? thumb.position.y : point.y, thumb.position.z);
	}
}
