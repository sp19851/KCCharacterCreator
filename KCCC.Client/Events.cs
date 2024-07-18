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
            public const string SetParent = "CharaterCreater.SetParent";
            public const string SetSkinMix = "CharaterCreater.SetSkinMix";
            public const string SetShapeMix = "CharaterCreater.SetShapeMix";
            public const string SetGender = "CharaterCreater.SetGender";
            public const string SetPedFaceFeatures = "CharaterCreater.SetPedFaceFeatures";
            public const string SetPedEarColor = "CharaterCreater.SetPedEarColor";
            public const string SetPedHeadOverlay = "CharaterCreater.SetPedHeadOverlay";
            public const string SetPedHeadOverlayOpacity = "CharaterCreater.SetPedHeadOverlayOpacity";
            public const string SetDrawableValue = "CharaterCreater.SetDrawableValue";
            public const string SetTextureValue = "CharaterCreater.SetTextureValue";
            public const string SetCamera = "CharaterCreater.SetCamera";
            public const string SetDrawablePropValue = "CharaterCreater.SetDrawablePropValue";
            public const string SetTexturePropValue = "CharaterCreater.SetTexturePropValue";
            public const string SetHairColor = "CharaterCreater.SetHairColor";
            public const string SetHairColor2 = "CharaterCreater.SetHairColor2";
            public const string SetColorBeard = "CharaterCreater.SetColorBeard";
            public const string SetEyebrowsColor = "CharaterCreater.SetEyebrowsColor";
            public const string SetChestHairColor = "CharaterCreater.SetChestHairColor";
            public const string SetMakeUp = "CharaterCreater.SetMakeUp";
            public const string SetMakeUp2 = "CharaterCreater.SetMakeUp2";
            public const string SetBlush = "CharaterCreater.SetBlush";
            public const string SetLipstick = "CharaterCreater.SetLipstick";

            public const string Camera_OnBtnClicked = "CharaterCreater.Camera_OnBtnClicked";
            public const string MovementX = "CharaterCreater.MovementX";
            public const string MovementY = "CharaterCreater.MovementY";
            public const string MovementWheel = "CharaterCreater.MovementWheel";

            public const string HandsUp = "CharaterCreater.HandsUp";
            public const string Save = "CharaterCreater.Save";

            public const string IdentityCreate = "Identity.Create";
            public const string TickReset = "CharaterCreater.TickReset";


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
