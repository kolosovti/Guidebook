using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Guidebook.Countries;
using Guidebook.CountrySelection;

public class CountryListDisplay : MonoBehaviour
{
    #region CountryListDisplay singleton
    private static CountryListDisplay instance;
    public static CountryListDisplay Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject _ = GameObject.Find("Content");
                instance = _.GetComponent(typeof(CountryListDisplay)) as CountryListDisplay;
            }
            return instance;
        }
    }
    #endregion

    [SerializeField] private GameObject contentDisplay;
    [SerializeField] private GameObject countryInfoPrefab;

    List<Country> selectedCountriesList;
    List<GameObject> instantiatedCountriesInfoPrefabs = new List<GameObject>();
    private Text[] countryTexts;

    public void InitializeDisplay(List<Country> newSelectedCountriesList)
    {
        selectedCountriesList = newSelectedCountriesList;
        while (instantiatedCountriesInfoPrefabs.Count != 0)
        {
            Destroy(instantiatedCountriesInfoPrefabs[0]);
            instantiatedCountriesInfoPrefabs.RemoveAt(0);
        }
        foreach(Country country in selectedCountriesList)
        {
            countryTexts = countryInfoPrefab.GetComponentsInChildren<Text>();

            countryTexts[0].text = country.countryName.ToString();
            countryTexts[1].text = country.countryArea.ToString();
            countryTexts[2].text = country.countryGdp.ToString();
            countryTexts[3].text = country.countryPopulation.ToString();
            instantiatedCountriesInfoPrefabs.Add(Instantiate(countryInfoPrefab, contentDisplay.transform));
        }
    }

    public void SortList(CountryFields type, bool reverseSort)
    {
        switch(type)
        {
            case CountryFields.Name:
                CountryCompareByName compareByName = new CountryCompareByName();
                selectedCountriesList.Sort(compareByName);
                break;
            case CountryFields.Area:
                CountryCompareByArea compareByArea = new CountryCompareByArea();
                selectedCountriesList.Sort(compareByArea);
                break;
            case CountryFields.Gdp:
                CountryCompareByGdp compareByGdp = new CountryCompareByGdp();
                selectedCountriesList.Sort(compareByGdp);
                break;
            case CountryFields.Population:
                CountryCompareByPopulation compareByPopulation = new CountryCompareByPopulation();
                selectedCountriesList.Sort(compareByPopulation);
                break;
        }
        if (reverseSort) selectedCountriesList.Reverse();
        InitializeDisplay(selectedCountriesList);
    }
}