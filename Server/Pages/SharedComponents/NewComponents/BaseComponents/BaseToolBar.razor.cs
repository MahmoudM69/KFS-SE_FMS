using Microsoft.AspNetCore.Components;
using MudBlazor;
using Common.Classes;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.SharedComponents.NewComponents.BaseComponents;

public partial class BaseToolBar : ComponentBase
{
    [Inject]
    public PageHistoryState? HistoryState { get; set; }

    bool isPinned = true;
    bool showFuncs = false;
    bool showAdvanced = false;
    string searchIcon = Icons.Material.Rounded.Search;
    string modeBtnIcon = Icons.Material.Rounded.Folder;

    [Parameter]
    public Stack<KeyValuePair<string, string>> History { get; set; } = new();


    [Parameter]
    public string Label { get; set; } = "Item";
    [Parameter]
    public string LabelIcon { get; set; } = "";
    [Parameter]
    public Color LabelColor { get; set; } = Color.Primary;
    [Parameter]
    public RenderFragment? LabelContent { get; set; }

    [Parameter]
    public EventCallback Refresh { get; set; }
    [Parameter]
    public bool ViewStyle { get; set; } = false;
    [Parameter]
    public Action<bool>? ViewStyleChanged { get; set; }
    [Parameter]
    public ItemMode ItemMode { get; set; } = ItemMode.Normal;
    [Parameter]
    public Func<ItemMode, Task>? ItemModeChanged { get; set; }


    [Parameter]
    public Dictionary<(object, int), string> Items { get; set; } = new();
    [Parameter]
    public Dictionary<(object, int), string> SelectedItems { get; set; } = new();

    [Parameter]
    public string SearchLabel { get; set; } = "Search Item";
    [Parameter]
    public Func<string, Task<IEnumerable<string>>>? SearchFunc { get; set; }
    [Parameter]
    public string SearchValue { get; set; } = string.Empty;

    [Parameter]
    public Func<Task>? CreateFunc { get; set; }
    [Parameter]
    public Func<object, Task>? DetailsFunc { get; set; }
    [Parameter]
    public Func<object, Task>? EditFunc { get; set; }
    [Parameter]
    public Func<IEnumerable<object>, Task>? SoftDeleteFunc { get; set; }
    [Parameter]
    public Func<IEnumerable<object>, Task>? RecoverFunc { get; set; }
    [Parameter]
    public Func<IEnumerable<object>, Task>? HardDeleteFunc { get; set; }
    [Parameter]
    public bool ShowDefault { get; set; }

    [Parameter]
    public RenderFragment? AdvancedContent { get; set; }
    [Parameter]
    public bool ShowDefaultAdvanced { get; set; } = true;

    private async Task ChangeItemMode()
    {
        ItemMode = ItemMode == ItemMode.Combined ? ItemMode.Normal : ++ItemMode;

        switch (ItemMode)
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

        if (ItemModeChanged != null) await ItemModeChanged(ItemMode);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        showFuncs = CreateFunc != null || DetailsFunc != null || EditFunc != null || SoftDeleteFunc != null || HardDeleteFunc != null || RecoverFunc != null;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender) 
        {
            History = (History.Any()) ? History : HistoryState!.GetHistory();
            StateHasChanged();
        }

        base.OnAfterRender(firstRender);
    }
}
