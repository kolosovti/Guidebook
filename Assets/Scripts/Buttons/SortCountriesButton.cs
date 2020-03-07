using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Guidebook.Countries;

public class SortCountriesButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private CountryFields sortByType;
    private bool reverseSort;

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
        CountryListDisplay.Instance.SortList(sortByType, reverseSort);
        reverseSort = reverseSort ? false : true;
    }
}
