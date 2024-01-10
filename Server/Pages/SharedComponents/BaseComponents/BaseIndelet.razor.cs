using Humanizer;
using LanguageExt.Common;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Server.Helpers.Extensions;
using Server.Shared;
using Common.Enums;
using Common.Interfaces.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.Components.BaseComponents;

public partial class BaseIndelet<T> : CancellableComponent where T : class, IBaseDTO, ISoftDeletableDTO, new()
{
    int _loadingStatus;
    ItemMode itemMode = ItemMode.Normal;
    IEnumerable<T> _dtos = Enumerable.Empty<T>();

    [Inject]
    public ISnackbar? Snackbar { get; set; }
    [Inject]
    public IDialogService? DialogService { get; set; }


    [Parameter, EditorRequired]
    public RenderFragment<IEnumerable<T>>? ChildContent { get; set; }

    [Parameter, EditorRequired]
    public string EntityName { get; set; } = string.Empty;

    [Parameter, EditorRequired]
    public Result<IEnumerable<T>> DTOsResults { get; set; }

    [Parameter, EditorRequired]
    public Func<ItemMode, Task<Result<IEnumerable<T>>>>? Prepare { get; set; }
    [Parameter]
    public EventCallback<bool> ViewStyleChangedEvent { get; set; }

    [Parameter]
    public Func<string, Task<IEnumerable<string>>>? Search { get; set; }
    [Parameter]
    public IEnumerable<string> DTONames { get; set; } = Enumerable.Empty<string>();

    [Parameter]
    public Action? Create { get; set; }

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

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            await PrepareEntity(itemMode);

            _loadingStatus = (ChildContent is null) ? 2 : _loadingStatus;
        }
        catch (Exception ex)
        {
            _dtos = Enumerable.Empty<T>();
            _loadingStatus = 2;
            ShowException(ex, ex.Message);
        }
    }

    private async Task PrepareEntity(ItemMode mode, bool forceReload = false)
    {
        try
        {
            itemMode = mode;

            if (forceReload || DTOsResults.IsBottom || DTOsResults.IsFaulted)
            {
                if (Prepare != null) DTOsResults = await Prepare(itemMode);
                else _loadingStatus = 2;
            }

            _dtos = DTOsResults.Match(succ => succ, HandleFailEntities);

            _loadingStatus = (_dtos is null || _loadingStatus == 2) ? 2 : 1;

            StateHasChanged();
        }
        catch (Exception ex)
        {
            _dtos = Enumerable.Empty<T>();
            _loadingStatus = 2;
            ShowException(ex, ex.Message);
        }
    }

    private async Task SearchEntity(string name)
    {
        try
        {
            if (Search != null) DTONames = await Search(name);
        }
        catch (Exception ex)
        {
            ShowException(ex, $"An error occurred when searching for the {EntityName.ToLower()}.");
        }
    }

    IEnumerable<T> HandleFailEntities(Exception excp)
    {
        _loadingStatus = 2;
        return HandleFailEntities(excp, $"Error: Couldn't get the required data for the {EntityName.Pluralize()}.");
    }
    IEnumerable<T> HandleFailEntities(Exception excp, string snackbarMessage, Severity snackbarSeverity = Severity.Error)
    {
        ShowException(excp, snackbarMessage, snackbarSeverity);
        return Enumerable.Empty<T>();
    }
    void ShowException(Exception excp, string snackbarMessage, Severity snackbarSeverity = Severity.Error)
    {
        Console.WriteLine(excp.Message);
        Snackbar.ShowSnackbar(snackbarMessage, snackbarSeverity);
    }

    #region SD
    //[Parameter]
    //public Action<int>? Edit { get; set; }
    //[Parameter]
    //public Action<int>? SoftDelete { get; set; }
    //[Parameter]
    //public Action<int>? Recover { get; set; }
    //[Parameter]
    //public Action<int>? HardDelete { get; set; }


    //private void EditEntity(int id)
    //{
    //    if (Edit != null) Edit(id);
    //}
    //private void SoftDeleteEntity(int id)
    //{
    //    try
    //    {
    //        if (SoftDelete == null) return;

    //        bool delete = false;
    //        ShowSnackbar($"ARE YOU SURE YOU WANT TO DELETE THIS {EntityName.ToUpper()}?", Severity.Success,
    //            "YES", Color.Inherit, () => { delete = true; return Task.CompletedTask; });

    //        if (delete) SoftDelete(id);

    //        //_ = (await SoftDeleteAsync(id, CancellationToken)).Match(
    //        //        succ =>
    //        //        {
    //        //            ShowSnackbar($"The {EntityName} was successfully deleted!", Severity.Success);
    //        //            NavMan!.NavigateTo($"/{EntityName}");
    //        //            return succ;
    //        //        },
    //        //        excp => HandleFailEntity(excp, $"Error: Couldn't delete the {EntityName}.")
    //        //    );
    //    }
    //    catch (Exception ex)
    //    {
    //        HandleException(ex, $"Error: Couldn't delete the {EntityName}.");
    //    }
    //}
    //public void RecoverEntity(int id)
    //{
    //    try
    //    {
    //        if (Recover == null) return;

    //        bool recover = false;
    //        ShowSnackbar($"Are you sure you want to recover this {EntityName}?", Severity.Normal,
    //            "YES", Color.Inherit, () => { recover = true; return Task.CompletedTask; });

    //        if (recover) Recover(id);

    //        //_ = (await RecoverAsync(id, CancellationToken)).Match(
    //        //    succ =>
    //        //    {
    //        //        ShowSnackbar($"The {EntityName} was successfully recovered!", Severity.Success,
    //        //            "Go Back?", Color.Inherit, () => { NavMan!.NavigateTo($"/{EntityName}"); ; return Task.CompletedTask; });
    //        //        return succ;
    //        //    },
    //        //    excp => HandleFailEntity(excp, $"Error: Couldn't recover the {EntityName}.")
    //        //);
    //    }
    //    catch (Exception ex)
    //    {
    //        HandleException(ex, $"Error: Couldn't recover the {EntityName}.");
    //    }
    //}
    //public async Task HardDeleteEntity(int id)
    //{
    //    try
    //    {
    //        if (HardDelete == null) return;

    //        bool delete = false;
    //        ShowSnackbar($"ARE YOU SURE YOU WANT TO DELETE THIS {EntityName.ToUpper()}?", Severity.Error,
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
    //            HardDelete(id);

    //            //_ = (await DeleteAsync(id, CancellationToken)).Match(
    //            //    succ =>
    //            //    {
    //            //        ShowSnackbar($"The {EntityName} was successfully deleted!", Severity.Success);
    //            //        NavMan!.NavigateTo($"/{EntityName}");
    //            //        return succ;
    //            //    },
    //            //    excp => HandleFailEntity(excp, $"Error: Couldn't delete the {EntityName}.")
    //            //);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        HandleException(ex, $"Error: Couldn't delete the {EntityName}.");
    //    }
    //}
    #endregion
}
