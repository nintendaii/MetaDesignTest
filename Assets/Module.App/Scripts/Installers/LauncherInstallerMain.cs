using BestHTTP;

namespace Module.App.Scripts
{
    public class LauncherInstallerMain: Core.Launchers.LauncherInstaller
    {
        protected override void InstallComponents()
        {
            RegisterComponents<IBindComponentMain>();
        }
    }
}