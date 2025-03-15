using UnityEngine;

public class BoundCollisionChecker : MonoBehaviour
{
    public delegate void CollisionEvent();
    public event CollisionEvent OnCollisionUnbound;

    public BoundStats stats;

    void OnCollisionEnter2D(Collision2D collision)
    {

        //if(collision.relativeVelocity)

        Debug.Log(collision.relativeVelocity);

    }

    public void CheckUnbound(float collisionVelocity){

        if(collisionVelocity > stats.MaxBreakingForce){

            OnCollisionUnbound?.Invoke();

        }

    }

}
