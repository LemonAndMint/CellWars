using Network;
using UnityEngine;

public class BoundManager : MonoBehaviour
{
    public string GameObjectLayerString;
    public string RigidbodyExcludeLayerString;
    public GameObject BoundPrefb;

    public GameObject Bound(Transform bindtransform, Transform toBeBoundtransform){ //RETURN BOND, MAKE THE BOUND IN HERE!

        Vector3 distanceVector = bindtransform.parent.position + toBeBoundtransform.position;
        float distance = Vector2.Distance(bindtransform.parent.position, toBeBoundtransform.localPosition);

        GameObject boundGO = Instantiate(BoundPrefb, distanceVector * 0.5f, Quaternion.identity);

        boundGO.transform.localScale =  new Vector3(distance, boundGO.transform.localScale.y, boundGO.transform.localScale.z);

        Vector2 direction = toBeBoundtransform.position - bindtransform.parent.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        boundGO.transform.rotation = Quaternion.Euler(0, 0, angle);

        boundGO.transform.parent = bindtransform;
        toBeBoundtransform.parent = bindtransform;

        boundGO.layer = LayerMask.NameToLayer(GameObjectLayerString);

        if(toBeBoundtransform.TryGetComponent(out Rigidbody2D rigidbody2D)){

            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            
            LayerMask mask = LayerMask.GetMask(RigidbodyExcludeLayerString);
            rigidbody2D.excludeLayers |= mask;
            //in order it to move with main cell, rigidbody needs to be kinematic.
            //opposite force will applied through predefined variables.

        }

        return boundGO;

    }

    public void Unbound(Bound? bound){

        (bound?.NextNode).CellGO.transform.parent = null;
        (bound?.NextNode).CellGO.layer = LayerMask.NameToLayer(GameObjectLayerString);

        if((bound?.NextNode).CellGO.TryGetComponent(out Rigidbody2D rigidbody2D)){

            rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            
            LayerMask mask = LayerMask.GetMask(RigidbodyExcludeLayerString);
            rigidbody2D.excludeLayers &= ~mask;
            //reverse what we have done in Bound operation

        }

        Transform.Destroy(bound?.BoundGO);

    }
    
}
