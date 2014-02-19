 using UnityEditor;
using UnityEngine;
#pragma warning disable 649

 //this wizard allows you to place objects in a rectangular pattern in your scene
class CreateTileUsingImage : ScriptableWizard
{
    public Texture2D image;
    public Color sPecialColor;
    public GameObject item; //the object that we place in our scene
    public GameObject item2; //the second gameObject used in some modes
    public bool fillEmptyPlacesWithItem2 = false;//should we fill empty places with item 2
    public bool middle; //is the position middle of our scene or botom left
    public Vector3 Position; //left bottom of the level
    public int width = 32, height = 32;
    public float tileSize = 1; //size of the each tile and our jumping size.


    void OnWizardUpdate()
    {
        isValid = true;
    }

    private Vector3 currentPosition;

    void OnWizardCreate()
    {
        Placement.CreateTileFromObjects(item, item2, Placement.CreateTileFromImage(image, sPecialColor, true, image.width / width, image.height / height), Position, tileSize);
    }


    [MenuItem("GameObject/Placement/Create Tile Using Image")]
    static void create()
    {
        ScriptableWizard.DisplayWizard<CreateTileUsingImage>("Load Image");
    }
}