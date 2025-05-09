using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Direction;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D targetRB;
    public float forceMultiplier;
    public float spinSpeed;
    public float maxAngularVelocity;

    private Transform targetTransform;
    private float prevAngle;

    private void Start(){
        
        if(targetRB == null){

            new ArgumentNullException(targetRB.name);
            return;

        }

        targetTransform = targetRB.transform;

    }

    
    public void Move(List<Direction> directions, float speed){

        Vector2 moveVector = DirectionConverter.TransformDirectionsToVector2(targetTransform, directions);

        targetRB.AddForce(moveVector.normalized * Time.deltaTime * speed * forceMultiplier);
        
    }


    public void Rotate(Vector3 mousePos){

        //https://discussions.unity.com/t/rotate-object-weapon-towards-mouse-cursor-2d/1172

        Vector3 mouse_pos = mousePos;
        mouse_pos.z = Camera.main.transform.position.z; 
        Vector3 object_pos = Camera.main.WorldToScreenPoint(targetTransform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;

        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;

        float deltaAngle = angle - prevAngle;
      
        targetRB.AddTorque(Mathf.Sign(deltaAngle) * spinSpeed, ForceMode2D.Impulse);

        if (targetRB.angularVelocity < -maxAngularVelocity) { targetRB.angularVelocity = -maxAngularVelocity; }
        if (targetRB.angularVelocity > maxAngularVelocity) { targetRB.angularVelocity = maxAngularVelocity; }

        prevAngle = angle;

    }

}
