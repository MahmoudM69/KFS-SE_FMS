//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Web;
//using MudBlazor;

//namespace Server.Shared;

//public partial class LoadingButton : ComponentBase
//{
//    [Parameter]
//    public ButtonType ButtonType { get; set; } = ButtonType.Button;

//    [Parameter]
//    public Variant Variant { get; set; } = Variant.Filled;

//    [Parameter]
//    public Color Color { get; set; } = Color.Primary;

//    [Parameter]
//    public string Class { get; set; }

//    [Parameter]
//    public string Style { get; set; }

//    [Parameter]
//    public string? Icon { get; set; }

//    [Parameter]
//    public Color IconColor { get; set; } = Color.Primary;

//    [Parameter]
//    public string Label { get; set; } = "Label";

//    [Parameter]
//    public string? Verb { get; set; }

//    [Parameter]
//    public bool IsLoading { get; set; } = false;

//    [Parameter]
//    public bool IsDisabled { get; set; } = false;

//    [Parameter]
//    public bool LoadOnClick { get; set; } = true;

//    [Parameter]
//    public bool DisableOnClick { get; set; } = true;

//    [Parameter]
//    public EventCallback<MouseEventArgs> BtnClicked { get; set; }

//    private async Task Clicked(MouseEventArgs e)
//    {
//        if (LoadOnClick) IsLoading = true;
//        if (DisableOnClick) IsDisabled = true;
//        if (BtnClicked.HasDelegate) await BtnClicked.InvokeAsync(e);
//    }

//    public void ToDefault()
//    {
//        IsLoading = false;
//        IsDisabled = false;
//    }
//}
