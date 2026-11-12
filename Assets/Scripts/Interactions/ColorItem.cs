using UnityEngine;

//Crear un enum para los posibles colores de los objetos
public enum ColorVariant
{
    Cigar, UnityPink, Chorizo
}

public class ColorItem : ItemPickup
{
    public ColorVariant colorVariant;

    //Para cuando se coloca encima de un pedestal
    public void Place(Vector3 placePosition)
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = placePosition;
    }

    public override void Throw(Vector3 force)
    {
        base.Throw(force);
    }
}
