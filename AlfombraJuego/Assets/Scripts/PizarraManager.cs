using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class PizarraManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    TurnManager turnManager;

    public TextMeshProUGUI textoDias;


    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Mouse sobre la imagen");
        // Puedes cambiar el color, sprite, etc.
        //GetComponent<UnityEngine.UI.Image>().color = Color.red;

        turnManager.activaBocadillosFixed();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Mouse fuera de la imagen");
        // Restaurar el color o realizar otra acción
        //GetComponent<UnityEngine.UI.Image>().color = Color.white;
        turnManager.desacticaBocadillosFixed();
    }

    // Update is called once per frame
    void Update()
    {
        textoDias.text = $"Dia {turnManager.getCurrentRound()} / {turnManager.getFirstFixedEventRound()}";
    }


        
}
