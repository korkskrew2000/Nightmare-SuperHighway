using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PrefabVariantThumbnailModification
{
	static PrefabVariantThumbnailModification()
	{
		EditorApplication.projectWindowItemOnGUI -= ProjectWindowItemOnGUICallback;
		EditorApplication.projectWindowItemOnGUI += ProjectWindowItemOnGUICallback;
	}

	private static void ProjectWindowItemOnGUICallback(string guid, Rect selectionRect)
	{
		// If the width is too wide, we don't need to put the icon.
		if (selectionRect.width / selectionRect.height >= 2)
		{
			return;
		}

		string fileName = AssetDatabase.GUIDToAssetPath(guid);

		// If we're a directory, ignore it.
		if (fileName.LastIndexOf('.') == -1)
		{
			return;
		}

		// If we're not a prefab, ignore it
		GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(fileName);
		if (go == null)
		{
			return;
		}

		// If we're not a variant, ignore it.
		if (PrefabUtility.GetPrefabAssetType(go) != PrefabAssetType.Variant)
		{
			return;
		}

		GUIContent icon = EditorGUIUtility.IconContent("PrefabVariant Icon");

		Vector2 selectionPosition = selectionRect.position;
		Vector2 selectionSize = selectionRect.size;

		// The x position should be a little left of the right edge.
		float xPosition = selectionPosition.x + selectionSize.x - selectionSize.x / 3.5f;

		// The y position should be a little above of the bottom edge.
		float yPosition = selectionPosition.y + selectionSize.y - selectionSize.y / 2;

		Vector2 position = new Vector2(xPosition, yPosition);

		Rect newRect = selectionRect;
		newRect.position = position;

		// This is a nice number to reduce the size of the rect by to make the icon smaller
		newRect.size /= 3.5f;

		GUI.Label(newRect, icon);
	}
}