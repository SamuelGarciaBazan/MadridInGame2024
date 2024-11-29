using UnityEngine;
using UnityEngine.InputSystem;

public class SelctionScript : MonoBehaviour
{
    Transform myTrasform;
    [SerializeField]
    Transform reference;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        Ray camray =Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if (Physics.Raycast(camray,out hit,500,LayerMask.GetMask("Floor")))
        {
            reference.position = hit.point;
            Debug.Log("tuvieja "+hit.transform.name);
        }
    }
}
