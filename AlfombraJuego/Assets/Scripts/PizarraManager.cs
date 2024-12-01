using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class PizarraManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    TurnManager turnManager;

    public TextMeshProUGUI textoDias;

    [SerializeField]
    Color originalColor;

    [SerializeField]
    Color hoverColor;


    [SerializeField]
    Light dirLight;

    [SerializeField]
    Light spotLight1;

    [SerializeField]
    Light spotLight2;


   


       

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse sobre la imagen");
        // Puedes cambiar el color, sprite, etc.
        //GetComponent<UnityEngine.UI.Image>().color = Color.red;

        turnManager.activaBocadillosFixed();

        dirLight.color = hoverColor;
        spotLight1.color = hoverColor;
        spotLight2.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse fuera de la imagen");
        // Restaurar el color o realizar otra acción
        //GetComponent<UnityEngine.UI.Image>().color = Color.white;
        turnManager.desacticaBocadillosFixed();

        dirLight.color = originalColor;
        spotLight1.color = originalColor;
        spotLight2.color = originalColor;
    }

    // Update is called once per frame
    void Update()
    {
        textoDias.text = $"Dia {turnManager.getCurrentRound()} / {turnManager.getFirstFixedEventRound()}";
    }


        
}
