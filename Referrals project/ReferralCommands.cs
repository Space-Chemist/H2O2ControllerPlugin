﻿using System.Collections.Generic;
using System.Linq;
using NLog;
using Sandbox.Game.Multiplayer;
using Sandbox.Game.Screens.ViewModels;
using Sandbox.Game.World;
using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;
using System.Threading.Tasks;
using Sandbox.Definitions;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Character;
using VRage.Game.Definitions.SessionComponents;
using VRage.Groups;
using VRageMath;

namespace Referrals_project
{
    [Category("referral")]
    public class ReferralCommands : CommandModule
    {
        private static readonly Logger Log = LogManager.GetLogger("Referrals");
        private MyCharacter myCharacter;
        public Referrals_project.ReferralCore Plugin
        {
            get { return (Referrals_project.ReferralCore) this.Context.Plugin; }
        }

        [Command("save", "Saves targeted grid in hangar")]
        [Permission(MyPromoteLevel.None)]
        public void Save()
        {
            var controlledEntity = Context.Player.Character;
            const float range = 5000;
            Matrix worldMatrix;
            Vector3D startPosition;
            Vector3D endPosition;

            worldMatrix = controlledEntity.GetHeadMatrix(true, true, false); // dead center of player cross hairs, or the direction the player is looking with ALT.
            startPosition = worldMatrix.Translation + worldMatrix.Forward * 0.5f;
            endPosition = worldMatrix.Translation + worldMatrix.Forward * (range + 0.5f);

            var list = new Dictionary<MyGroups<MyCubeGrid, MyGridPhysicalGroupData>.Group, double>();
            var ray = new RayD(startPosition, worldMatrix.Forward);

            foreach (var group in MyCubeGridGroups.Static.Physical.Groups)
            {

                foreach (MyGroups<MyCubeGrid, MyGridPhysicalGroupData>.Node groupNodes in group.Nodes)
                {

                    MyCubeGrid cubeGrid = groupNodes.NodeData;
                    if (cubeGrid != null)
                    {

                        if (cubeGrid.Physics == null)
                            continue;

                        // check if the ray comes anywhere near the Grid before continuing.    
                        if (ray.Intersects(cubeGrid.PositionComp.WorldAABB).HasValue)
                        {

                            Vector3I? hit = cubeGrid.RayCastBlocks(startPosition, endPosition);

                            if (hit.HasValue)
                            {
                                Log.Error(cubeGrid.Name.ToString);
                                var GridName = cubeGrid.Name;
                                var FolderDirectory = ReferralCore.Instance.StoragePath;
                                GridMethods methods = new GridMethods(FolderDirectory, GridName);
                                Task T = new Task(() => methods.SaveGrids(cubeGrid, GridName));
                                T.Start();
                            }
                        }
                    }
                }
            }
        }

        [Command("load", "Loads given grid from hangar")]
        [Permission(MyPromoteLevel.None)]
        public void Load(string GridName)
        {
            var FolderDirectory = ReferralCore.Instance.StoragePath;
            var myIdentity = ((MyPlayer)Context.Player).Identity;
            var TargetIdentity = myIdentity.IdentityId;
            var myCharacter = myIdentity.Character;

            GridMethods methods = new GridMethods(FolderDirectory, GridName);
            Task T = new Task(() => methods.LoadGrid(GridName, myCharacter, TargetIdentity));
            T.Start();
        }
            


        [Command("new", "get your referral bonus", "requries steamId/Name")]
        [Permission(MyPromoteLevel.None)]
        public void Knew(string player)
        {
            if (Context.Player == null)
            {
                Log.Error("Why are you running this in the console?");
                return;
            }

            var identity = ReferralCore.GetIdentityByNameOrIds(player);
            if (identity == null)
            {
                Context.Respond("X: player not found, are you sure you have the right steam id or name?");
                return;
            }

            var user1 = ReferralCore.GetUser(Context.Player.SteamUserId);

            if (user1.ReferralByUser != null)
            {
                if ((bool) user1.ReferralByUser)
                {
                    Context.Respond("X: You already did this?");
                    return;
                }
            }

            if (user1.ReferralByCode != null)
            {
                if ((bool) user1.ReferralByCode)
                {
                    Context.Respond("X: You already did this?");
                    return;
                }
            }

            var user2 = ReferralCore.GetUser(MySession.Static.Players.TryGetSteamId(identity.IdentityId));

            var check = ReferralCore.Dostuff(user1);
            if (!check)
            {
                Log.Error("Failed to do stuff");
                Context.Respond("X: Failed to do stuff");
                return;
            }

            var referredDescription = new ReferredDescription
                {ReferredUserName = user1.Name, ReferredUserId = user1.SteamId, Claimed = false};
            user1.ReferralByUser = true;
            user1.ReferredBy = user2.SteamId;
            user2.ReferredDescriptions.Add(referredDescription);

            ReferralCore.SaveUser(user1);
            ReferralCore.SaveUser(user1);
            Context.Respond("Claimed");
        }


        [Command("claim", "Get your referral bonus", "Get your referral bonus")]
        [Permission(MyPromoteLevel.None)]
        public void Claim()
        {
            if (Context.Player == null)
            {
                Log.Error("Why are you running this in the console?");
                return;
            }

            var user = ReferralCore.GetUser(Context.Player.SteamUserId);

            user.ReferredDescriptions.ForEach(
                rd
                    =>
                {
                    if (rd.Claimed) return;
                    var c = ReferralCore.Dostuff(user);
                    if (c)
                    {
                        rd.Claimed = true;
                        ReferralCore.SaveUser(user);
                        Context.Respond($"Claimed for {rd.ReferredUserName}");
                    }
                    else
                    {
                        //lmao
                        Context.Respond($"Failed to Claim for {rd.ReferredUserName}, This should never happen, see a admin and report code 42");
                    }
                }
            );
            Context.Respond("done.");

        }

    }
}