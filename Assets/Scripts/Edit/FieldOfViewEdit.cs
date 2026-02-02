using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(FildOnView))]
public class FieldOfViewEdit : MonoBehaviour
{
    private void OnSceneGUI()
    {
        FildOnView fov = (FildOnView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.Radius);

        Vector3 viewAngleLeft   = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.Angle / 2);
        Vector3 viewAngleRight  = DirectionFromAngle(fov.transform.eulerAngles.y, fov.Angle / 2);

        Handles.color = Color.yellow;

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.Radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov.Radius);

        if (fov.CenSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position,fov.Player.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }
}
