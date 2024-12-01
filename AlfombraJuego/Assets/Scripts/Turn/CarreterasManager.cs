using System.Collections.Generic;
using UnityEngine;

public class CarreterasManager : MonoBehaviour
{

    //debe tener a todos los objetos como hijos
    public List<Carretera> carreteras;

    public void enableOutline(bool enable)
    {
        foreach (var item in GetComponentsInChildren<Outline>())
        {
            item.enabled = enable;
        }
    }


    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            carreteras.Add( transform.GetChild(i).GetComponent<Carretera>());

        }
    }

 
}
