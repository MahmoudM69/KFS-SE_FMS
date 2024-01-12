using Humanizer;
using LanguageExt;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Classes;

public class PageHistoryState : IDisposable
{
    private bool AutoRecode = true;
    private Stack<KeyValuePair<string, string>> _historyPages = new();
    public readonly NavigationManager navigationManager;

    private Stack<KeyValuePair<string, string>> HistoryPages
    {
        get { ValidateHistory(); return _historyPages; }
        set { if (value.Count > 0) _historyPages = value; else ResetHistory(); }
    }

    public PageHistoryState(NavigationManager navigationManager)
    {
        ResetHistory();
        this.navigationManager = navigationManager;
        this.navigationManager.LocationChanged += HandleLocationChanged!;
    }

    private void ValidateHistory()
    {
        if (_historyPages == null || _historyPages.Count < 1) ResetHistory();
    }
    public void ResetHistory(KeyValuePair<string, string>? home = null)
    {
        home ??= new("/", "Home");
        _historyPages = new();
        _historyPages.Push(home.Value);
    }

    private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
    {
        if (AutoRecode) AddRecord(e.Location);
        AutoRecode = true;
    }


    public Stack<KeyValuePair<string, string>> GetHistory() => HistoryPages;
    public bool CanGoBack(int pagesNumber = 1)
    {
        bool canGoBack = HistoryPages.Count > pagesNumber;
        return canGoBack;
    }
    public bool CanGoUp(int pagesNumber = 1) // => navigationManager.Uri.Replace(navigationManager.BaseUri, "").Count(x => x == '/') >= pagesNumber;
    {
        var currentUri = navigationManager.Uri.Replace(navigationManager.BaseUri, "/");
        if (currentUri[^1] != '/') currentUri = currentUri + "/";
        return currentUri.Count(x => x == '/') > pagesNumber;
    }

    public void AddRecord(string url) => AddRecord(url.Replace(navigationManager.BaseUri, "/"), url.Replace(navigationManager.BaseUri, "/").Humanize());
    public void AddRecord(string url, string name)
    {
        if (url == "" || url == "/") name = "Home";
        var previousPage = HistoryPages.Peek();
        if (previousPage.Key != url || previousPage.Value != name) HistoryPages.Push(new(url, name));
    }

    public void NavigateTo(string url, bool forceLoad = false, bool addRecord = true)
    {
        AutoRecode = addRecord;
        navigationManager.NavigateTo(url, forceLoad);
    }
    public void NavigateTo(KeyValuePair<string, string> page, bool forceLoad = false, bool addRecord = true)
    {
        AutoRecode = false;

        if (addRecord) AddRecord(page.Key, page.Value);
        navigationManager.NavigateTo(page.Key, forceLoad);
    }

    public void NavigateFromHistory(KeyValuePair<string, string> page, bool forceLoad = false)
    {
        AutoRecode = false;

        KeyValuePair<string, string> historyPage;

        while (HistoryPages.Count > 0)
        {
            historyPage = HistoryPages.Pop();

            bool urlHasValueAndExistsInHistory = string.IsNullOrWhiteSpace(page.Key) || historyPage.Key == page.Key; // if value doesn't exist then true 
            bool nameHasValueAndExistsInHistory = string.IsNullOrWhiteSpace(page.Value) || historyPage.Value == page.Value; // if value doesn't exist then true 

            if (urlHasValueAndExistsInHistory && nameHasValueAndExistsInHistory)
            {
                NavigateTo(historyPage, forceLoad, true);
                break;
            }
        }
    }

    public void NavigateBack(int pagesNumber = 1, bool forceLoad = false, bool addRecord = true)
    {
        AutoRecode = false;

        while (HistoryPages.Count > 1 && pagesNumber > 0)
        {
            HistoryPages.Pop();
            --pagesNumber;
        }

        var previousPage = HistoryPages.Pop();

        NavigateTo(previousPage, forceLoad, addRecord);
    }

    public void NavigateUp(int upsNumber = 1, bool forceLoad = false, bool addRecord = true)
    {
        AutoRecode = false;

        var currrentUri = navigationManager.Uri.Replace(navigationManager.BaseUri, "/");

        while (upsNumber > 0)
        {
            int lastSlashIndex = currrentUri.LastIndexOf('/') + 1;
            currrentUri = currrentUri[..lastSlashIndex];
            --upsNumber;
        }

        NavigateTo(currrentUri, forceLoad, addRecord);
    }

    void IDisposable.Dispose()
    {
        navigationManager.LocationChanged -= (obj, arg) => NavigateTo(arg.Location, false);
        GC.SuppressFinalize(this);
    }
}
