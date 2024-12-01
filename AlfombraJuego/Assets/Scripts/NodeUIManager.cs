using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeUIManager : MonoBehaviour
{


    [SerializeField]
    List<Image> values;

    [SerializeField]
    Sprite[] imgs;

    Nodo mNode;

    public void updateUI(List<int> lst)
    {
        //Debug.Log(lst);
        int i = 0;
        foreach(var t in values)
        {
            if (lst[i] <= -3) t.sprite = imgs[0];
            if (lst[i] == -2) t.sprite = imgs[1];
            if (lst[i] == -1) t.sprite = imgs[2];
            if (lst[i] == 0) t.sprite = imgs[3];
            if (lst[i] == 1) t.sprite = imgs[4];
            if (lst[i] == 2) t.sprite = imgs[5];
            if (lst[i] >= 3) t.sprite = imgs[6];
            i++;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mNode = GetComponent<Nodo>();
        foreach (var t in values)
        {
            t.sprite = imgs[3];
        }

        imgs[0].

    }

    // Update is called once per frame
    void Update()
    {
        //updateUI();
    }
}
