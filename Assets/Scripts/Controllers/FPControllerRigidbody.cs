using UnityEngine;

public class FPControllerRigidbody : MonoBehaviour
{
    public float movementSpeed = 4;
    public float rotationSpeed = 10;
    //El objeto que rota hacia arriba y abajo (camara, brazos, etc)
    public Transform pivot;
    public float jumpForce = 6;

    //Guarda la rotacion en el eje X que necesita el pivote para mirar arriba y abajo
    private float pivotRotationX;

    //Almacenar el input del jugador
    private Vector3 input;

    private Rigidbody rb;
    private GroundChecker groundChecker;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundChecker = GetComponent<GroundChecker>();
    }

    void Update()
    {
        //Guardar el valor de movimiento en los ejes X Z
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Hay que cambiar el vector de input para que sea relativo al transform del jugador y siempre
        //se mueva según hacia donde esta mirando y no relativo al mundo
        input = transform.TransformDirection(input);

        if (Input.GetButtonDown("Jump") && groundChecker.IsGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }

        //Rotacion horizontal
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0));

        //Al valor actual de rotacion le sumamos o restamos segun si subimos o bajamos el raton
        pivotRotationX -= Input.GetAxisRaw("Mouse Y") * rotationSpeed;

        //Limitar el valor de la rotacion para que siempre este entre los valores -90 y 90
        pivotRotationX = Mathf.Clamp(pivotRotationX, -90, 90);

        //Actualizar los angulos de rotacion del pivote para que su rotacion en X sea la que hemos calculado con el raton
        pivot.localEulerAngles = new Vector3(pivotRotationX, 0, 0);
    }

    private void FixedUpdate()
    {
        Vector3 movement = input.normalized * movementSpeed;
        //Mantener la velocity Y original para no reventar la gravedad
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }
}
