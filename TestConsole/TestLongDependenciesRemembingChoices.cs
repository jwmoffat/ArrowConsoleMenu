using System;
using System.Collections.Generic;
using ArrowConsoleMenu;

namespace TestConsole
{
    /// <summary>
    /// Used in case of selecting an item on page other than the first page.  
    /// - Going back into menu should remember it.
    /// - Choosing another dependent menu item choice (ie: results in new list shown) should restart at 1 (not remembering entry #)
    /// </summary>
    static class TestLongDependenciesRemembingChoices
    {
        public static Menu GetExampleMenu()
        {
            var setCountryChoice = new MenuChoices<string>("Set Country", new List<string> { "Canada", "USA" });
            var setLocalRegionChoice = new MenuChoices<string>("Set Local Region", () => GetLocalRegionsFor(setCountryChoice.SelectedItem)) {PageSize = 5};

            var menu = new Menu("Dependent Choice Menu");
            menu.AddChoices(setCountryChoice);
            menu.AddChoices(setLocalRegionChoice);
            menu.AddCommand("Print Current Region", () => { Console.WriteLine($"Current region = {setLocalRegionChoice.SelectedItem}"); });
            return menu;
        }

        static List<string> GetLocalRegionsFor(string country)
        {
            switch (country)
            {
                case "Canada":
                    return new List<string> { "AB", "BC","MB","NB","NL","NS","NT","NU","ON","PE","QC","SK","YT", };    // etc..
                case "USA":
                    return new List<string>
                    {
                        "AL","AK","AS","AZ",
                        "AR","CA","CO","CT","DE",
                        "DC","FL","GA","GU","HI",
                        "ID","IL","IN","IA","KS",
                        "KY","LA","ME","MD","MH",
                        "MA","MI","FM","MN","MS",
                        "MO","MT","NE","NV","NH",
                        "NJ","NM","NY","NC","ND",
                        "MP","OH","OK","OR","PW",
                        "PA","PR","RI","SC","SD",
                        "TN","TX","UT","VT","VA",
                        "VI","WA","WV","WI","WY",
                    };
                default:
                    throw new NotImplementedException();
            }
        }
    }
}