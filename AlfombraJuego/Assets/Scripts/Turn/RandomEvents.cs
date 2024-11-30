using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    [System.Serializable]
    public class RecurseEventData
    {
        public Figure.RecurseType type;
        public int cantidad;
        public int targetNodeIndex;
    }

    [System.Serializable]
    public class RandomEventConfig
    {
        //las probabilidades se aplican sobre 100, si no suman 100, no se aplicara evento siempre

        //probabiliad para cada tipo
        public List<int> typeChances = new List<int>((int)Figure.RecurseType.END_ENUM);

        //deben ser mayores que 1
        public List<int> cantidades = new List<int>();
        public List<int> cantidadesChances = new List<int>();


        public List<int> nodesChances = new List<int>();


        //si los booleanos estan activados, las correspondientes listas se ignoran
        public bool typeChancesUniform = false;

        public int cantidadesUniform = 0;
        public bool cantidadesChancesUniform = false;

        public bool nodesChancesUniform = false;
    }


    static System.Random random = new System.Random();

    [SerializeField]
    TurnManager turnManager;

    [SerializeField]
    RandomEventConfig config;



    //devuelve un evento random que ocurrira en el juego
    public RecurseEventData getRandomEvent()
    {
        RecurseEventData data = new RecurseEventData();

        //print("1");

        if (config.typeChancesUniform)
        {
            //print("2");

            data.type = (Figure.RecurseType)random.Next(0, (int)Figure.RecurseType.END_ENUM);
        }
        else
        {
            //print("3");

            int rnd = random.Next(1, 101);

            data.type = Figure.RecurseType.END_ENUM;

            for(int i = 0;i < config.typeChances.Count; i++)
            {
                if (rnd <= config.typeChances[i])
                {
                    data.type = (Figure.RecurseType)i;
                    //print("i: " + i);
                    break;
                }
                else rnd -= config.typeChances[i];
            }


        }

        if (config.cantidadesChancesUniform)
        {
            data.cantidad = random.Next(1, config.cantidadesUniform +1); //random de 1- cantidadesUniform
        }
        else 
        {
            int rnd = random.Next(1, 101);

            data.cantidad = -1;

            for (int i = 0; i < config.cantidadesChances.Count; i++)
            {
                if (rnd <= config.cantidadesChances[i])
                {
                    data.cantidad = config.cantidades[i];
                    break;
                }
                else rnd -= config.cantidadesChances[i];

            }
            print(data.cantidad);
        }

        if (config.nodesChancesUniform)
        {
            data.targetNodeIndex = random.Next(0, turnManager.getNodosCount());
        }
        else
        {
            int rnd = random.Next(1, 101);

            data.targetNodeIndex = -1;

            for (int i = 0; i < config.nodesChances.Count; i++)
            {
                if (rnd <= config.nodesChances[i])
                {
                    data.targetNodeIndex = i;
                    break;
                }
                else rnd -= config.nodesChances[i];
            }
        }

        return data;
    }

  
}
