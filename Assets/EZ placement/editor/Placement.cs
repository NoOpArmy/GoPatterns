using UnityEngine;

/// <summary>
/// This class contains different methods to place objects procedurally in a level. Different methods allow you to place objects in different patterns like cubical and circular and
/// also using custom patterns created with boolean arrays. All features that you see in the eidotr are also
/// accessible at runtime using this class methods.
/// </summary>
static class Placement
{
    /// <summary>
    /// filling style of the generation function
    /// </summary>
    public enum FillMode { fill, empty, YesNo };
    private static Vector3 currentPosition;

    /// <summary>
    /// Creates a set of objects in a circular patern.
    /// </summary>
    /// <param name="item">the GameObject to be placed in the patern</param>
    /// <param name="center">center of the circle</param>
    /// <param name="radius">radius of the circle</param>
    /// <param name="objectCountOnRadius">number of objects between the center and border on radius</param>
    /// <param name="objectCountOnBorder">number of objects on the border of the circle</param>
    /// <param name="vertical">should the method create a vertical circle on x-y plane instead of x-z</param>
    /// <returns>returns if it was successful or not</returns>
    public static bool CreateCircular(GameObject item, Vector3 center, float radius, uint objectCountOnRadius, uint objectCountOnBorder, bool vertical)
    {
        if (radius <= 0 || item == null || objectCountOnRadius == 0 || objectCountOnBorder == 0)
        {
            return false;
        }
        //place one object in center
        GameObject.Instantiate(item, center, Quaternion.identity);
        float angle;
        float r;
        r = radius / objectCountOnRadius;
        while (r <= radius)
        {
            for (int i = 0; i < objectCountOnBorder; i++)
            {
                angle = i * Mathf.PI * 2f / (float)objectCountOnBorder;
                if (vertical)
                    GameObject.Instantiate(item, center+new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * r, Quaternion.identity);
                else
                    GameObject.Instantiate(item,center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * r, Quaternion.identity);
            }
            r += radius / objectCountOnRadius;
        }
        return true;
    }

    /// <summary>
    /// Create objects in a cubical patern
    /// </summary>
    /// <param name="item">the GameObject to be placed in our cubic patern</param>
    /// <param name="item2">the second type of GameObject to be placed in empty places of our patern (instead of being empty)</param>
    /// <param name="position">position of the corner of the cube</param>
    /// <param name="width">width of the cube</param>
    /// <param name="height">height of the cube</param>
    /// <param name="depth">depth of the cube</param>
    /// <param name="tileSize">size of each tile of the cube (i.e distance between items)</param>
    /// <param name="fill">fill mode of the cube</param>
    /// <returns>returns if it was successful or not</returns>
    public static bool CreateCubical(GameObject item, GameObject item2, Vector3 position, float width, float height, float depth, float tileSize, FillMode fill)
    {
        if (item == null || width <= 0 || height <= 0 || depth <= 0 || tileSize <= 0)
        {
            return false;
        }
        //some modes can not tolerate even numbers
        if (fill == FillMode.YesNo)
        {
            if (width % 2 == 0) width++;
            if (height % 2 == 0) height++;
            if (depth % 2 == 0) depth++;
        }
        currentPosition = position;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                for (int k = 0; k < depth; k++)
                {
                    if (fill == FillMode.fill)
                    {
                        GameObject.Instantiate(item, currentPosition, Quaternion.identity);
                    }
                    else if (fill == FillMode.empty)
                    {
                        if (i == 0 || j == 0 || k == 0 || i == width - 1 || j == height - 1 || k == depth - 1)
                        {
                            GameObject.Instantiate(item, currentPosition, Quaternion.identity);
                        }
                        else if (item2 != null)
                        {
                            GameObject.Instantiate(item2, currentPosition, Quaternion.identity);
                        }
                    }
                    else if (fill == FillMode.YesNo)
                    {
                        if ((i % 2 == 0 && j % 2 == 0 && k % 2 == 0) || i == 0 || j == 0 || k == 0 || i == width - 1 || j == height - 1 || k == depth - 1)
                        {
                            GameObject.Instantiate(item, currentPosition, Quaternion.identity);
                        }
                        else if (item2 != null)
                        {
                            GameObject.Instantiate(item2, currentPosition, Quaternion.identity);
                        }
                    }
                    currentPosition.z += tileSize;
                }
                currentPosition.y += tileSize;
                currentPosition.z = position.z;
            }
            currentPosition.x += tileSize;
            currentPosition.y = position.y;
            currentPosition.z = position.z;
        }
        return true;
    }

    /// <summary>
    /// creates a set of objects in random positions inside a sphere
    /// </summary>
    /// <param name="item">the item to be placed in our sphere</param>
    /// <param name="position">center of the sphere</param>
    /// <param name="radius">radius of the sphere</param>
    /// <param name="objectCount">number of objects of type "item" to be placed in our sphere</param>
    /// <returns>returns if it was successful or not</returns>
    public static bool CreateRandomInsideSphere(GameObject item, Vector3 position, float radius, uint objectCount)
    {
        if (item == null || objectCount == 0 || radius <= 0)
        {
            return false;
        }
        for (int i = 0; i < objectCount; i++)
        {
            GameObject.Instantiate(item, (Random.insideUnitSphere * radius) + position, Quaternion.identity);
        }
        return true;
    }

    /// <summary>
    /// creates a set of objects in random positions on the surface of a sphere
    /// </summary>
    /// <param name="item">the item to be placed on the surface of our sphere</param>
    /// <param name="position">center of the sphere</param>
    /// <param name="radius">radius of the sphere</param>
    /// <param name="objectCount">number of objects that should be placed on our sphere</param>
    /// <returns>returns if it was successful or not</returns>
    public static bool CreateRandomOnSphere(GameObject item, Vector3 position, float radius, uint objectCount)
    {
        if (item == null || objectCount == 0 || radius <= 0)
        {
            return false;
        }
        for (int i = 0; i < objectCount; i++)
        {
            GameObject.Instantiate(item, (Random.onUnitSphere * radius) + position, Quaternion.identity);
        }
        return true;
    }

    /// <summary>
    /// creates a set of objects in random positions in a cube
    /// </summary>
    /// <param name="item">the GameObject to be placed in our cube</param>
    /// <param name="position">left botom corner of the cube</param>
    /// <param name="width">width of the cube</param>
    /// <param name="height">height of the cube</param>
    /// <param name="depth">depth of the cube</param>
    /// <param name="objectCount">number of objects to be placed in our cube</param>
    /// <returns></returns>
    public static bool CreateRandomInsideCube(GameObject item, Vector3 position, float width, float height, float depth, uint objectCount)
    {
        if (item == null || objectCount == 0 || width <= 0 || height <= 0 || depth <= 0)
        {
            return false;
        }
        for (int i = 0; i < objectCount; i++)
        {
            GameObject.Instantiate(item, new Vector3(Random.Range(position.x, position.x + width), Random.Range(position.y, position.y + height), Random.Range(position.z, position.z + depth)), Quaternion.identity);
        }
        return true;
    }

    /// <summary>
    /// creates a Tileset of objects based on a bool array
    /// </summary>
    /// <param name="item1">the object to be placed in positions of true elements in bool array</param>
    /// <param name="item2">the object to be placed in false elements of the tile array (can be null to leave those places empty)</param>
    /// <param name="tiles">a 2D bool array to fill the tileset based on it</param>
    /// <param name="position">left corner of the tileset</param>
    /// <param name="tileSize">size of each tile (i.e distance between tile objects)</param>
    /// <returns>returns if it was successful or not</returns>
    public static bool CreateTileFromObjects(GameObject item1, GameObject item2, bool[,] tiles, Vector3 position, float tileSize)
    {
        if (item1 == null || tileSize <= 0 || tiles == null)
        {
            return false;
        }
        int width, height;
        width = tiles.GetLength(1);
        height = tiles.GetLength(0);
        
        currentPosition = position;
        for (int i = 0; i < height; i++)
        {
            currentPosition.x = position.x;
            for (int j = 0; j < width; j++)
            {
                if (tiles[i, j] == true)
                {
                    GameObject.Instantiate(item1, currentPosition, Quaternion.identity);
                }
                else if (item2 != null)
                {
                    GameObject.Instantiate(item2, currentPosition, Quaternion.identity);
                }
                currentPosition.x += tileSize;
            }
            currentPosition.z += tileSize;
        }
        return true;
    }
    
    /// <summary>
    /// Creates a 2d bool array using an image.
    /// </summary>
    /// <param name="image">The Image to create the tile from</param>
    /// <param name="specialColor">The color which is the special color on the image</param>
    /// <param name="isSpecialColorTrue">Should special color be treated as true or false</param>
    /// <param name="blockWidth">Width of each block of the texture which represents one array element</param>
    /// <param name="blockHeight">Height of each block of the texture which represents one array element</param>
    /// <returns>a 2d bool array which is populated based on the image</returns>
    /// <remarks>
    /// The middle pixel of each block is checked only
    /// The texture should have isReadable flag set to true in import settings (i.e. should be readable using Texture2d.GetPixel).
    /// </remarks>
    public static bool[,] CreateTileFromImage(Texture2D image,Color specialColor,bool isSpecialColorTrue,int blockWidth,int blockHeight)
    {
        int tileXCount = image.width / blockWidth;
        int tileYCount = image.height / blockHeight;
        bool[,] tile = new bool[tileYCount,tileXCount];
        Color currentPixel;
        for (int i = 0; i < tileYCount; ++i)
        {
            for (int j = 0; j < tileXCount; ++j)
            {
                currentPixel = image.GetPixel(j * blockWidth + blockWidth / 2, i * blockHeight + blockHeight / 2);
                tile[j,i] = (AreColorsApproxematelyEqual(currentPixel,specialColor,0.1f)) ? isSpecialColorTrue : !isSpecialColorTrue;
            }
        }
        return tile;
    }

    private static bool AreColorsApproxematelyEqual(Color c1, Color c2,float sensitivity)
    {
        if(Mathf.Abs(c1.r-c2.r) > sensitivity)
            return false;
        if (Mathf.Abs(c1.g - c2.g) > sensitivity)
            return false;
        if (Mathf.Abs(c1.b-c2.b) > sensitivity)
            return false;
        return true;
    }

    /// <summary>
    /// puts an object on the surface of the collider below it.
    /// </summary>
    /// <param name="item">the game object to set it's position</param>
    /// <returns>returns if it was successful or not</returns>
    public static bool PutOnGround(GameObject item)
    {
        if (item == null || item.collider == null)
        {
            return false;
        }
        RaycastHit h;
        LayerMask l = item.layer;
        item.layer = LayerMask.NameToLayer("Ignore Raycast");
        if (Physics.Raycast(new Ray(item.transform.position, new Vector3(0, -1, 0)), out h))
        {
            Vector3 v = item.transform.position;
            v.y = h.point.y + item.transform.collider.bounds.extents.y;
            item.transform.position = v;
        }
        else
        {
            return false;
        }
        item.layer = l;
        return true;
    }

    /// <summary>
    /// puts an object on the middle of the surface of the collider below it
    /// </summary>
    /// <param name="item">the object to set it's position</param>
    /// <returns>returns if it was successful or not</returns>
    public static bool PutOnMiddleOf(GameObject item)
    {
        if (item == null || item.collider == null)
        {
            return false;
        }
        RaycastHit h;
        //change the gameobject's layer to something that we don't cast against and return it back after raycast.
        LayerMask l = item.layer;
        item.layer = LayerMask.NameToLayer("Ignore Raycast");
        if (Physics.Raycast(new Ray(item.transform.position, new Vector3(0, -1, 0)), out h))
        {
            Vector3 v = item.transform.position;
            v.y = h.point.y + item.transform.collider.bounds.extents.y;
            v.x = h.collider.bounds.center.x;
            v.z = h.collider.bounds.center.z;
            item.transform.position = v;
        }
        else
        {
            return false;
        }
        item.layer = l;
        return true;
    }
}