using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New UnitsSettings", menuName = "Settings/UnitsSettings", order = 51)]

public class UnitsSettings : ScriptableObject
{    

    [field: SerializeField, Space(20f)]
    [field: Tooltip("Белые фишки>")]
    public Material WhiteUnit { get; private set; }

    [field: SerializeField]
    [field: Tooltip("Черные фишки>")]
    public Material BlackUnit { get; private set; }
}
