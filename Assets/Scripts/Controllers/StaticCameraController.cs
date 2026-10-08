using UnityEngine;

public class StaticCameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    private Vector3 input;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Hacer que el movimiento sea relativo a la cámara y a lo que está viendo
        input = Camera.main.transform.TransformDirection(input);
        //LookRotation calcula la rotacion necesaria para mirar hacia una direccion concreta
        //En este caso, queremos que el personaje rote para mirar en la direccion en la que se mueve (input)
        //Solo actualiza la rotacion si el personaje se esta moviendo
        if (input.sqrMagnitude > 0)
        {
            //Guardar la rotacion como un vector con los valores en angulos en vez de como quaternion
            Vector3 lookRotation = Quaternion.LookRotation(input).eulerAngles;
            //Eliminar la rotacion en el eje X
            lookRotation.x = 0;
            //Interpolar la rotacion actual con la rotacion calculada para mirar hacia donde se mueve
            //Como hemos guardado la rotacion en angulos como vector, hay que transformarlo en Quaterion con la funcion Euler
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(lookRotation), Time.deltaTime * rotationSpeed);
        }

    }

    private void FixedUpdate()
    {
        Vector3 movement = input.normalized * moveSpeed;
        //Mantener la velocity Y original para no reventar la gravedad
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }
}
