using UnityEngine;
using UnityEngine.EventSystems;

public class PizarraManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    TurnManager turnManager;    

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
