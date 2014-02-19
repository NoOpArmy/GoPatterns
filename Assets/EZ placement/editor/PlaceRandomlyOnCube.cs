using UnityEngine;
using UnityEditor;
#pragma warning disable 649

class PlaceRandomlyOnCube : ScriptableWizard
{
    public GameObject item;
    public bool middle;
    public Vector3 Position;
    public float width, height, depth;
    public int objectCount = 10;

    void OnWizardUpdate()
    {
        isValid = true;
        helpString = "choose the object you want to create and dimentions of the cube and the wizard places objects in the cube in random positions";
        if (item == null || width <= 0 || height <= 0 || depth <= 0 || objectCount <= 0)
        {
            isValid = false;
            errorString = "item can not be null and all numbers should be greater than or equal to one";
        }
        else
        {
            errorString = "";
        }
    }

    void OnWizardCreate()
    {
        if (middle)
        {
            Position.x -= width / 2;
            Position.y -= height / 2;
            Position.z -= depth / 2;
        }
        Placement.CreateRandomInsideCube(item, Position, width, height, depth, (uint)objectCount);
    }

    [MenuItem("GameObject/Placement/Place randomly on a cube")]
    static void CubeRandom()
    {
        ScriptableWizard.DisplayWizard<PlaceRandomlyOnCube>("Place randomly on a cube");
    }
}