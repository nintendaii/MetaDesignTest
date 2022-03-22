using Module.Project.CommandSignals;
using Module.Project.Managers;
using UnityEngine;
using Zenject;

namespace Module.Project
{
    public class LauncherInstaller : Core.Launchers.LauncherInstaller
    {

        protected override void InstallCommandSignals()
        {
            
        }

        protected override void InstallComponents()
        {
            base.InstallComponents();
        }

        protected override void InstallFactory()
        {
        }
    }
}