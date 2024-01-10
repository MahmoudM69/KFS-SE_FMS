using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace Server.Pages.SharedComponents.NewComponents.BaseComponents;

public partial class BaseCardItem : BaseItemComponent
{
    [Parameter]
    public override Dictionary<string, object> ContainerAttributes { get; set; } = new Dictionary<string, object> {
        { "Class", "rounded-xl" },
    };

    [Parameter]
    public override Dictionary<string, object> ImageAttributes { get; set; } = new Dictionary<string, object> {
        { "ObjectFit", ObjectFit.Cover },
        { "Elevation", 0 },
        { "Class", "rounded-t-xl rounded-b" },
        { "Style", "height:100%; width:100%;" }
    };

    [Parameter]
    public override Dictionary<string, object> ImageContainerAttributes { get; set; } = new Dictionary<string, object> {
        { "xs", 12 },
        { "ms", 4 },
        { "class", "rounded-t-xl rounded-b mud-width-full" },
        { "style", "height:100%; min-height:250px; min-width:100%;" }
    };

    [Parameter]
    public override Dictionary<string, object> ActionAttributes { get; set; } = new Dictionary<string, object> {
        { "Class", "rounded-xl" }
    };

    [Parameter]
    public override Dictionary<string, object> ActionContainerAttributes { get; set; } = new Dictionary<string, object> {
        { "Class", "d-flex justify-space-around flex-grow-1 gap-2" }
    };

    [Parameter]
    public RenderFragment? HeaderContent { get; set; }

    protected override void OnInitialized()
    {
        //ContainerAttributes["Class"] = "rounded-xl";
        
        //ImageAttributes["ObjectFit"] = ObjectFit.Cover;
        //ImageAttributes["Elevation"] = 0;
        //ImageAttributes["Class"] = "rounded-t-xl rounded-b";
        
        //ImageContainerAttributes["Class"] = "rounded-t-xl rounded-b mud-width-full";
        //ImageContainerAttributes["Style"] = "height:100%; min-height:250px;";
        
        //ActionAttributes["Class"] = "rounded-xl";
        
        //ActionContainerAttributes["Class"] = "d-flex justify-space-around flex-grow-1 gap-2";

        base.OnInitialized();
    }
}
