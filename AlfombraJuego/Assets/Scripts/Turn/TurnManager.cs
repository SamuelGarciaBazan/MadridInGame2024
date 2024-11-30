using UnityEngine;

//se encarga de la gestion del ciclo de turnos
public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private int actionsPoints;

    private int currentPoints;

    private int currentRound = 0;

    [SerializeField]
    private int nFigures = 0;



    [SerializeField]
    RandomDropper randomDropper;

    [SerializeField]
    RandomEvents randomEvents;

    [SerializeField]
    FixedEvents fixedEvents;

    [SerializeField]
    NodosManager nodosManager;

    public int getNodosCount()
    {
        return nodosManager.GetNodes().Count;
    }

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
        currentRound++;
        print("Enter in next turn");
        print($"Current Round: {currentRound}");

        var v1  = randomEvents.getRandomEvent();

        print("Random event: Type:  " + v1.type + " cantidad " +v1.cantidad + " targetnodeID: " + v1.targetNodeIndex);

        var v2 = randomDropper.getFiguresSet(nFigures);

        for (int i = 0; i < v2.Count; i++)
        {
            print("Random droper "+ i + " Type " + v2[i].type + " Level " +v2[i].level + " WaitingTurns " + v2[i].waitingTurns);

        }
        var v3 = fixedEvents.getFixedEvents(currentRound);

        if(v3 == null)
        {
            print("No fixed events this turn");
        }
        else
        {
            for(int i = 0;i < v3.Count; i++)
            {
                print("Fixed event: Type:  " + v3[i].type + " cantidad " +v3[i].cantidad + " targetnodeID: " + v3[i].targetNodeIndex);

            }

        }

    }
    void Start()
    {
        initializeTurn();
    }

    void Update()
    {
        
    }
}
