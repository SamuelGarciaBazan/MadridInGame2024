using UnityEngine;
using UnityEngine.EventSystems;

public class PizarraTooltipDetector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    GameObject toolTip = null;


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toolTip == null) return;
        toolTip.SetActive(true);
        toolTip.GetComponent<NodeUIManager>().updateUI(transform.parent.GetComponent<NodeUIManager>().lastList);
        //update info

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (toolTip == null) return;
        toolTip.SetActive(false);

    }


}
