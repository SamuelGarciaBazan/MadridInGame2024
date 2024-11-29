using UnityEngine;
using System.Collections.Generic;

public class Nodo : MonoBehaviour
{

    //estados finales 
    List<int> globalStates = new List<int>((int)Figure.RecurseType.END_ENUM);

    //puede cambiar el tipo
    List<int> negativesModifiers = new List<int>((int)Figure.RecurseType.END_ENUM);

    //figura que está colocada, cambiar por enum
    Figure figure = null;


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

  
}
