using System;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
   
    //devuelve un evento random que ocurrira en el juego
    public Tuple<Figure.RecurseType,int> getRandomEvent()
    {
        //recurso , cantidad
        Tuple<Figure.RecurseType, int> tuple = new Tuple<Figure.RecurseType, int>(Figure.RecurseType.END_ENUM, 0);


        return tuple;
    }


}
