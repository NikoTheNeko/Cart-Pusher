using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Cart Items")]
public class ItemScriptableObject : ScriptableObject{

    public string ItemName;
    public int Weight;
    public int ScoreValue;

    public GameObject ItemPrefab;

    public GameObject SceneObject;

}
