using System.Collections;
using UnityEngine;

public class ColorPedestal : MonoBehaviour
{
    public ColorVariant colorVariant;
    //Cuando coincida el color del objeto con el del pedestal se marca como completado
    public bool isComplete = false;
    //Tamaño del Overlap que detecta si tiene un objeto encima
    [SerializeField] private Vector3 checkBoxSize = new Vector3(0.35f, 0.15f, 0.35f);
    //Donde se crea la caja que detecta los objetos encima
    [SerializeField] private Vector3 checkBoxPosition = Vector3.up;
    [SerializeField] private LayerMask itemLayers;
    //El objeto que se le haya puesto encima (si lo hay)
    private ColorItem itemOnTop;

    void Start()
    {
        StartCoroutine(DetectCrt());
    }

    //En vez usar un Update, se llama a la función de detectar cada poco tiempo para ahorrar
    IEnumerator DetectCrt()
    {
        while (true)
        {
            DetectItems();
            yield return new WaitForSeconds(0.1f);
        }
    }

    void DetectItems()
    {
        //Crea un OverlapBox encima para detectar si hay algún objeto colocado
        Collider[] detectedItems = Physics.OverlapBox(transform.position + checkBoxPosition,
            checkBoxSize * 0.5f, transform.rotation, itemLayers);
        //Si detecta algo y NO TIENE YA un objeto encima, lo guarda
        if (detectedItems.Length > 0)
        {
            if(itemOnTop == null)
            {
                itemOnTop = detectedItems[0].GetComponent<ColorItem>();
                //Comprueba si el colorVariant del objeto y el de este pedestal son iguales
                //Si coinciden, el pedestal está completo
                isComplete = itemOnTop.colorVariant == colorVariant;
                //Mover el objeto colocado
                itemOnTop.Place(transform.position + checkBoxPosition);
            }
        }
        //Si no detecta nada pero tenía un objeto encima, es porque se ha quitado
        else if(itemOnTop != null)
        {
            itemOnTop = null;
            isComplete = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blanchedAlmond;
        Gizmos.DrawWireCube(transform.position + checkBoxPosition, checkBoxSize);
    }
}
