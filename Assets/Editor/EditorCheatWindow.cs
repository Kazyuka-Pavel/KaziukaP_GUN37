using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public static class EditorCheatWindow 
{    
    [MenuItem("Netologia/Windows/Editor Cheat Window", priority = 0)]
    public static void EditorCheatWindows()
    {      
        var controls = new EditorControls();
        controls.Enable();
        controls.Main.Kill.performed += OnKill;
        controls.Main.NextTurn.performed += OnNewTurn;
        Debug.Log("Cheat is activated. Press 1 to new turn, 2 to kill unit under cursor");
    }

    private static void OnKill(InputAction.CallbackContext obj)
    {
        if (!EditorApplication.isPlaying) return;

        var cells = UnityEngine.Object.FindObjectsOfType<Cell>();
        var selectedcell= from c in cells where c.focused orderby c select c;
        foreach (var cell in selectedcell)
        {
            if (!cell.IsEmpty)
            {
                cell.Unit.DestroyGameObject();
            }
        }            
    }

    private static void OnNewTurn(InputAction.CallbackContext obj)
    {
        if (!EditorApplication.isPlaying) return;
        
        var controller      = UnityEngine.Object.FindObjectOfType<PlayerController>();
        var type = controller.GetType();
        //var methodInfo = type.GetMethod("Callback", BindingFlags.Instance | BindingFlags.NonPublic); // приватый метод
        //methodInfo.Invoke(controller, new object[] { GameEvent.NewTurn });
        var methodInfo = type.GetMethod("EndTurn", BindingFlags.Instance | BindingFlags.NonPublic); // приватый метод
        methodInfo.Invoke(controller, new object[] { });        
    }

    private static void Test()
    {
        if (!EditorApplication.isPlaying) return;

        var controller = UnityEngine.Object.FindObjectOfType<BattleController>();

        var cell = UnityEngine.Object.FindObjectOfType<Cell>();
        //System.Type        
        var type = cell.GetType();
        //Позволяет определять:
        //● иерархию наследования
        //● ключевые свойства типа(абстрактный
        //класс / класс / интерфейс / структура и тд)
        //● модуль, сборку, имя, пространство имен и
        //прочие данные типа

        //ConstructorInfo
        // СОздание ScriptableObject через рефлексию, используя метаданные
        type = typeof(UnitGameSettings);
        //var constr = type.GetConstructor(Array.Empty<Type>());
        //var settings = constr.Invoke(Array.Empty<object>());

        //FieldInfo
        var fieldInfo = type.GetField("focusMesh", BindingFlags.Instance | BindingFlags.NonPublic); // приватные свойства можно получить через настрйоку бинда
        var renderer = fieldInfo.GetValue(cell) as MeshRenderer;
        fieldInfo.SetValue(cell, default(MeshRenderer));
        EditorUtility.SetDirty(cell); // оповещение об изменении (грязный)

        type = typeof(EditorCheatWindow);
        var methodInfo = type.GetMethod(nameof(EditorCheatWindows), BindingFlags.Instance | BindingFlags.NonPublic); // приватый метод
        methodInfo.Invoke(typeof(EditorCheatWindow), new object[] { });  // для объектов  передается сам объект, для статических - тип

        //Альтернативный вариант без рефлекии SerializedObject SerializedProperty
        var serializedObject = new SerializedObject(cell);
        serializedObject.Update(); //подтягивание всех изменений из cell
        var property = serializedObject.FindProperty("focusMesh"); // не важно приватный или нет, если сериализуемый
        property.objectReferenceValue = default(MeshRenderer);
        serializedObject.ApplyModifiedProperties(); // оповещение об изменении (грязный)
    }
}
