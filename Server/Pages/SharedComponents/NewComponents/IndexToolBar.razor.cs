using Microsoft.AspNetCore.Components;
using MudBlazor;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Pages.SharedComponents.NewComponents;

public partial class IndexToolBar : ComponentBase
{
    bool viewStyle;
    ItemMode itemMode = ItemMode.Normal;
    string value = string.Empty;
    string searchIcon = Icons.Material.Rounded.Search;
    string modeBtnIcon = Icons.Material.Rounded.Folder;

    [Parameter, EditorRequired]
    public IEnumerable<string> Values { get; set; } = new List<string>();
    [Parameter, EditorRequired]
    public Func<string, Task<IEnumerable<string>>>? Search { get; set; }
    [Parameter]
    public EventCallback RefreshEvent { get; set; }
    [Parameter]
    public EventCallback<bool> ViewStyleChangedEvent { get; set; }
    [Parameter]
    public EventCallback<ItemMode> ViewItemModeChangedEvent { get; set; }

    //private async Task<IEnumerable<string>> Search(string value)
    //{
    //    if(SearchEvent.HasDelegate) await SearchEvent.InvokeAsync(value);
    //    return Values ?? new List<string>();
    //}

    private async Task Refresh()
    {
        if (RefreshEvent.HasDelegate) await RefreshEvent.InvokeAsync();
    }

    private async Task ViewStyleChanged(bool style)
    {
        viewStyle = style;
        if (ViewStyleChangedEvent.HasDelegate) await ViewStyleChangedEvent.InvokeAsync(style);
    }

    private async Task ViewItemModeChanged()
    {
        itemMode = itemMode >= ItemMode.Combined ? ItemMode.Normal : ++itemMode;
        switch (itemMode)
        {
            case ItemMode.Deleted:
                searchIcon = Icons.Material.Rounded.SearchOff;
                modeBtnIcon = Icons.Material.Rounded.FolderDelete;
                break;
            case ItemMode.Combined:
                searchIcon = Icons.Material.Rounded.SavedSearch;
                modeBtnIcon = Icons.Material.Rounded.FolderSpecial;
                break;
            default:
                searchIcon = Icons.Material.Rounded.Search;
                modeBtnIcon = Icons.Material.Rounded.Folder;
                break;
        }
        if (ViewItemModeChangedEvent.HasDelegate) await ViewItemModeChangedEvent.InvokeAsync(itemMode);
    }
}
