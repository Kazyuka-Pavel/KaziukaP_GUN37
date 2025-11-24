using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New CellPaletteSettings", menuName = "CellPaletteSettings", order = 51)]

public class CellPaletteSettings : ScriptableObject
{
    [field: SerializeField, Space(20f)]
    [field: Tooltip("Клетка под юнитом")]
    public Material SelectCell { get; private set; }
    
    [field: SerializeField]
    [field: Tooltip("Клетка для передвижения")]
    public Material MoveCell { get; private set; }

    [field: SerializeField]
    [field: Tooltip("Клетка доступная для атаки")]
    public Material AttackCell { get; private set; }

    [field: SerializeField]
    [field: Tooltip("Клетка доступная и для атаки и для движения")]
    public Material MoveAndAttackCell { get; private set; }
}
