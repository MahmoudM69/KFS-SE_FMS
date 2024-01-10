using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Pages.SharedComponents.NewComponents.BaseComponents;

public partial class BaseItemComponent : ComponentBase
{
    [Parameter]
    public virtual Dictionary<string, object> ContainerAttributes { get; set; } = new Dictionary<string, object>();

    [Parameter]
    public virtual Dictionary<string, object> ContentAttributes { get; set; } = new Dictionary<string, object>();

    [Parameter]
    public virtual Dictionary<string, object> ImageContainerAttributes { get; set; } = new Dictionary<string, object>();

    [Parameter]
    public virtual Dictionary<string, object> ImageAttributes { get; set; } = new Dictionary<string, object>
    {
        { "Style", "min-width: 100%; min-height: 100%;" }
    };

    [Parameter]
    public virtual Dictionary<string, object> ActionContainerAttributes { get; set; } = new Dictionary<string, object>();

    [Parameter]
    public virtual Dictionary<string, object> ActionAttributes { get; set; } = new Dictionary<string, object>();

    [Parameter]
    public IEnumerable<string> ImagesSrc { get; set; } = new List<string>();
    [Parameter]
    public string ImageLink { get; set; } = string.Empty;
    [Parameter]
    public bool CarouselAutoCycle { get; set; } = true;

    [Parameter, EditorRequired]
    public RenderFragment? ChildContent { get; set; }
    [Parameter]
    public RenderFragment? ActionContent { get; set; }

    [Parameter]
    public EventCallback DetailsClicked { get; set; }
    [Parameter]
    public EventCallback EditClicked { get; set; }
    [Parameter]
    public EventCallback RecoverClicked { get; set; }
    [Parameter]
    public EventCallback DeleteClicked { get; set; }
    [Parameter]
    public EventCallback DeleteForeverClicked { get; set; }

    protected bool showImage = false;
    protected bool showDefaultButton = false;

    protected override void OnParametersSet()
    {
        showImage = ImagesSrc is not null && ImagesSrc.Any();
        showDefaultButton = DetailsClicked.HasDelegate || EditClicked.HasDelegate ||
                            RecoverClicked.HasDelegate || DeleteClicked.HasDelegate || DeleteForeverClicked.HasDelegate;
    }

    protected async Task BtnClicked(EventCallback<MouseEventArgs> clickEvent) => await clickEvent.InvokeAsync();
}
