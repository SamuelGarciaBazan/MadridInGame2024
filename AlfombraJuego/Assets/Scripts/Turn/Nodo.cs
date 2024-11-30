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

        NodosManager.TransitivityMode tMode = nodosManager.GetTransitivityMode();

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


                    //añadir el beneficio del edificio del otro nodo
                    globalStates[(int)fig.GetRecurseType()] += fig.GetLevel() + 1;
                }


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
                    //Debug.Log("SeVino");
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

            if(!figure.UpgradeLevel()) return false; // si no se puede subir nivel (porque nivelMax) se devuelve false

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

        UIman.updateUI(globalStates);

    }
}
