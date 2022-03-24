using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathFollower))]
public class PathFollowerEditor : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        var pathFollower = (PathFollower)target;
        
        if (!pathFollower) return;
        
        var path = pathFollower.path;
        var zero = pathFollower.transform.position;

        for (var i = 0; i < path.sections.Length; i++)
        {
            DrawSection(path, i, zero);
        }

        if (path.loop && path.sections.Length > 1) // sync values with prev
        {
            SyncSections(path.sections[0], path.sections[^1]);
        }
    }

    private void DrawSection(Path path, int i1, Vector3 zero)
    {
        PathSection Next(int i) => i == path.sections.Length - 1 ? path.sections[^1] : path.sections[0];
        {
            var section = path.sections[i1];
            var isLast = i1 == path.sections.Length - 1;

            HandlePosition(ref section.startPoint, zero);

            if (!isLast || !path.loop)
            {
                HandlePosition(ref section.endPoint, zero);
            }

            HandlePosition(ref section.startTangent, zero + section.startPoint);


            if ((isLast && !path.loop) || !Next(i1).bindTangentToPrevious)
            {
                // in case next section didn't bind to current we can move
                HandlePosition(ref section.endTangent, zero + section.endPoint);
            }

            if (i1 > 0) // sync values with prev
            {
                SyncSections(section, path.sections[i1 - 1]);
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
    }

    private void SyncSections(PathSection current, PathSection prev)
    {
        prev.endPoint = current.startPoint;

        if (current.bindTangentToPrevious)
        {
            prev.endTangent = -current.startTangent;
        }
    }

    private void HandlePosition(ref Vector3 value, Vector3 zero)
    {
        EditorGUI.BeginChangeCheck();
        var newPosition = Handles.PositionHandle(zero + value, Quaternion.identity) - zero;

        if (!EditorGUI.EndChangeCheck())
            return;

        Undo.RecordObject(target, "Update path");
        value = newPosition;
    }

    private void OnEnable() => SceneView.duringSceneGui += OnSceneViewGUI;

    private void OnDisable() => SceneView.duringSceneGui -= OnSceneViewGUI;
}