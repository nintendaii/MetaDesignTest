using BestHTTP;

namespace Module.App.Scripts
{
    public class LauncherInstaller: Core.Launchers.LauncherInstaller
    {
        protected override void InstallComponents()
        {
            RegisterComponents<IBindComponent>();
        }
    }
}