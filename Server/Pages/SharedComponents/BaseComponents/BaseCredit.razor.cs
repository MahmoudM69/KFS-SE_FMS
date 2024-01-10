using LanguageExt.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Server.Helpers.Extensions;
using Server.Pages.SharedComponents.NewComponents;
using Server.Shared;
using Common.Interfaces.DTOs;
using System;
using System.Threading.Tasks;

namespace Server.Pages.Components.BaseComponents;

public partial class BaseCredit<T> : CancellableComponent where T : IBaseDTO, ISoftDeletableDTO, new()
{
    T _dto = new();
    private int _loadingStatus;
    private LoadingButton _submit = new();
    private LoadingButton _reset = new();

    [Inject]
    public ISnackbar? Snackbar { get; set; }

    [CascadingParameter]
    public Task<AuthenticationState>? AuthenticationState { get; set; }

    [Parameter, EditorRequired]
    public Func<Task<Result<T>>>? Prepare { get; set; }

    [Parameter, EditorRequired]
    public Func<T, Task<Result<T>>>? Submit { get; set; }

    [Parameter]
    public Func<Task<Result<T>>>? Reset { get; set; }

    [Parameter]
    public Result<T> DTOResult { get; set; } = new(new T());

    [Parameter, EditorRequired]
    public bool IsEdit { get; set; } = false;

    [Parameter]
    public string EntityName { get; set; } = "Entity";

    [Parameter]
    public RenderFragment<T>? ChildContent { get; set; }

    [Parameter]
    public string ContainerClass { get; set; } = "pa-4 gap-2";
    [Parameter]
    public string ContainerStyle { get; set; } = string.Empty;
    [Parameter]
    public string ActionBtnContainerClass { get; set; } = "d-flex justify-end flex-grow-1 gap-4";
    [Parameter]
    public string ActionBtnContainerStyle { get; set; } = string.Empty;
    [Parameter]
    public string ActionBtnClass { get; set; } = string.Empty;
    [Parameter]
    public string ActionBtnStyle { get; set; } = string.Empty;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) await PrepareEntity();

        _loadingStatus = (ChildContent is null) ? 2 : _loadingStatus;
    }

    private async Task PrepareEntity()
    {
        try
        {
            if (Prepare is not null) DTOResult = await Prepare();
            else if(IsEdit) _loadingStatus = 2;

            _dto = DTOResult.Match(succ => succ, HandleFailPrepare);

            _loadingStatus = (_dto is null || _loadingStatus == 2) ? 2 : 1;

            StateHasChanged();
        }
        catch (Exception ex)
        {
            _dto = new();
            _loadingStatus = 2;
            Console.WriteLine(ex.Message);
        }
    }
    T HandleFailPrepare(Exception excp)
    {
        _loadingStatus = 2;
        return new();
    }

    private async Task SubmitEntity()
    {
        if (Submit != null) await Submit(_dto);
        _submit.ToDefault();
    }

    public async Task ResetEntity()
    {
        if (Reset != null) await Reset();
        else await PrepareEntity();
    }

    void ShowSnackbar(string message, Severity severity,
        string? actionText = null, Color actionColor = Color.Inherit, Func<Task>? OnClick = null)
    {
        Snackbar.ShowSnackbar(message, severity, actionText, actionColor, OnClick);
    }

    //T HandleSubmitting(T succ)
    //{
    //    NavMan!.NavigateTo($"/{EntityName}/Details/{succ.Id}");
    //    return succ;
    //}
    //T HandleSubmitting(Exception excp)
    //{
    //    ShowSnackbar((Id == null) ? $"Error Creating the {EntityName}" : $"Error Editing the {EntityName}", Severity.Error);
    //    return null!;
    //}
}
