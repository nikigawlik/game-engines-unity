using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swamp : MonoBehaviour {
    [SerializeField] private float slowdown = 0.5f;

    public float Slowdown
    {
        get
        {
            return slowdown;
        }
    }
}
