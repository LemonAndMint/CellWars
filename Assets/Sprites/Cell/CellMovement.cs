using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Direction;

public class CellMovement : MonoBehaviour
{
    public Rigidbody2D targetRB;
    
    private void Start() {
        
        if(targetRB == null){

            new ArgumentNullException(targetRB.name);
            return;

        }

    }

    public void Move(Vector2 direction){

        targetRB.MovePosition(direction);
        
    }

}