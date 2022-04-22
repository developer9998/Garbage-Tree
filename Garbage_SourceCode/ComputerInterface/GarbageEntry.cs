using System;
using System.Collections.Generic;
using System.Text;
using ComputerInterface;
using ComputerInterface.Interfaces;

namespace PracticeMod
{
	public class GarbageEntry : IComputerModEntry
	{
		public string EntryName => "Garbage Tree Remastered";

		public Type EntryViewType => typeof(GarbageView);
	}
}
