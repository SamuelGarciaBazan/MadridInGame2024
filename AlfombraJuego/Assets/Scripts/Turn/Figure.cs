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
        NODO,
        CARRETERA
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

    [SerializeField]
    int waitingTurns = 0;

    public int getWaitingTurns()
    {
        return waitingTurns;
    }
    
    public void advanceTurn(int nturns = 1)
    {
        waitingTurns -= nturns;
    }

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
        waitingTurns = data.waitingTurns;
    }

    public int GetLevel()
    {
        return level;
    }

    public bool UpgradeLevel() 
    {
        if(level < maxLevel) {
            level++;
            SetVisualBasedOnLevel();
            return true;
        }
        return false;
    }

    private void SetVisualBasedOnLevel() {

        if(visuals == null)
        {
            print("No hay referencia a contenedor de visuales ");
            return;
        }

        for(int i = 0; i < visuals.childCount; i++) {
            if (i == level) visuals.GetChild(i).gameObject.SetActive(true);
            else visuals.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Start() {
        SetVisualBasedOnLevel();
    }

}
