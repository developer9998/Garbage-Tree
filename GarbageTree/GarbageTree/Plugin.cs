using System;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;
using Utilla;
using System.ComponentModel;
using System.Collections;
using Bepinject;

namespace Garbage
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [Description("HauntedModMenu")]
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class GarbageTree : BaseUnityPlugin
    {

        //This mess was modified by dev9998

        bool inRoom;

        GameObject GarbageBase = null;
        GameObject Garbage1 = null;
        GameObject Garbage2 = null;
        GameObject Garbage3 = null;
        GameObject Garbage4 = null;
        GameObject Garbage5 = null;
        GameObject Garbage6 = null;
        GameObject Garbage7 = null;

        public static ConfigEntry<bool> treeCollision;
        public static ConfigEntry<Int32> treeArea;

        void Start()
        {
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/
            
            var customFile = new ConfigFile(Path.Combine(Paths.ConfigPath, "GarbageTree.cfg"), true);
            treeCollision = customFile.Bind("Configuration", "Tree Collision", true, "Does the tree have collision?");
            treeArea = customFile.Bind("Configuration", "Tree Area", 4, "Where does the tree spawn? 0-4 for each of the maps in the order they came out.");

            if (inRoom)
                GarbageBase.SetActive(this.enabled);
                Garbage1.SetActive(this.enabled);
                Garbage2.SetActive(this.enabled);
                Garbage3.SetActive(this.enabled);
                Garbage4.SetActive(this.enabled);
                Garbage5.SetActive(this.enabled);
                Garbage6.SetActive(this.enabled);
                Garbage7.SetActive(this.enabled);
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            GarbageBase.SetActive(false);
            Garbage1.SetActive(false);
            Garbage2.SetActive(false);
            Garbage3.SetActive(false);
            Garbage4.SetActive(false);
            Garbage5.SetActive(false);
            Garbage6.SetActive(false);
            Garbage7.SetActive(false);

        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            /* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */

            GarbageBase = GameObject.Find("Level/mountain/garbage");
            Garbage1 = GameObject.Find("Level/mountain/garbage/pinetree");
            Garbage2 = GameObject.Find("Level/mountain/garbage/pinetreebranch");
            Garbage3 = GameObject.Find("Level/mountain/garbage/pinetreebranch.001");
            Garbage4 = GameObject.Find("Level/mountain/garbage/pinetreebranch.002");
            Garbage5 = GameObject.Find("Level/mountain/garbage/pinetreebranchesspiralbranches");
            Garbage6 = GameObject.Find("Level/mountain/garbage/pinetreebranchesspiralleaves");
            Garbage7 = GameObject.Find("Level/mountain/garbage/pinetreetop");

            Garbage1.GetComponent<MeshCollider>().enabled = treeCollision.Value;
            Garbage2.GetComponent<MeshCollider>().enabled = treeCollision.Value;
            Garbage3.GetComponent<MeshCollider>().enabled = treeCollision.Value;
            Garbage4.GetComponent<MeshCollider>().enabled = treeCollision.Value;
            Garbage5.GetComponent<MeshCollider>().enabled = treeCollision.Value;
            Garbage6.GetComponent<MeshCollider>().enabled = treeCollision.Value;
            Garbage7.GetComponent<MeshCollider>().enabled = treeCollision.Value;

            GarbageBase.transform.SetParent(null, true);

            if (treeArea.Value==0)
            {
                GarbageBase.transform.position = new Vector3(-47.99439f, 2.081555f, -55.9823f); // forest
            } else if(treeArea.Value == 1)
            {
                GarbageBase.transform.position = new Vector3(-74.48f, -13.87f, -48.32f); // caves
            } else if (treeArea.Value == 2)
            {
                GarbageBase.transform.position = new Vector3(-95.68f, 9.3f, -127.24f); // canyons
            } else if (treeArea.Value == 3)
            {
                GarbageBase.transform.position = new Vector3(-53.34f, 16.27f, -106.13f); // city
            } else if (treeArea.Value == 4)
            {
                GarbageBase.transform.position = new Vector3(37.30341f, -3.987558f, -71.54142f); // mountain
            } else
            {
                GarbageBase.transform.position = new Vector3(37.30341f, -3.987558f, -71.54142f); // mountain (default)
            }

        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = true;

            GarbageBase.SetActive(this.enabled);
            Garbage1.SetActive(this.enabled);
            Garbage2.SetActive(this.enabled);
            Garbage3.SetActive(this.enabled);
            Garbage4.SetActive(this.enabled);
            Garbage5.SetActive(this.enabled);
            Garbage6.SetActive(this.enabled);
            Garbage7.SetActive(this.enabled);
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;

            GarbageBase.SetActive(false);
            Garbage1.SetActive(false);
            Garbage2.SetActive(false);
            Garbage3.SetActive(false);
            Garbage4.SetActive(false);
            Garbage5.SetActive(false);
            Garbage6.SetActive(false);
            Garbage7.SetActive(false);
        }
    }
}
