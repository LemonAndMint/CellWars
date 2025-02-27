using UnityEngine;

public class BoundManager : MonoBehaviour
{
    public GameObject BoundPrefb;

    public void Bound(Transform bindtransform, Transform toBeBoundtransform){

        Vector3 distanceVector = bindtransform.parent.position + toBeBoundtransform.position;

        float boundAngle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg; 

        GameObject boundGO = Instantiate(BoundPrefb, distanceVector * 0.5f, Quaternion.identity);

        boundGO.transform.localScale =  new Vector3(distanceVector.x * 1.5f, boundGO.transform.localScale.y, boundGO.transform.localScale.z);
        boundGO.transform.rotation = Quaternion.Euler(0, 0, boundAngle);

        boundGO.transform.parent = bindtransform;
        toBeBoundtransform.parent = bindtransform;

        if(toBeBoundtransform.TryGetComponent(out Rigidbody2D rigidbody2D)){

            rigidbody2D.simulated = false; 
            //in order it to move with main cell, rigidbody needs to stop for the other cells.
            //opposite force will applied through predefined variables.

        }

    }
    
}
