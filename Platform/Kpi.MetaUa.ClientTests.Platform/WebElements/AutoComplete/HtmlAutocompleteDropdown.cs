﻿using System.Linq;
using Kpi.MetaUa.ClientTests.Model.Platform.Locator;
using Kpi.MetaUa.ClientTests.Model.Platform.WebElements.AutoComplete;
using Kpi.MetaUa.ClientTests.Platform.Element;
using OpenQA.Selenium.Support.PageObjects;

namespace Kpi.MetaUa.ClientTests.Platform.WebElements.AutoComplete
{
    public class HtmlAutocompleteDropdown : HtmlElement, IHtmlAutocompleteDropdown
    {
        private HtmlLink[] Items =>
            FindAll<HtmlLink>(new Locator(How.XPath, ".//div[@id='autocomplete-results']")).ToArray();

        public HtmlLink[] GetItems() => Items;

        public string[] GetValues() => Items.Select(x => x.GetText()).ToArray();

        public void Select(string value) => Items.First(i => i.GetText().Equals(value)).Click();
    }
}
