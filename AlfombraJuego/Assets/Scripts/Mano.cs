using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Mano : MonoBehaviour
    {


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddFigure(Transform figure) {

        for (int i = 0; i < transform.childCount; i++) {
            Transform figuraPosition = transform.GetChild(i);
            if(figuraPosition.childCount == 0) {
                figure.SetParent(figuraPosition);

                //Debug.Log("y Bounds "+figure.GetComponent<Collider>().bounds.extents.y);
                //Debug.Log("y Center "+figure.GetComponent<Collider>().bounds.center.y);

                figure.position = figuraPosition.position+ new Vector3(0, figure.GetComponent<Collider>().bounds.extents.y, 0);
                
                return true;
            }
        }
        
        return false;
    }
}
