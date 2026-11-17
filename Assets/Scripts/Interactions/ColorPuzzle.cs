using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ColorPuzzle : MonoBehaviour
{
    //Todos los pedestales que haya que completar para superar el puzzle
    [SerializeField] private ColorPedestal[] pedestals;

    //Evento para que pasen cositas cuando se complete el puzzle
    [SerializeField] private UnityEvent onPuzzleComplete;

    private bool isComplete;

    //Cuántos pedestales están ya correctos
    private int pedestalsCompleted = 0;

    void Start()
    {
        //Asigna a cada pedestal este puzzle
        foreach (ColorPedestal pedestal in pedestals)
        {
            pedestal.assignedPuzzle = this;
        }
        //StartCoroutine(CheckPedestalsCrt());
    }

    public void CompletePedestal()
    {
        if (isComplete == true) return;

        //Añade un pedestal correcto
        pedestalsCompleted++;
        //Si hay la misma cantidad de pedestales correctos que pedestales en total, el puzzle
        //está completo :DDD
        if(pedestalsCompleted == pedestals.Length)
        {
            isComplete = true;
            //Llamar al evento de puzzle completado
            onPuzzleComplete?.Invoke();
        }
    }

    public void RemovePedestal()
    {
        if (isComplete == true) return;

        pedestalsCompleted--;
        //Impedir que haya un valor negativo de pedestales completos
        pedestalsCompleted = pedestalsCompleted < 0 ? 0 : pedestalsCompleted;
    }

    IEnumerator CheckPedestalsCrt()
    {
        while(isComplete == false)
        {
            CheckPedestals();
            yield return new WaitForSeconds(0.2f);
        }
    }

    void CheckPedestals()
    {
        //Contador de cuántos pedestales están correctos
        int completedPedestals = 0;
        //Comprobar si todos los pedestales están correctos
        foreach (ColorPedestal pedestal in pedestals)
        {
            //Si el pedestal está correcto, suma 1 al contador
            if (pedestal.isComplete == true)
            {
                completedPedestals++;
            }
            //En cuanto encuentra un pedestal que NO esté correcto, no sigue comprobando
            else break;
        }
        //Si el contador de pedestales correcto coincide con el total de pedestales
        //significa que todo están correctos :DDD
        if(completedPedestals == pedestals.Length)
        {
            isComplete = true;
            //Llamar al evento de puzzle completado
            onPuzzleComplete?.Invoke();
        }
    }
}
