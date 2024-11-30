using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeUIManager : MonoBehaviour
{


    [SerializeField]
    List<TMP_Text> values;

    Nodo mNode;

    public void updateUI(List<int> lst)
    {
        //Debug.Log(lst);
        int i = 0;
        foreach(var t in values)
        {
            t.text = lst[i].ToString();
            i++;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mNode = GetComponent<Nodo>();
        foreach (var t in values)
        {
            t.text = 2.ToString();//GetComponent<Nodo>().getGlobalStates()[i].ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //updateUI();
    }
}
