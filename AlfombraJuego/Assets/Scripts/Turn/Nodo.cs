using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Nodo : MonoBehaviour
{

    //estados finales 
    List<int> globalStates = new List<int>((int)Figure.RecurseType.END_ENUM);

    //puede cambiar el tipo

    //los valores estan en negativo
    List<int> negativesModifiers = new List<int>((int)Figure.RecurseType.END_ENUM);

    //figura que está colocada, cambiar por enum
    Figure figure = null;


    public void addNegativeEffect(Figure.RecurseType type,int amount = 1)
    {
        negativesModifiers[(int)type] += amount;    
    }

    //para setear una figura
    public bool setFigure(Figure fig)
    {
        //si esta vacio
        if (figure == null)
        {
            Debug.Log("Colocado");
            figure = fig;
            return true;
        }
        else if (figure.GetRecurseType() != fig.GetRecurseType()) //tipos distintos no hacer nada
        {
            Debug.Log("No valido");

            return false;
        }
        else { //tipos iguales,actualizar modelo y sumar nivel
            //TODO:upgrade


            Debug.Log("Mejorado");
            Destroy(fig.gameObject);
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
