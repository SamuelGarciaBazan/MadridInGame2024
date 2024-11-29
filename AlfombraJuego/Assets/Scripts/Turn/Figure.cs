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

    int level = 1;
    RecurseType type;

    public RecurseType GetRecurseType() { 
        return type; 
    }



}
