using UnityEngine;
using UnityEngine.EventSystems;

public class PizarraTooltipDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject toolTip;


    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.SetActive(true);
        toolTip.GetComponent<NodeUIManager>().updateUI(transform.parent.GetComponent<NodeUIManager>().lastList);
        //update info

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);

    }


}
