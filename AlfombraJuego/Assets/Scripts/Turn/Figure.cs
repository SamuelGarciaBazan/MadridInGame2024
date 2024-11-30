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

    public enum FigurePlacement 
    {
        TREN,
        MANO,
        NODO
    }

    [SerializeField]
    int level = 1;

    [SerializeField]
    RecurseType type;


    [SerializeField]
    FigurePlacement placement;

    bool asigned = false;
    
    public RecurseType GetRecurseType() { 
        return type; 
    }

    public FigurePlacement GetFigurePlacement() {
        return placement;
    }

    public void SetFigurePlacement(FigurePlacement p) {
        placement = p;
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
