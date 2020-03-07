using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Guidebook.CountrySelection;

public class ClearButton : MonoBehaviour
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
        CountrySelectionManager.Instance.ClearList();
    }
}
