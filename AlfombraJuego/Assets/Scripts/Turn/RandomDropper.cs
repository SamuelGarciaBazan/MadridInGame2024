using System;
using System.Collections.Generic;
using UnityEngine;
using static Figure;

public class RandomDropper : MonoBehaviour
{
    public struct FigureDropData
    {
        Figure.RecurseType type;
        int level;
        int waitingTurns;
    }

    //parametros publicos para editar las probabilidades


    //devuelve un set de figuras para elegir en esta ronda
    public List<FigureDropData> getFiguresSet(int nDrops)
    {
        List<FigureDropData> figureDropDatas = new List<FigureDropData>();

        //rellenar la lista
        for (int i = 0; i < nDrops; i++)
        {

        }


        return figureDropDatas;
    }

}
