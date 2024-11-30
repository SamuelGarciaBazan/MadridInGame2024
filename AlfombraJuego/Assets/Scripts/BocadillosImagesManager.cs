using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BocadillosImagesManager : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> images;


    public static BocadillosImagesManager Instance
    {
        get
        {
            return _instance;
        }
    }

    // Instancia estática privada
    private static BocadillosImagesManager _instance = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else Destroy(this);
    }
   
}
