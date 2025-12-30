using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UnitGameSettings", menuName = "Settings/UnitGameSettings", order = 51)]

public class UnitGameSettings : ScriptableObject
{
    [field: SerializeField, Space(20f)]
    [field: Tooltip("Диагональная атака")]
    public bool DiagonalAttack { get; private set; }

    [field: SerializeField]
    [field: Tooltip("Максимальное количество шагов за ход")]
    public int MaxSteps { get; private set; }
    
}