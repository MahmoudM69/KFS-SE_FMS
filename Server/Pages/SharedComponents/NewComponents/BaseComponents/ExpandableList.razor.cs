using DTO.DTOs.BaseDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Server.Shared;
using System.Collections.Generic;

namespace Server.Pages.SharedComponents.NewComponents.BaseComponents;

public partial class ExpandableList<T> : CancellableComponent where T : BaseDTO
{
    [Parameter]
    public string PanelsClass { get; set; } = "rounded-xl";
    [Parameter]
    public bool MultiExpansion { get; set; } = true;
    [Parameter]
    public bool PanelsDense { get; set; } = true;
    [Parameter]
    public bool PanelsDisableGutters { get; set; }
    [Parameter]
    public bool PanelsDisableBorders { get; set; }
    [Parameter]
    public string? PanelClass { get; set; }
    [Parameter]
    public bool PanelDense { get; set; } = true;
    [Parameter]
    public bool PanelDisableGutters { get; set; } = true;

    [Parameter, EditorRequired]
    public IDictionary<RenderFragment, RenderFragment>? Content { get; set; }

    [Parameter, EditorRequired]
    public RenderFragment? BodyContent { get; set; }
}
