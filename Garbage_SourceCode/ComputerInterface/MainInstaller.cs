using System;
using System.Collections.Generic;
using System.Text;
using ComputerInterface;
using ComputerInterface.Interfaces;
using Zenject;

namespace PracticeMod
{
	internal class MainInstaller : Installer
	{
		public override void InstallBindings()
		{
			Container.Bind<IComputerModEntry>().To<GarbageEntry>().AsSingle();
		}
	}
}
