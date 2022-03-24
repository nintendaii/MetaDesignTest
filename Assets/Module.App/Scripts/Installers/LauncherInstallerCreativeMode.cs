using BestHTTP;

namespace Module.App.Scripts
{
    public class LauncherInstallerCreativeMode: Core.Launchers.LauncherInstaller
    {
        protected override void InstallComponents()
        {
            RegisterComponents<IBindComponentCreativeMode>();
        }
    }
}