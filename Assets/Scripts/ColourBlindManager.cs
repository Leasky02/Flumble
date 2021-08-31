using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourBlindManager : MonoBehaviour
{
    public static bool colourBlindMode;

    public void ChangeColourBlindMode(bool mode)
    {
        colourBlindMode = mode;
        Debug.Log(mode);
    }

    public bool ReturnMode()
    {
        return colourBlindMode;
    }
}
