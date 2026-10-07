using UnityEngine;

public class StaticCameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
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
        transform.rotation = Quaternion.LookRotation(input);
    }

    private void FixedUpdate()
    {
        Vector3 movement = input.normalized * moveSpeed;
        //Mantener la velocity Y original para no reventar la gravedad
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }
}
