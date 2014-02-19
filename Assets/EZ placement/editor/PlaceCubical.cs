 using UnityEditor;
using UnityEngine;
#pragma warning disable 649

 //this wizard allows you to place objects in a rectangular pattern in your scene
class PlaceCubical : ScriptableWizard
{
    public GameObject item; //the object that we place in our scene
    public GameObject item2; //the second gameObject used in some modes
    public bool fillEmptyPlacesWithItem2 = true;//should we fill empty places with item 2
    public bool middle; //is the position middle of our scene or botom left
    public Vector3 Position; //left bottom of the level
    public int width = 11, height = 11,depth=11; //width and height of the level
    public float tileSize = 1; //size of the each tile and our jumping size.
    public Placement.FillMode fillMode; //fill the rectangle

    void OnWizardUpdate()
    {
        isValid = true;
        helpString = "select the GameObject that you want to place in your level and set numbers";
        string e1, e2, e3;
        if (item == null)
        {
            e1 = "select an item gameObject\n";
            isValid = false;
        }
        else
        {
            e1 = "";
        }
        if (fillEmptyPlacesWithItem2 && fillMode != Placement.FillMode.fill && item2 == null)
        {
            e2 = "in this mode you need to select item2 too\n";
            isValid = false;
        }
        else
        {
            e2 = "";
        }
        if (width < 1 || height < 1 || depth < 1 || tileSize <= 0)
        {
            e3 = "tileSize should be > 0 and width/height/depth should be greater than or equal to one. all of them are integer numbers";
            isValid = false;
        }
        else
        {
            e3 = "";
        }
        errorString = e1 + e2 + e3;
    }

    private Vector3 currentPosition;

    void OnWizardCreate()
    {
        //if specified position is middle and not bottom left
        if (middle)
        {
            Position.x -= width / 2 * tileSize;
            Position.y -= height / 2 * tileSize;
            Position.z -= depth / 2 * tileSize;
        }
        currentPosition = Position;
        Placement.CreateCubical(item, (fillEmptyPlacesWithItem2) ? item2 : null, currentPosition, width, height, depth, tileSize, fillMode);
    }


    [MenuItem("GameObject/Placement/Place Cubical")]
    static void create()
    {
        ScriptableWizard.DisplayWizard<PlaceCubical>("Place Cubical");
    }
}