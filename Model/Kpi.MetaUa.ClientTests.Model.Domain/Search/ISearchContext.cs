﻿namespace Kpi.MetaUa.ClientTests.Model.Domain.Search
{
    public interface ISearchContext
    {
        void OpenAndSetValue(string value);

        void Search();

        string SearchResultStatisticsText();
    }
}
