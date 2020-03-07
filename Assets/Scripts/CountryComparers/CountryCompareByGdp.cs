using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guidebook.Countries;

class CountryCompareByGdp : IComparer<Country>
{
    public int Compare(Country country1, Country country2)
    {
        if (country1.countryGdp > country2.countryGdp)
            return 1;
        else if (country1.countryGdp < country2.countryGdp)
            return -1;
        else
            return 0;
    }
}