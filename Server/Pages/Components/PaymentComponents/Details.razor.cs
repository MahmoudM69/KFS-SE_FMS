using Business.IServices.IPaymentServices;
using DTO.DTOs.OrderDTOs;
using DTO.DTOs.PaymentDTOs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Server.Pages.SharedComponents.NewComponents;
using Server.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Server.Helpers.Extensions;
using System.Linq;
using Business.IServices.IOrderServices;

namespace Server.Pages.Components.PaymentComponents;

public partial class Details : CancellableComponent
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
    public int Id { get; set; }

    [Parameter]
    public PaymentDTO? PaymentDTO { get; set; } = new();

    [Parameter]
    public IEnumerable<OrderDTO>? OrderDTOs { get; set; }

    [Parameter]
    public PaymentServiceDTO? PaymentServiceDTO { get; set; }

    int LoadingStatus;
    LoadingButton? SoftDeleteBtn;
    LoadingButton? RecoverBtn;
    LoadingButton? HardDeleteBtn;
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
            PaymentDTO ??= (await PaymentDTOService!.GetByIdAsync(Id, cancellationToken: base.CancellationToken)).Match(
                    succ => succ,
                    HandleFailPrepare<PaymentDTO>
                );
            PaymentServiceDTO ??= PaymentDTO.PaymentServiceDTO ??
                (await PaymentServiceDTOService!.GetByIdAsync(PaymentDTO.Id, cancellationToken: base.CancellationToken)).Match(
                        succ => succ,
                        HandleFailPrepare<PaymentServiceDTO>
                    );
            OrderDTOs ??= PaymentDTO.OrderDTOs ??
                (await OrderDTOService!.FindAsync(x => x.PaymentId == PaymentDTO.Id, cancellationToken: base.CancellationToken)).Match(
                        succ => succ,
                        HandleFailPrepare<List<OrderDTO>>
                    );
            
            LoadingStatus = (PaymentDTO != null && PaymentServiceDTO != null && OrderDTOs != null && OrderDTOs.Any()) ? 1 : 2;
            
            PaymentDTO!.PaymentServiceDTO = PaymentServiceDTO;
            PaymentDTO.OrderDTOs = OrderDTOs;
        }
        catch (Exception ex)
        {
            PaymentDTO = null;
            PaymentServiceDTO = null;
            OrderDTOs = null;
            LoadingStatus = 2;
            Console.WriteLine(ex.Message);
        }
    }
    R HandleFailPrepare<R>(Exception excp) where R : class
    {
        LoadingStatus = 2;
        return null!;
    }


    public async Task SoftDelete()
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
            _ = (await PaymentDTOService!.SoftDeleteAsync(PaymentDTO!.Id)).Match(
                    succ =>
                    {
                        Snackbar.Add("The Payment was successfully deleted!", Severity.Success);
                        NavMan!.NavigateTo("/Payment");
                        return succ;
                    },
                    excp =>
                    {
                        Console.WriteLine(excp.Message);
                        Snackbar!.Add("Error: Coundn't delete the payment.", Severity.Error);
                        return null!;
                    }
                );
            SoftDeleteBtn!.ToDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Snackbar!.Add("Error: Coundn't delete the payment.", Severity.Error);
        }
    }
    public async Task Recover()
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
            _ = (await PaymentDTOService!.RecoverAsync(PaymentDTO!.Id)).Match(
                succ =>
                {
                    Snackbar!.Add("The Payment was successfully recovered!", Severity.Success, config =>
                    {
                        config.Action = "Go Back?";
                        config.ActionColor = Color.Inherit;
                        config.Onclick = snackbar =>
                        {
                            NavMan!.NavigateTo("/Payment");
                            return Task.CompletedTask;
                        };
                    });
                    return succ;
                },
                excp =>
                {
                    Console.WriteLine(excp.Message);
                    Snackbar!.Add("Error: Coundn't delete the payment.", Severity.Error);
                    return null!;
                }
            );
            RecoverBtn!.ToDefault();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Snackbar!.Add("Error: Coundn't delete the payment.", Severity.Error);
        }
    }
    public async Task HardDelete()
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
                _ = (await PaymentDTOService!.DeleteAsync(PaymentDTO!.Id)).Match(
                    succ =>
                    {
                        Snackbar!.Add("The Payment was successfully deleted!", Severity.Success);
                        NavMan!.NavigateTo("/Payment");
                        return succ;
                    },
                    excp =>
                    {
                        Console.WriteLine(excp.Message);
                        Snackbar!.Add("Error: Coundn't delete the payment.", Severity.Error);
                        return null!;
                    }
                );
                HardDeleteBtn!.ToDefault();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Snackbar!.Add("Error: Coundn't delete the payment.", Severity.Error);
        }
    }
}
