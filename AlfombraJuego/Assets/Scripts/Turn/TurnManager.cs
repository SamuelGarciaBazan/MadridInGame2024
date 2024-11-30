using UnityEngine;

//se encarga de la gestion del ciclo de turnos
public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private int actionsPoints;

    private int currentPoints;


    RandomDropper randomDropper;
    RandomEvents randomEvents;
    FixedEvents fixedEvents;

    public int getCurrentPoints()
    {
        return currentPoints;
    }

    public void spendPoints(int n = 1) {
    
    
    }

    void initializeTurn()
    {
        currentPoints = actionsPoints;

    }

    public void nextTurn()
    {
        print("next turn");
    }
    void Start()
    {
        initializeTurn();
    }

    void Update()
    {
        
    }
}
