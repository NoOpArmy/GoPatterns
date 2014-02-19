using UnityEngine;
using UnityEditor;


class TileCreatorWindow : EditorWindow
{
    private GameObject object1, object2;
    private int width=5, height=5;
    private int levelWidth=5, levelHeight=5;
    private float tileSize = 1; //size of each grid tile
    private bool[,] tiles = new bool[5,5];
    private Vector3 currentPosition;
    private float startingx; //we will store the starting x in it to restore in the loop
    private bool mid; //is the position middle of the tileset or botom left
    private bool fillEmptyPlacesWithObject2=true; //should we fill empty places with object 2

    [MenuItem("Window/Tile editor")]
    static void create()
    {
        EditorWindow.GetWindow<TileCreatorWindow>();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        object1 = EditorGUILayout.ObjectField("object 1", (UnityEngine.Object)object1, typeof(GameObject),true) as GameObject;
        object2 = EditorGUILayout.ObjectField("object 2", (UnityEngine.Object)object2, typeof(GameObject),true) as GameObject;
        mid = EditorGUILayout.Toggle("Middle", mid);
        fillEmptyPlacesWithObject2 = EditorGUILayout.Toggle("Fill empty places with object 2",fillEmptyPlacesWithObject2);
        currentPosition = EditorGUILayout.Vector3Field("Start Position", currentPosition);
        width=EditorGUILayout.IntField("width",width);
        height = EditorGUILayout.IntField("height", height);
        tileSize = EditorGUILayout.FloatField("tile size",tileSize);
        if (levelHeight != height || levelWidth != width)
        {
            levelWidth = width;
            levelHeight = height;
            tiles = new bool[height, width];
        }
        for (int i = height-1; i >= 0; i--)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < width; j++)
            {
                tiles[i, j] = GUILayout.Toggle(tiles[i, j],"");
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Check All"))
        {
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    tiles[i, j] = true;
                }
            }
        }
        if (GUILayout.Button("Clear All"))
        {
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    tiles[i, j] = false;
                }
            }
        }
        if (GUILayout.Button("Revert All"))
        {
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    tiles[i, j] = !tiles[i, j];
                }
            }
        }
        if (GUILayout.Button("Check border"))
        {
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == width - 1)
                    {
                        tiles[i, j] = true;
                    }
                }
            }
        }
        if (GUILayout.Button("Check border and clean others"))
        {
            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == width - 1)
                    {
                        tiles[i, j] = true;
                    }
                    else
                    {
                        tiles[i, j] = false;
                    }
                }
            }
        }
        if (GUILayout.Button("Mirror left half"))
        {
            if (width % 2 == 1)//for odd with maps
            {
                for (int i = height - 1; i >= 0; i--)
                {
                    for (int j = width / 2 + 1; j < width; j++)
                    {
                        tiles[i, j] = tiles[i, width / 2 - (j - width / 2)];
                    }
                }
            }
            else //for even width maps
            {
                for (int i = height - 1; i >= 0; i--)
                {
                    for (int j = width / 2; j < width; j++)
                    {
                        tiles[i, j] = tiles[i, width / 2 - (j+1 - width / 2)];
                    }
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        if (object1 != null && (object2 != null || fillEmptyPlacesWithObject2==false) && width > 0 && height > 0 && tileSize > 0)
        {
            if (GUILayout.Button("Create"))
            {
                if (mid)
                {
                    currentPosition.x -= width / 2;
                    currentPosition.z -= height / 2;
                }
                Placement.CreateTileFromObjects(object1, 
                    (fillEmptyPlacesWithObject2) ? object2 : null, 
                    tiles, 
                    currentPosition, 
                    tileSize);
            }
            currentPosition = Vector3.zero;
        }
        else
        {
            GUILayout.Label("You should choose 2 gameObjects for object1 and object 2 and type numbers greater than 0 in all numeric fields");
        }
        EditorGUILayout.EndVertical();
    }
}