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


    int state = 0;

    NodosManager nodeManager;

    Nodo n1;
    Nodo n2;

    public void drag(InputAction.CallbackContext context)
    {
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(state == 0)
        {
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


            ///TODO:
            ///comprobar que la jugada puede ser valida segun la economía de acciones
            ///detectar que se ha cogido una ficha y se ha guardado en la reserva
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
        selection(ref context,ref camray);
    }

    public void selection(ref InputAction.CallbackContext context, ref Ray camray)
    {
        //Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (context.performed && state == 0 && Physics.Raycast(camray, out hit, 1000) && hit.transform.GetComponent<ConnectionScript>() != null)
        {
            Debug.Log("On Conection Mode");
            n1 = null;
            n2 = null;
            state = 1;
        }
        else if (context.performed && state == 1 && Physics.Raycast(camray, out hit, 1000) && (n1 = hit.transform.GetComponent<Nodo>()) != null)
        {
            Debug.Log("Nodo 1 " + n1.name);
            state = 2;
        }
        else if (context.performed && state == 2 && Physics.Raycast(camray, out hit, 1000) && (n2 = hit.transform.GetComponent<Nodo>()) != null)
        {
            Debug.Log("Nodo 2 " + n2.name);
            state = 0;
            nodeManager.ponArista(n1, n2);
            n1.updateGlobalStates();
            n2.updateGlobalStates();
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectReference = null;
        reference.gameObject.SetActive(false);
        nodeManager = FindAnyObjectByType<NodosManager>();
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
