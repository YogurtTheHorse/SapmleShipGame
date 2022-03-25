using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyShipUiLabel : MonoBehaviour
{
    public float horizontalEllipseCoefficient = 0.9f;
    public float verticalEllipseCoefficient = 0.9f;

    public Image planeImagePrefab;

    public float fontSize = 12;

    public Vector3 textOffset = Vector3.zero;

    [Inject(Id = "player")]
    private GameObject _player;

    [Inject(Id = "icons-canvas")]
    private Canvas _iconsCanvas;

    [Inject(Id = "distance-canvas")]
    private Canvas _distanceCanvas;

    private RectTransform _iconsCanvasRect;

    private TextMeshPro _text;
    private Image _planeImage;
    private Renderer _renderer;

    private void Start()
    {
        _text = CreateText();
        _renderer = GetComponent<Renderer>();
        _iconsCanvasRect = _iconsCanvas.GetComponent<RectTransform>();
        _planeImage = Instantiate(planeImagePrefab, _iconsCanvas.transform);
    }

    private TextMeshPro CreateText()
    {
        var go = new GameObject("Distance label");
        var textMeshPro = go.AddComponent<TextMeshPro>();
        textMeshPro.alignment = TextAlignmentOptions.Center;

        go.transform.parent = _distanceCanvas.transform;

        return textMeshPro;
    }

    private void Update()
    {
        if (!Camera.main || !_text)
            return;

        var isVisible = _renderer.isVisible;

        UpdateDistanceText();

        _planeImage.gameObject.SetActive(!isVisible);

        if (!isVisible)
        {
            var ellipseRHorizontal = Screen.width * horizontalEllipseCoefficient * 0.5f;
            var ellipseRVertical = Screen.height * verticalEllipseCoefficient * 0.5f;

            var posInCamera = Camera.main.WorldToScreenPoint(transform.position);
            var screePos = (Vector2)posInCamera;
            var isBehind = posInCamera.z < 0;
            
            var posFromCenter = new Vector2(-Screen.width * 0.5f, -Screen.height * 0.5f) + screePos;
            var ellipseAngle = Vector2.Angle(Vector2.right, posFromCenter) * Mathf.Deg2Rad;
            var maxDistanceFromCenterScreen = ellipseRHorizontal // ellipse radius
                                              * ellipseRVertical
                                              / Mathf.Sqrt(
                                                  Mathf.Pow(ellipseRHorizontal * Mathf.Sin(ellipseAngle), 2)
                                                  + Mathf.Pow(ellipseRVertical * Mathf.Cos(ellipseAngle), 2)
                                              );

            var div = posFromCenter.magnitude / maxDistanceFromCenterScreen;

            if (isBehind)
            {
                div *= -1;
            }
            
            _planeImage.rectTransform.position = new Vector2(Screen.width, Screen.height) * 0.5f + posFromCenter / div;
        }
    }

    private void UpdateDistanceText()
    {
        var pos = transform.position;
        var cameraPos = Camera.main!.transform.position;
        var textTans = _text.transform;
        var distance = (_player.transform.position - pos).magnitude;

        _text.SetText(Mathf.Round(distance).ToString(CultureInfo.InvariantCulture));

        _text.fontSize = fontSize;
        textTans.position = pos + textOffset;
        textTans.LookAt(pos - (cameraPos - pos));
    }

    private void OnDestroy()
    {
        if (!_text)
            return;

        Destroy(_text.gameObject);
    }
}