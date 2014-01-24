using UnityEngine;
using UnityEditor;
using System.Collections;
using TMI;

[CustomEditor(typeof(TMILevel))]
public class TilemapEditor : Editor {
	public int currentPaintLayer = 0;
	public Vector2 scrollPosition = Vector2.zero;
	public int selectedPalletteIndex = 0;
	private TilemapManager manager {
		get {
			return TMI.TilemapManager.Instance;
		}
		set {}
	}

	public void OnSceneGUI() {

		Event e = Event.current;


		if (manager.layers.Count == 0) {
			manager.CreateTilemap(10,10);
		}

		if (currentPaintLayer >= manager.layers.Count) {
			currentPaintLayer = 0;
		}

		if (manager.layers.Count > 0) {
			for (int i = 0; i < manager.layers[currentPaintLayer].width; i++) {
					for (int j = 0; j < manager.layers[currentPaintLayer].height; j++) {
						Color highlightColor = new Color(1f,1f,1f,0.05f);
						Handles.DrawSolidRectangleWithOutline (new Vector3[4] { new Vector3 (i - 0.5f, j - 0.5f, currentPaintLayer),
																				new Vector3 (i - 0.5f, j + 0.5f, currentPaintLayer),
																				new Vector3 (i + 0.5f, j + 0.5f, currentPaintLayer),
																				new Vector3 (i + 0.5f, j - 0.5f, currentPaintLayer)}, 
																				highlightColor, 
																				Color.gray);
					}
			}
		}

		string[] layerSelection = new string[manager.layers.Count];
		for (int i = 0; i < manager.layers.Count; i++) {
			layerSelection[i] = "Layer " + (i + 1);
		}

		currentPaintLayer = GUI.SelectionGrid (new Rect (10, 10, 60, 100), currentPaintLayer, layerSelection, 1);

		if (GUI.Button (new Rect (10, 120, 30, 30), "+"))
			manager.CreateTilemap (10, 10);

		if (GUI.Button (new Rect (40, 120, 30, 30), "-"))
			manager.DestroyTilemap (currentPaintLayer);

		if ((e.type == EventType.MouseDrag || e.type == EventType.MouseDown || e.type == EventType.MouseUp) && e.button == 0) {
			Debug.Log("YO");
			if(Camera.current != null) {
				
				Vector3 worldMouse = Camera.current.ScreenToWorldPoint(new Vector3(e.mousePosition.x,e.mousePosition.y, 10));
				int x = Mathf.RoundToInt(worldMouse.x);
				int y = Mathf.RoundToInt(worldMouse.y);
				if(x > 0 && x < manager.layers [currentPaintLayer].width && y > 0 && y < manager.layers [currentPaintLayer].height) {
					manager.layers [currentPaintLayer].SetTile(selectedPalletteIndex,x,y);
				}
				
			}

		}

		e.Use();
	}

	public override void OnInspectorGUI() {
		if(manager.layers.Count > 0) {
			EditorGUILayout.LabelField ("Pallette");
			manager.layers [currentPaintLayer].tileset = (Tileset)EditorGUILayout.ObjectField ("Tileset", manager.layers [currentPaintLayer].tileset, typeof(Tileset), false);

			if (manager.layers [currentPaintLayer].tileset.Tiles.Length > 0) {
				for (int i = 0; i < Mathf.CeilToInt((float) manager.layers [currentPaintLayer].tileset.Tiles.Length/5) + 1; i++) {
					GUILayout.BeginHorizontal ();
					GUILayout.Space (10f);
					for (int j = 0; j < 5; j++) {
						if (i*5 + j < manager.layers [currentPaintLayer].tileset.Tiles.Length) {
							Texture2D tex = AssetPreview.GetAssetPreview (manager.layers [currentPaintLayer].tileset.Tiles [i*5 + j]);
							if(i*5 + j != selectedPalletteIndex) {
								if (GUILayout.Button (tex, GUIStyle.none)) {
									selectedPalletteIndex = i*5 + j;
								}
							} else {
								if (GUILayout.Button (tex)) {
									selectedPalletteIndex = i*5 + j;
								}
							}			
						}
					}
					GUILayout.EndHorizontal ();
				}
			}
		}
	}
}
