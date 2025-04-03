using System;
using UnityEngine;
using static BoundManager;

public class BoundCollisionChecker : MonoBehaviour
{

    public event BoundUnboundEvent onUnbound;
    public float BreakingForce;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "Cells" && _isBreaking(collision.relativeVelocity.magnitude)){

            onUnbound?.Invoke(this.gameObject);

        }

    }

    private bool _isBreaking(float currentForce){

        throw new NotImplementedException();

    }

}
