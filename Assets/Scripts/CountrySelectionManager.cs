using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guidebook.Countries;

namespace Guidebook.CountrySelection
{
    public enum ClickType
    {
        Tap, 
        LongTap
    }

    public class CountrySelectionManager
    {
        #region CountrySelectionManager singleton
        private static CountrySelectionManager instance;
        public static CountrySelectionManager Instance
        {
            get
            {
                if(instance == null)
                    instance = new CountrySelectionManager();
                return instance;
            }
        }
        #endregion

        List<Country> selectedCountriesList = new List<Country>();
        Country lastClickedCountry = null;

        public void ChangeSelectionStatus(Country country)
        {
            if(!selectedCountriesList.Remove(country))
                selectedCountriesList.Add(country);
            UpdateCountriesSelectingUi();
            Debug.Log(GetSelectedCountriesListSize());
        }

        public int GetSelectedCountriesListSize()
        {
            return selectedCountriesList.Count;
        }

        public void ClearList()
        {
            while(GetSelectedCountriesListSize() != 0)
            {
                Debug.Log(GetSelectedCountriesListSize());
                selectedCountriesList[0].ChangeTexture();
                selectedCountriesList[0].ChangeSelecting();
                selectedCountriesList.RemoveAt(0);
                Debug.Log(GetSelectedCountriesListSize());
            }
            selectedCountriesList.Clear();
            UpdateCountriesSelectingUi();
        }

        public void UpdateCountriesSelectingUi()
        {
            if(GetSelectedCountriesListSize() >= 1)
                UiController.Instance.ShowSelectionPanel();
            else
                UiController.Instance.HideSelectionPanel();

            if(GetSelectedCountriesListSize() >= 2)
                UiController.Instance.ShowClearButton();
            else
                UiController.Instance.HideClearButton();
        }

        public void ManageCountryClick(ClickType clickType, Country country)
        {
            switch(clickType)
            {
                case ClickType.Tap:
                    if(GetSelectedCountriesListSize() == 0)
                    {
                        if(lastClickedCountry) 
                        {
                            lastClickedCountry.ChangeTexture();
                        }
                            
                        if(country != lastClickedCountry)
                        {
                            UiController.Instance.ShowCountryInfo(country);
                            lastClickedCountry = country;
                            country.ChangeTexture();
                        }
                        else
                        {
                            UiController.Instance.HideCountryInfo();
                            lastClickedCountry = null;
                        }
                    }
                    break;

                case ClickType.LongTap:
                    if (lastClickedCountry == null)
                    {
                        country.ChangeTexture();
                        country.ChangeSelecting();
                        ChangeSelectionStatus(country);
                        UpdateCountriesSelectingUi();
                        break;
                    }
                    else
                    {
                        if (lastClickedCountry == country)
                        {
                            UiController.Instance.HideCountryInfo();
                            country.ChangeSelecting();
                            ChangeSelectionStatus(country);
                            UpdateCountriesSelectingUi();
                            lastClickedCountry = null;
                            break;
                        }
                        if (lastClickedCountry != country)
                        {
                            lastClickedCountry.ChangeTexture();
                            country.ChangeSelecting();
                            country.ChangeTexture();
                            ChangeSelectionStatus(country);
                            UpdateCountriesSelectingUi();
                            lastClickedCountry = null;
                            break;
                        }
                    }
                    break;
            }
        }

        public List<Country> CopySelectedCountriesList()
        {
            List<Country> newCountriesList = new List<Country>(selectedCountriesList);
            return newCountriesList;
        }
    }
}