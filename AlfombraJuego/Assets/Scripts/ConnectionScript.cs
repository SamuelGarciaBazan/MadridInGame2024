using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConnectionScript : MonoBehaviour
{

    [SerializeField]
    NodosManager nodeManager;

    int state = 0;

    Nodo n1;
    Nodo n2;

    public void selection(InputAction.CallbackContext context)
    {
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(context.performed &&state ==0&& Physics.Raycast(camray, out hit, 1000)&&hit.transform.GetComponent<ConnectionScript>()!=null){
            Debug.Log("On Conection Mode");
            n1 = null;
            n2 = null;
            state = 1;
        }
        else if (context.performed && state == 1&& Physics.Raycast(camray, out hit, 1000) && (n1 = hit.transform.GetComponent<Nodo>()) != null)
        {
            Debug.Log("Nodo 1 " + n1.name);
            state = 2;
        }
        else if(context.performed && state == 2&& Physics.Raycast(camray, out hit, 1000) && (n2=hit.transform.GetComponent<Nodo>()) != null)
        {

            Debug.Log("Nodo 2 " + n2.name);
            state = 0;
            nodeManager.ponArista(n1, n2);
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nodeManager = FindAnyObjectByType<NodosManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
