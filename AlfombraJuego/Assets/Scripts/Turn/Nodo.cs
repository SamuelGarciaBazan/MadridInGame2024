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

    //figura que est� colocada, cambiar por enum
    Figure figure = null;

   
    [SerializeField]
    NodosManager nodosManager = null;

    NodeUIManager UIman;

    //recalcula los estados por si han cambiado
    public void updateGlobalStates()
    {
        //Debug.Log("TusMuertoPisao");
        //print(" levelfigure: " + figure.GetLevel());


        for (int i = 0; i < globalStates.Count; i++)
        {
            //globalStates[i] = -negativesModifiers[i];
            globalStates[i] = 0;
        }

        //version solo mirar el propio nodo
        if (figure != null)
        {
            //globalStates[(int)figure.GetRecurseType()] += figure.GetLevel();
        }

        //version mirando los adyacentes
        if(nodosManager  != null)
        {
            //Debug.Log("LatuyaPorSiAcaso");
            List<Nodo> conectados;

            conectados = nodosManager.GetConectedNodes(this);

            for (int i = 0; i < conectados.Count; i++) { 
            
                Figure fig = conectados[i].GetFigure();
                
                //transitividad buena
                if(fig != null)
                {
                    //Debug.Log("PonganleCondon");
                    //print(" levelfig: " + fig.GetLevel());
                    globalStates[(int)fig.GetRecurseType()] += fig.GetLevel();
                }


                List<int> negMods;

                negMods = conectados[i].GetNegativeModifiers();

                //transitividad negativa
                for (int j = 0; j < negMods.Count; j++)
                {
                    globalStates[j] += -negMods[j];
                    //Debug.Log("SeVino");
                }
            }
        }

    }

    public void addNegativeEffect(Figure.RecurseType type,int amount = 1)
    {
        negativesModifiers[(int)type] += amount;   
        updateGlobalStates();
    }

    //para setear una figura
    public bool setFigure(Figure fig)
    {
        //si esta vacio
        if (figure == null)
        {
            Debug.Log("Colocado");
            figure = fig;
            updateGlobalStates();
            UIman.updateUI(globalStates);
            return true;
        }
        else if (figure.GetRecurseType() != fig.GetRecurseType()) //tipos distintos no hacer nada
        {
            Debug.Log("No valido");

            return false;
        }
        else { //tipos iguales,actualizar modelo y sumar nivel
            //TODO:upgrade


            //destruimos la otra, actualizamos la actual
            Debug.Log("Mejorado");
            Destroy(fig.gameObject);


            updateGlobalStates() ;
            UIman.updateUI(globalStates);
            return true;
        }
    }

    public Figure GetFigure()
    {
        return figure;
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
    }
}
