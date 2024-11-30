using UnityEngine;
using UnityEngine.UI;

public class NodoUIBocadillo : MonoBehaviour
{
    Image bocadilloImage; 
    
    private BocadillosImagesManager imagesManager;

    private void OnEnable()
    {
        bocadilloImage = GetComponent<Image>();
        imagesManager = BocadillosImagesManager.Instance;
    }
    //private void Start()
    //{
    //    bocadilloImage = GetComponent<Image>();
    //    imagesManager = BocadillosImagesManager.Instance;
    //}

    public void setBocadillo(Figure.RecurseType type)
    {
        bocadilloImage.sprite = imagesManager.images[(int)type];
    }
}
