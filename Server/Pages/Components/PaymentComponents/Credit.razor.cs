using Business.IServices.IPaymentServices;
using DTO.DTOs.OrderDTOs;
using DTO.DTOs.PaymentDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System;
using System.Collections.Generic;
using Server.Helpers.Extensions;
using Server.Shared;
using System.Threading.Tasks;
using Business.IServices.IOrderServices;
using Common.Exceptions;
using Server.Pages.SharedComponents.NewComponents;

namespace Server.Pages.Components.PaymentComponents;

public partial class Credit : CancellableComponent
{
    [Inject]
    public NavigationManager? NavMan {get;}
    [Inject]
    public ISnackbar? Snackbar {get;}
    [Inject]
    public IPaymentDTOService? PaymentDTOService {get;}
    [Inject]
    public IPaymentServiceDTOService? PaymentServiceDTOService {get; }
    [Inject]
    public IOrderDTOService? OrderDTOService { get; }

    
    [CascadingParameter]
    public Task<AuthenticationState>? AuthenticationState {get;}

    [Parameter]
    public int? Id { get; set; }

    [Parameter]
    public PaymentDTO? PaymentDTO { get; set; }

    [Parameter]
    public IEnumerable<OrderDTO>? OrderDTOs { get; set; }

    public IEnumerable<PaymentServiceDTO>? PaymentServiceDTOs { get; set; }

    int LoadingStatus;
    LoadingButton? Submit;
    LoadingButton? Reset;
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
            
            PaymentDTO ??= new();
            
            if (Id != null)
                PaymentDTO = (await PaymentDTOService!.GetByIdAsync(Id.Value, cancellationToken: base.CancellationToken))
                    .Match(succ => succ, HandleFailPrepare<PaymentDTO>);
            
            PaymentServiceDTOs = (LoadingStatus != 2) ? (await PaymentServiceDTOService!.GetAllAsync(
                cancellationToken: base.CancellationToken)).Match(succ => succ, HandleFailPrepare<List<PaymentServiceDTO>>) : null;
            
            OrderDTOs = (LoadingStatus != 2) ? (await OrderDTOService!.FindAsync(x => x.PaymentId == PaymentDTO!.Id,
                cancellationToken: base.CancellationToken)).Match(
                succ => 
                {
                    PaymentDTO!.OrderDTOs = succ;
                    return succ;
                },
                excp =>
                {
                    if (excp is NotFoundException) return new List<OrderDTO>();
                    return HandleFailPrepare<List<OrderDTO>>(excp);
                }) : null;
            
            LoadingStatus = (LoadingStatus != 2) ? 1 : LoadingStatus;
        }
        catch (Exception ex)
        {
            PaymentDTO = null;
            PaymentServiceDTOs = null;
            LoadingStatus = 2;
            Console.WriteLine(ex.Message);
        }
    }
    R HandleFailPrepare<R>(Exception excp) where R : class
    {
        LoadingStatus = 2;
        return null!;
    }

    public async Task Submitting()
    {
        PaymentDTO!.Id = (Id == null || Id == 0) ? 0 : PaymentDTO.Id;
        if (Id == null || Id == 0) _ = (await PaymentDTOService!.CreateAsync(PaymentDTO!)).Match(HandllSubmitting, HandleSubmitting);
        else _ = (await PaymentDTOService!.UpdateAsync(PaymentDTO!)).Match(HandllSubmitting, HandleSubmitting);
        Submit!.ToDefault();
    }
    PaymentDTO HandllSubmitting(PaymentDTO succ)
    {
        NavMan!.NavigateTo($"/Payment/Details/{succ.Id}");
        return succ;
    }
    PaymentDTO HandleSubmitting(Exception excp)
    {
        Snackbar!.Add((Id == null) ? $"Error Creating the Payment" : $"Error Editing the Payment", Severity.Error);
        return null!;
    }


    public async Task Resetting()
    {
        await Prepare();
        Reset!.ToDefault();
    }
}
