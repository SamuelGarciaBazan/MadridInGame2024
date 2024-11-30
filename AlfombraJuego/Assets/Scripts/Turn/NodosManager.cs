using System.Collections.Generic;
using UnityEngine;

public class NodosManager : MonoBehaviour
{
    //lista de todos los nodos
    [SerializeField]
    private List<Nodo> nodes = new List<Nodo>();

    public List<Nodo> GetNodes()
    {
        return nodes;
    }

    //gestionar la conectividad entre nodos,grafo?

}
