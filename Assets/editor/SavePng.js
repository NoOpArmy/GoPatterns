//Opens a file selection dialog for a PNG file and saves a selected texture to the file.
import System.IO;

@MenuItem("Assets/Save Texture to file")
static function Apply () {

var texture : Texture2D = Selection.activeObject as Texture2D;
if (texture == null) {
EditorUtility.DisplayDialog("Select Texture",
"You Must Select a Texture first!", "Ok");
return;
}

var path = EditorUtility.SaveFilePanelInProject("Save texture as PNG",
texture.name + ".png", "png", 
"Please enter a file name to save the texture to");
if (path.Length != 0) {
// Convert the texture to a format compatible with EncodeToPNG
if ( texture.format != TextureFormat.ARGB32) {
var newTexture = Texture2D(texture.width, texture.height);
newTexture.SetPixels(texture.GetPixels(0),0);
texture = newTexture;
}
var pngData = texture.EncodeToPNG();
if (pngData != null) {
File.WriteAllBytes(path, pngData);
}
}
}