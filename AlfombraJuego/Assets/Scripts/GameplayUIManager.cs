using TMPro;
using UnityEngine;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text acciones;
    [SerializeField]
    TMP_Text turno;


    TurnManager turnManager;

    public void updateUI()
    {
        acciones.text = "Acciones: " +turnManager.CurrentActions;
        turno.text = "Turno " +turnManager.CurrentRound;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turnManager = FindAnyObjectByType<TurnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }
}
