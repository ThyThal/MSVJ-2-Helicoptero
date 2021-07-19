using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Button _button;
    private TextMeshProUGUI _text;
    [SerializeField] private SceneIndex sceneIndex;
    [SerializeField] private SceneChangingScript sceneChanger;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _text.text = sceneIndex.ToString();
    }

    // Events
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.IsInteractable())
            LeanTween.scale(_text.gameObject, Vector3.one * 1.1f, .25f).setEaseOutSine();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button.IsInteractable())
            LeanTween.scale(_text.gameObject, Vector3.one, .25f).setEaseOutSine();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_button.IsInteractable())
        {
            LeanTween.scale(_text.gameObject, Vector3.one, .25f).setEaseOutBounce();
            sceneChanger.LoadScene(sceneIndex);
        }

    }
}
