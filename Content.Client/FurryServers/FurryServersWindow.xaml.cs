using System.Linq;
using Content.Client.Administration.Managers;
using Content.Client.Stylesheets;
using Content.Client.UserInterface.Controls;
using Content.Client.UserInterface.Systems.EscapeMenu;
using Content.Shared.Administration;
using JetBrains.Annotations;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Console;
using Robust.Client.ResourceManagement;
using Robust.Shared.Utility;

namespace Content.Client.FurryServers
{
    [GenerateTypedNameReferences]
    public sealed partial class FurryServersWindow : FancyWindow
    {
        [Dependency] private readonly IResourceCache _resourceManager = default!;
        [Dependency] private readonly IUriOpener _uri = default!;

        public FurryServersWindow()
        {
            RobustXamlLoader.Load(this);
            IoCManager.InjectDependencies(this);
            Stylesheet = IoCManager.Resolve<IStylesheetManager>().SheetSpace;

            BlepstationHeader.AddStyleClass(StyleBase.StyleClassLabelHeading);
            BlepstationHeader.FontColorOverride = Color.FromHex("#7687f2");

            var description = FormattedMessage.FromMarkup(_resourceManager.ContentFileReadAllText($"/FurryServers/Blepstation.txt"));
            BlepstationDescription.SetMessage(description);

            BlepstationWebsite.OnPressed += _ =>
            {
                _uri.OpenUri("https://blepstation.com");
            };
        }

        protected override void Opened()
        {
            base.Opened();
        }
    }

    [UsedImplicitly, AnyCommand]
    public sealed class FurryServersCommand : IConsoleCommand
    {
        public string Command => "furry";
        public string Description => "Shows list of furry space station 14 servers";
        public string Help => "Usage: furry";

        public void Execute(IConsoleShell shell, string argStr, string[] args)
        {
            IoCManager.Resolve<IUserInterfaceManager>().GetUIController<FurryServersUIController>().OpenWindow();
        }
    }
}
