using UnityEngine;
using System.Collections.Generic;

public class Nodo : MonoBehaviour
{

    //estados finales 
    List<int> globalStates = new List<int>((int)Figure.RecurseType.END_ENUM);

    //puede cambiar el tipo

    //los valores estan en negativo
    List<int> negativesModifiers = new List<int>((int)Figure.RecurseType.END_ENUM);

    //figura que est� colocada, cambiar por enum
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
            figure = fig;
            return true;
        }
        else if (figure.GetRecurseType() != fig.GetRecurseType()) //tipos distintos no hacer nada
        {


            return false;
        }
        else { //tipos iguales,actualizar modelo y sumar nivel


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
            globalStates.Add(0);
            negativesModifiers.Add(0);

        }
    }
}