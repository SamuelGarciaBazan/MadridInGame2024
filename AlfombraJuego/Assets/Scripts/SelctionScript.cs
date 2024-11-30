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
        //click
        if (context.performed&& Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Drageable")))
        {
            preDragPosition = hit.transform.position;
            objectReference = hit.transform;
            reference.gameObject.SetActive(true);
            objectReference.GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("TuViejaPick");
        }
        //release
        else if (context.canceled&&objectReference!=null)
        {
            //comprobar que se suelta en una peana y que se puede dejar
            if(Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Nodo"))&&
                hit.transform.GetComponent<Nodo>().setFigure(objectReference.gameObject.GetComponent<Figure>()))
            {
                objectReference.position = hit.transform.position+
                    new Vector3(0,hit.collider.bounds.extents.y,0)
                    +new Vector3(0, objectReference.GetComponent<Collider>().bounds.extents.y, 0);
                objectReference.GetComponent<Collider>().enabled = false;
            }
            //reposicionar si es necesario
            else
            {
                objectReference.position = preDragPosition+new Vector3(0,heith,0);
                objectReference.GetComponent<Rigidbody>().isKinematic = false;
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
        if (Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Floor")))
        {
            reference.position = hit.point;
            if(objectReference != null)
            {
                objectReference.transform.position = new Vector3(0, heith, 0) + hit.point;
            }
        }
    }
}
