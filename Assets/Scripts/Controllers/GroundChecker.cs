using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded = false;

    //Tamaño de la caja que detecta el suelo
    [SerializeField] private Vector3 boxSize = new Vector3(0.25f, 0.1f, 0.25f);

    //Donde se va a crear la caja que detecta el suelo
    [SerializeField] private Vector3 offset = new Vector3(0, -0.5f, 0);

    //Capas que se consideran suelo y que detectara la caja
    [SerializeField] private LayerMask groundLayers;


    void Update()
    {
        //Crear una caja que detecta si hay algun collider en esa capa
        //Como la funcion devuelve un bool, lo igualamos directamente a la variable IsGrounded
        IsGrounded = Physics.CheckBox(transform.position + offset, boxSize * 0.5f, Quaternion.identity, groundLayers);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.papayaWhip;
        //Dibujar la caja que detecta el suelo
        Gizmos.DrawWireCube(transform.position + offset, boxSize);
    }
}
