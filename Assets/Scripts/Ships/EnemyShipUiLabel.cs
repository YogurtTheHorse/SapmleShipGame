using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using Zenject;

public class EnemyShipUiLabel : MonoBehaviour
{
    [Inject(Id = "canvas")]
    private Canvas _canvas;

    [Inject(Id = "player")]
    private GameObject _player;

    private TextMeshPro _text;

    public float fontSize = 12;
    
    public Vector3 textOffset = Vector3.zero;

    private void Start()
    {
        _text = CreateText();
    }

    private TextMeshPro CreateText()
    {
        var go = new GameObject("Distance label");
        var textMeshPro = go.AddComponent<TextMeshPro>();
        textMeshPro.alignment = TextAlignmentOptions.Center;

        go.transform.parent = _canvas.transform;

        return textMeshPro;
    }

    private void Update()
    {
        if (!Camera.main || !_text) return;
        
        var pos = transform.position;
        var cameraPos = Camera.main.transform.position;
        var textTans = _text.transform;
        var distance = (_player.transform.position - pos).magnitude;
        
        _text.SetText(Mathf.Round(distance).ToString(CultureInfo.InvariantCulture));
        
        _text.fontSize = fontSize;
        textTans.position = pos + textOffset;
        textTans.LookAt(pos - (cameraPos - pos));
    }

    private void OnDestroy()
    {
        if (!_text) return;
        
        Destroy(_text.gameObject);
    }
}