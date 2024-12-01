using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelctionScript : MonoBehaviour
{
    [SerializeField]
    Transform reference;
    [SerializeField]
    float heith=10;

    [SerializeField]
    TurnManager turnManager;
    
    Transform objectReference;
    Vector3 preDragPosition;


    int state = 0;

    NodosManager nodeManager;
    CarreterasManager carreterasManager;

    [SerializeField]
    Outline mallaCajaConOutliner;

    Nodo n1;
    Nodo n2;

    [SerializeField]
    float scaleMultiplier;

    Vector3 previousScale;


    private void ProccesDragable(ref Ray camray)
    {
        RaycastHit hit;
        if(Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Drageable")))
        {
            SoundManager.THIS.PlaySoundByIndex(4);
            preDragPosition = hit.transform.position;
            objectReference = hit.transform;
            reference.gameObject.SetActive(true);
            objectReference.GetComponent<Rigidbody>().isKinematic = true;
            //Debug.Log("TuViejaPick");


            //si es una carretera
            if (objectReference.GetComponent<Figure>().GetRecurseType() == Figure.RecurseType.END_ENUM)
            {
                carreterasManager.enableOutline(true);
            }
            else // si es un edificio
            {
                nodeManager.enableOutline(true);

                print(objectReference.GetChild(0).transform.localScale);
                //previousScale = transform.localScale;
                objectReference.GetChild(0).transform.localScale = objectReference.GetChild(0).transform.localScale * scaleMultiplier;
            }

            mallaCajaConOutliner.enabled = true;

        }
    }

    private void dropBuild(Ray camray)
    {
        RaycastHit hit;
        nodeManager.enableOutline(false);
        mallaCajaConOutliner.enabled = false;

        //restauramos la escala
        objectReference.GetChild(0).transform.localScale = objectReference.GetChild(0).transform.localScale / scaleMultiplier;

        // CASO 1 : DEL TREN A UN NODO
        if (Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Nodo")) &&    // Si se suelta en un nodo
            objectReference.GetComponent<Figure>().GetFigurePlacement() == Figure.FigurePlacement.TREN &&   // Si viene del tren
            turnManager.getCurrentPoints() >= 2 &&  // Si tenemos puntos para pasarlo de un tren a un nodo
            objectReference.GetComponent<Figure>().getWaitingTurns() <= 0 && //ver si la figure no tiene que esperar
            hit.transform.GetComponent<Nodo>().setFigure(objectReference.gameObject.GetComponent<Figure>())) // Si se puede colocar en el nodo
        {
            turnManager.spendPoints(2);


            //resulta que no coinciden centro y posicion del objeto
            Debug.Log(objectReference.GetComponent<Collider>().bounds.center);
            Debug.Log(objectReference.position);

            //reubicamos
            objectReference.position =
                hit.transform.position +
                new Vector3(0, hit.collider.bounds.extents.y, 0) +
                new Vector3(0, objectReference.GetComponent<Collider>().bounds.extents.y, 0);

            Debug.Log(objectReference.position);

            objectReference.GetComponent<Collider>().enabled = false;

            // Cambiamos el padre de la figura para que ya no dependa del tren
            objectReference.SetParent(hit.collider.transform);

            objectReference.GetComponent<Figure>().SetFigurePlacement(Figure.FigurePlacement.NODO);

        }
        // CASO 2 : DE LA MANO A UN NODO
        else if (Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Nodo")) &&    // Si se suelta en un nodo
            objectReference.GetComponent<Figure>().GetFigurePlacement() == Figure.FigurePlacement.MANO &&   // Si viene de la mano
            turnManager.getCurrentPoints() >= 1 && // Si tenemos puntos para pasarlo de la mano a un nodo 
              objectReference.GetComponent<Figure>().getWaitingTurns() <= 0 && //ver si la figure no tiene que esperar
            hit.transform.GetComponent<Nodo>().setFigure(objectReference.gameObject.GetComponent<Figure>())// Si se puede colocar en el nodo IMPORTANTE: ESTE VA ULTIMO!!!!
            )
        {
            turnManager.spendPoints(1);
            objectReference.position =
                hit.transform.position +
                new Vector3(0, hit.collider.bounds.extents.y, 0) +
                new Vector3(0, objectReference.GetComponent<Collider>().bounds.extents.y, 0);

            objectReference.GetComponent<Collider>().enabled = false;

            // Cambiamos el padre de la figura para que ya no dependa de la mano
            objectReference.SetParent(hit.collider.transform);

            objectReference.GetComponent<Figure>().SetFigurePlacement(Figure.FigurePlacement.NODO);
        }
        // CASO 3 : DEL TREN A LA MANO
        else if (Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Mano")) &&   // Si se suelta en la mano
            objectReference.GetComponent<Figure>().GetFigurePlacement() == Figure.FigurePlacement.TREN &&   // Si viene del tren
            turnManager.getCurrentPoints() >= 1 &&  // Si tenemos puntos para pasarlo del tren a la mano
            hit.transform.GetComponent<Mano>().AddFigure(objectReference.transform))    // Si se puede añadir la figura a la mano
        {
            turnManager.spendPoints(1);

            //objectReference.position =
            //    hit.transform.position +
            //    new Vector3(0, hit.collider.bounds.extents.y, 0) +
            //    new Vector3(0, objectReference.GetComponent<Collider>().bounds.extents.y, 0);

            objectReference.GetComponent<Figure>().SetFigurePlacement(Figure.FigurePlacement.MANO);

        }
        //CASO 4 : SE QUEDA DONDE ESTÁ
        else
        {
            objectReference.position = preDragPosition + new Vector3(0, heith, 0);
            objectReference.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void dropRoad(Ray camray)
    {
        RaycastHit hit;

        carreterasManager.enableOutline(false);
        mallaCajaConOutliner.enabled = false;


        // CASO 1 : DEL TREN A UNA CARRETERA
        if (Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Carretera")) &&    // Si se suelta en un nodo
            objectReference.GetComponent<Figure>().GetFigurePlacement() == Figure.FigurePlacement.TREN &&   // Si viene del tren
            turnManager.getCurrentPoints() >= 2 &&  // Si tenemos puntos para pasarlo de un tren a un nodo
            !hit.transform.GetComponent<Carretera>().isOcuped) // Si se puede colocar en la carretera
        {

            //actualizar cosillas
            hit.transform.GetComponent<Carretera>().isOcuped = true;

            nodeManager.ponArista(
                hit.transform.GetComponent<Carretera>().node1,
                hit.transform.GetComponent<Carretera>().node2);

            nodeManager.uptateAllNodesStats();

            turnManager.spendPoints(2);

            objectReference.GetComponent<Collider>().enabled = false;

            // Cambiamos el padre de la figura para que ya no dependa del tren
            objectReference.SetParent(hit.collider.transform);

            objectReference.GetComponent<Figure>().SetFigurePlacement(Figure.FigurePlacement.CARRETERA);

            objectReference.transform.localScale = new Vector3(1, 1, 1.22f);
            objectReference.localRotation = Quaternion.identity;

            objectReference.position = new Vector3(
                                    objectReference.position.x, 0.08f,
                                        objectReference.position.z);

        }
        // CASO 2 : DE LA MANO A UNA CARRETERA
        else if (Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Carretera")) &&    // Si se suelta en un nodo
            objectReference.GetComponent<Figure>().GetFigurePlacement() == Figure.FigurePlacement.MANO &&   // Si viene de la mano
            turnManager.getCurrentPoints() >= 1 && // Si tenemos puntos para pasarlo de la mano a un nodo 
            !hit.transform.GetComponent<Carretera>().isOcuped// Si se puede colocar en el nodo IMPORTANTE: ESTE VA ULTIMO!!!!
            )
        {

            //actualizar cosillas
            hit.transform.GetComponent<Carretera>().isOcuped = true;

            nodeManager.ponArista(
                hit.transform.GetComponent<Carretera>().node1,
                hit.transform.GetComponent<Carretera>().node2);

            nodeManager.uptateAllNodesStats();

            turnManager.spendPoints(1);

            objectReference.GetComponent<Collider>().enabled = false;

            // Cambiamos el padre de la figura para que ya no dependa de la mano
            objectReference.SetParent(hit.collider.transform);

            objectReference.GetComponent<Figure>().SetFigurePlacement(Figure.FigurePlacement.CARRETERA);

            objectReference.transform.localScale = new Vector3(1, 1, 1.22f);
            objectReference.localRotation = Quaternion.identity;
            objectReference.position = new Vector3(
                                    objectReference.position.x, 0.08f,
                                        objectReference.position.z);



        }
        // CASO 3 : DEL TREN A LA MANO
        else if (Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Mano")) &&   // Si se suelta en la mano
            objectReference.GetComponent<Figure>().GetFigurePlacement() == Figure.FigurePlacement.TREN &&   // Si viene del tren
            turnManager.getCurrentPoints() >= 1 &&  // Si tenemos puntos para pasarlo del tren a la mano
            hit.transform.GetComponent<Mano>().AddFigure(objectReference.transform))    // Si se puede añadir la figura a la mano
        {
            turnManager.spendPoints(1);
            objectReference.GetComponent<Figure>().SetFigurePlacement(Figure.FigurePlacement.MANO);
        }
        //CASO 4 : SE QUEDA DONDE ESTÁ
        else
        {
            objectReference.position = preDragPosition + new Vector3(0, heith, 0);
            objectReference.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    public void drag(InputAction.CallbackContext context)
    {
        Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(state == 0)
        {
            //click
            if (context.performed/*&& Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Drageable"))*/)
            {
                ProccesDragable(ref camray);
            }
            //release cuando sujetamos una ficha

            else if (context.canceled&&objectReference!=null)
            {
               
                //si no somos carretera
                if (objectReference.GetComponent<Figure>().GetRecurseType() != Figure.RecurseType.END_ENUM)
                {
                    dropBuild(camray);
                }
                else
                {
                    dropRoad(camray);
                    
                }
                objectReference = null;
                //Debug.Log("TuViejaDrop");
                reference.gameObject.SetActive(false);

            }
        }
        //selection(ref context,ref camray);
    }

    public void selection(ref InputAction.CallbackContext context, ref Ray camray)
    {
        //Ray camray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (context.performed && state == 0 && Physics.Raycast(camray, out hit, 1000) && hit.transform.GetComponent<ConnectionScript>() != null)
        {
            Debug.Log("On Conection Mode");
            n1 = null;
            n2 = null;
            state = 1;
        }
        else if (context.performed && state == 1 && Physics.Raycast(camray, out hit, 1000) && (n1 = hit.transform.GetComponent<Nodo>()) != null)
        {
            Debug.Log("Nodo 1 " + n1.name);
            state = 2;
        }
        else if (context.performed && state == 2 && Physics.Raycast(camray, out hit, 1000) && (n2 = hit.transform.GetComponent<Nodo>()) != null)
        {
            Debug.Log("Nodo 2 " + n2.name);
            state = 0;
            nodeManager.ponArista(n1, n2);
            nodeManager.uptateAllNodesStats();
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectReference = null;
        reference.gameObject.SetActive(false);
        nodeManager = FindAnyObjectByType<NodosManager>();
        carreterasManager = FindAnyObjectByType<CarreterasManager>();

        if (turnManager == null) print("¡¡¡¡¡BROOOO LA REFERENCIA DEL TURN MANAGEEEEEEER!!!!!");


        //desactivar outliners
        int nNodes = nodeManager.transform.childCount;
        for (int i = 0; i < nNodes; i++)
        {
            nodeManager.transform.GetChild(i).GetComponent<Outline>().enabled = false;
        }

        //desactivar outliners 
        nNodes = carreterasManager.transform.childCount;
        for (int i = 0; i < nNodes; i++)
        {
            carreterasManager.transform.GetChild(i).GetComponent<Outline>().enabled = false;
        }

        mallaCajaConOutliner.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Ray camray =Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit;
        if (Physics.Raycast(camray, out hit, 1000, LayerMask.GetMask("Floor")))
        {
            reference.position = hit.point;
            if(objectReference != null)
            {
                objectReference.transform.position = new Vector3(0, heith, 0) + hit.point;
            }
        }
    }
}
