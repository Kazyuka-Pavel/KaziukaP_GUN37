using UnityEngine;
using UnityEditor;

public static class SampleTestPerfomance
{
    [MenuItem("Netologia/Tests/Reflection Perfomance", priority = 0)]
    public static void ReflectionPerfomance()
    {
        var cell = Object.FindObjectOfType<Cell>();
        var type =cell.GetType();

    }
}
