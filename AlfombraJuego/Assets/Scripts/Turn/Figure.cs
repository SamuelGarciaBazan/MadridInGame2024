using UnityEngine;

public class Figure : MonoBehaviour
{

    //enum de los tipos
    public enum RecurseType
    {
        SEGURIDAD,
        OCIO,
        SANIDAD,
        ZONAS_VERDES,
        END_ENUM
    }
    [SerializeField]
    int level = 1;
    [SerializeField]
    RecurseType type;
    int level = 1;
    bool asigned = false;
    
    public RecurseType GetRecurseType() { 
        return type; 
    }



}
