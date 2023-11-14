using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class TextLabelEffect : BaseEffect
{
    [Tooltip("The label describing the piece " +
        "\nIf not referenced will try to get the first TMP_Text component in children" +
        "\nIf not found either will create one at runtime using the prefab 'TextLabelEffect_LabelPrefab' in Resources")]
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _labelText = "Lego Brick";

    private void Awake()
    {
        if (_text == null)
            _text = GetComponentInChildren<TMP_Text>();

        // if a text object is neither referenced nor found in children then add one
        if (_text == null)
        {
            GameObject textObject = Resources.Load("TextLabelEffect_LabelPrefab", typeof(GameObject)) as GameObject;

            if (textObject != null)
            {
                textObject = Instantiate(textObject);
                textObject.transform.SetParent(transform);
                _text = textObject.GetComponent<TMP_Text>();
                _text.rectTransform.anchoredPosition = Vector3.zero;
                _text.rectTransform.localPosition = Vector3.zero;
            }
        }

        if (_text != null)
        {
            _text.text = _labelText;
            _text.enabled = false;
        }
    }

    public override void DisableEffect()
    {
        if (_text != null)
            _text.enabled = false;
    }

    public override void EnableEffect(bool temp = false)
    {
        if (_text != null)
            _text.enabled = true;

        base.EnableEffect(temp);
    }
}
