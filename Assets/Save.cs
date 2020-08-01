using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewName", menuName = "Shop/Data")]
public class Save : ScriptableObject
{
    public int money;
    public int currentScore;
    public int fuel;
    public int life;
    public Vector3 playerPosition;
}
