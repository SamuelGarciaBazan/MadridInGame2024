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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(actualText == 4 && caja.childCount > 0) {
            tutoBg.SetActive(true);
        }
    }

    public void NextText() {
        actualText++;
        tutoText.text = textosTuto[actualText];

        if(actualText == 4) {
            tutoBg.SetActive(false);
            // corrutine arrastrar figure de tren a caja
        }

        if (actualText == 5) {
            tutoBg.SetActive(false);
            // corrutine arrastrar figure de caja a barrio
        }
    }

    
}
