﻿namespace Kpi.MetaUa.ClientTests.Model.Platform.WebElements
{
    public interface ISearchDropdown
    {
        void Search(string value);

        void SetValue(string value);

        void Select(string value);

        string[] GetValues();

        bool IsDropdownDisplayed();
    }
}
