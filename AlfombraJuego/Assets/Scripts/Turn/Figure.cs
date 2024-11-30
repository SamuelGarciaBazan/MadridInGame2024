using UnityEngine;

public class Figure : MonoBehaviour
{

    //enum de los tipos
    public enum RecurseType
    {
        SEGURIDAD,
        OCIO,
        SANIDAD,
        ZONAS_VERDES,
        END_ENUM
    }

    [SerializeField]
    int level = 1;

    [SerializeField]
    RecurseType type;

    bool asigned = false;
    
    public RecurseType GetRecurseType() { 
        return type; 
    }


    public void setFigure(RandomDropper.FigureDropData data)
    {
        level = data.level;
    }

    public int GetLevel()
    {
        return level;
    }



}
