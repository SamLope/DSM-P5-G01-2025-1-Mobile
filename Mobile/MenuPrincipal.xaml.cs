using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace Mobile;
public partial class MenuPrincipal : Microsoft.Maui.Controls.TabbedPage
{
    public MenuPrincipal()
    {
        InitializeComponent();


        On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetToolbarPlacement(Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.ToolbarPlacement.Bottom);


    }
}