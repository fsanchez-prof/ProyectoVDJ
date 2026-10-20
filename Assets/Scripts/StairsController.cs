using UnityEngine;

public class StairsController : MonoBehaviour
{
    public float rayLength = .5f;
    public Transform rayOriginPoint;
    public bool OnStairs = false;
    public LayerMask stairsLayers;

    private FPControllerRigidbody controller;

    void Start()
    {
        TryGetComponent(out controller);
    }

    void Update()
    {
        if(controller.input.y < -0.01f && controller.groundChecker.IsGrounded == true)
        {
            StopClimb();
            return;
        }
        if (Physics.Raycast(rayOriginPoint.position, transform.forward, rayLength, stairsLayers))
        {
            BeginClimb();
        }
        else StopClimb();
    }

    void BeginClimb()
    {
        if (OnStairs == false)
        {
            OnStairs = true;
            controller.Climb(true);
        }
    }

    void StopClimb()
    {
        if (OnStairs == true)
        {
            OnStairs = false;
            controller.Climb(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayOriginPoint.position, transform.forward * rayLength);
    }
}
