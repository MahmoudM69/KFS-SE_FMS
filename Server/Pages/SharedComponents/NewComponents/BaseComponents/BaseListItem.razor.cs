using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Server.Pages.SharedComponents.NewComponents.BaseComponents;

public partial class BaseListItem : BaseItemComponent
{
    protected override void OnInitialized()
    {
        ContainerAttributes["Class"] = "d-flex justify-space-between rounded-xl";

        ContentAttributes["xs"] = 10;
        ContentAttributes["sm"] = 7;
        ContentAttributes["Class"] = "pt-4 pb-5 pl-6";
        ContentAttributes["Style"] = "display: grid;";
    
        ImageAttributes["ObjectFit"] = ObjectFit.Cover;
        ImageAttributes["Elevation"] = 0;
        ImageAttributes["Class"] = "rounded-xl";
        ImageAttributes["Style"] = "height:100%; width:100%;";

        ImageContainerAttributes["xs"] = 12;
        ImageContainerAttributes["sm"] = 4;
        ImageContainerAttributes["class"] = "rounded-t rounded-br rounded-bl-xl mud-width-full";
        ImageContainerAttributes["style"] = "height:100%; min-height:250px; min-width:100%;";

        ActionAttributes["Class"] = "rounded-xl";

        ActionContainerAttributes["xs"] = 2;
        ActionContainerAttributes["sm"] = 1;
        ActionContainerAttributes["Class"] = "d-flex flex-wrap flex-column justify-space-around align-center align-content-end pr-4 pb-4";

        base.OnInitialized();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ContentAttributes["sm"] = showImage ? 7 : 11;
    }
}
