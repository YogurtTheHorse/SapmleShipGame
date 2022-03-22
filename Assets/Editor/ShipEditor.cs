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
            ship.transform.position = new Vector3(0, 16, 0);
            ship.transform.rotation = Quaternion.identity;
            
            ship.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ship.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}