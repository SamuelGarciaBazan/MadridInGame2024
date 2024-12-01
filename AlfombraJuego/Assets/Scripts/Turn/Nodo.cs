using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;


public class Nodo : MonoBehaviour
{

    //estados finales 

    //IMPORTANTE: PUBLIC PARA DEBUGEAR, CAMBIAR !!!!
    public List<int> globalStates = new List<int>((int)Figure.RecurseType.END_ENUM);

    //los valores estan en negativo

    //IMPORTANTE: PUBLIC PARA DEBUGEAR, CAMBIAR !!!!
    public List<int> negativesModifiers = new List<int>((int)Figure.RecurseType.END_ENUM);


    List<Figure> figuresList = new List<Figure>();

    [SerializeField]
    private int maxFigures = 2;

   
    [SerializeField]
    NodosManager nodosManager = null;

    //seteados desde el editor
    [SerializeField]
    List<Transform> buildsPositions;
    
    NodeUIManager UIman;

    //recalcula los estados por si han cambiado
    public void updateGlobalStates()
    {
        //modo de transitividad
        NodosManager.TransitivityMode tMode = nodosManager.GetTransitivityMode();

        //aplicacion de estados negativos/reset
        for (int i = 0; i < globalStates.Count; i++)
        {
            if(tMode == NodosManager.TransitivityMode.ONLY_GOOD)
            {
                //aplicar solo sus modificadores negativos,setear a ese valor
                globalStates[i] = -negativesModifiers[i];
            }
            else if(tMode == NodosManager.TransitivityMode.GOOD_AND_BAD)
            {
                //resetear a 0
                globalStates[i] = 0;
            }
        }

        //version mirando los adyacentes
        if(nodosManager  != null)
        {
            //lista de todos los nodos conectados a este por el grafo
            //NOTA: el propio nodo ESTÁ incluido
            List<Nodo> conectados = nodosManager.GetConectedNodes(this);

            //para cada nodo...
            for (int i = 0; i < conectados.Count; i++) { 
            
                //lista de las otras figuras 
                List<Figure> otherFigList = conectados[i].GetFiguresList();

                //para cada figura del otro nodo...
                for (int j = 0; j < otherFigList.Count; j++)
                {
                    Figure fig = otherFigList[j];

                    //transitividad buena
                    if (fig != null)
                    {
                        //añadir el beneficio del edificio del otro nodo
                        globalStates[(int)fig.GetRecurseType()] += fig.GetLevel() + 1;
                    }

                }
                
                //modificadores negativos del otro nodo
                List<int> negMods;

                negMods = conectados[i].GetNegativeModifiers();

                //transitividad negativa
                for (int j = 0; j < negMods.Count; j++)
                {

                    if (tMode == NodosManager.TransitivityMode.ONLY_GOOD)
                    {
                        //si no hay transitividad negativa, no sumo nada


                    }
                    else if (tMode == NodosManager.TransitivityMode.GOOD_AND_BAD)
                    {

                        //si hay transitividad negativa, sumar los modificadores negativos del otro nodo
                        globalStates[j] += -negMods[j];
                    }
                }
            }
        }


        UIman.updateUI(globalStates);
    }

    public void addNegativeEffect(Figure.RecurseType type,int amount = 1)
    {
        negativesModifiers[(int)type] += amount;   
        updateGlobalStates();
    }

    //devuelve -1 si no se puede mejorar ningun nodo
    //si algun nodo se puede mejorar, devuelve su indice
    private int checkCanImprove(Figure fig)
    {
        int resp = -1;

        //recorremos la lista
        for(int i = 0; i < figuresList.Count; i++)
        {
            //si son del mismo tipo
            if (figuresList[i].GetRecurseType() ==fig.GetRecurseType())
            {
                //si se ha podido mejorar, devolvemos el indice correspondiente
                if (figuresList[i].UpgradeLevel()) //esto ya actualiza la visual y el level de la figure
                {
                    resp = i;
                    break;
                }
            }
        }


        return resp;
    }


    //para setear una figura
    public bool setFigure(Figure fig)
    {
        int index = checkCanImprove(fig);

        //si no puedo mejorar ningun edificio
        if (index == -1)
        {
            if(figuresList.Count < maxFigures) //queda hueco
            {
                //Debug.Log("Colocado");
                figuresList.Add(fig);
                nodosManager.uptateAllNodesStats();
                UIman.updateUI(globalStates);
                return true;
            }
            else //si no queda hueco, no hacer nada
            {
                return false;
            }
        }
        else // si sí se puede mejorar
        {
            //destruir la otra figura
            Destroy(fig.gameObject);

            //actualizar puntuaciones y UI
            nodosManager.uptateAllNodesStats();
            UIman.updateUI(globalStates);
            return true;
        }       
    }
    public List<Figure> GetFiguresList()
    {
        return figuresList;
    }

    public List<int> getGlobalStates()
    {
        return globalStates;
    }

    public List<int> GetNegativeModifiers()
        { return negativesModifiers; }

    private void Start()
    {
        for (int i = 0; i< (int)Figure.RecurseType.END_ENUM; i++)
        {
            //inicializar los arrays
            globalStates.Add(0);
            negativesModifiers.Add(0);
        }
        nodosManager = GetComponentInParent<NodosManager>();
        UIman = GetComponent<NodeUIManager>();

        UIman.updateUI(globalStates);
    }

    private void Update()
    {
        for (int i = 0; i< figuresList.Count; i++) {
            figuresList[i].transform.position = buildsPositions[i].position;
        }
    }

}
