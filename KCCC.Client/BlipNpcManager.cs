using CitizenFX.Core;
using CitizenFX.Core.Native;
using Core.Shared.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCCC.Client
{
    public static class BlipNpcManager
    {
        private static Dictionary<string, CitizenFX.Core.Blip> Blips = new Dictionary<string, CitizenFX.Core.Blip>();
        private static List<Npc> npcs = new List<Npc>();
        public static void CreateBlip(string name, int sprite, string label, float scale, Vector3 position, string scene = "", int assignedToEntity = -1)
        {
            var blip = World.CreateBlip(position);
            blip.Sprite = (BlipSprite)sprite;
            blip.Name = label;
            blip.Scale = scale;


            Blips.Add(name, blip);
                
        }

        public static async Task<bool> LoadModel(uint hash)
        {
            if (API.IsModelValid(hash))
            {

                API.RequestModel(hash);

                while (!API.HasModelLoaded(hash))
                {
                    await BaseScript.Delay(100);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        public static async Task CreateNPC(int model, int variation, Vector4 position, string scenario = "", string scene = "", bool isNetwork = false, bool netMissionEntity = false)
        {
            if (!API.HasModelLoaded((uint)model))
            {
                await LoadModel((uint)model);
            }

            var handle = API.CreatePed(4, (uint)model, position.X, position.Y, position.Z - 0.98f, position.W, isNetwork, netMissionEntity);

            API.SetBlockingOfNonTemporaryEvents(handle, true);
            API.SetEntityVisible(handle, true, true);
            API.SetEntityInvincible(handle, true);
            API.SetPedCanBeTargetted(handle, false);
            API.SetPedCanPlayGestureAnims(handle, true);
            API.SetPedCanRagdoll(handle, false);
            API.FreezeEntityPosition(handle, true);
            API.SetEntityAsMissionEntity(handle, netMissionEntity, netMissionEntity);
            //Utils.SetPedOutfitPreset(handle, variation);
            //API.SetPedDefaultComponentVariation(handle);
            API.SetPedRandomComponentVariation(handle, false);

            if (scenario != "") API.TaskStartScenarioInPlace(handle, scenario, -1, true);   //API.TaskStartScenarioInPlace(handle, API.GetHashKey(scenario), -1, 1, 0, 0, 0);

            var npc = new Npc(handle, scene);
            npcs.Add(npc);
           
        }
        public static void Delete()
        {
            for (int i = 0; i < Blips.Count; i++)
            {
                var blip = Blips.ElementAt(i);
                var handle = blip.Value.Handle;
                API.RemoveBlip(ref handle);
            }

            for (int i = 0; i < npcs.Count; i++)
            {
                var npc = npcs[i];
                var handle = npc.Handle;
                API.DeleteEntity(ref handle);
            }
            Blips.Clear();
            npcs.Clear();
        }

        [EventHandler("onResourceStop")]
        private static void OnResourceStop(string resourceName)
        {
            if (resourceName == "KCCharacterCreator")
            {
                Delete();
            }
        }

        
    }
}
