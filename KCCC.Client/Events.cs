using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCCC.Client
{
    public static class Events
    {
        public static class CharacterCreater
        {
            public const string SetParent = "CharacterCreater.SetParent";
            public const string SetSkinMix = "CharacterCreater.SetSkinMix";
            public const string SetShapeMix = "CharacterCreater.SetShapeMix";
            public const string SetGender = "CharacterCreater.SetGender";
            public const string SetPedFaceFeatures = "CharacterCreater.SetPedFaceFeatures";
            public const string SetPedEarColor = "CharacterCreater.SetPedEarColor";
            public const string SetPedHeadOverlay = "CharacterCreater.SetPedHeadOverlay";
            public const string SetPedHeadOverlayOpacity = "CharacterCreater.SetPedHeadOverlayOpacity";
            public const string SetDrawableValue = "CharacterCreater.SetDrawableValue";
            public const string SetTextureValue = "CharacterCreater.SetTextureValue";
            public const string SetCamera = "CharacterCreater.SetCamera";
            public const string SetDrawablePropValue = "CharacterCreater.SetDrawablePropValue";
            public const string SetTexturePropValue = "CharacterCreater.SetTexturePropValue";
            public const string SetHairColor = "CharacterCreater.SetHairColor";
            public const string SetHairColor2 = "CharacterCreater.SetHairColor2";
            public const string SetColorBeard = "CharacterCreater.SetColorBeard";
            public const string SetEyebrowsColor = "CharacterCreater.SetEyebrowsColor";
            public const string SetChestHairColor = "CharacterCreater.SetChestHairColor";
            public const string SetMakeUp = "CharacterCreater.SetMakeUp";
            public const string SetMakeUp2 = "CharacterCreater.SetMakeUp2";
            public const string SetBlush = "CharacterCreater.SetBlush";
            public const string SetLipstick = "CharacterCreater.SetLipstick";

            public const string Camera_OnBtnClicked = "CharacterCreater.Camera_OnBtnClicked";
            public const string MovementX = "CharacterCreater.MovementX";
            public const string MovementY = "CharacterCreater.MovementY";
            public const string MovementWheel = "CharacterCreater.MovementWheel";

            public const string HandsUp = "CharacterCreater.HandsUp";
            public const string Save = "CharacterCreater.Save";

            public const string IdentityCreate = "Identity.Create";
            public const string TickReset = "CharacterCreater.TickReset";


        }
        public static class Character
        {
            public const string OnSetCitizenIdBankAccount = "Character.OnSetCitizenIdBankAccount";
            public const string OnSave = "Character.OnSave";
        }
        public class Banking
        {
            public const string OnOpen = "Banking.Open";
        }
    }
    
}
