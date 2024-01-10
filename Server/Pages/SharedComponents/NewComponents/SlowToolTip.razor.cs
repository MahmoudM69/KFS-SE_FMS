using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Server.Pages.SharedComponents.NewComponents;

public partial class SlowToolTip : ComponentBase
{
    [Parameter]
    public bool ShowToolTip { get; set; } = false;
    [Parameter]
    public EventCallback<bool> ShowToolTipChanged { get; set; }
    [Parameter]
    public double Duration { get; set; } = 150;
    [Parameter]
    public string Text { get; set; } = "Text";
    [Parameter]
    public bool Arrow { get; set; } = true;
    [Parameter]
    public RenderFragment? Item { get; set; }
    [Parameter]
    public RenderFragment? Tip { get; set; }

    [Parameter]
    public bool Lock { get; set; } = false;

    bool _mouseOn = false;
    bool _mouseOnItem = false;

    private async Task MouseOn()
    {
        _mouseOn = true;
        await ChangeVisiblity();
    }
    private async Task MouseOut()
    {
        _mouseOn = false;
        await ChangeVisiblity();
    }
    private async Task MouseOnItem()
    {
        _mouseOnItem = true;
        await ChangeVisiblity();
    }
    private async Task MouseOutItem()
    {
        _mouseOnItem = false;
        await ChangeVisiblity();
    }

    public async Task ChangeVisiblity()
    {
        if (!_mouseOnItem && !_mouseOn) await Task.Delay(250);
        if (_mouseOn || _mouseOnItem)
        {
            ShowToolTip = true;
        }
        else if (!Lock)
        {
            ShowToolTip = false;
        }
        await ShowToolTipChanged.InvokeAsync(ShowToolTip);
    }
}
