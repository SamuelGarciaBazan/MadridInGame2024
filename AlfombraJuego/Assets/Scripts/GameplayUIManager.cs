using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text acciones;
    [SerializeField]
    TMP_Text turno;

    [SerializeField]
    GameObject buttonImage;

    TurnManager turnManager;

    public void updateUI()
    {
        acciones.text = "Acciones: " +turnManager.CurrentActions;
        turno.text = "Turno " +turnManager.CurrentRound;
        if(turnManager.CurrentActions == 0)
        {
            buttonImage.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            buttonImage.GetComponent<Image>().color = Color.white;
        }
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
