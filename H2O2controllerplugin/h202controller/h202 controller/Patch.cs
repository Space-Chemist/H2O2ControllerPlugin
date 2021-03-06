using System;
using System.Reflection;
using NLog;
using Sandbox.Game.Entities.Blocks;
using Torch.Managers.PatchManager;
using IMyGasGenerator = Sandbox.ModAPI.IMyGasGenerator;


namespace h202_controller
{
    public class Patch
    {
        [PatchShim]
        public static class MyAssemblerBasePatch {

            public static readonly Logger Log = LogManager.GetCurrentClassLogger();

            internal static readonly MethodInfo update =
                typeof(MyGasGenerator).GetMethod(nameof(MyGasGenerator.UpdateAfterSimulation), BindingFlags.Instance | BindingFlags.Public) ??
                throw new Exception("Failed to find patch method");

            internal static readonly MethodInfo updatePatch =
                typeof(MyAssemblerBasePatch).GetMethod(nameof(TestPatchMethod), BindingFlags.Static | BindingFlags.Public) ??
                throw new Exception("Failed to find patch method");

            public static void Patch(PatchContext ctx) {

                ctx.GetPattern(update).Prefixes.Add(updatePatch);
                Log.Info("Patching Successful MyLargeTurretBase!");
            }

            public static void TestPatchMethod(MyGasGenerator __instance) {
                try
                {
                    if (H202Core.Instance.Config.Enabled)
                    {
                        ((IMyGasGenerator) __instance).ProductionCapacityMultiplier = H202Core.Instance.Config.Production;
                        ((IMyGasGenerator) __instance).PowerConsumptionMultiplier = H202Core.Instance.Config.Power;
                    }    
                    
                }
                catch (Exception e)
                {
                    Log.Warn("Error in H2O2 Controller:" + e);
                }
                
                //var generator = __instance as IMyGasGenerator;
                //generator.PowerConsumptionMultiplier = 1.0f * multiplier; 
            }
        }
    }
}