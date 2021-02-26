namespace _02.DistrictManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DistrictManager : IDistrictManager
    {
        Dictionary<int, Country> countries = new Dictionary<int, Country>();

        Dictionary<int, District> districts = new Dictionary<int, District>();

        public void CreateDistrict(District district, Country country)
        {
            if (this.districts.ContainsKey(district.Id) || !this.countries.ContainsKey(country.Id))
            {
                throw new ArgumentException();
            }

            district.Country = country;
            this.districts.Add(district.Id, district);

            country.Districts.Add(district);
            this.countries[country.Id] = country;
        }

        public void CreateCountry(Country country)
        {
            if (this.countries.ContainsKey(country.Id))
            {
                throw new ArgumentException();
            }

            this.countries.Add(country.Id, country);
        }

        public bool Contains(District district) => this.districts.ContainsKey(district.Id);

        public bool Contains(Country country) => countries.ContainsKey(country.Id);

        public Country RemoveCountry(int id)
        {
            if (!this.countries.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var countryToRemove = this.countries[id];
            var districtsToRemove = countryToRemove.Districts;

            this.countries.Remove(id);

            foreach (var key in districtsToRemove.Select(d => d.Id))
            {
                this.districts.Remove(key);
            }

            return countryToRemove;
        }

        public District RemoveDistrict(int id)
        {
            if (!this.districts.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var districtToRemove = this.districts[id];
            var countryToUpdate = districtToRemove.Country;

            countryToUpdate.Districts.Remove(districtToRemove);

            this.countries[countryToUpdate.Id] = countryToUpdate;

            this.districts.Remove(id);

            return districtToRemove;
        }

        public int CountCountries() => countries.Count;

        public int CountDistricts() => districts.Count;

        public IEnumerable<District> GetDistricts(Country country)
        {
            if (!this.countries.ContainsKey(country.Id))
            {
                throw new ArgumentException();
            }

            if (this.countries[country.Id].Districts.Count == 0)
            {
                return new List<District>();
            }

            return this.countries[country.Id].Districts.ToList();
        }

        public IEnumerable<District> GetDistrictsOrderedBySize()
        {
            if (this.districts.Count == 0)
            {
                return new List<District>();
            }

            return this.districts.Values
                .OrderBy(d => d.SqMeters)
                .ToList();
        }

        public IEnumerable<Country> GetCountriesOrderedByPopulationThenByNameDesc()
        {
            return this.countries.Values
                .OrderBy(c => c.Population)
                .ThenByDescending(c => c.Name)
                .ToList();
        }

        public Dictionary<Country, HashSet<District>> GetCountriesAndDistrictsOrderedByDistrictsCountDescThenByCountryPopulationAsc()
        {
            Dictionary<Country, HashSet<District>> dictionary = new Dictionary<Country, HashSet<District>>();

            IEnumerable<Country> query = this.countries.Values
                .OrderByDescending(c => c.Districts.Count)
                .ThenBy(c => c.Population)
                .ToArray();

            foreach (Country country in query)
            {
                dictionary[country] = country.Districts;
            }

            return dictionary;
        }
    }
}