using UnityEngine;

public class Figure : MonoBehaviour
{

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

    public RecurseType GetRecurseType() { 
        return type; 
    }



}
