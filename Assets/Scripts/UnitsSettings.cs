using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New UnitsSettings", menuName = "Settings/UnitsSettings", order = 51)]

public class UnitsSettings : ScriptableObject
{

    [SerializeField]
    private TurnMaterial _default;

    [SerializeField]
    private TurnMaterial[] _presets;

    public TurnMaterial this[Team team]
    {
        get
        {
            var index = Array.FindIndex(_presets, t => t.Team == team);
            if (index == -1)
            {
                return _default;
            }
            return _presets[index];
        }
    }


    [Serializable]
    public struct TurnMaterial
    {
        public Material Material;        
        public Team Team;
    }
}
