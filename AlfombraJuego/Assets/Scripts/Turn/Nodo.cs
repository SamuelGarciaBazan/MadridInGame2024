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


    //recalcula los estados por si han cambiado
    public void updateGlobalStates()
    {
        for (int i = 0; i < globalStates.Count; i++)
        {
            globalStates[i] = -negativesModifiers[i];
        }

        if (figure != null)
        {
            globalStates[(int)figure.GetRecurseType()] += figure.GetLevel();
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
            return true;
        }
    }

    public List<int> getGlobalStates()
    {
        return globalStates;
    }


    private void Start()
    {
        for (int i = 0; i< (int)Figure.RecurseType.END_ENUM; i++)
        {
            //inicializar los arrays
            globalStates.Add(0);
            negativesModifiers.Add(0);
        }
    }
}
