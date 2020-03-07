using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Guidebook.Countries;
using Guidebook.CountrySelection;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject countryDataPanel;
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private GameObject selectedCountriesPanel;
    [SerializeField] private GameObject clearButton;
    [SerializeField] private Text countryNameText;
    [SerializeField] private Text countryAreaText;
    [SerializeField] private Text countryGdpText;
    [SerializeField] private Text countryPopulationText;
    [SerializeField] private Animator countryDataPanelAnimator;
    [SerializeField] private Animator selectionPanelAnimator;
    [SerializeField] private Animator selectedCountriesPanelAnimator;

    #region UiController singleton
    private static UiController instance;

    public static UiController Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject _ = GameObject.Find("Canvas");
                instance = _.GetComponent( typeof(UiController) ) as UiController;
            }
            return instance;
        }
    }
    #endregion

    public void ShowOnMapUi()
    {
        if (countryDataPanel.activeSelf)
            countryDataPanelAnimator.Play("ShowCountryDataPanel");
        if (selectionPanel.activeSelf)
            selectionPanelAnimator.Play("ShowSelectedPanel");
    }

    public void HideOnMapUi()
    {
        if(countryDataPanel.activeSelf)
            countryDataPanelAnimator.Play("HideCountryDataPanel");
        if (selectionPanel.activeSelf)
            selectionPanelAnimator.Play("HideSelectedPanel");
    }

    public void ShowSelectionPanel()
    {
        selectionPanel.SetActive(true);
    }

    public void HideSelectionPanel()
    {
        selectionPanelAnimator.Play("HideSelectedPanel");
        StartCoroutine(SetPanelUnactive(
            selectionPanelAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length,
            selectionPanel));
    }

    public void ShowClearButton()
    {
        clearButton.SetActive(true);
    }

    public void HideClearButton()
    {
        clearButton.SetActive(false);
    }

    public void ShowSelectedCountriesPanel()
    {
        selectedCountriesPanel.SetActive(true);
    }

    public void HideSelectedCountriesPanel()
    {
        selectedCountriesPanelAnimator.Play("HideSelectedCountriesPanel");
        StartCoroutine(SetPanelUnactive(
            selectedCountriesPanelAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length,
            selectedCountriesPanel));
    }

    public void ShowCountryInfo(Country country)
    {
        countryDataPanel.SetActive(true);
        countryNameText.text = country.countryName.ToString();
        countryAreaText.text = string.Format("Area: {0} sq.km..", country.countryArea);
        countryGdpText.text = string.Format("GDP: ${0} billion", country.countryGdp);
        countryPopulationText.text = string.Format("Population: {0}", country.countryPopulation);
    }

    public void HideCountryInfo()
    {
        
        countryDataPanelAnimator.Play("HideCountryDataPanel");
        StartCoroutine(SetPanelUnactive(
            countryDataPanelAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length,
            countryDataPanel));
    }

    private IEnumerator SetPanelUnactive(float time, GameObject panel)
    {
        yield return new WaitForSeconds(time);
        panel.SetActive(false);
    }
}
