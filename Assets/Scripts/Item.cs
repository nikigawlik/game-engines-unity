using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
	ExtraLife,
	Mine
}

public class Item : MonoBehaviour {
	public ItemType type;
}
