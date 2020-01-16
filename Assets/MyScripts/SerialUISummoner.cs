using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialUISummoner : MonoBehaviour
{
    public float minDistance = 2;
    public float delay = 0.5f;
    public bool showing = false;
    public Transform player;

    protected Animator[] children;

    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<Animator>();
        for (int a = 0; a < children.Length; a++)
        {
            children[a].SetBool("Shown", showing);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = Camera.main.transform.position - transform.position;

        if(delta.magnitude < minDistance)
        {
            if (showing) return;            
            StartCoroutine(ActivateInTurn());           
        }
        else
        {
            if (!showing) return;
            StartCoroutine("DeactivateInTurn");
        }       
    }
    public IEnumerator ActivateInTurn()
    {
        showing = true;
        Debug.Log("Activate");
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
        Debug.Log("Deactivate"); 
        yield return new WaitForSeconds(delay);
        for (int a = 0; a < children.Length; a++)
        {
            children[a].SetBool("Shown", false);
            yield return new WaitForSeconds(delay);
        }
    }
}
