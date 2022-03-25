using Ships;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpaceShip))]
public class ShipEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var ship = (SpaceShip)target;

        if (GUILayout.Button("Reset"))
        {
            ship.Reset();
        }
    }
}