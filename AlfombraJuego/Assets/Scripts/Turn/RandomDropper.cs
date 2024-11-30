using System;
using System.Collections.Generic;
using System.Linq;
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

    [System.Serializable]
    public class RandomDropperConfig
    {
        //probabilidad para cada tipo, END_ENUM = carreteras, DEBEN SUMAR 100
        public List<int> typeChances = new List<int>((int)Figure.RecurseType.END_ENUM+1);

        //niveles de los objetos
        public List<int> levels = new List<int>();

        //probabilidad para cada nivel, DEBEN SUMAR 100
        public List<int> levelsChances = new List<int>();


        //lista de rondas que tendrán si o si una carretera 
        public List<int> roundsWithRoad = new List<int>();

    }

    [SerializeField]
    int nFigures ; //numero de figuras a elegir


    [SerializeField]
    RandomDropperConfig config;

    //devuelve un set de figuras para elegir en esta ronda
    private List<FigureDropData> getFiguresSet(int currRound,int nDrops)
    {
        List<FigureDropData> figureDropDatas = new List<FigureDropData>();


        bool road = false;

        for(int i = 0; i < config.roundsWithRoad.Count; i++)
        {
            if(currRound == i)
            {
                road = true;
                break;
            }
        }

        //rellenar la lista
        for (int i = 0; i < nDrops; i++)
        {
            FigureDropData data = new FigureDropData();

            //de momento no hay esperas
            data.waitingTurns = 0;



            //añadir la carretera si toca
            if (road && i == 0)
            {
                data.type = Figure.RecurseType.END_ENUM;

                figureDropDatas.Add(data);

                continue;
            }

            //si no hay carretera, calculamos de forma aleatoria

            int rnd = random.Next(1, 101);

            //elegir el tipo del objeto de forma aleatoria
            data.type = Figure.RecurseType.END_ENUM;

            for (int j = 0; j < config.typeChances.Count; j++)
            {
                if (rnd <= config.typeChances[j])
                {
                    data.type = (RecurseType)j;
                    break;
                }
                else rnd -= config.typeChances[j];
            }

            rnd = random.Next(1, 101);

            for(int j = 0; j < config.levelsChances.Count; j++)
            {
                if (rnd <= config.levelsChances[j])
                {
                    data.level = config.levels[j];
                    break;
                }
                else rnd -= config.levelsChances[j];
            }

           
            figureDropDatas.Add(data);

        }


        return figureDropDatas;
    }

    public List<FigureDropData> getFiguresSet(int currRound)
    {
        return getFiguresSet(currRound,transforms.Count);
    }

    public List<FigureDropData> generateFigures(int currRound)
    {
        // Primero borramos las figuras ya generadas
        deleteFigures();


        print("1");
        // Generamos una nueva lista de figuras
        List<FigureDropData> listaFiguras = getFiguresSet(currRound,nFigures);

        print(listaFiguras.Count);


        // Instanciamos la nueva lista
        for (int i = 0; i < listaFiguras.Count; i++)
        {
            //print((int)listaFiguras[i].type);


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
