﻿namespace Kpi.MetaUa.ClientTests.Model.Platform.WebElements
{
    public interface IDropDown
    {
        string[] GetOptions();

        void Choose(string option);
    }
}
