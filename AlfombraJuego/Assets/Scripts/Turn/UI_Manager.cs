using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textActions;

    [SerializeField]
    private TurnManager turnManager;


    void Update()
    {
        textActions.text = "Actions: " + turnManager.getCurrentPoints();
    }
}
