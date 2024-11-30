using System.Collections.Generic;
using UnityEngine;
using static RandomDropper;
using static RandomEvents;

//se encarga de la gestion del ciclo de turnos
public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private int actionsPoints;

    private int currentPoints;

    private int currentRound = 0;

    [SerializeField]
    private int nFigures = 0;

    
    RecurseEventData currEventData;

    List<FigureDropData> currFiguresSet;

    List<RecurseEventData> currFixedEvents;

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

    private void debugNextTurn()
    {
        print("Enter in next turn");
        print($"Current Round: {currentRound}");

        

        print("Random event: Type:  " + currEventData.type +
            " cantidad " +currEventData.cantidad + " targetnodeID: " + 
            currEventData.targetNodeIndex);


        for (int i = 0; i < currFiguresSet.Count; i++)
        {
            print("Random droper "+ i + 
                " Type " + currFiguresSet[i].type + " Level " 
                +currFiguresSet[i].level + " WaitingTurns " + currFiguresSet[i].waitingTurns);

        }
       

        if (currFixedEvents == null)
        {
            print("No fixed events this turn");
        }
        else
        {
            for (int i = 0; i < currFixedEvents.Count; i++)
            {
                print("Fixed event: Type:  " 
                    + currFixedEvents[i].type +
                    " cantidad " +currFixedEvents[i].cantidad + 
                    " targetnodeID: " + currFixedEvents[i].targetNodeIndex);

            }

        }
    }

    void getInput()
    {
        currEventData = randomEvents.getRandomEvent();
        currFiguresSet = randomDropper.getFiguresSet(nFigures);
        currFixedEvents = fixedEvents.getFixedEvents(currentRound);
    }
    public void nextTurn()
    {
        currentRound++;
        getInput();
        
        debugNextTurn();

    }
    void Start()
    {
        initializeTurn();
    }

    void Update()
    {
        
    }
}
