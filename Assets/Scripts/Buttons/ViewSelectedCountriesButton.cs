using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Guidebook.CountrySelection;

public class ViewSelectedCountriesButton : MonoBehaviour
{
    [SerializeField] private Button _button;

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
        UiController.Instance.ShowSelectedCountriesPanel();
        CountryListDisplay.Instance.InitializeDisplay(CountrySelectionManager.Instance.CopySelectedCountriesList());
    }
}
