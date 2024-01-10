using Humanizer;
using LanguageExt.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using Server.Helpers.Extensions;
using Server.Shared;
using Common.Interfaces.DTOs;
using System;
using System.Threading.Tasks;

namespace Server.Pages.Components.BaseComponents;

public partial class BaseDetails<T> : CancellableComponent where T : class, IBaseDTO, ISoftDeletableDTO, new()
{
    T _dto = new();
    int _loadingStatus;

    [Inject]
    public ISnackbar? Snackbar { get; set; }
    [Inject]
    public IDialogService? DialogService { get; set; }

    [Parameter, EditorRequired]
    public string EntityName { get; set; } = string.Empty;

    [Parameter, EditorRequired]
    public Result<T> DTOResult { get; set; } = new();

    [Parameter]
    public Func<Task<Result<T>>>? Prepare { get; set; }

    [Parameter]
    public Func<int, Task>? Edit { get; set; }

    [Parameter]
    public Func<int, Task>? SoftDelete { get; set; }

    [Parameter]
    public Func<int, Task>? Recover { get; set; }

    [Parameter]
    public Func<int, Task>? HardDelete { get; set; }

    [Parameter]
    public RenderFragment<T>? ChildContent { get; set; }

    [Parameter]
    public string ContainerClass { get; set; } = "pa-4 gap-2";
    [Parameter]
    public string ContainerStyle { get; set; } = string.Empty;
    [Parameter]
    public string ContentClass { get; set; } = "d-flex justify-end flex-grow-1 gap-2";
    [Parameter]
    public string ContentStyle { get; set; } = string.Empty;
    [Parameter]
    public string ActionBtnContainerClass { get; set; } = string.Empty;
    [Parameter]
    public string ActionBtnContainerStyle { get; set; } = string.Empty;
    [Parameter]
    public string ActionBtnClass { get; set; } = string.Empty;
    [Parameter]
    public string ActionBtnStyle { get; set; } = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        await PrepareEntity();

        _loadingStatus = (ChildContent is null) ? 2 : _loadingStatus;

        await base.OnParametersSetAsync();
    }

    private async Task PrepareEntity()
    {
        try
        {
            if (Prepare is not null) DTOResult = await Prepare();
            else _loadingStatus = 2;

            _dto = DTOResult.Match(succ => succ, HandleFailEntity);
            _loadingStatus = (_loadingStatus == 2) ? 2 : 1;
        }
        catch (Exception ex)
        {
            _dto = new();
            _loadingStatus = 2;
            Console.WriteLine(ex.Message);
        }
    }

    T HandleFailEntity(Exception excp)
    {
        _loadingStatus = 2;
        return HandleFailEntity(excp, $"Error: Couldn't get the required data for the {EntityName.Pluralize()}.");
    }
    T HandleFailEntity(Exception excp, string snackbarMessage, Severity snackbarSeverity = Severity.Error)
    {
        HandleException(excp, snackbarMessage, snackbarSeverity);
        return new();
    }
    void HandleException(Exception excp, string snackbarMessage, Severity snackbarSeverity = Severity.Error)
    {
        Console.WriteLine(excp.Message);
        Snackbar.ShowSnackbar(snackbarMessage, snackbarSeverity);
    }

    #region SD
    //public async Task EditEntity()
    //{
    //    try
    //    {
    //        if (Edit is not null) await Edit(_dto.Id);
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //        Snackbar.ShowSnackbar($"Error: Couldn't delete the {EntityName}.", Severity.Error);
    //    }
    //}
    //public async Task SoftDeleteEntity()
    //{
    //    try
    //    {
    //        if (SoftDelete == null || _dto.SoftDelete >= 0) return;

    //        bool delete = false;
    //        Snackbar.ShowSnackbar($"ARE YOU SURE YOU WANT TO DELETE THIS {EntityName.ToUpper()}?", Severity.Success,
    //            "YES", Color.Inherit, () => { delete = true; return Task.CompletedTask; });

    //        if (delete) await SoftDelete(_dto.Id);
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //        Snackbar.ShowSnackbar($"Error: Couldn't delete the {EntityName}.", Severity.Error);
    //    }
    //}
    //public async Task RecoverEntity()
    //{
    //    try
    //    {

    //        if (Recover == null || _dto.SoftDelete < 0) return;

    //        bool recover = false;
    //        Snackbar.ShowSnackbar($"Are you sure you want to recover this {EntityName}?", Severity.Normal,
    //            "YES", Color.Inherit, () => { recover = true; return Task.CompletedTask; });
    //        if (recover) await Recover(_dto.Id);
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //        Snackbar.ShowSnackbar($"Error: Couldn't delete the {EntityName}.", Severity.Error);
    //    }
    //}
    //public async Task HardDeleteEntity()
    //{
    //    try
    //    {
    //        if (HardDelete == null || _dto.SoftDelete < 0) return;

    //        bool delete = false;
    //        Snackbar.ShowSnackbar($"ARE YOU SURE YOU WANT TO DELETE THIS {EntityName.ToUpper()}?", Severity.Error,
    //            "YES", Color.Inherit, () => { delete = true; return Task.CompletedTask; });
    //        if (!delete) return;

    //        var parameters = new DialogParameters<Dialog>()
    //        {
    //            { x => x.Header, $"DELETING {EntityName.ToUpper()}" },
    //            { x => x.Content, $"ARE YOU SURE YOU WANT TO DELETE THIS {EntityName.ToUpper()} FOREVER?" },
    //            { x => x.ButtonText, "YES DELETE!" },
    //            { x => x.ButtonColor, Color.Error },
    //        };

    //        var dialog = await DialogService!.ShowAsync<Dialog>("FINAL WARNING", parameters);
    //        var result = await dialog.Result;

    //        if (!result.Canceled && (bool)result.Data)
    //        {
    //            await HardDelete(_dto.Id);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.Message);
    //        Snackbar.ShowSnackbar($"Error: Couldn't delete the {EntityName}.", Severity.Error);
    //    }
    //}
    #endregion
}
