using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RandomDropper;
using static RandomEvents;

//se encarga de la gestion del ciclo de turnos
public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private int actionsPoints;

    private int currentPoints;

    public int CurrentActions {  get { return currentPoints; } }

    private int currentRound = 1;
    public int CurrentRound {  get { return currentRound; } }
    
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

    [SerializeField]
    Mano mano;

    [SerializeField] GameObject buttonNextTurn;
    [SerializeField] GameObject buttonFinishLevel;
    [SerializeField] GameObject gameOverPanel;


    [SerializeField]
    float bocadillosTime;

    public int getNodosCount()
    {
        return nodosManager.GetNodes().Count;
    }

    public int getCurrentPoints()
    {
        return currentPoints;
    }

    //devuelve true si se ha podido gastar
    public bool spendPoints(int n = 1) {
        
        if(n <= currentPoints)
        {
            currentPoints -= n;
            return true;
        }
        return false;
    }

    void initializeTurn()
    {
        currentPoints = actionsPoints;
        currFiguresSet =  randomDropper.generateFigures(currentRound);
    }

    private void debugFigureSet()
    {
        for (int i = 0; i < currFiguresSet.Count; i++)
        {
            print("Random droper "+ i +
                " Type " + currFiguresSet[i].type + " Level "
                +currFiguresSet[i].level + " WaitingTurns " + currFiguresSet[i].waitingTurns);

        }
    }
    private void debugNextTurn()
    {
        print("Enter in next turn");
        print($"Current Round: {currentRound}");

        

        print("Random event: Type:  " + currEventData.type +
            " cantidad " +currEventData.cantidad + " targetnodeID: " + 
            currEventData.targetNodeIndex);


       debugFigureSet();
       

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
        //currFiguresSet = randomDropper.getFiguresSet(nFigures);
        currFixedEvents = fixedEvents.getFixedEvents(currentRound);

        if(currFixedEvents != null) {
            buttonFinishLevel.SetActive(true);
            buttonNextTurn.SetActive(false);
        }
    }


    void applyRecurseEventData(RecurseEventData data)
    {
        if(data.type == Figure.RecurseType.END_ENUM)
        {
            print("tipo de evento invalido");
            return;
        }
        if(data.cantidad == -1)
        {
            print("cantidad de evento invalida");
            return;
        }
        if(data.targetNodeIndex == -1)
        {
            print("nodo objetivo de evento invalido");
            return;

        }
        SoundManager.THIS.PlaySoundByIndex((int)data.type);
        //switch (data.type)
        //{
        //    case Figure.RecurseType.SEGURIDAD:
                
        //        SoundManager.THIS.PlaySoundByIndex((int)data.type);
        //        break;
        //    case Figure.RecurseType.OCIO:
        //        break;
        //    case Figure.RecurseType.ZONAS_VERDES:
        //        break;
        //    case Figure.RecurseType.SANIDAD:
        //        break;
        //}

        nodosManager.GetNodes()[data.targetNodeIndex].addNegativeEffect(
            data.type, data.cantidad);
    }


    void applyVisualCurrEvent()
    {
        //nodosManager.GetNodes()[currEventData.targetNodeIndex].transform.GetChild(1).gameObject.SetActive(true);
        //nodosManager.GetNodes()[currEventData.targetNodeIndex].transform.GetChild(1).GetComponent<NodoUIBocadillo>().setBocadillo(currEventData.type);

        StartCoroutine(corrutinaBocadillo());
    }

    System.Collections.IEnumerator corrutinaBocadillo()
    {

        print("a");
        var obj = nodosManager.GetNodes()[currEventData.targetNodeIndex].transform.GetChild(1).gameObject;
        obj.SetActive(true);

        obj.GetComponent<NodoUIBocadillo>().setBocadillo(currEventData.type);


        yield return new WaitForSeconds(bocadillosTime);

        obj.SetActive(false);

        yield return null;
    }


    void applyLogicCurrEvent()
    {
       applyRecurseEventData(currEventData);  
    }

    void applyVisualCurrFigures()
    {

    }

    void applyLogicCurrFigures() 
    { 
        //vacio
    }
    void applyVisualCurrFixedEvents()
    {

    }
    void applyLogicCurrFixedEvents()
    {
        if (currFixedEvents == null) return;

        for(int i = 0; i < currFixedEvents.Count; i++)
        {
            applyRecurseEventData(currFixedEvents[i]);
        }
    }

    void checkWinDefeatConditions()
    {
        
    }

    public void nextTurn()
    {
        currentRound++;


        for (int i = 0; i < mano.transform.childCount; i++) {

            if(mano.transform.GetChild(i).childCount > 0)
            {
                Figure fig = mano.transform.GetChild(i).GetChild(0).GetComponent<Figure>();
                if (fig != null)
                {
                    fig.advanceTurn();
                    print("se avanza turno"); 
                }
            }

           
        }

        initializeTurn();
        getInput();       
        debugNextTurn();

        //aplicar los efectos visuales y de logica
        applyVisualCurrEvent();
        applyVisualCurrFigures();
        applyVisualCurrFixedEvents();

        applyLogicCurrEvent();
        applyLogicCurrFigures();
        applyLogicCurrFixedEvents();

        //importante
        nodosManager.uptateAllNodesStats();



    }


    public void FinishLevel() {
        checkWinDefeatConditions();
        gameOverPanel.SetActive(true);
    }

    public void activaBocadillosFixed()
    {
        print("activa");

        var lista = fixedEvents.getFirstFixedEvents();

        for (int i = 0; i< lista.Count; i++)
        {
            var data = lista[i];    

            var obj = nodosManager.GetNodes()[data.targetNodeIndex].transform.GetChild(1).gameObject;
            obj.SetActive(true);

            obj.GetComponent<NodoUIBocadillo>().setBocadillo(data.type);

            //obj.GetComponent<Image>().color =  Color.red;
            
        }
    }

    public void desacticaBocadillosFixed()
    {
        print("desactiva");
        var lista = fixedEvents.getFirstFixedEvents();

        for (int i = 0; i< lista.Count; i++)
        {
            var data = lista[i];    

            var obj = nodosManager.GetNodes()[data.targetNodeIndex].transform.GetChild(1).gameObject;
            obj.SetActive(false);


            //obj.GetComponent<Image>().color =  Color.red;
            
        }
    }

    void Start()
    {
        initializeTurn();
        debugFigureSet();
    }

}
