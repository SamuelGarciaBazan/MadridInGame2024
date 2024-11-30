using System;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using static Figure;

public class RandomDropper : MonoBehaviour  
{
    public class FigureDropData
    {
        public Figure.RecurseType type;
        public int level;
        public int waitingTurns;
    }

    static System.Random random = new System.Random();

    //parametros publicos para editar las probabilidades
    [SerializeField]
    List<Transform> transforms = new List<Transform>();

    [SerializeField]
    List<GameObject> firgurePrefabs = new List<GameObject>();

    //devuelve un set de figuras para elegir en esta ronda
    private List<FigureDropData> getFiguresSet(int nDrops)
    {
        List<FigureDropData> figureDropDatas = new List<FigureDropData>();

        //rellenar la lista
        for (int i = 0; i < nDrops; i++)
        {
            FigureDropData data = new FigureDropData();


            data.type = (Figure.RecurseType)random.Next(0,(int)Figure.RecurseType.END_ENUM+1);

            if (data.type == Figure.RecurseType.END_ENUM)
            {
                figureDropDatas.Add(data);

                continue;
            }

            data.level = random.Next(1,3);//devuelve 1 o 2

            if (data.level == 2) {
                data.waitingTurns = 1;
            }
            else {
                data.waitingTurns = 0;
            }
            figureDropDatas.Add(data);

        }


        return figureDropDatas;
    }

    public List<FigureDropData> getFiguresSet()
    {
        return getFiguresSet(transforms.Count);
    }

    public List<FigureDropData> generateFigures()
    {
        // Primero borramos las figuras ya generadas
        deleteFigures();

        // Generamos una nueva lista de figuras
        List<FigureDropData> listaFiguras = getFiguresSet(transforms.Count);

        // Instanciamos la nueva lista
        for (int i = 0; i < listaFiguras.Count; i++)
        {
            GameObject aux = Instantiate(firgurePrefabs[(int)listaFiguras[i].type], transforms[i]);
            aux.GetComponent<Figure>().setFigure(listaFiguras[i]);
        }
        return listaFiguras;
    }


    public void deleteFigures() {
        for(int i = 0; i < transforms.Count; i++) {
            if(transforms[i].childCount > 0) {
                Destroy(transforms[i].GetChild(0).gameObject);
            }
        }
    }

    private void Start()
    {

    }
}
