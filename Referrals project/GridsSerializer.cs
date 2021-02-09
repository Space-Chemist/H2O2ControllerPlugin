﻿using ProtoBuf;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage.Game.Entity;
using VRageMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using NLog;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game.Entities.Character;
using Sandbox.Game.Entities.Cube;
using Sandbox.Game.World;
using Torch.Commands;
using VRage.Game;
using VRage.ObjectBuilders;

namespace Referrals_project
{
    public class GridMethods
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public string FolderPath;
        private ulong SteamID;
        private CommandContext Context;

        public GridMethods(string FolderDirectory, string GridName) 
        {
            FolderPath = Path.Combine(FolderDirectory, GridName.ToString());
            Directory.CreateDirectory(FolderPath);
        }

        private bool SaveGridToFile(string path, string filename, List<MyObjectBuilder_CubeGrid> objectBuilders)
        {

            MyObjectBuilder_ShipBlueprintDefinition definition =
                MyObjectBuilderSerializer.CreateNewObject<MyObjectBuilder_ShipBlueprintDefinition>();

            definition.Id = new MyDefinitionId(new MyObjectBuilderType(typeof(MyObjectBuilder_ShipBlueprintDefinition)),
                filename);
            definition.CubeGrids = objectBuilders.Select(x => (MyObjectBuilder_CubeGrid) x.Clone()).ToArray();

            /* Reset ownership as it will be different on the new server anyway */
            foreach (MyObjectBuilder_CubeGrid cubeGrid in definition.CubeGrids)
            {
                foreach (MyObjectBuilder_CubeBlock cubeBlock in cubeGrid.CubeBlocks)
                {
                    cubeBlock.Owner = 0L;
                    cubeBlock.BuiltBy = 0L;
                }
            }

            MyObjectBuilder_Definitions builderDefinition =
                MyObjectBuilderSerializer.CreateNewObject<MyObjectBuilder_Definitions>();
            builderDefinition.ShipBlueprints = new MyObjectBuilder_ShipBlueprintDefinition[] {definition};


            return MyObjectBuilderSerializer.SerializeXML(path, false, builderDefinition);
        }


        public bool LoadGrid(string GridName, MyCharacter Player, long TargetPlayerID)
        {
            string path = Path.Combine(FolderPath, GridName + ".sbc");

            if (!File.Exists(path))
            {
                Log.Fatal("Grid doesnt exsist @" + path);
                return false;
            }

            try
            {
                if (MyObjectBuilderSerializer.DeserializeXML(path,
                    out MyObjectBuilder_Definitions myObjectBuilder_Definitions))
                {
                    var shipBlueprints = myObjectBuilder_Definitions.ShipBlueprints;


                    if (shipBlueprints == null)
                    {

                        Log.Error("not grid found for blueprint");
                        return false;
                    }

                    //If the configs have keep originial position on, we dont want to align this to gravity.

                    foreach (var shipBlueprint in shipBlueprints)
                    {
                        Vector3D TargetSpawnPos = Vector3D.Zero;
                        if (Player != null)
                        {
                            TargetSpawnPos = Player.PositionComp.GetPosition();
                        }


                        if (!LoadShipBlueprint(shipBlueprint, TargetSpawnPos))
                        {
                            Log.Debug("Error Loading ShipBlueprints from File '" + path + "'");
                            return false;
                        }
                    }

                    File.Delete(path);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to deserialize grid: " + path + " from file! Is this a shipblueprint?");
            }

            return false;
        }

        private bool LoadShipBlueprint(MyObjectBuilder_ShipBlueprintDefinition shipBlueprint,
            Vector3D PlayerLocation)
        {
            var grids = shipBlueprint.CubeGrids;

            if (grids == null || grids.Length == 0)
            { 
                Log.Info("No grids in blueprint!");
                return false;
            }

            Vector3D TargetLocation = PlayerLocation;
            bool AlignToGravity = true;
            

            ParallelSpawner Spawner = new ParallelSpawner(grids, AlignToGravity);
            Log.Info("Attempting Grid Spawning @" + TargetLocation.ToString());
            return Spawner.Start(TargetLocation);
        }
    }
}