﻿namespace Kpi.MetaUa.ClientTests.Model.Platform.WebElements
{
    public interface ISelectBox
    {
        void OpenedDropdown();

        void SetValue(string value);

        string GetSelected();

        string[] GetValues();
    }
}
