using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Guidebook.CountrySelection;

public class ClosePanelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _panel;

    void Start()
    {
        if (_button == null)
        {
            _button = GetComponent<Button>();
        }

        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        UiController.Instance.HideSelectedCountriesPanel();
        //_panel.SetActive(false);
    }
}
