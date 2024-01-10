using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server.Helpers.Extensions;

public static class ServerExtensions
{
    public static async Task<ClaimsPrincipal> GetUserFromAuthState(this Task<AuthenticationState> authenticationState)
    {
        var auth = await authenticationState;
        return auth.User;
    }

    public static void ShowSnackbar(this ISnackbar? snackbar, string message, Severity severity,
        string? actionText = null, Color actionColor = Color.Inherit, Func<Task>? OnClick = null)
    {
        if (snackbar is not null)
        {
            snackbar.Add(message, severity, config =>
            {
                if (actionText is not null)
                {
                    config.Action = actionText;
                    config.ActionColor = actionColor;
                }
                if (OnClick is not null) config.Onclick = async snackbar => await OnClick();
            });
        }
    }

    public static T? If<T>(this T value, bool condition) where T : Delegate
    {
        return condition ? value : default;
    }

    public static async Task<T?> If<T>(Task<T> value, bool condition) where T : Delegate
    {
        return condition ? await value : default;
    }

    //public static Expression? If(this Expression value, bool condition)
    //{
    //    return condition ? value : default;
    //}
}
