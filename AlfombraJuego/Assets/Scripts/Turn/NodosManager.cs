using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/*
 * 
 * 
 * Emplear como objeto padre de un grupo de nodos. El indice de los nodos se vera determinado por su orden en la jerarquia
 *
 *
 */
public class NodosManager : MonoBehaviour
{

    //lista de todos los nodos,vertices
    [SerializeField]
    private List<Nodo> nodes = new List<Nodo>();

    //lista de aristas, lista de nodos,y cada nodo tiene una lista de adyacentes (pos en nodes)
    List<List<int>> adyacentes = new List<List<int>>();


    public List<Nodo> GetNodes()
    {
        return nodes;
    }

    void dfs(int v,List<bool> visit)
    {
        visit[v] = true;

        for (int i = 0; i < adyacentes[v].Count; i++)
        {
            int w = adyacentes[v][i];

            if (!visit[w])
            {
                dfs(w, visit);
            }
        }
    }

    public List<Nodo> GetConectedNodes(Nodo nodo)
    {
        int v = getNodeIndex(nodo);

        if (v == -1) return null;// nodo invalido

        //vector de visitados
        List<bool> visit = new List<bool>();   


        for(int i = 0; i < nodes.Count;i++)
            visit.Add(false);

        //dfs

        //print("mi vertice es: " +  v);  

        dfs(v, visit);


        //mamon contruye el array en el dfs

        //devolver los visitados
        List<Nodo> resp = new List<Nodo>(); 

        for(int i = 0;i < visit.Count; i++)
        {
            if (visit[i])
            {
                resp.Add(nodes[i]);
                //print("añadido vertice :" +  i);
            }
        }

        return resp;
    }

    //indice del nodo
    public int getNodeIndex(Nodo nodo)
    {
        for(int i = 0;i < nodes.Count;i++) 
            if(nodes[i] == nodo) return i;
        return -1;
    }

    //false si los nodos no existen, 
    //si la arista ya estaba false tambien (para detectar si no se puede poner)
    public bool ponArista(Nodo nodo1,Nodo nodo2)
    {
        int index1 = getNodeIndex(nodo1);
        int index2 = getNodeIndex(nodo2);

        if(index1 == -1 || index2 == -1) return false;

        for(int i = 0; i < adyacentes[index1].Count;i++)
            if (nodes[adyacentes[index1][i]] == nodo2) return false; //el nodo ya está

        //añadir los adyacentes
        adyacentes[index1].Add(index2);
        adyacentes[index2].Add(index1);

        return true;
    }

    public void uptateAllNodesStats()
    {
        for(int i = 0; i <= nodes.Count; i++)
        {
            nodes[i].updateGlobalStates();
        }



    }
    private void Start()
    {
        //captura los nodos de los hijos (su orden se ve determinado por el orden de la jerarquia)
        nodes.AddRange(GetComponentsInChildren<Nodo>());
        
        //inicializar la lista de adyacentes con listas vacías
        for (int i = 0; i < nodes.Count; i++)
            adyacentes.Add(new List<int>());

        //UIman = GetComponent<NodeUIManager>();

    }

}
