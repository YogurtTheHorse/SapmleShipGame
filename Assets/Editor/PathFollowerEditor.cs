using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathFollower))]
public class PathFollowerEditor : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        var pf = (PathFollower)target;
        var zero = pf.transform.position;

        for (var i = 0; i < pf.sections.Length; i++)
        {
            var section = pf.sections[i];
            var isLast = i == pf.sections.Length - 1;

            HandlePosition(ref section.startPoint, zero);

            if (!isLast || !pf.loop)
            {
                HandlePosition(ref section.endPoint, zero);
            }

            HandlePosition(ref section.startTangent, zero + section.startPoint);


            if ((isLast && !pf.loop) || !Next(i).bindTangentToPrevious)
            {
                // in case next section didn't bind to current we can move
                HandlePosition(ref section.endTangent, zero + section.endPoint);
            }

            if (i > 0) // sync values with prev
            {
                SyncSections(section, pf.sections[i - 1]);
            }

            Handles.DrawBezier(
                zero + section.startPoint,
                zero + section.endPoint,
                zero + section.startPoint + section.startTangent,
                zero + section.endPoint + section.endTangent,
                Color.red,
                null,
                2f
            );

            Handles.color = Color.black;
            Handles.DrawLine(section.startPoint + zero, section.startPoint + zero + section.startTangent, 2);
            Handles.DrawLine(section.endPoint + zero, section.endPoint + zero + section.endTangent, 2);
        }

        if (pf.loop) // sync values with prev
        {
            SyncSections(pf.sections[0], pf.sections[^1]);
        }

        void SyncSections(PathSection current, PathSection prev)
        {
            prev.endPoint = current.startPoint;

            if (current.bindTangentToPrevious)
            {
                prev.endTangent = -current.startTangent;
            }
        }

        PathSection Next(int i) => i == pf.sections.Length - 1 ? pf.sections[^1] : pf.sections[0];
    }

    private bool HandlePosition(ref Vector3 value, Vector3 zero)
    {
        EditorGUI.BeginChangeCheck();
        var newPosition = Handles.PositionHandle(zero + value, Quaternion.identity) - zero;

        if (!EditorGUI.EndChangeCheck())
            return false;

        Undo.RecordObject(target, "Update path");
        value = newPosition;

        return true;
    }

    private void OnEnable() => SceneView.duringSceneGui += OnSceneViewGUI;

    private void OnDisable() => SceneView.duringSceneGui -= OnSceneViewGUI;
}