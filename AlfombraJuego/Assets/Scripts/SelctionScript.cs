using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelctionScript : MonoBehaviour
{
    [SerializeField]
    Transform reference;
    [SerializeField]
    float heith=10;
    
    Transform objectReference;
    Vector3 preDragPosition;


    public void drag(InputAction.CallbackContext context)
    {
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (context.performed&& Physics.Raycast(camray, out hit, 500, LayerMask.GetMask("Drageable")))
        {
            preDragPosition = hit.transform.position;
            objectReference = hit.transform;
            reference.gameObject.SetActive(true);
            Debug.Log("TuViejaPick");
        }
        else if (context.canceled&&objectReference!=null)
        {
            if(Physics.Raycast(camray, out hit, 500, LayerMask.GetMask("Nodo")))
            {
                objectReference.position = hit.transform.position+
                    new Vector3(0,hit.collider.bounds.extents.y,0)
                    +new Vector3(0, objectReference.GetComponent<Collider>().bounds.extents.y, 0);
            }
            else
            {
                objectReference.position = preDragPosition;
            }
            objectReference = null;
            Debug.Log("TuViejaDrop");
            reference.gameObject.SetActive(false);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectReference = null;
        reference.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Ray camray =Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if (Physics.Raycast(camray, out hit, 500, LayerMask.GetMask("Floor","Drageable")))
        {
            reference.position = hit.point;
            if(objectReference != null)
            {
                objectReference.transform.position = new Vector3(0, heith, 0) + hit.point;
            }
            Debug.Log("tuvieja "+hit.transform.name);
        }
    }
}