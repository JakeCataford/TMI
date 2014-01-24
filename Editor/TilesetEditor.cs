using UnityEngine;
using UnityEditor;
using System.Collections;

namespace TMI {
	[CustomEditor(typeof(Tileset))]
	public class TilesetEditor : Editor {
		public SerializedObject sTileset;

		public void OnEnable() {
			if (sTileset == null) {
				sTileset = new SerializedObject (target);
			}
		}

		public override void OnInspectorGUI() {
			sTileset.Update ();

			Tileset tileset = (Tileset) target;

			SerializedProperty tiles = sTileset.FindProperty ("Tiles");
			EditorGUILayout.PropertyField (tiles, true);

			if (tileset.Tiles.Length > 0) {
				for (int i = 0; i < Mathf.CeilToInt((float) tileset.Tiles.Length/5) + 1; i++) {
					GUILayout.BeginHorizontal ();
					GUILayout.Space (10f);
					for (int j = 0; j < 5; j++) {
						if (i*5 + j < tileset.Tiles.Length) {
							Texture2D tex = AssetPreview.GetAssetPreview (tileset.Tiles [i*5 + j]);
							if(tileset.IsSolid(i*5 + j)) {
								if (GUILayout.Button (tex, GUIStyle.none)) {
									tileset.ToggleSolid(i*5 + j);
									EditorUtility.SetDirty(tileset);
								}
							} else {
								if (GUILayout.Button (tex)) {
									tileset.ToggleSolid(i*5 + j);
									EditorUtility.SetDirty(tileset);
								}
							}			
						}
					}
					GUILayout.EndHorizontal ();
				}
			}


			sTileset.ApplyModifiedProperties();
		}
	}
}
