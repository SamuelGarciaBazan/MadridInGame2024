using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] TurnManager tm;
    [SerializeField] GameObject buttonNextTurn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimHalf() {
        print("Evento animación!!");
        tm.AnimHalfWay();
    }

    public void AnimFull() {
        buttonNextTurn.SetActive(true);
    }
}
