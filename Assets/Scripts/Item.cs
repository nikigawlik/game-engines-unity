using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
	ExtraLife,
	Mine,
	Juice,
	Coffee,
}

public class Item : MonoBehaviour {
	public ItemType type;
}
