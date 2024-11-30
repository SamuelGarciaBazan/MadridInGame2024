using System.Collections.Generic;
using UnityEngine;
using static RandomEvents;

public class FixedEvents : MonoBehaviour
{
    static System.Random random = new System.Random();

    [System.Serializable]
    public struct FixedEventInfo
    {
        public int roundTime;
        public List<RecurseEventData> fixedEvents;
    }

    [SerializeField]
    public List<FixedEventInfo> fixedEventsList;

    public List<RecurseEventData> getFixedEvents(int roundTime)
    {
        //si hay una lista con ese indice, se devuelve la sublista
        for(int i = 0; i < fixedEventsList.Count; i++)
        {
            if(fixedEventsList[i].roundTime == roundTime) return fixedEventsList[i].fixedEvents;
        }
        //sino devolvemos null
        return null;
    }


}
