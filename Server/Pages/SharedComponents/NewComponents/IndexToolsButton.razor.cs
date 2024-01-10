using Microsoft.AspNetCore.Components;
using MudBlazor;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Pages.SharedComponents.NewComponents;

public partial class IndexToolsButton : ComponentBase
{
    bool showBar;
    bool isPined;
    bool isBarOpen;
    string fabBtnIcon = Icons.Material.Outlined.MoreVert;

    [Inject]
    public NavigationManager? NavMan { get; set; }

    [Parameter, EditorRequired]
    public IEnumerable<string> Values { get; set; } = new List<string>();
    [Parameter, EditorRequired]
    public Func<string, Task<IEnumerable<string>>>? Search { get; set; }
    [Parameter]
    public EventCallback RefreshEvent { get; set; }
    [Parameter]
    public EventCallback CreateEvent { get; set; }
    [Parameter]
    public EventCallback<bool> ViewStyleChangedEvent { get; set; }
    [Parameter]
    public EventCallback<ItemMode> ViewItemModeChangedEvent { get; set; }

    protected override void OnParametersSet() =>
        showBar = Search is not null || RefreshEvent.HasDelegate || ViewStyleChangedEvent.HasDelegate || ViewItemModeChangedEvent.HasDelegate;

    void ChangeBarState(bool isOpen, bool isClicked = false)
    {
        isPined = (isClicked) ? !isPined : isPined;
        isBarOpen = (isClicked || isPined) ? isPined : isOpen;

        fabBtnIcon = (isPined) ?
            (isOpen ? Icons.Material.Rounded.PushPin : Icons.Material.Rounded.PushPin) :
            //(isOpen?  Icons.Material.Rounded.Close : Icons.Material.Rounded.PushPin) :
            (isOpen ? Icons.Material.Rounded.PushPin : Icons.Material.Outlined.MoreVert);
    }

    async Task Create()
    {
        if (CreateEvent.HasDelegate) await CreateEvent.InvokeAsync();
    }
}
