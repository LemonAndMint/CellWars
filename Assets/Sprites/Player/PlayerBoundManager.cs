using UnityEngine;

public class BoundManager : MonoBehaviour
{
    public GameObject BoundPrefb;

    public void Bound(Transform bindtransform, Transform toBeBoundtransform){

        Vector3 distanceVector = bindtransform.parent.position + toBeBoundtransform.position;
        float distance = Vector2.Distance(bindtransform.parent.position, toBeBoundtransform.localPosition);

        GameObject boundGO = Instantiate(BoundPrefb, distanceVector * 0.5f, Quaternion.identity);

        boundGO.transform.localScale =  new Vector3(distance, boundGO.transform.localScale.y, boundGO.transform.localScale.z);

        Vector2 direction = toBeBoundtransform.position - bindtransform.parent.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        boundGO.transform.rotation = Quaternion.Euler(0, 0, angle);

        boundGO.transform.parent = bindtransform;
        toBeBoundtransform.parent = bindtransform;

        if(toBeBoundtransform.TryGetComponent(out Rigidbody2D rigidbody2D)){

            rigidbody2D.simulated = false; 
            //in order it to move with main cell, rigidbody needs to stop for the other cells.
            //opposite force will applied through predefined variables.

        }

    }
    
}
