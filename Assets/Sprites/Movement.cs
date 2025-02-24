using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Direction;

public class Movement : MonoBehaviour
{
    public Rigidbody2D targetRB;
    private Transform targetTransform;

    private void Start() {
        
        if(targetRB == null){

            new ArgumentNullException(targetRB.name);
            return;

        }

        targetTransform = targetRB.transform;

    }

    public void Move(List<Direction> directions){

        Vector2 moveVector = DirectionConverter.DirectionsToVector2(directions);

        targetRB.MovePosition(moveVector);
        
    }

    public void Rotate(Vector3 mousePos){

        //https://discussions.unity.com/t/rotate-object-weapon-towards-mouse-cursor-2d/1172

        Vector3 mouse_pos = mousePos;
        mouse_pos.z = Camera.main.transform.position.z; 
        Vector3 object_pos = Camera.main.WorldToScreenPoint(targetRB.transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg - 90;
        targetRB.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Vector3 direction = targetRB.transform.TransformDirection(Vector3.up) * 5;
        Gizmos.DrawRay(targetRB.transform.position, direction);
        
    }

}