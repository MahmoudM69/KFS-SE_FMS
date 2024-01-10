using Business.IServices.IOrderServices;
using Business.IServices.IPaymentServices;
using DTO.DTOs.PaymentDTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Server.Pages.SharedComponents.NewComponents;
using Server.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Helpers.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Linq;

namespace Server.Pages.Components.PaymentComponents;

public partial class Indelet : CancellableComponent
{
    [Inject]
    public NavigationManager? NavMan { get; }
    [Inject]
    public ISnackbar? Snackbar { get; }
    [Inject]
    public IDialogService? DialogService { get; set; }
    [Inject]
    public IPaymentDTOService? PaymentDTOService { get; }
    [Inject]
    public IPaymentServiceDTOService? PaymentServiceDTOService { get; }
    [Inject]
    public IOrderDTOService? OrderDTOService { get; set; }

    [CascadingParameter]
    public Task<AuthenticationState>? AuthenticationState { get; }

    [Parameter]
    public IEnumerable<PaymentDTO>? PaymentDTOs { get; set; }

    int LoadingStatus;
    readonly LoadingButton? SoftDeleteBtn;
    readonly LoadingButton? RecoverBtn;
    readonly LoadingButton? HardDeleteBtn;
    protected override async Task OnParametersSetAsync()
    {
        await Prepare();
    }

    private async Task Prepare()
    {
        LoadingStatus = 0;
        try
        {
            var user = await AuthenticationState!.GetUserFromAuthState();
            //PaymentDTOs ??= await PaymentDTOService!.GetAllAsync(cancellationToken: base.CancellationToken) ?? throw new NotFoundException();
            LoadingStatus = (PaymentDTOs != null && PaymentDTOs.Any()) ? 1 : 2;
        }
        catch (Exception ex)
        {
            PaymentDTOs = null;
            LoadingStatus = 2;
            Console.WriteLine(ex.Message);
        }
    }

    public async Task SoftDelete(int id)
    {
        try
        {
            bool delete = false;
            Snackbar!.Add("ARE YOU SURE YOU WANT TO DELETE THIS PAYMENT?", Severity.Error, config =>
            {
                config.Action = "YES";
                config.ActionColor = Color.Inherit;
                config.Onclick = snackbar =>
                {
                    delete = true;
                    return Task.CompletedTask;
                };
            });
            if (!delete) return;
            //var deletedEntity = (await PaymentDTOService!.SoftDeleteAsync(id)) ?? throw new NotFoundException();
            SoftDeleteBtn!.ToDefault();
            Snackbar.Add("The Payment was successfully deleted!", Severity.Success);
            NavMan!.NavigateTo("/Payment");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Snackbar!.Add("Error: Coundn't delete the payment.", Severity.Error);
        }
    }
    public async Task Recover(int id)
    {
        try
        {
            bool recover = false;
            Snackbar!.Add("Are you sure you want to recover this Payment?", Severity.Normal, config =>
            {
                config.Action = "YES";
                config.ActionColor = Color.Inherit;
                config.Onclick = snackbar =>
                {
                    recover = true;
                    return Task.CompletedTask;
                };
            });
            if (!recover) return;
            //var deletedEntity = (await PaymentDTOService!.RecoverAsync(id)) ?? throw new NotFoundException();
            RecoverBtn!.ToDefault();
            Snackbar.Add("The Payment was successfully recovered!", Severity.Success, config =>
            {
                config.Action = "Go Back?";
                config.ActionColor = Color.Inherit;
                config.Onclick = snackbar =>
                {
                    NavMan!.NavigateTo("/Payment");
                    return Task.CompletedTask;
                };
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Snackbar!.Add("Error: Coundn't delete the payment.", Severity.Error);
        }
    }
    public async Task HardDelete(int id)
    {
        try
        {
            bool delete = false;
            Snackbar!.Add("ARE YOU SURE YOU WANT TO DELETE THIS PAYMENT?", Severity.Error, config =>
            {
                config.Action = "YES";
                config.ActionColor = Color.Inherit;
                config.Onclick = snackbar =>
                {
                    delete = true;
                    return Task.CompletedTask;
                };
            });
            if (!delete) return;

            var parameters = new DialogParameters<Dialog>()
            {
                { x => x.Header, "DELETING PAYMENT" },
                { x => x.Content, "ARE YOU SURE YOU WANT TO DELETE THIS PAYMENT FOREVER?" },
                { x => x.ButtonText, "YES DELETE!" },
                { x => x.ButtonColor, Color.Error },
            };

            var dialog = await DialogService!.ShowAsync<Dialog>("FINAL WARNING", parameters);
            var result = await dialog.Result;

            if (!result.Canceled && (bool)result.Data)
            {
                //var deletedEntity = (await PaymentDTOService!.DeleteAsync(id)) ?? throw new NotFoundException();
                HardDeleteBtn!.ToDefault();
                Snackbar.Add("The Payment was successfully deleted!", Severity.Success);
                NavMan!.NavigateTo("/Payment");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Snackbar!.Add("Error: Coundn't delete the payment.", Severity.Error);
        }
    }
}
