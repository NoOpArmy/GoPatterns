using UnityEngine;
using UnityEditor;
#pragma warning disable 649
class PlaceRandomlyOnSphere : ScriptableWizard
{
    public GameObject item;
    public Vector3 Position;
    public float radius;
    public bool inside = true;
    public int objectCount;

    void OnWizardUpdate()
    {
        isValid = true;
        helpString = "choose the object that you want and size of the sphere to create objects randomly in it";
        if (item == null || radius <= 0 || objectCount <= 0)
        {
            isValid = false;
            errorString = "item can not be null and all other numbers hsould be greater than 0";
        }
        else
        {
            errorString = "";
        }

    }

    void OnWizardCreate()
    {
        if (inside)
            Placement.CreateRandomInsideSphere(item, Position, radius, (uint)objectCount);
        else
            Placement.CreateRandomOnSphere(item, Position, radius, (uint)objectCount);
    }

    [MenuItem("GameObject/Placement/Place randomly on a sphere")]
    static void randomPlacement()
    {
        ScriptableWizard.DisplayWizard<PlaceRandomlyOnSphere>("Place randomly on a sphere");
    }
}