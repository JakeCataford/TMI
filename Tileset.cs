using UnityEngine;
using System.Collections.Generic;

public class Tileset : MonoBehaviour {

	public Sprite[] Tiles;
	
	public List<int> solidIndexes = new List<int>();

	public void SetSolid(int index) {
		if (!solidIndexes.Contains (index)) {
			solidIndexes.Add (index);
		}
	}

	public void SetNotSolid(int index) {
		if (solidIndexes.Contains (index)) {
			solidIndexes.Remove (index);
		}
	}

	public void ToggleSolid(int index) {
		if (solidIndexes.Contains (index)) {
			SetNotSolid (index);
		} else {
			SetSolid(index);
		}
	}

	public bool IsSolid(int index) {
		return solidIndexes.Contains (index);
	}
}
