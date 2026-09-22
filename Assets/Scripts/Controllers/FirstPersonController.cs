using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float movementSpeed = 4;

    void Start()
    {
        
    }

    void Update()
    {
        //Guardar el valor de movimiento en los ejes X Z
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += input * movementSpeed * Time.deltaTime;
    }
}
