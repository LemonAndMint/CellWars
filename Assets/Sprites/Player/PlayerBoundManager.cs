using Network;
using UnityEngine;

public class BoundManager : MonoBehaviour
{
    public string GameObjectPlayerLayerString;
    public string GameObjectLayerString;
    public string RigidbodyExcludeLayerString;
    public GameObject BoundPrefb;
    public Rigidbody2D mainRb;

    /// <summary>
    /// bind trans is parent cell
    /// </summary>
    /// <param name="bindtransform"></param>
    /// <param name="toBeBoundtransform"></param>
    /// <returns></returns>
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

        toBeBoundtransform.gameObject.layer = LayerMask.NameToLayer(GameObjectPlayerLayerString);

        if(toBeBoundtransform.TryGetComponent(out Rigidbody2D rigidbody2D)){

            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            //in order it to move with main cell, rigidbody needs to be kinematic.
            //opposite force will applied through predefined variables.
            
            LayerMask mask = LayerMask.GetMask(RigidbodyExcludeLayerString);
            rigidbody2D.excludeLayers |= mask;

        }

        if(mainRb != null){
            
            //https://www.reddit.com/r/Unity3D/comments/idd0ib/do_you_know_you_can_change_a_rigidbodys_center_of/

            mainRb.centerOfMass = Vector2.zero; 
            //reset the center of mass because if we dont, torque will be applied 
            //based on whole system's center of mass.

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

            //rigidbody2D.AddForce(rigidbody2D.transform.forward.normalized * 10, ForceMode2D.Impulse);
            //#TODO

        }

        Transform.Destroy(bound?.BoundGO);

    }
    
}
