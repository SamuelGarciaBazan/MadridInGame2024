using System;
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

    static System.Random random = new System.Random();

    [SerializeField]
    TurnManager turnManager;

    //devuelve un evento random que ocurrira en el juego
    public RecurseEventData getRandomEvent()
    {
        RecurseEventData data = new RecurseEventData();

        data.type = (Figure.RecurseType)random.Next(0, (int)Figure.RecurseType.END_ENUM);
        data.cantidad = random.Next(1, 3); //random de 1-2
        data.targetNodeIndex = random.Next(0, turnManager.getNodosCount());

        return data;
    }


}
