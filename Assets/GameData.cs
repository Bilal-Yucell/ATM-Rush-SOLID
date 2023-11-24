using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : ISaveableEntity
{
    public int Coin;
    public int Level;
    public bool Haptic;
    public int SFX;

    public string GetKey()
    {
        throw new System.NotImplementedException();
    }
}
