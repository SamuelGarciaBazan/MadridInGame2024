using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    string[] textosTuto;

    [SerializeField]
    int actualText = 0;

    [SerializeField]
    Text tutoText;

    [SerializeField]
    GameObject tutoBg;

    [SerializeField] Transform caja;
    [SerializeField] Transform[] nodes;
    [SerializeField] TurnManager turnManager;

    [SerializeField] GameObject nextTurnButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutoText.text = textosTuto[actualText];
        nextTurnButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(actualText == 5 && caja.childCount > 0) {
            tutoBg.SetActive(true);
            nextTurnButton.SetActive(false);
        }

        bool figureOnNode = false;
        foreach(Transform t in nodes) { if (t.childCount > 2) figureOnNode = true; }
        if(actualText == 6 && figureOnNode) {
            tutoBg.SetActive(true);
            nextTurnButton.SetActive(false);
        }

        if (turnManager.CurrentRound == 2 && actualText < 8) {
            tutoBg.SetActive(true);
            nextTurnButton.SetActive(false);
            actualText = 7;
            tutoText.text = textosTuto[actualText];
        }
        if (turnManager.CurrentRound == 3 && actualText < 9) {
            tutoBg.SetActive(true);
            nextTurnButton.SetActive(false);
            actualText = 8;
            tutoText.text = textosTuto[actualText];
        }

        if (turnManager.CurrentRound == 4 && actualText < 11) {
            tutoBg.SetActive(true);
            nextTurnButton.SetActive(false);
            actualText = 10;
            tutoText.text = textosTuto[actualText];
        }
    }

    public void NextText() {
        actualText++;
        if(actualText == 12) {
            tutoBg.SetActive(false);
            nextTurnButton.SetActive(true);
            return;
        }
        tutoText.text = textosTuto[actualText];

        if(actualText == 5) {
            tutoBg.SetActive(false);
            nextTurnButton.SetActive(true);
        }

        if (actualText == 6) {
            tutoBg.SetActive(false);
            nextTurnButton.SetActive(true);
        }

        if (actualText == 7) {
            tutoBg.SetActive(false);
            nextTurnButton.SetActive(true);
        }

        if (actualText == 8) {
            tutoBg.SetActive(false);
            nextTurnButton.SetActive(true);
        }

        if (actualText == 10) {
            tutoBg.SetActive(false);
            nextTurnButton.SetActive(true);
        }
    }

    
}
