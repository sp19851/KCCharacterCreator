using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Core.Shared.Entity;
using Core.Shared.Enums;
using Core.Shared.Models;
using KCCC.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.UI.Screen;
using static KCCC.Client.Models.CharacterCreate;
using static System.Net.Mime.MediaTypeNames;

namespace KCCC.Client
{
    public class Main : BaseScript
    {
        public static EventHandlerDictionary Handlers { get; private set; }

        private CharacterSkin characterSkin;

        public PedHeadBlend CurrentPedHeadBlend { get; set; } = new PedHeadBlend();
        public PedFaceFeatures CurrentPedFaceFeatures { get; set; } = new PedFaceFeatures();
        public PedHeadOverlays CurrentPedHeadOverlay { get; set; } = new PedHeadOverlays();


        public List<Core.Shared.Models.PedComponent> CurrentPedComponents { get; set; } = new List<Core.Shared.Models.PedComponent>();
        public List<Core.Shared.Models.PedProp> CurrentPedProps { get; set; } = new List<Core.Shared.Models.PedProp>();
        public PedHair CurrentPedHair { get; set; } = new PedHair();

        public int EarColor { get; set; }
        public List<PedHair> PedHairs { get; set; } = new List<PedHair> { };
        public List<PedHair> MakeUpColors { get; set; } = new List<PedHair> { };


        public int Gender { get; set; } = 0;
        public string CurrentPedModel = "mp_m_freemode_01";
        public string SceneName = "CharacterCreate";
        public CameraModel mainCam;
        public CameraModel clothesCam;
        private Vector3 FirstCamRot { get; set; } = new Vector3(0, 0, 0);
        //private Vector3 FirstPedRot { get; set; } = new Vector3(0, 0, 0);
        private float FirstPedHeading { get; set; } = 0f;
        private float CurrentCamHeight { get; set; } = 0;
        private float CurrentCamDist { get; set; } = 0;
        private bool CanCamMove { get; set; } = false;
        private bool HandsIsUp { get; set; } = false;
        private string AnimDict { get; set; } = "missminuteman_1ig_2";

        private string Anim { get; set; } = "handsup_base";
        private string Scenario { get; set; } = "WORLD_HUMAN_GUARD_STAND";

        private int interactId = -1;
        public bool IsReady { get; set; } = false;

        private bool CanInterract { get; set; } = true;
        private bool canInteract = false;
        private bool canControl = false;


        private Core.Shared.Entity.Character characterData = null;
        public static Models.CharacterCreate Config { get; set; }
        private string BankAccount { get; set; }
        private string CitizenId { get; set; }

        public Main()
        {
            Handlers = EventHandlers;
            RegisterNuiEvents();
            Config = Configuration<Models.CharacterCreate>.Parse("config/create_character");
        }
        private async Task Init(string json)
        {
            if (json.StartsWith("{"))
            {
                var data = JsonConvert.DeserializeObject<Character>(json);
                if (data != null)
                {
                    characterData = data;
                    TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 10500, "Вы не закончили создание персонажа", "red", "Warning");
                    
                }
            }
            else
            {
                await SetDefaultPed();
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 10500, "Пройдите вперед на маркер для создания персонажа", "blue", "Info");
            }
            Debug.WriteLine($"Init {CitizenId} {BankAccount}");
            //0-18, 24, 26,27.28, 55-60
            int[] colorindexs = new int[]
            {
                0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,24,26,27,28,55,56,57,58,59,60
            };

            for (int i = 0; i < colorindexs.Length; i++)
            {
                //Logger.Warn($"colorindex {colorindexs[i]}");
                var r = 0;
                var g = 0;
                var b = 0;
                API.GetPedHairRgbColor(colorindexs[i], ref r, ref g, ref b);
                var _pedHair = new PedHair();
                _pedHair.R = r;
                _pedHair.G = g;
                _pedHair.B = b;
                _pedHair.R2 = r;
                _pedHair.G2 = g;
                _pedHair.B2 = b;
                //Logger.Warn($"rgb {r} {g} {b}");
                PedHairs.Add(_pedHair);
            }


            //int[] makeupcolorindexs = new int[]
            //{
            //    //0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,25,26,51,60
            //    //0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,48,49,55,56,62,63
            //};
            int[] makeupcolorindexs = new int[64];
            for (int i = 0; i < 63; i++)
            {
                makeupcolorindexs[i] = i;
            }

            for (int i = 0; i < makeupcolorindexs.Length; i++)
            {
                //Logger.Warn($"colorindex {colorindexs[i]}");
                var r = 0;
                var g = 0;
                var b = 0;
                API.GetPedMakeupRgbColor(makeupcolorindexs[i], ref r, ref g, ref b);
                var _pedHair = new PedHair();
                _pedHair.R = r;
                _pedHair.G = g;
                _pedHair.B = b;
                _pedHair.R2 = r;
                _pedHair.G2 = g;
                _pedHair.B2 = b;

                //Logger.Warn($"GetPedMakeupRgbColor index {i} rgb {r} {g} {b}");
                MakeUpColors.Add(_pedHair);
            }


            await SetModelForPed(CurrentPedModel);
            var ped = API.PlayerPedId();
            //0: Face
            //1: Mask
            //2: Hair
            //3: Torso
            //4: Leg
            //5: Parachute / bag
            //6: Shoes
            //7: Accessory
            //8: Undershirt
            //9: Kevlar
            //10: Badge
            //11: Torso 2
            for (int compIndex = 0; compIndex < 12; compIndex++)
            {
                var components = new Core.Shared.Models.PedComponent();
                components.Component_Id = compIndex;
                components.MaxDrawableValue = API.GetNumberOfPedDrawableVariations(ped, components.Component_Id);
                components.MaxTextureValue = new List<TextureModel>();
                //Logger.Warn($"components {components.Component_Id} MaxDrawableValue {components.MaxDrawableValue}");
                for (int i = 0; i < components.MaxDrawableValue; i++)
                {
                    var textureValue = new TextureModel();
                    textureValue.DrawableId = i;
                    textureValue.MaxValue = API.GetNumberOfPedTextureVariations(ped, components.Component_Id, i);
                    components.MaxTextureValue.Add(textureValue);
                    //Logger.Warn($"MaxDrawableValue {components.Component_Id} i {i} {API.GetNumberOfPedTextureVariations(ped, components.Component_Id, i)}");
                }


                CurrentPedComponents.Add(components);
            }

            for (int propIndex = 0; propIndex < 8; propIndex++)
            {
                var props = new Core.Shared.Models.PedProp();
                props.Prop_Id = propIndex;
                props.MaxDrawableValue = API.GetNumberOfPedDrawableVariations(ped, props.Prop_Id);
                props.MaxTextureValue = new List<PropModel>();
                //Logger.Warn($"components {components.Component_Id} MaxDrawableValue {components.MaxDrawableValue}");
                for (int i = 0; i < props.MaxDrawableValue; i++)
                {
                    var propValue = new PropModel();
                    propValue.DrawableId = i;
                    propValue.MaxValue = API.GetNumberOfPedTextureVariations(ped, props.Prop_Id, i);
                    props.MaxTextureValue.Add(propValue);
                    //Logger.Warn($"MaxDrawableValue {components.Component_Id} i {i} {API.GetNumberOfPedTextureVariations(ped, components.Component_Id, i)}");
                }


                CurrentPedProps.Add(props);
            }
            CurrentPedHair.R = 28;
            CurrentPedHair.G = 31;
            CurrentPedHair.B = 33;
            CurrentPedHair.R2 = 28;
            CurrentPedHair.G2 = 31;
            CurrentPedHair.B2 = 33;
            CurrentPedHair.Style = 0;
            CurrentPedHair.Highlight = 0;



            CurrentPedHeadOverlay.Beard.R = 28;
            CurrentPedHeadOverlay.Beard.G = 31;
            CurrentPedHeadOverlay.Beard.B = 33;
            CurrentPedHeadOverlay.Eyebrows.R = 28;
            CurrentPedHeadOverlay.Eyebrows.G = 31;
            CurrentPedHeadOverlay.Eyebrows.B = 33;
            CurrentPedHeadOverlay.ChestHair.R = 28;
            CurrentPedHeadOverlay.ChestHair.G = 31;
            CurrentPedHeadOverlay.ChestHair.B = 33;
            CurrentPedHeadOverlay.MakeUp.R = 153;
            CurrentPedHeadOverlay.MakeUp.G = 37;
            CurrentPedHeadOverlay.MakeUp.B = 50;
            CurrentPedHeadOverlay.Blush.R = 153;
            CurrentPedHeadOverlay.Blush.G = 37;
            CurrentPedHeadOverlay.Blush.B = 50;
            CurrentPedHeadOverlay.Lipstick.R = 153;
            CurrentPedHeadOverlay.Lipstick.G = 37;
            CurrentPedHeadOverlay.Lipstick.B = 50;
            //Logger.Warn($"CurrentPedComponents {JsonConvert.SerializeObject(CurrentPedComponents)}");

            //blip
            
            foreach (var shop in Config.Points)
            {
            
                var blip = shop.Blip;
                BlipNpcManager.CreateBlip(shop.Name, blip.Sprite, blip.Label, blip.Scale, blip.Position, SceneName);
                //Logger.Warn($"CreateBlip {blip.Sprite} {blip.Label} {blip.Scale} {blip.Position}");
                //var handle = Utils.CreateBlip(blip.Sprite, blip.Label, blip.Scale, blip.Position);
                var npc = shop.Npc;
                if (npc == null) continue;
                await BlipNpcManager.CreateNPC(API.GetHashKey(npc.Model), 0, npc.Position, npc.Scenario, SceneName);
                
            }
            //await SetDefaultPed();
            TickReset();
            
            //Tick += Update;
            //Tick += UpdateInteract;
            //Tick += UpdateControl;

            IsReady = true;
            

        }
        private async Task Deinitialization()
        {
            Tick -= Update;
            Tick -= UpdateInteract;
            Tick -= UpdateControl;
            BlipNpcManager.Delete();
        }
        #region Tick

        private async Task Update()
        {
            if (Config == null) return;
            var playerPos = Game.PlayerPed.Position;
            var nearestMarker = Config.Points.FirstOrDefault(s => Vector3.Distance((Vector3)s.Interact.Position, playerPos) <= 10f);
            if (nearestMarker != null && CanInterract)
            {
                var newpos = new Vector3(nearestMarker.Interact.Position.X, nearestMarker.Interact.Position.Y, nearestMarker.Interact.Position.Z);
                World.DrawMarker(MarkerType.VerticalCylinder, newpos, Vector3.Zero, Vector3.Zero, new Vector3(nearestMarker.Marker.ScaleX, nearestMarker.Marker.ScaleY, 0.3f),
                    Color.FromArgb(255, nearestMarker.Marker.ColorR, nearestMarker.Marker.ColorG, nearestMarker.Marker.ColorB));



                canInteract = true;
                //Tick += UpdateInteract;

            }
            else
            {
                //Tick -= UpdateInteract;
                canInteract = false;
            }
        }

        private async Task UpdateInteract()
        {
            if (!canInteract)
            {
                await Delay(1000);
                return;
            }
            var playerPos = Game.PlayerPed.Position;
            var nearestInteract = Config.Points.FirstOrDefault(s => Vector3.Distance((Vector3)s.Interact.Position, playerPos) <= s.Interact.Radius);
            if (nearestInteract != null)
            {
                interactId = API.GetPlayerServerId(API.PlayerId());
                TriggerEvent("Hud.InteractMessageAdd", interactId, "Для взаимодействия");
                if (API.IsControlJustReleased(0, (int)_Keys.E))
                {

                    TriggerEvent("Hud.InteractMessageHide", interactId);
                    CanInterract = false;

                    Tick -= Update;
                    //Tick -= UpdateInteract;
                    canInteract = false;
                    //notification.Schedule(TitlesNotifications.interact, NotifyTypes.interact, 5000, new Notification.TextBuilder("Вы нажали кнопку Е", "blue"));

                    switch (nearestInteract.Name)
                    {
                        case "ClotheShop":
                            CreaterRun();
                            break;
                        case "Bank":
                            TriggerEvent(Events.Banking.OnOpen);
                            break;
                        case "Identy":
                            NUI.Execute(new
                            {
                                request = "identity.show",
                                cards = Config.Cards
                            });
                            NUI.Focus(true, true);
                            break;
                        case "Exit":

                            TryOut();
                            break;
                        default:
                            break;
                    }



                    await Delay(1500);

                }
            }
            else
            {
                if (interactId != -1)
                {
                    TriggerEvent("Hud.InteractMessageHide", interactId);
                    interactId = -1;
                }
                await Delay(500);
            }
        }



        private async Task UpdateControl()
        {
            if (!canControl)
            {
                await Delay(1000);
                return;
            }
            API.DisableControlAction(0, 59, true); // Disable steering in vehicle
            API.DisableControlAction(0, 21, true);  // disable sprint
            API.DisableControlAction(0, 24, true);  // disable attack
            API.DisableControlAction(0, 25, true);  // disable aim
            API.DisableControlAction(0, 47, true);  // disable weapon
            API.DisableControlAction(0, 58, true);  // disable weapon
            API.DisableControlAction(0, 71, true);  // veh forward
            API.DisableControlAction(0, 72, true);  // veh backwards
            API.DisableControlAction(0, 63, true);  // veh turn left
            API.DisableControlAction(0, 64, true);  // veh turn right
            API.DisableControlAction(0, 263, true);  // disable melee
            API.DisableControlAction(0, 264, true);  // disable melee
            API.DisableControlAction(0, 257, true);  // disable melee
            API.DisableControlAction(0, 140, true);  // disable melee
            API.DisableControlAction(0, 141, true);  // disable melee
            API.DisableControlAction(0, 142, true);  // disable melee
            API.DisableControlAction(0, 143, true);  // disable melee
            API.DisableControlAction(0, 75, true);  // disable exit vehicle
            API.DisableControlAction(27, 75, true);  // disable exit vehicle
        }

        #endregion

        #region Methods

        private int GetColorIndexByRgb(int r, int g, int b)
        {
            int[] colorindexs = new int[]
            {
                0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,24,26,27,28,55,56,57,58,59,60

            };
            var needColorIndex = -1;
            for (int i = 0; i < colorindexs.Length; i++)
            {
                //Logger.Warn($"colorindex {colorindexs[i]}");
                var _r = 0;
                var _g = 0;
                var _b = 0;
                API.GetPedHairRgbColor(colorindexs[i], ref _r, ref _g, ref _b);
                if (_r == r && _g == g && _b == b)
                {
                    needColorIndex = i;
                    break;
                }
            }
            return needColorIndex;
        }
        private int GetColorIndexByMakeUpRgb(int r, int g, int b)
        {
            int[] colorindexs = new int[64];
            for (int i = 0; i < 64; i++)
            {
                colorindexs[i] = i;
            }
            var needColorIndex = -1;
            for (int i = 0; i < colorindexs.Length; i++)
            {

                var _r = 0;
                var _g = 0;
                var _b = 0;
                API.GetPedMakeupRgbColor(colorindexs[i], ref _r, ref _g, ref _b);
                if (_r == r && _g == g && _b == b)
                {
                    needColorIndex = i;
                    break;
                }
            }
            return needColorIndex;
        }

        private void SetPedChestHairAsync(int r, int g, int b)
        {
            var ped = API.PlayerPedId();
            var colorIndex = GetColorIndexByRgb(r, g, b);
            API.SetPedHeadOverlayColor(ped, 10, 1, colorIndex, 0);
            CurrentPedHeadOverlay.ChestHair.R = r; CurrentPedHeadOverlay.ChestHair.G = g; CurrentPedHeadOverlay.ChestHair.B = b;
            NUI.Execute(
                  new
                  {
                      request = "PedHeadOverlay.update",
                      Blemishes = CurrentPedHeadOverlay.Blemishes,
                      Beard = CurrentPedHeadOverlay.Beard,
                      Eyebrows = CurrentPedHeadOverlay.Eyebrows,
                      Ageing = CurrentPedHeadOverlay.Ageing,
                      MakeUp = CurrentPedHeadOverlay.MakeUp,
                      Blush = CurrentPedHeadOverlay.Blush,
                      Complexion = CurrentPedHeadOverlay.Complexion,
                      SunDamage = CurrentPedHeadOverlay.SunDamage,
                      Lipstick = CurrentPedHeadOverlay.Lipstick,
                      MoleAndFreckles = CurrentPedHeadOverlay.MoleAndFreckles,
                      ChestHair = CurrentPedHeadOverlay.ChestHair,
                      BodyBlemishes = CurrentPedHeadOverlay.BodyBlemishes,
                      AddBodyBlemishes = CurrentPedHeadOverlay.AddBodyBlemishes,

                  });
        }
        private void SetPedHairEyebrowsAsync(int r, int g, int b)
        {
            var ped = API.PlayerPedId();
            var colorIndex = GetColorIndexByRgb(r, g, b);
            //Logger.Warn($"SetPedHairEyebrowsAsync colorIndex {colorIndex}");
            API.SetPedHeadOverlayColor(ped, 2, 1, colorIndex, 0);
            CurrentPedHeadOverlay.Eyebrows.R = r; CurrentPedHeadOverlay.Eyebrows.G = g; CurrentPedHeadOverlay.Eyebrows.B = b;
            NUI.Execute(
                  new
                  {
                      request = "PedHeadOverlay.update",
                      Blemishes = CurrentPedHeadOverlay.Blemishes,
                      Beard = CurrentPedHeadOverlay.Beard,
                      Eyebrows = CurrentPedHeadOverlay.Eyebrows,
                      Ageing = CurrentPedHeadOverlay.Ageing,
                      MakeUp = CurrentPedHeadOverlay.MakeUp,
                      Blush = CurrentPedHeadOverlay.Blush,
                      Complexion = CurrentPedHeadOverlay.Complexion,
                      SunDamage = CurrentPedHeadOverlay.SunDamage,
                      Lipstick = CurrentPedHeadOverlay.Lipstick,
                      MoleAndFreckles = CurrentPedHeadOverlay.MoleAndFreckles,
                      ChestHair = CurrentPedHeadOverlay.ChestHair,
                      BodyBlemishes = CurrentPedHeadOverlay.BodyBlemishes,
                      AddBodyBlemishes = CurrentPedHeadOverlay.AddBodyBlemishes,

                  });
        }
        private void SetPedHairBeardAsync(int r, int g, int b)
        {
            var ped = API.PlayerPedId();
            var colorIndex = GetColorIndexByRgb(r, g, b);
            //Logger.Warn($"SetPedHeadOverlayColor needColorIndex {colorIndex}");
            API.SetPedHeadOverlayColor(ped, 1, 1, colorIndex, 0);
            CurrentPedHeadOverlay.Beard.R = r; CurrentPedHeadOverlay.Beard.G = g; CurrentPedHeadOverlay.Beard.B = b;
            NUI.Execute(
                  new
                  {
                      request = "PedHeadOverlay.update",
                      Blemishes = CurrentPedHeadOverlay.Blemishes,
                      Beard = CurrentPedHeadOverlay.Beard,
                      Eyebrows = CurrentPedHeadOverlay.Eyebrows,
                      Ageing = CurrentPedHeadOverlay.Ageing,
                      MakeUp = CurrentPedHeadOverlay.MakeUp,
                      Blush = CurrentPedHeadOverlay.Blush,
                      Complexion = CurrentPedHeadOverlay.Complexion,
                      SunDamage = CurrentPedHeadOverlay.SunDamage,
                      Lipstick = CurrentPedHeadOverlay.Lipstick,
                      MoleAndFreckles = CurrentPedHeadOverlay.MoleAndFreckles,
                      ChestHair = CurrentPedHeadOverlay.ChestHair,
                      BodyBlemishes = CurrentPedHeadOverlay.BodyBlemishes,
                      AddBodyBlemishes = CurrentPedHeadOverlay.AddBodyBlemishes,

                  });
        }
        private void SetPedMakeUpAsync(int r, int g, int b)
        {
            var ped = API.PlayerPedId();

            var needColorIndex = GetColorIndexByMakeUpRgb(r, g, b);
            //var needColorIndex2 = GetColorIndexByMakeUpRgb((int)CurrentPedHeadOverlay.MakeUp.R2, (int)CurrentPedHeadOverlay.MakeUp.G2, (int)CurrentPedHeadOverlay.MakeUp.B2);
            API.SetPedHeadOverlayColor(ped, 4, 0, needColorIndex, 0);

            CurrentPedHeadOverlay.MakeUp.R = r; CurrentPedHeadOverlay.MakeUp.G = g; CurrentPedHeadOverlay.MakeUp.B = b;


            NUI.Execute(
                  new
                  {
                      request = "PedHeadOverlay.update",
                      Blemishes = CurrentPedHeadOverlay.Blemishes,
                      Beard = CurrentPedHeadOverlay.Beard,
                      Eyebrows = CurrentPedHeadOverlay.Eyebrows,
                      Ageing = CurrentPedHeadOverlay.Ageing,
                      MakeUp = CurrentPedHeadOverlay.MakeUp,
                      Blush = CurrentPedHeadOverlay.Blush,
                      Complexion = CurrentPedHeadOverlay.Complexion,
                      SunDamage = CurrentPedHeadOverlay.SunDamage,
                      Lipstick = CurrentPedHeadOverlay.Lipstick,
                      MoleAndFreckles = CurrentPedHeadOverlay.MoleAndFreckles,
                      ChestHair = CurrentPedHeadOverlay.ChestHair,
                      BodyBlemishes = CurrentPedHeadOverlay.BodyBlemishes,
                      AddBodyBlemishes = CurrentPedHeadOverlay.AddBodyBlemishes,

                  });
        }
        private void SetPedLipstickAsync(int r, int g, int b)
        {
            var ped = API.PlayerPedId();

            var needColorIndex = GetColorIndexByMakeUpRgb(r, g, b);
            //var needColorIndex2 = GetColorIndexByMakeUpRgb((int)CurrentPedHeadOverlay.MakeUp.R2, (int)CurrentPedHeadOverlay.MakeUp.G2, (int)CurrentPedHeadOverlay.MakeUp.B2);
            API.SetPedHeadOverlayColor(ped, 8, 2, needColorIndex, 0);

            CurrentPedHeadOverlay.Lipstick.R = r; CurrentPedHeadOverlay.Lipstick.G = g; CurrentPedHeadOverlay.Lipstick.B = b;


            NUI.Execute(
                  new
                  {
                      request = "PedHeadOverlay.update",
                      Blemishes = CurrentPedHeadOverlay.Blemishes,
                      Beard = CurrentPedHeadOverlay.Beard,
                      Eyebrows = CurrentPedHeadOverlay.Eyebrows,
                      Ageing = CurrentPedHeadOverlay.Ageing,
                      MakeUp = CurrentPedHeadOverlay.MakeUp,
                      Blush = CurrentPedHeadOverlay.Blush,
                      Complexion = CurrentPedHeadOverlay.Complexion,
                      SunDamage = CurrentPedHeadOverlay.SunDamage,
                      Lipstick = CurrentPedHeadOverlay.Lipstick,
                      MoleAndFreckles = CurrentPedHeadOverlay.MoleAndFreckles,
                      ChestHair = CurrentPedHeadOverlay.ChestHair,
                      BodyBlemishes = CurrentPedHeadOverlay.BodyBlemishes,
                      AddBodyBlemishes = CurrentPedHeadOverlay.AddBodyBlemishes,

                  });
        }
        private void SetPedBlushAsync(int r, int g, int b)
        {
            var ped = API.PlayerPedId();

            var needColorIndex = GetColorIndexByMakeUpRgb(r, g, b);
            //var needColorIndex2 = GetColorIndexByMakeUpRgb((int)CurrentPedHeadOverlay.MakeUp.R2, (int)CurrentPedHeadOverlay.MakeUp.G2, (int)CurrentPedHeadOverlay.MakeUp.B2);
            API.SetPedHeadOverlayColor(ped, 5, 2, needColorIndex, 0);

            CurrentPedHeadOverlay.Blush.R = r; CurrentPedHeadOverlay.Blush.G = g; CurrentPedHeadOverlay.Blush.B = b;


            NUI.Execute(
                  new
                  {
                      request = "PedHeadOverlay.update",
                      Blemishes = CurrentPedHeadOverlay.Blemishes,
                      Beard = CurrentPedHeadOverlay.Beard,
                      Eyebrows = CurrentPedHeadOverlay.Eyebrows,
                      Ageing = CurrentPedHeadOverlay.Ageing,
                      MakeUp = CurrentPedHeadOverlay.MakeUp,
                      Blush = CurrentPedHeadOverlay.Blush,
                      Complexion = CurrentPedHeadOverlay.Complexion,
                      SunDamage = CurrentPedHeadOverlay.SunDamage,
                      Lipstick = CurrentPedHeadOverlay.Lipstick,
                      MoleAndFreckles = CurrentPedHeadOverlay.MoleAndFreckles,
                      ChestHair = CurrentPedHeadOverlay.ChestHair,
                      BodyBlemishes = CurrentPedHeadOverlay.BodyBlemishes,
                      AddBodyBlemishes = CurrentPedHeadOverlay.AddBodyBlemishes,

                  });
        }
        private void SetPedMakeUpAsync2(int r, int g, int b)
        {
            var ped = API.PlayerPedId();

            var needColorIndex = GetColorIndexByMakeUpRgb((int)CurrentPedHeadOverlay.MakeUp.R, (int)CurrentPedHeadOverlay.MakeUp.G, (int)CurrentPedHeadOverlay.MakeUp.B);
            var needColorIndex2 = GetColorIndexByMakeUpRgb(r, g, b);
            API.SetPedHeadOverlayColor(ped, 4, 1, needColorIndex, needColorIndex2);

            CurrentPedHeadOverlay.MakeUp.R = r; CurrentPedHeadOverlay.MakeUp.G = g; CurrentPedHeadOverlay.MakeUp.B = b;


            NUI.Execute(
                  new
                  {
                      request = "PedHeadOverlay.update",
                      Blemishes = CurrentPedHeadOverlay.Blemishes,
                      Beard = CurrentPedHeadOverlay.Beard,
                      Eyebrows = CurrentPedHeadOverlay.Eyebrows,
                      Ageing = CurrentPedHeadOverlay.Ageing,
                      MakeUp = CurrentPedHeadOverlay.MakeUp,
                      Blush = CurrentPedHeadOverlay.Blush,
                      Complexion = CurrentPedHeadOverlay.Complexion,
                      SunDamage = CurrentPedHeadOverlay.SunDamage,
                      Lipstick = CurrentPedHeadOverlay.Lipstick,
                      MoleAndFreckles = CurrentPedHeadOverlay.MoleAndFreckles,
                      ChestHair = CurrentPedHeadOverlay.ChestHair,
                      BodyBlemishes = CurrentPedHeadOverlay.BodyBlemishes,
                      AddBodyBlemishes = CurrentPedHeadOverlay.AddBodyBlemishes,

                  });
        }
        private void SetPedHairAsync(int r, int g, int b)
        {
            var ped = API.PlayerPedId();

            var needColorIndex = GetColorIndexByRgb(r, g, b);
            var needColorIndex2 = GetColorIndexByRgb(CurrentPedHair.R2, CurrentPedHair.G2, CurrentPedHair.B2);
            API.SetPedHairColor(ped, needColorIndex, needColorIndex2);
            CurrentPedHair.R = r; CurrentPedHair.G = g; CurrentPedHair.B = b;

            NUI.Execute(
              new
              {
                  request = "PedHairColor.update",
                  PedHair = CurrentPedHair

              });
        }
        private void SetPedHairAsync2(int r2, int g2, int b2)
        {
            var ped = API.PlayerPedId();
            var needColorIndex = GetColorIndexByRgb(CurrentPedHair.R, CurrentPedHair.G, CurrentPedHair.B);
            var needColorIndex2 = GetColorIndexByRgb(r2, g2, b2);
            API.SetPedHairColor(ped, needColorIndex, needColorIndex2);
            CurrentPedHair.R2 = r2; CurrentPedHair.G2 = g2; CurrentPedHair.B2 = b2;
            NUI.Execute(
              new
              {
                  request = "PedHairColor.update",
                  PedHair = CurrentPedHair

              });
        }
        private void SetComponentsAsync(Core.Shared.Models.PedComponent component)
        {
            if (component != null)
            {
                API.SetPedComponentVariation(API.PlayerPedId(), component.Component_Id, component.Drawable, component.Texture, 2);
            }

            NUI.Execute(
               new
               {
                   request = "PedComponents.update",
                   Components = CurrentPedComponents

               });
        }

        private void SetPropsAsync(Core.Shared.Models.PedProp prop)
        {
            if (prop != null)
            {
                if (prop.Drawable == -1)
                {
                    API.ClearPedProp(API.PlayerPedId(), prop.Prop_Id);
                }
                API.SetPedPropIndex(API.PlayerPedId(), prop.Prop_Id, prop.Drawable, prop.Texture, true);
            }

            NUI.Execute(
               new
               {
                   request = "PedProps.update",
                   Props = CurrentPedProps

               });
        }


        private void SetPedHeadOverlayAsync()
        {
            var ped = API.PlayerPedId();
            //Blemishes
            API.SetPedHeadOverlay(ped, 0, CurrentPedHeadOverlay.Blemishes.Index, CurrentPedHeadOverlay.Blemishes.Opacity);
            ////Beard
            API.SetPedHeadOverlay(ped, 1, CurrentPedHeadOverlay.Beard.Index, CurrentPedHeadOverlay.Beard.Opacity);
            SetPedHairBeardAsync((int)CurrentPedHeadOverlay.Beard.R, (int)CurrentPedHeadOverlay.Beard.G, (int)CurrentPedHeadOverlay.Beard.B);
            //Eyebrows
            API.SetPedHeadOverlay(ped, 2, CurrentPedHeadOverlay.Eyebrows.Index, CurrentPedHeadOverlay.Eyebrows.Opacity);
            SetPedHairEyebrowsAsync((int)CurrentPedHeadOverlay.Eyebrows.R, (int)CurrentPedHeadOverlay.Eyebrows.G, (int)CurrentPedHeadOverlay.Eyebrows.B);
            //Ageing
            API.SetPedHeadOverlay(ped, 3, CurrentPedHeadOverlay.Ageing.Index, CurrentPedHeadOverlay.Ageing.Opacity);
            //MakeUp
            API.SetPedHeadOverlay(ped, 4, CurrentPedHeadOverlay.MakeUp.Index, CurrentPedHeadOverlay.MakeUp.Opacity);
            //SetPedHairEyebrowsAsync((int)CurrentPedHeadOverlay.MakeUp.R, (int)CurrentPedHeadOverlay.MakeUp.G, (int)CurrentPedHeadOverlay.MakeUp.B);
            SetPedMakeUpAsync((int)CurrentPedHeadOverlay.MakeUp.R, (int)CurrentPedHeadOverlay.MakeUp.G, (int)CurrentPedHeadOverlay.MakeUp.B);
            //Blush
            API.SetPedHeadOverlay(ped, 5, CurrentPedHeadOverlay.Blush.Index, CurrentPedHeadOverlay.Blush.Opacity);
            SetPedBlushAsync((int)CurrentPedHeadOverlay.Blush.R, (int)CurrentPedHeadOverlay.Blush.G, (int)CurrentPedHeadOverlay.Blush.B);
            //SetPedHairEyebrowsAsync((int)CurrentPedHeadOverlay.Blush.R, (int)CurrentPedHeadOverlay.Blush.G, (int)CurrentPedHeadOverlay.Blush.B);
            //Complexion
            API.SetPedHeadOverlay(ped, 6, CurrentPedHeadOverlay.Complexion.Index, CurrentPedHeadOverlay.Complexion.Opacity);
            //SunDamage
            API.SetPedHeadOverlay(ped, 7, CurrentPedHeadOverlay.SunDamage.Index, CurrentPedHeadOverlay.SunDamage.Opacity);
            //Lipstick
            API.SetPedHeadOverlay(ped, 8, CurrentPedHeadOverlay.Lipstick.Index, CurrentPedHeadOverlay.Lipstick.Opacity);
            SetPedLipstickAsync((int)CurrentPedHeadOverlay.Lipstick.R, (int)CurrentPedHeadOverlay.Lipstick.G, (int)CurrentPedHeadOverlay.Lipstick.B);
            //MoleAndFreckles
            API.SetPedHeadOverlay(ped, 9, CurrentPedHeadOverlay.MoleAndFreckles.Index, CurrentPedHeadOverlay.MoleAndFreckles.Opacity);
            //ChestHair
            API.SetPedHeadOverlay(ped, 10, CurrentPedHeadOverlay.ChestHair.Index, CurrentPedHeadOverlay.ChestHair.Opacity);
            SetPedChestHairAsync((int)CurrentPedHeadOverlay.ChestHair.R, (int)CurrentPedHeadOverlay.ChestHair.G, (int)CurrentPedHeadOverlay.ChestHair.B);
            //BodyBlemishes
            API.SetPedHeadOverlay(ped, 11, CurrentPedHeadOverlay.BodyBlemishes.Index, CurrentPedHeadOverlay.BodyBlemishes.Opacity);
            //AddBodyBlemishes
            API.SetPedHeadOverlay(ped, 12, CurrentPedHeadOverlay.AddBodyBlemishes.Index, CurrentPedHeadOverlay.AddBodyBlemishes.Opacity);

            NUI.Execute(
                new
                {
                    request = "PedHeadOverlay.update",
                    Blemishes = CurrentPedHeadOverlay.Blemishes,
                    Beard = CurrentPedHeadOverlay.Beard,
                    Eyebrows = CurrentPedHeadOverlay.Eyebrows,
                    Ageing = CurrentPedHeadOverlay.Ageing,
                    MakeUp = CurrentPedHeadOverlay.MakeUp,
                    Blush = CurrentPedHeadOverlay.Blush,
                    Complexion = CurrentPedHeadOverlay.Complexion,
                    SunDamage = CurrentPedHeadOverlay.SunDamage,
                    Lipstick = CurrentPedHeadOverlay.Lipstick,
                    MoleAndFreckles = CurrentPedHeadOverlay.MoleAndFreckles,
                    ChestHair = CurrentPedHeadOverlay.ChestHair,
                    BodyBlemishes = CurrentPedHeadOverlay.BodyBlemishes,
                    AddBodyBlemishes = CurrentPedHeadOverlay.AddBodyBlemishes,

                });

        }

        private async void SetPedFaceFeaturesAsync(PedFaceFeatures currentPedFaceFeatures)
        {
            var ped = API.PlayerPedId();
            API.SetPedFaceFeature(ped, 0, currentPedFaceFeatures.NoseWidth / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 1, currentPedFaceFeatures.NosePeakHigh / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 2, currentPedFaceFeatures.NoseWidth / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 3, currentPedFaceFeatures.NosePeakSize / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 4, currentPedFaceFeatures.NoseBoneHigh / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 5, currentPedFaceFeatures.NosePeakLowering / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 6, currentPedFaceFeatures.EyeBrownHigh / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 7, currentPedFaceFeatures.EyeBrownForward / 10 + 0.0f);

            API.SetPedFaceFeature(ped, 9, currentPedFaceFeatures.CheeksWidth / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 10, currentPedFaceFeatures.CheeksBoneWidth / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 11, currentPedFaceFeatures.EyesOpening / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 12, currentPedFaceFeatures.LipsThickness / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 13, currentPedFaceFeatures.JawBoneWidth / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 14, currentPedFaceFeatures.JawBoneBackSize / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 15, currentPedFaceFeatures.ChinBoneLowering / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 16, currentPedFaceFeatures.ChinBoneLenght / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 17, currentPedFaceFeatures.ChinBoneSize / 10 + 0.0f);
            API.SetPedFaceFeature(ped, 18, currentPedFaceFeatures.ChinHole / 10 + 0.0f);
            //API.SetPedFaceFeature(ped, 19, currentPedFaceFeatures.NeckThickness); толщина шеи


            //await Delay(500);
        }
        private async void SetModelForPedAsync(string currentPedModel)
        {
            await SetModelForPed(CurrentPedModel);
        }

        public async Task SetDefaultPed()
        {
            var ped = API.PlayerPedId();
            API.SetPedHeadBlendData(ped, 21, 0, 0, CurrentPedHeadBlend.SkinFirst, CurrentPedHeadBlend.SkinSecond, 0,
                 1, 0.5f, 0f, false);
            //футболка
            CurrentPedComponents.FirstOrDefault(c => c.Component_Id == 8).Drawable = 15;
            API.SetPedComponentVariation(API.PlayerPedId(), 8, 15, 0, 2);
            //куртка
            CurrentPedComponents.FirstOrDefault(c => c.Component_Id == 11).Drawable = 15;
            API.SetPedComponentVariation(API.PlayerPedId(), 11, 15, 0, 2);
            //руки
            CurrentPedComponents.FirstOrDefault(c => c.Component_Id == 3).Drawable = 15;
            API.SetPedComponentVariation(API.PlayerPedId(), 3, 15, 0, 2);
            //штаны
            CurrentPedComponents.FirstOrDefault(c => c.Component_Id == 4).Drawable = 14;
            API.SetPedComponentVariation(API.PlayerPedId(), 4, 14, 0, 2);
            //обувь
            CurrentPedComponents.FirstOrDefault(c => c.Component_Id == 6).Drawable = 16;
            API.SetPedComponentVariation(API.PlayerPedId(), 6, 16, 0, 2);
        }


        private async void CreaterRun()
        {
            API.RequestStreamedTextureDict("pause_menu_pages_char_mom_dad", true);
            API.RequestStreamedTextureDict("char_creator_portraits", true);
            while (!API.HasStreamedTextureDictLoaded("pause_menu_pages_char_mom_dad") || !API.HasStreamedTextureDictLoaded("char_creator_portraits"))
            {
                await Delay(100);
            }
            var ped = API.PlayerPedId();
            string[] windows = new string[3] { "all", "clothes", "barber" };
            NUI.Execute(
                new
                {
                    request = "characterCreate.show",
                    pedFaceFeatures = CurrentPedFaceFeatures,
                    EarColor = EarColor,
                    //pedHeadOverlay = CurrentPedHeadOverlay,
                    Blemishes = CurrentPedHeadOverlay.Blemishes,
                    Beard = CurrentPedHeadOverlay.Beard,
                    Eyebrows = CurrentPedHeadOverlay.Eyebrows,
                    Ageing = CurrentPedHeadOverlay.Ageing,
                    MakeUp = CurrentPedHeadOverlay.MakeUp,
                    Blush = CurrentPedHeadOverlay.Blush,
                    Complexion = CurrentPedHeadOverlay.Complexion,
                    SunDamage = CurrentPedHeadOverlay.SunDamage,
                    Lipstick = CurrentPedHeadOverlay.Lipstick,
                    MoleAndFreckles = CurrentPedHeadOverlay.MoleAndFreckles,
                    ChestHair = CurrentPedHeadOverlay.ChestHair,
                    BodyBlemishes = CurrentPedHeadOverlay.BodyBlemishes,
                    AddBodyBlemishes = CurrentPedHeadOverlay.AddBodyBlemishes,
                    Components = CurrentPedComponents,
                    Props = CurrentPedProps,
                    PedHairColors = PedHairs,
                    MakeUpColors = MakeUpColors,
                    PedHair = CurrentPedHair,
                    Windows = windows

                });
            //NUI.FocusKeepInput();
            NUI.Focus(true, true);


            //Hud.SetRadarVisibility(false);

        }


        private async void TryOut()
        {
            //проверить, что б все было чики
            if (characterData.Firstname == null)
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 7500, "Вы не завершили создание персонажа. Не готова Id-карта.", "crimson", "Warning");
                TickReset();
                return;
            }
            if (characterData.CharacterSkin.Model == null)
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 7500, "Вы не завершили создание персонажа. Вы не посетили маркер создания персонажа.", "crimson", "Warning");
                TickReset();
                return;
            }

            //отправляем сигнал ядру о том, что скрипт закончил работу
            TriggerEvent(Events.Character.OnGetDataFromCreator);

            //пометка в базу

            //characterData.DoneCreated = true;
            //characterManager.SaveForce();

            //ролик прилета

            //if (!Main.ScriptIsStarted<CutSceneManager>())
            //{
            //    Main.LoadScript(new CutSceneManager(Main));
            //}
            //var cutscene = Main.GetScript<CutSceneManager>();
            //await cutscene.InitIntroAirport();
            ////запуск всех систем, в том числе выход из соло
            //weatherTimeManager.SetStaticWeather(!bool.Parse(Constant.Config["Weathertime"]["Need"].ToString()));
            //Hud.SetRadarVisibility(true);
            //blipManager.RemoveScene(SceneName);

            //spawnManager.TpPlayerToDefaultSpawnExitWordAsync(SceneName);



            //characterManager.LastInit(characterData);

            await Deinitialization();
        }

       

        #endregion

        #region Camera
        private async void SetCameraAsync(string camera)
        {

            var ped = API.PlayerPedId();
            if (CameraManager.GetActiveCam() != null)
            {
                API.SetCamRot(CameraManager.GetActiveCam().Handle, FirstCamRot.X, FirstCamRot.Y, FirstCamRot.Z + 180f, 2);
                API.SetEntityHeading(ped, FirstPedHeading);
                await Delay(100);
            }
            API.FreezeEntityPosition(ped, true);
            var offsetCoord = API.GetOffsetFromEntityInWorldCoords(ped, 0f, 0.6f, 0.65f);

            await CameraManager.DeleteScene(SceneName);

            mainCam = await CameraManager.CreateCam("createCharacterCam", offsetCoord.X, offsetCoord.Y, offsetCoord.Z, 0f, 0f, 0f + 180f, 50f, SceneName);
            var camRot = API.GetCamRot(mainCam.Handle, 2);


            FirstPedHeading = API.GetEntityHeading(ped);
            camRot.Z = FirstPedHeading;


            API.SetCamRot(mainCam.Handle, camRot.X, camRot.Y, camRot.Z + 180f, 2);
            CameraManager.ActivateCam(mainCam);

            offsetCoord = API.GetOffsetFromEntityInWorldCoords(ped, 0f, 3.0f, 0.0f);
            clothesCam = await CameraManager.CreateCam("createCharacterCam_Clothes", offsetCoord.X, offsetCoord.Y, offsetCoord.Z, 0f, 0f, 0f + 180f, 50f, SceneName);
            API.SetCamRot(clothesCam.Handle, camRot.X, camRot.Y, camRot.Z + 180f, 2);

            switch (camera)
            {
                case "Parents":
                    CameraManager.ActivateCam(mainCam);
                    API.ClearPedTasks(ped);
                    CanCamMove = false;
                    break;
                case "Face":
                    CameraManager.ActivateCam(mainCam);
                    API.ClearPedTasks(ped);
                    CanCamMove = false;
                    break;
                case "Clothes":
                    CameraManager.ActivateCam(clothesCam);
                    API.TaskStartScenarioInPlace(ped, Scenario, -1, true);
                    CanCamMove = true;
                    break;
                case "Hair":
                    CameraManager.ActivateCam(mainCam);
                    API.ClearPedTasks(ped);
                    CanCamMove = false;
                    break;
                case "Makeup":
                    CameraManager.ActivateCam(mainCam);
                    API.ClearPedTasks(ped);
                    CanCamMove = false;
                    break;
                default:
                    break;
            }
            if (CameraManager.GetActiveCam() == null || CameraManager.GetActiveCam().Handle == -1) return;
            //Logger.Warn($"Activecam {Camera.GetActiveCam().Name} {Camera.GetActiveCam().Handle}");





        }


        private async Task SaveIdentity(string firstname, string lastname, string sex, string dob, string placeB, string nationality, int hight, int activecard)
        {

            characterData.Firstname = firstname;

            characterData.Lastname = lastname;

            characterData.DateOfBirth = dob;

            characterData.CityOfBirth = placeB;

            characterData.Nationality = nationality;
            CardModel card = new CardModel();
            switch (activecard)
            {
                case 1:
                    card = Config.Cards.FirstOrDefault(c => c.Id == activecard);
                    break;
                case 2:
                    card = Config.Cards.FirstOrDefault(c => c.Id == activecard);
                    break;
                case 3:
                    card = Config.Cards.FirstOrDefault(c => c.Id == activecard);
                    break;
                case 4:
                    card = Config.Cards.FirstOrDefault(c => c.Id == activecard);
                    break;
                default:
                    break;
            }
            if (card.Id != 0)
            {
                characterData.Ratio = card.Ratio;
                if (card.Adrenaline)
                {
                    characterData.Dependences.Adrenaline = 100;
                }
            }
            var cash = Config.StartCash;
            if (cash != null)
            {
                var cashInt = int.Parse(cash.ToString());
                characterData.Economy.Money = cashInt;
            }

            var bank = Config.StartBank;
            if (bank != null)
            {
                var bankInt = int.Parse(bank.ToString());
                characterData.Economy.Bank = bankInt;
                characterData.Economy.BankAccount = BankAccount;
            }
            //Создаем уникальный citizenId
            if (characterData.CitizenId == null) characterData.CitizenId = CitizenId;
            TriggerServerEvent(Events.Character.OnSave, API.GetCurrentResourceName(), JsonConvert.SerializeObject(characterData));

            NUI.Execute(new
            {
                request = "identity.hide"
            });
            NUI.Focus(false, false);

            var item = new
            {
                Name = "id_card",
                Data = new
                {
                    Firstname = characterData.Firstname,
                    Lastname = characterData.Lastname,
                    DateOfBirth = characterData.DateOfBirth,
                    CitizenId = characterData.CitizenId,
                }
            };

            //if (!Main.ScriptIsStarted<EconomyManager>())
            //{
            //    Main.LoadScript(new EconomyManager(Main));

            //}
            //if (!Main.ScriptIsStarted<InventoryManager>())
            //{
            //    Main.LoadScript(new InventoryManager(Main));

            //}

            //storage = Main.GetScript<InventoryManager>();
            //await storage.IsReady();


            //if (!storage.HaveItem("id_card")) TriggerEvent("Storage.OnGiveToStorage", JsonConvert.SerializeObject(item), 1); ;
            TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 7500, "Вернитесь назад и сделайте себе карту в отделении банка", "blue", "Info");
            TickReset();
        }
        private async Task SaveAsync()
        {

            characterSkin = new CharacterSkin();
            characterSkin.Model = CurrentPedModel;
            await ResetPedComponentsUnnecessaryData();
            characterSkin.Components = CurrentPedComponents;
            await ResetPedPropsUnnecessaryData();

            characterSkin.Props = CurrentPedProps;
            characterSkin.HeadBlend = CurrentPedHeadBlend;
            characterSkin.FaceFeatures = CurrentPedFaceFeatures;
            characterSkin.HeadOverlays = CurrentPedHeadOverlay;
            characterSkin.Hair = CurrentPedHair;
            characterSkin.EarColor = EarColor;


            NUI.Execute(new { request = "characterCreate.hide" });
            NUI.Focus(false, false);
            //Hud.SetRadarVisibility(true);
            var curCam = CameraManager.GetActiveCam();

            CameraManager.DeActivateCam(curCam);
            CameraManager.SwitchCameraToPlayer();

            await ResetTask();

            characterData.CharacterSkin = characterSkin;

            if (characterData.CitizenId == null) characterData.CitizenId = CitizenId;


            TriggerServerEvent(Events.Character.OnSave, API.GetCurrentResourceName(), JsonConvert.SerializeObject(characterData));
            TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 7500, "Пройдите в правый конец зала и посетите полицейский участок для оформления ID карты", "blue", "Info");


            await Delay(5000);

            TickReset();
        }

        private async Task ResetPedComponentsUnnecessaryData()
        {
            foreach (var component in CurrentPedComponents)
            {
                component.MaxTextureValue = new List<TextureModel>();
            }

        }
        private async Task ResetPedPropsUnnecessaryData()
        {
            foreach (var component in CurrentPedProps)
            {
                component.MaxTextureValue = new List<PropModel>();
            }
        }

        private async Task HandsUpAsync()
        {
            HandsIsUp = !HandsIsUp;

            var ped = API.PlayerPedId();
            API.ClearPedTasksImmediately(ped);
            if (HandsIsUp)
            {

                await LoadDict(AnimDict);
                API.TaskPlayAnim(ped, AnimDict, Anim, 8f, 8f, -1, 50, 0, false, false, false);
                if (API.IsPedInAnyVehicle(ped, false))
                {
                    var vehicle = API.GetVehiclePedIsIn(ped, false);
                    if (API.GetPedInVehicleSeat(vehicle, -1) == ped)
                    {
                        //Tick += UpdateControl;
                        canControl = true;
                        await Delay(0);
                    }
                }

            }
            else
            {
                API.TaskStartScenarioInPlace(ped, Scenario, -1, true);
                //Tick -= UpdateControl;
                canControl = false;
            }
        }

        private async Task MoveCameraWheelAsync(int delta)
        {
            if (!CanCamMove) return;
            var currentCam = CameraManager.GetActiveCam();
            if (currentCam == null || currentCam.Handle == -1) return;
            var ped = API.PlayerPedId();
            var pedPos = API.GetEntityCoords(ped, false);
            //var distance = Vector3.Distance(API.GetCamCoord(currentCam.Handle), pedPos);
            if (CurrentCamDist == 0) CurrentCamDist = Vector3.Distance(API.GetCamCoord(currentCam.Handle), pedPos);
            if (CurrentCamDist > 3.0f) CurrentCamDist = 3.0f;
            if (CurrentCamDist < 0.5f) CurrentCamDist = 0.5f;
            if (delta > 0)
            {
                CurrentCamDist += 0.2f;
            }
            else
            {
                CurrentCamDist -= 0.2f;
            }
            var offsetCoord = API.GetOffsetFromEntityInWorldCoords(ped, 0f, CurrentCamDist, 0f);
            await CameraManager.DeleteScene("tempCam");
            var tempCam = await CameraManager.CreateCam("createCharacterCam_tempCam", offsetCoord.X, offsetCoord.Y, currentCam.Pos.Z, 0f, 0f, 0f + 180f, 50f, "tempCam");
            
            var camRot = API.GetCamRot(tempCam.Handle, 0);
            camRot.Z = FirstPedHeading;
            API.SetCamRot(tempCam.Handle, camRot.X, camRot.Y, camRot.Z + 180f, 2);
            CameraManager.ActivateCam(tempCam);
            API.PointCamAtCoord(currentCam.Handle, pedPos.X, pedPos.Y, pedPos.Z);
        }

        private async Task MoveCameraXYAsync(int x, int y)
        {
            if (!CanCamMove) return;
            //Logger.Warn($"x {x} y {y}");
            var currentCam = CameraManager.GetActiveCam();
            //Logger.Warn($"currentCam {currentCam?.Name}");
            if (currentCam == null || currentCam.Handle == -1) return;
            var ped = API.PlayerPedId();

            if (x != 0)
            {
                if (x >= 1)
                {
                    var newRot = API.GetEntityRotation(ped, 2);
                    API.SetEntityRotation(ped, newRot.X, newRot.Y, newRot.Z + 1f, 2, true);               
                }
                if (x <= -1)
                {
                    var newRot = API.GetEntityRotation(ped, 2);
                    API.SetEntityRotation(ped, newRot.X, newRot.Y, newRot.Z - 1f, 2, true);
                }
                return;
            }
            else
            {
                if (y == 0 || y == 1 || y == -1) return;
                var pedPos = API.GetEntityCoords(ped, false);
                //var height = API.GetCamCoord(currentCam.Handle).Z;
                if (CurrentCamHeight == 0) CurrentCamHeight = currentCam.Pos.Z;
                if (CurrentCamDist == 0) CurrentCamDist = Vector3.Distance(API.GetCamCoord(currentCam.Handle), pedPos);
                if (y > 1)
                {
                    CurrentCamHeight -= 0.01f;
                }
                if (y < -1)
                {
                    CurrentCamHeight += 0.01f;
                }
             
                if (CurrentCamHeight > pedPos.Z + 1.0f) CurrentCamHeight = pedPos.Z + 1.0f;
                if (CurrentCamHeight < pedPos.Z - 0.5f) CurrentCamHeight = pedPos.Z - 0.5f;
                var offsetCoord = API.GetOffsetFromEntityInWorldCoords(ped, 0f, CurrentCamDist, CurrentCamHeight);
                await CameraManager.DeleteScene("tempCam");                
                var tempCam = await CameraManager.CreateCam("createCharacterCam_tempCam", currentCam.Pos.X, currentCam.Pos.Y, CurrentCamHeight, 0f, 0f, 0f + 180f, 50f, "tempCam");
                var camRot = API.GetCamRot(tempCam.Handle, 0);
                camRot.Z = FirstPedHeading;
                API.SetCamRot(tempCam.Handle, camRot.X, camRot.Y, camRot.Z + 180f, 0);
                //Logger.Error($"tempCam Handle -1");
                CameraManager.ActivateCam(tempCam);
                API.PointCamAtCoord(currentCam.Handle, pedPos.X, pedPos.Y, pedPos.Z);
            }


        }
        #endregion

        #region NUI
        private void RegisterNuiEvents()
        {
            NUI.RegisterNUICallback(Events.CharacterCreater.SetParent, OnSetParent);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetShapeMix, OnSetParent);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetSkinMix, OnSetParent);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetGender, OnSetGender);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetPedFaceFeatures, SetPedFaceFeatures);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetPedEarColor, OnSetPedEarColor);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetPedHeadOverlay, OnSetPedHeadOverlay);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetPedHeadOverlayOpacity, OnSetPedHeadOverlayOpacity);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetDrawableValue, SetDrawableValue);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetTextureValue, SetTextureValue);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetDrawablePropValue, SetDrawablePropValue);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetTexturePropValue, SetTexturePropValue);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetCamera, SetCamera);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetHairColor, SetHairColor);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetHairColor2, SetHairColor2);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetColorBeard, SetColorBeard);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetEyebrowsColor, SetEyebrowsColor);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetChestHairColor, SetChestHairColor);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetMakeUp, SetMakeUp);
            //NUI.RegisterNUICallback(Events.CharacterCreater.SetMakeUp2, SetMakeUp2);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetBlush, SetBlush);
            NUI.RegisterNUICallback(Events.CharacterCreater.SetLipstick, SetLipstick);

            NUI.RegisterNUICallback(Events.CharacterCreater.MovementWheel, MovementWheel);
            NUI.RegisterNUICallback(Events.CharacterCreater.MovementX, MovementX);
            NUI.RegisterNUICallback(Events.CharacterCreater.MovementY, MovementY);

            NUI.RegisterNUICallback(Events.CharacterCreater.HandsUp, HandsUp);
            NUI.RegisterNUICallback(Events.CharacterCreater.Save, Save);

            NUI.RegisterNUICallback(Events.CharacterCreater.IdentityCreate, IdentityCreate);
            NUI.RegisterNUICallback(Events.CharacterCreater.IdentityCancel, IdentityCancel);



        }

        private CallbackDelegate IdentityCancel(IDictionary<string, object> data, CallbackDelegate result)
        {
            NUI.Execute(new { request = "characterCreate.hide" });
            NUI.Focus(false, false);
            result("ok");
            return result;
        }

        private CallbackDelegate IdentityCreate(IDictionary<string, object> data, CallbackDelegate result)
        {


            var firstname = data["firstname"].ToString();
            if (firstname == null || firstname == "")
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Имя не может быть пустым", "crimson", "Warning");
                result("error");
                return result;
            }

            if (firstname.Length <= 3)
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Имя не может быть не может быть таким коротким", "crimson", "Warning");
                result("error");
                return result;
            }

            var lastname = data["lastname"].ToString();
            if (lastname == null || lastname == "")
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Фамилия не может быть пустой", "crimson", "Warning");
                result("error");
                return result;
            }

            if (lastname.Length <= 3)
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Фамилия не может быть такой короткой", "crimson", "Warning");
                result("error");
                return result;
            }

            var sex = data["sex"].ToString();
            if (sex == null || sex == "")
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Пол не может быть пустым", "crimson", "Warning");
                result("error");
                return result;
            }


            var dob = data["dob"].ToString();
            DateTime dateTime = DateTime.Now;
            if (dob == null || dob == "")
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Дата рождения не может быть пустой", "crimson", "Warning");
                result("error");
                return result;
            }

            try
            {
                dateTime = DateTime.Parse(dob);
                Console.WriteLine("The specified date is valid: " + dob);
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to parse the specified date");
            }
            var today = DateTime.Today;
            if ((today - dateTime).TotalDays > 365 * 85)
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Ты слишком стар для этого дерьма", "crimson", "Warning");
                result("error");
                return result;
            }

            if ((today - dateTime).TotalDays < 365 * 18)
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Ты слишком молодой для нашего штата", "crimson", "Warning");
                result("error");
                return result;
            }

            var placeB = data["placeb"].ToString();
            if (placeB == null || placeB == "")
            {

                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Место рождения не может быть пустым", "Warning");
                result("error");
                return result;
            }

            var nationality = data["nationality"].ToString();
            if (nationality == null || nationality == "")
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Национальность не может быть пустой", "Warning");
                result("error");
                return result;
            }

            var hight = int.Parse(data["hight"].ToString());

            var activeCard = int.Parse(data["activeCard"].ToString());
            if (activeCard == -1)
            {
                TriggerEvent("Notification.AddAdvanceNotif", "ПЕРСОНАЖ", "", 5500, "Вы не выбрали карточку персонажа", "Warning");
                result("error");
                return result;
            }

            SaveIdentity(firstname, lastname, sex, dob, placeB, nationality, hight, activeCard);
            result("ok");
            return result;
        }

        private CallbackDelegate Save(IDictionary<string, object> data, CallbackDelegate result)
        {

            SaveAsync();
            result("ok");
            return result;
        }

        private CallbackDelegate HandsUp(IDictionary<string, object> data, CallbackDelegate result)
        {

            HandsUpAsync();
            result("ok");
            return result;
        }

        private CallbackDelegate MovementY(IDictionary<string, object> data, CallbackDelegate result)
        {
            var delta = int.Parse(data["delta"].ToString());
            MoveCameraXYAsync(0, delta);
            result("ok");
            return result;
        }



        private CallbackDelegate MovementX(IDictionary<string, object> data, CallbackDelegate result)
        {
            var delta = int.Parse(data["delta"].ToString());
            MoveCameraXYAsync(delta, 0);
            result("ok");
            return result;
        }

        private CallbackDelegate MovementWheel(IDictionary<string, object> data, CallbackDelegate result)
        {
            var delta = int.Parse(data["delta"].ToString());
            MoveCameraWheelAsync(delta);
            result("ok");
            return result;
        }

        private CallbackDelegate SetMakeUp(IDictionary<string, object> data, CallbackDelegate result)
        {
            var R = int.Parse(data["R"].ToString());
            var G = int.Parse(data["G"].ToString());
            var B = int.Parse(data["B"].ToString());
            SetPedMakeUpAsync(R, G, B);
            result("ok");
            return result;
        }
        private CallbackDelegate SetBlush(IDictionary<string, object> data, CallbackDelegate result)
        {
            var R = int.Parse(data["R"].ToString());
            var G = int.Parse(data["G"].ToString());
            var B = int.Parse(data["B"].ToString());
            SetPedBlushAsync(R, G, B);
            result("ok");
            return result;
        }
        private CallbackDelegate SetLipstick(IDictionary<string, object> data, CallbackDelegate result)
        {
            var R = int.Parse(data["R"].ToString());
            var G = int.Parse(data["G"].ToString());
            var B = int.Parse(data["B"].ToString());
            SetPedLipstickAsync(R, G, B);
            result("ok");
            return result;
        }

        private CallbackDelegate SetMakeUp2(IDictionary<string, object> data, CallbackDelegate result)
        {
            var R2 = int.Parse(data["R2"].ToString());
            var G2 = int.Parse(data["G2"].ToString());
            var B2 = int.Parse(data["B2"].ToString());
            SetPedMakeUpAsync2(R2, G2, B2);
            result("ok");
            return result;
        }

        private CallbackDelegate SetChestHairColor(IDictionary<string, object> data, CallbackDelegate result)
        {
            var R = int.Parse(data["R"].ToString());
            var G = int.Parse(data["G"].ToString());
            var B = int.Parse(data["B"].ToString());
            SetPedChestHairAsync(R, G, B);
            result("ok");
            return result;
        }
        private CallbackDelegate SetEyebrowsColor(IDictionary<string, object> data, CallbackDelegate result)
        {
            var R = int.Parse(data["R"].ToString());
            var G = int.Parse(data["G"].ToString());
            var B = int.Parse(data["B"].ToString());
            SetPedHairEyebrowsAsync(R, G, B);
            result("ok");
            return result;
        }

        private CallbackDelegate SetColorBeard(IDictionary<string, object> data, CallbackDelegate result)
        {
            var R = int.Parse(data["R"].ToString());
            var G = int.Parse(data["G"].ToString());
            var B = int.Parse(data["B"].ToString());
            SetPedHairBeardAsync(R, G, B);
            result("ok");
            return result;
        }

        private CallbackDelegate SetHairColor(IDictionary<string, object> data, CallbackDelegate result)
        {
            var R = int.Parse(data["R"].ToString());
            var G = int.Parse(data["G"].ToString());
            var B = int.Parse(data["B"].ToString());
            //var Highlight = int.Parse(data["Highlight"].ToString());

            SetPedHairAsync(R, G, B);
            //var hair = JsonConvert.DeserializeObject<PedHair>(hairJson);
            //Logger.Warn($"SetHairColor hair {JsonConvert.SerializeObject(hair)}");
            result("ok");
            return result;
        }


        private CallbackDelegate SetHairColor2(IDictionary<string, object> data, CallbackDelegate result)
        {
            var R2 = int.Parse(data["R2"].ToString());
            var G2 = int.Parse(data["G2"].ToString());
            var B2 = int.Parse(data["B2"].ToString());
            //var Highlight = int.Parse(data["Highlight"].ToString());

            SetPedHairAsync2(R2, G2, B2);
            //var hair = JsonConvert.DeserializeObject<PedHair>(hairJson);
            //Logger.Warn($"SetHairColor hair {JsonConvert.SerializeObject(hair)}");
            result("ok");
            return result;
        }


        private CallbackDelegate SetTexturePropValue(IDictionary<string, object> data, CallbackDelegate result)
        {
            var propId = int.Parse(data["propId"].ToString());
            var drawableId = int.Parse(data["drawableId"].ToString());
            var textureId = int.Parse(data["value"].ToString());

            var prop = CurrentPedProps.FirstOrDefault(c => c.Prop_Id == propId);
            prop.Texture = textureId;

            SetPropsAsync(prop);
            result("ok");
            return result;
        }

        private CallbackDelegate SetDrawablePropValue(IDictionary<string, object> data, CallbackDelegate result)
        {
            var propId = int.Parse(data["propId"].ToString());
            var drawableId = int.Parse(data["value"].ToString());
            var prop = CurrentPedProps.FirstOrDefault(c => c.Prop_Id == propId);
            prop.Drawable = drawableId;
            SetPropsAsync(prop);
            result("ok");
            return result;
        }

        private CallbackDelegate SetCamera(IDictionary<string, object> data, CallbackDelegate result)
        {
            var camera = data["camera"].ToString();
            SetCameraAsync(camera);
            result("ok");
            return result;
        }



        private CallbackDelegate SetTextureValue(IDictionary<string, object> data, CallbackDelegate result)
        {
            var componentId = int.Parse(data["componentId"].ToString());
            var drawableId = int.Parse(data["drawableId"].ToString());
            var textureId = int.Parse(data["value"].ToString());

            var component = CurrentPedComponents.FirstOrDefault(c => c.Component_Id == componentId);
            component.Texture = textureId;

            SetComponentsAsync(component);
            result("ok");
            return result;
        }

        private CallbackDelegate SetDrawableValue(IDictionary<string, object> data, CallbackDelegate result)
        {
            var componentId = int.Parse(data["componentId"].ToString());
            var drawableId = int.Parse(data["value"].ToString());

            var component = CurrentPedComponents.FirstOrDefault(c => c.Component_Id == componentId);

            component.Drawable = drawableId;
            SetComponentsAsync(component);
            result("ok");
            return result;
        }



        private CallbackDelegate OnSetPedHeadOverlayOpacity(IDictionary<string, object> data, CallbackDelegate result)
        {
            var _type = int.Parse(data["type"].ToString());
            var value = int.Parse(data["value"].ToString());
            switch (_type)
            {
                case 0:
                    CurrentPedHeadOverlay.Blemishes.Opacity = value;
                    break;
                case 1:
                    CurrentPedHeadOverlay.Beard.Opacity = value;
                    break;
                case 2:
                    CurrentPedHeadOverlay.Eyebrows.Opacity = value;
                    break;
                case 3:
                    CurrentPedHeadOverlay.Ageing.Opacity = value;
                    break;
                case 4:
                    CurrentPedHeadOverlay.MakeUp.Opacity = value;
                    break;
                case 5:
                    CurrentPedHeadOverlay.Blush.Opacity = value;
                    break;
                case 6:
                    CurrentPedHeadOverlay.Complexion.Opacity = value;
                    break;
                case 7:
                    CurrentPedHeadOverlay.SunDamage.Opacity = value;
                    break;
                case 8:
                    CurrentPedHeadOverlay.Lipstick.Opacity = value;
                    break;
                case 9:
                    CurrentPedHeadOverlay.MoleAndFreckles.Opacity = value;
                    break;
                case 10:
                    CurrentPedHeadOverlay.ChestHair.Opacity = value;
                    break;
                case 11:
                    CurrentPedHeadOverlay.BodyBlemishes.Opacity = value;
                    break;
                case 12:
                    CurrentPedHeadOverlay.AddBodyBlemishes.Opacity = value;
                    break;
                default:
                    break;
            }

            SetPedHeadOverlayAsync();
            result("ok");
            return result;
        }

        private CallbackDelegate OnSetPedHeadOverlay(IDictionary<string, object> data, CallbackDelegate result)
        {
            //Logger.Info($";  //;  //;  //;  //;  //;  //;  //;  //;  //;  //;  //OnSetPedHeadOverlay;  //;  //;  //;  //;  //;  //;  //;  //;  //;  //;  //-");
            //var json = data["pedHeadOverlay"].ToString();
            ////Logger.Info($"json {json}");
            //if (data["pedHeadOverlay"] != null) CurrentPedHeadOverlay = JsonConvert.DeserializeObject<PedHeadOverlays>(json);
            //Logger.Info($"CurrentPedHeadOverlay {JsonConvert.SerializeObject(CurrentPedHeadOverlay)}");

            var _type = int.Parse(data["type"].ToString());
            var index = int.Parse(data["index"].ToString());
            //var value = int.Parse(data["value"].ToString());
            switch (_type)
            {
                case 0:
                    CurrentPedHeadOverlay.Blemishes.Index = index;
                    break;
                case 1:
                    CurrentPedHeadOverlay.Beard.Index = index;
                    break;
                case 2:
                    CurrentPedHeadOverlay.Eyebrows.Index = index;
                    break;
                case 3:
                    CurrentPedHeadOverlay.Ageing.Index = index;
                    break;
                case 4:
                    CurrentPedHeadOverlay.MakeUp.Index = index;
                    break;
                case 5:
                    CurrentPedHeadOverlay.Blush.Index = index;
                    break;
                case 6:
                    CurrentPedHeadOverlay.Complexion.Index = index;
                    break;
                case 7:
                    CurrentPedHeadOverlay.SunDamage.Index = index;
                    break;
                case 8:
                    CurrentPedHeadOverlay.Lipstick.Index = index;
                    break;
                case 9:
                    CurrentPedHeadOverlay.MoleAndFreckles.Index = index;
                    break;
                case 10:
                    CurrentPedHeadOverlay.ChestHair.Index = index;
                    break;
                case 11:
                    CurrentPedHeadOverlay.BodyBlemishes.Index = index;
                    break;
                case 12:
                    CurrentPedHeadOverlay.AddBodyBlemishes.Index = index;
                    break;
                default:
                    break;
            }

            SetPedHeadOverlayAsync();
            result("ok");
            return result;
        }

        private CallbackDelegate OnSetPedEarColor(IDictionary<string, object> data, CallbackDelegate result)
        {
            var ped = API.PlayerPedId();

            if (data["EarColor"] != null) EarColor = int.Parse(data["EarColor"].ToString());
            API.SetPedEyeColor(ped, EarColor);
            NUI.Execute(
                new
                {
                    request = "PedEyeColor.update",
                    EarColor = EarColor,

                });
            result("ok");
            return result;
        }

        private CallbackDelegate SetPedFaceFeatures(IDictionary<string, object> data, CallbackDelegate result)
        {
            ////Logger.Info($"CurrentPedFaceFeatures {JsonConvert.SerializeObject(CurrentPedFaceFeatures)}");
            ////Logger.Info($"pedFaceFeatures {JsonConvert.SerializeObject(data["pedFaceFeatures"])}");
            var json = data["pedFaceFeatures"].ToString();
            ////Logger.Info($"json {json}");
            if (data["pedFaceFeatures"] != null) CurrentPedFaceFeatures = JsonConvert.DeserializeObject<PedFaceFeatures>(json);
            ////Logger.Info($"CurrentPedFaceFeatures {JsonConvert.SerializeObject(CurrentPedFaceFeatures)}");





            SetPedFaceFeaturesAsync(CurrentPedFaceFeatures);
            result("ok");
            return result;
        }

        private CallbackDelegate OnSetGender(IDictionary<string, object> data, CallbackDelegate result)
        {
            var ped = API.PlayerPedId();
            if (data["Gender"] != null) Gender = int.Parse(data["Gender"].ToString());
            if (Gender == 0 && CurrentPedModel != "mp_m_freemode_01")
            {
                CurrentPedModel = "mp_m_freemode_01";
                SetModelForPedAsync(CurrentPedModel);

            }
            if (Gender == 1 && CurrentPedModel != "mp_f_freemode_01")
            {
                CurrentPedModel = "mp_f_freemode_01";
                SetModelForPedAsync(CurrentPedModel);
            }
            result("ok");
            return result;
        }

        private CallbackDelegate OnSetParent(IDictionary<string, object> data, CallbackDelegate result)
        {
            Debug.WriteLine("OnSetParent 000");
            var ped = API.PlayerPedId();
            if (data["ParentFirst"] != null) CurrentPedHeadBlend.SkinFirst = int.Parse(data["ParentFirst"].ToString());
            if (data["ParentSecond"] != null) CurrentPedHeadBlend.SkinSecond = int.Parse(data["ParentSecond"].ToString());
            if (data["ShapeMix"] != null) CurrentPedHeadBlend.ShapeMix = (float)Convert.ToDouble(data["ShapeMix"].ToString()) / 10;
            if (data["SkinMix"] != null) CurrentPedHeadBlend.SkinMix = (float)Convert.ToDouble(data["SkinMix"].ToString()) / 10;


            API.SetPedHeadBlendData(ped, CurrentPedHeadBlend.SkinFirst, CurrentPedHeadBlend.SkinSecond, 0, CurrentPedHeadBlend.SkinFirst, CurrentPedHeadBlend.SkinSecond, 0,
                CurrentPedHeadBlend.ShapeMix, CurrentPedHeadBlend.SkinMix, 0f, false);
            //SetPedHeadBlendData(playerPed, skinData['face'].mom, skinData['face'].dad, 0, skinData['face'].mom, skinData['face'].dad, 0, weightFace, weightSkin, 0.0, false)
            result("ok");
            Debug.WriteLine("OnSetParent +++");
            return result;
        }

        public static async Task SetModelForPed(string model)
        {
            var hash = (uint)API.GetHashKey(model);
            if (!API.IsModelInCdimage(hash)) return;
            var player = API.PlayerId();
            API.RequestModel(hash);
            while (!API.HasModelLoaded(hash)) await BaseScript.Delay(10);
            API.SetPlayerModel(player, hash);
            //API.SetPedComponentVariation();
            //API.SetPedRandomComponentVariation(API.PlayerPedId(), false);
            API.SetPedDefaultComponentVariation(API.PlayerPedId());
            //Logger.Info($"SetPlayerModel done model {model} hash {hash}");
            await BaseScript.Delay(1000);
            API.SetModelAsNoLongerNeeded((uint)API.GetHashKey(model));
        }


        public static async Task LoadDict(string dict)
        {
            API.RequestAnimDict(dict);
            while (!API.HasAnimDictLoaded(dict))
            {
                await BaseScript.Delay(100);
            }
        }
        public static async Task ResetTask()
        {
            var ped = API.PlayerPedId();
            API.ClearPedTasks(ped);
            API.FreezeEntityPosition(ped, false);
            await BaseScript.Delay(100);
        }




        #endregion

        #region Events
        [EventHandler(Events.CharacterCreater.TickReset)]
        private void TickReset()
        {

            CanInterract = true;
            Tick += Update;
            Tick += UpdateInteract;
            Tick += UpdateControl;
        }

        [EventHandler(Events.Character.OnSetCitizenIdBankAccount)]
        private async void OnSetCitizenIdBankAccount(string citizenId, string bankaccount, string json = "")
        {
            Debug.WriteLine($"OnSetCitizenIdBankAccount {json}");
            BankAccount = bankaccount;
            CitizenId = citizenId;
            await Init(json);
        }


        #endregion

        
    }
}
