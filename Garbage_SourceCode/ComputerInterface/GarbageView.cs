using System;
using System.Collections.Generic;
using System.Text;
using ComputerInterface;
using ComputerInterface.ViewLib;
using GorillaLocomotion;
// using PracticeMod.Patches;

//yes i know i used the practice mod as a base just like airjump and gravity monke

namespace PracticeMod
{
	public class GarbageView : ComputerView
	{
		const string highlightColor = "58892Cff";
		const string highlightColorAlt = "892C41";

		private readonly UISelectionHandler _selectionHandler;

		public static bool treeCollsion = true;
		int testNumber = 0; // https://cdn.discordapp.com/attachments/887962015040147486/966995064800817243/2022-04-22_06-54-23_Trim_2.mp4
		public static int testNumber2 = 0;
	
		string[] mapLocationNames = new string[] { "Forest", "Caves", "Canyons", "City", "Mountains" };

		public GarbageView()
		{
			_selectionHandler = new UISelectionHandler(EKeyboardKey.Up, EKeyboardKey.Down, EKeyboardKey.Enter);
			// the max zero indexed entry (2 entries - 1 since zero indexed)
			_selectionHandler.MaxIdx = 3;
			// when the "selection" key is pressed (we set it to enter above)
			_selectionHandler.OnSelected += OnEntrySelected;
			// since you quite often want to have an indicator of the selected item
			// I added helper function for that.
			// Basically you specify the prefix and suffix added to the selected item
			// and an prefix and suffix if the item isn't selected
			_selectionHandler.ConfigureSelectionIndicator($"<color=#{highlightColor}>></color> ", "", "  ", "");
		}

		public override void OnShow(object[] args)
		{
			base.OnShow(args);
			// changing the Text property will fire an PropertyChanged event
			// which lets the computer know the text has changed and update it
			UpdateScreen();
		}

		public void UpdateScreen()
		{
			// when your text function isn't that complex
			// you can use this method which creates a string builder
			// passes it via the specified callback function and sets the text at the end
			SetText(str =>
			{
				str.BeginCenter();
				str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10");
				str.AppendClr("Garbage Tree (Remastered)", highlightColor).EndColor().AppendLine();
				str.AppendLine("A mod by dev9998.");
				str.MakeBar('-', SCREEN_WIDTH, 0, "ffffff10");
				str.EndAlign().AppendLines(1);

				if (!GorillaTagModTemplateProject.Plugin.Allowed)
				{
					DrawDisabled(str);
				} else
                {
					DrawBody(str);
                }
			});
		}

		void DrawDisabled(StringBuilder str)
		{
			str.AppendLine();
			str.BeginCenter();
			str.BeginColor("ff0000");
			//str.AppendLine("You must be alone in a private");
			//str.AppendLine("to use this mod!");
			str.AppendLine("Join a modded lobby to access.");
			str.EndColor();
			str.EndAlign();
		}

		void DrawBody(StringBuilder str)
		{
			// get the item with the prefix and suffix configured above
			// see how this results in a lot less lines and logic
			str.BeginCenter();
			if (treeCollsion == true)
            {
				str.AppendLine(_selectionHandler.GetIndicatedText(0, $"<color={(treeCollsion ? "#" + highlightColor : "white")}>[Collision Enabled]</color>"));
			} else
            {
				str.AppendLine(_selectionHandler.GetIndicatedText(0, $"<color={(!treeCollsion ? "#" + highlightColorAlt : "white")}>[Collision Disabled]</color>"));
			}

			str.AppendLine(_selectionHandler.GetIndicatedText(1, $"Location: <color=#{highlightColor}>{mapLocationNames[testNumber2]}</color>"));

			str.AppendLine();
			str.BeginColor("FFFFFF");
			str.AppendLine("Current Settings:");
			str.AppendLine($"Collision: {treeCollsion} Map: {testNumber2}");
			str.AppendLine();
			str.EndColor();
			str.EndAlign();

		}

		//void DrawBody(StringBuilder str)
		//{
		//	str.AppendLine($"  Jump Multiplier: {(MovementSpeedPatch.ShouldOverride ? MovementSpeedPatch.OverrideJumpMultiplier : Player.Instance.jumpMultiplier):F2}");
		//	str.AppendLine($"  Max Jump Speed: {(MovementSpeedPatch.ShouldOverride ? MovementSpeedPatch.OverrideMaxJumpSpeed : Player.Instance.maxJumpSpeed):F2}");
		//	str.AppendLine();

		// get the item with the prefix and suffix configured above
		// see how this results in a lot less lines and logic
		//	str.AppendLine(_selectionHandler.GetIndicatedText(0, $"<color={(infected ? "#"+highlightColor: "white")}>[Infected]</color>"));
		//	str.AppendLine(_selectionHandler.GetIndicatedText(1, $"<color={(!infected ? "#"+highlightColor: "white")}>[Survivor]</color>"));
		//	str.AppendLine(_selectionHandler.GetIndicatedText(2, $"Infected Count: {infectedPlayers}"));
		//	str.AppendLine(_selectionHandler.GetIndicatedText(3, $"Player Count: {totalPlayers}"));

		//	str.AppendLine();
		//	str.BeginColor("ffffff10").AppendLine("  ▲/▼ Select  Enter/◀/▶ Adjust").EndColor();
		//	}

		private void OnEntrySelected(int index)
		{
			switch (index)
			{
				case 0:
		//			testNumber++;
					treeCollsion = !treeCollsion;
					UpdateScreen();
					break;
			}
		}

		private void OnEntryAdjusted(int index, bool increase)
		{
			int offset = increase ? 1 : -1;
			switch (index)
			{
				case 1:
					testNumber2 = UnityEngine.Mathf.Clamp(testNumber2 + offset, 0, 5);
					UpdateScreen();
					break;
			}
		}

		public override void OnKeyPressed(EKeyboardKey key)
		{
			if (GorillaTagModTemplateProject.Plugin.Allowed)
			{
				// check if the pressed key is already handled
				// by the selection handler (if yes returns true)
				// don't check for other buttons if it's handled
				// just update the screen
				if (_selectionHandler.HandleKeypress(key))
				{
					UpdateScreen();
					return;
				}
				
				// check if the pressed key is adjusting a setting
				if (key == EKeyboardKey.Left || key == EKeyboardKey.Right)
				{
					OnEntryAdjusted(_selectionHandler.CurrentSelectionIndex, key == EKeyboardKey.Right);
					UpdateScreen();
				}
			}

			switch (key)
			{
				case EKeyboardKey.Back:
					ReturnView();
					break;
			}
		}
	}
}
