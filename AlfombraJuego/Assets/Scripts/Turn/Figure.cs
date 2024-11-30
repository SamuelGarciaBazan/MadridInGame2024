using UnityEngine;

public class Figure : MonoBehaviour
{
    [SerializeField] Transform visuals;

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
    int level = 0;

    [SerializeField]
    int maxLevel = 1;

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

    public void UpgradeLevel() 
    {
        if(level < maxLevel) {
            level++;
            SetVisualBasedOnLevel();
        }

    }

    private void SetVisualBasedOnLevel() {
        for(int i = 0; i < visuals.childCount; i++) {
            if (i == level) visuals.GetChild(i).gameObject.SetActive(true);
            else visuals.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Start() {
        SetVisualBasedOnLevel();
    }

}
