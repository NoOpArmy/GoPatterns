using UnityEngine;
using UnityEditor;
#pragma warning disable 649
//this wizard allows you to place objects in a circular patern in your scene
class PlaceCircular : ScriptableWizard
{
    public GameObject item; //the object that you want to place in your circle
    public Vector3 center; //center of the circle
    public float radius=5; //radius of the circle
    public int objectCountOnRadius; //count of objects from center to radius
    public int ObjectCountOnBorder=20; //place this count object on border
    public bool vertical; //object will be created on a x,y plane instead of x,z plane

    void OnWizardUpdate()
    {
        helpString = "choose the gameObject that you want to place and the center/radius of the circle, then choose number of objects from center to boundary";
        isValid = true;
        if (item == null | objectCountOnRadius < 1 || radius <= 0 || ObjectCountOnBorder < 1)
        {
            isValid = false;
            if (objectCountOnRadius < 1) objectCountOnRadius = 1;
            if (ObjectCountOnBorder < 1) ObjectCountOnBorder = 1;
            errorString = "item can not be nuul. radius should be greater than 0 and object count/object count on border should be greater than or equal to one.";
        }
        else
        {
            errorString = "";
        }
    }

    void OnWizardCreate()
    {
        Placement.CreateCircular(item, center, radius, (uint)objectCountOnRadius, (uint)ObjectCountOnBorder, vertical);
    }

    [MenuItem("GameObject/Placement/Place Circular")]
    static void menu()
    {
        ScriptableWizard.DisplayWizard<PlaceCircular>("Place Circular");
    }
}