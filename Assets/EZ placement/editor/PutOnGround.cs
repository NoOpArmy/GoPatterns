using UnityEngine;
using UnityEditor;

public class PutOnGround
{

    [MenuItem("GameObject/Placement/PutOnGround %g", true)]
    public static bool PutOnGrVal()
    {
        if (Selection.activeTransform != null && Selection.activeTransform.collider != null)
            return true;
        else
            return false;
    }


    [MenuItem("GameObject/Placement/PutOnGround %g")]
    public static void PutOnGr()
    {
        RaycastHit h;
        LayerMask l = Selection.activeTransform.gameObject.layer;
        Selection.activeTransform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        if (Physics.Raycast(new Ray(Selection.activeTransform.position, new Vector3(0, -1, 0)), out h))
        {
            Vector3 v = Selection.activeTransform.position;
            v.y = h.point.y + Selection.activeTransform.collider.bounds.extents.y;
            Selection.activeTransform.position = v;
        }
        else
        {
            Debug.LogError("There is nothing below the selected object");
        }
        Selection.activeTransform.gameObject.layer = l;
    }

    [MenuItem("GameObject/Placement/PutOnMiddleOf %m", true)]
    public static bool PutOnMiddleVal()
    {
        if (Selection.activeTransform != null && Selection.activeTransform.collider != null)
            return true;
        else
            return false;
    }

    [MenuItem("GameObject/Placement/PutOnMiddleOf %m")]
    public static void PutOnMid()
    {
        RaycastHit h;
        //change the gameobject's layer to something that we don't cast against and return it back after raycast.
        LayerMask l = Selection.activeTransform.gameObject.layer;
        Selection.activeTransform.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        if (Physics.Raycast(new Ray(Selection.activeTransform.position, new Vector3(0, -1, 0)), out h))
        {
            Vector3 v = Selection.activeTransform.position;
            v.y = h.point.y + Selection.activeTransform.collider.bounds.extents.y;
            v.x = h.collider.bounds.center.x;
            v.z = h.collider.bounds.center.z;
            Selection.activeTransform.position = v;
        }
        else
        {
            Debug.LogError("There is nothing below the selected object");
        }
        Selection.activeTransform.gameObject.layer = l;
    }

}