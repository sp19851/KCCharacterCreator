const CharacterCreate = {
    data() {
        return {
            keys: [],
            characterCreateShow: false,
            title: "",
            window: "",
            Windows: [],
            containerShow: false,
            mams: [
                { "id": 0, "SkinId": 21, "Name": "Хана", "Value": "female_0" },
                { "id": 1, "SkinId": 22, "Name": "Одри", "Value": "female_1" },
                { "id": 2, "SkinId": 23, "Name": "Жасмин", "Value": "female_2" },
                { "id": 3, "SkinId": 24, "Name": "Жизель", "Value": "female_3" },
                { "id": 4, "SkinId": 25, "Name": "Амелия", "Value": "female_4" },
                { "id": 5, "SkinId": 26, "Name": "Изабелла", "Value": "female_5" },
                { "id": 6, "SkinId": 27, "Name": "Зои", "Value": "female_6" },
                { "id": 7, "SkinId": 28, "Name": "Ава", "Value": "female_7" },
                { "id": 8, "SkinId": 29, "Name": "Камила", "Value": "female_8" },
                { "id": 9, "SkinId": 30, "Name": "Виолет", "Value": "female_9" },
                { "id": 10, "SkinId": 31, "Name": "София", "Value": "female_10" },
                { "id": 11, "SkinId": 32, "Name": "Эвелин", "Value": "female_11" },
                { "id": 12, "SkinId": 33, "Name": "Николь", "Value": "female_12" },
                { "id": 13, "SkinId": 34, "Name": "Грейс", "Value": "female_13" },
                { "id": 14, "SkinId": 35, "Name": "Брианна", "Value": "female_14" },
                { "id": 15, "SkinId": 36, "Name": "Натали", "Value": "female_15" },
                { "id": 16, "SkinId": 37, "Name": "Одивия", "Value": "female_16" },
                { "id": 17, "SkinId": 38, "Name": "Элизабет", "Value": "female_17" },
                { "id": 18, "SkinId": 39, "Name": "Шарлота", "Value": "female_18" },
                { "id": 19, "SkinId": 40, "Name": "Шарлота", "Value": "female_19" },
                { "id": 20, "SkinId": 41, "Name": "Эмма", "Value": "female_20" },
                { "id": 21, "SkinId": 45, "Name": "Мисти", "Value": "special_female_0" },
            ],
            dads: [
                { "id": 0, "SkinId": 0, "Name": "Бенжамин", "Value": "male_0" },
                { "id": 1, "SkinId": 1, "Name": "Даниель", "Value": "male_1" },
                { "id": 2, "SkinId": 2, "Name": "Джошуа", "Value": "male_2" },
                { "id": 3, "SkinId": 3, "Name": "Ной", "Value": "male_3" },
                { "id": 4, "SkinId": 4, "Name": "Эндри", "Value": "male_4" },
                { "id": 5, "SkinId": 5, "Name": "Хуан", "Value": "male_5" },
                { "id": 6, "SkinId": 6, "Name": "Алекс", "Value": "male_6" },
                { "id": 7, "SkinId": 7, "Name": "Иссак", "Value": "male_7" },
                { "id": 8, "SkinId": 8, "Name": "Эван", "Value": "male_8" },
                { "id": 9, "SkinId": 9, "Name": "Итан", "Value": "male_9" },
                { "id": 10, "SkinId": 10, "Name": "Винцент", "Value": "male_10" },
                { "id": 11, "SkinId": 11, "Name": "Энджен", "Value": "male_11" },
                { "id": 12, "SkinId": 12, "Name": "Диего", "Value": "male_12" },
                { "id": 13, "SkinId": 13, "Name": "Андриан", "Value": "male_13" },
                { "id": 14, "SkinId": 14, "Name": "Габриель", "Value": "male_14" },
                { "id": 15, "SkinId": 15, "Name": "Михаэль", "Value": "male_15" },
                { "id": 16, "SkinId": 16, "Name": "Саньтяго", "Value": "male_16" },
                { "id": 17, "SkinId": 17, "Name": "Кевин", "Value": "male_17" },
                { "id": 18, "SkinId": 18, "Name": "Луиз", "Value": "male_18" },
                { "id": 19, "SkinId": 19, "Name": "Самуэль", "Value": "male_19" },
                { "id": 20, "SkinId": 20, "Name": "Энтони", "Value": "male_20" },
                { "id": 21, "SkinId": 42, "Name": "Клауд", "Value": "male_21" },
                { "id": 22, "SkinId": 43, "Name": "Нико", "Value": "male_22" },
                { "id": 23, "SkinId": 44, "Name": "Джон", "Value": "male_23" },
            ],
            mamImgName: "female_0",
            mamImgValue: "Хана",
            mamId: 21,
            dadImgName: "male_0",
            dadImgValue: "Бенжамин",
            dadId: 0,
            shapeMix: 5,
            skinMix: 5,
            PedFaceFeatures: "",
            EarColor: "black",
            /*NoseWidth:0,
            NosePeakHigh:0,
            NosePeakSize:0,
            NoseBoneHigh:0,
            NosePeakLowering:0,
            NoseBoneTwist:0,
            EyeBrownHigh:0,
            EyeBrownForward:0,
            CeeksBoneHigh:0,
            CheeksBoneWidth:0,
            CheeksWidth:0,
            EyesOpening:0,
            LipsThickness:0,
            JawBoneWidth:0,
            JawBoneBackSize:0,
            ChinBoneLowering:0,
            ChinBoneLenght:0,
            ChinBoneSize:0,
            ChinHole:0,
            NeckThickness:0,*/
            //PedHeadOverlay
            //{
            //Style
            //MaxValue
            //Index
            //Opacity
            //Color

            //}
            Blemishes: {
                Style: 0,
                Index: 2,
                MaxValue: 3,
                Opacity: 0

            },

            Beard: 0,
            Eyebrows: 0,
            Ageing: 0,
            MakeUp: 0,
            Blush: 0,
            Complexion: 0,
            SunDamage: 0,
            Lipstick: 0,
            MoleAndFreckles: 0,
            ChestHair: 0,
            BodyBlemishes: 0,
            AddBodyBlemishes: 0,
            Components: [
                { "Drawable": 0, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 1, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 2, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 3, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 4, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 5, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 6, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 7, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 8, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 9, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 10, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 11, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 12, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
            ],
            Props: [
                { "Drawable": 0, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 0, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 0, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 0, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 0, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 0, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 0, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
                { "Drawable": 0, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
            ],
            Hairs: [
                { "Drawable": 12, "Texture": 0, "MaxDrawableValue": 0, "MaxTextureValue": [] },
            ],
            HairsColors: [
                { "id": 0, "Style": 0, "R": 28, "G": 31, "B": 33 },
            ],
            HairsColors2: [
                { "id": 0, "Style": 0, "R": 28, "G": 31, "B": 33 },
            ],
            MakeUpColors: [
                { "id": 0, "Style": 0, "R": 28, "G": 31, "B": 33 }
            ],
            Hair: {
                R: 0,
                G: 0,
                B: 0,
                R2: 0,
                G2: 0,
                B2: 0,

            },


        }
    },
    methods: {
        onShow() {
            this.characterCreateShow = true;
            this.containerShow = false;
        },
        onClose() {
            this.characterCreateShow = false;
        },
        GetWindow(window) {
            if (this.window == window) {
                return true;
            }
            return false;
        },
        SelectWindow(window) {
            switch (window) {
                case "Parents":
                    this.title = "Наследственность",
                        this.window = window;
                    break;
                case "Face":
                    this.title = "Лицо",
                        this.window = window;
                    break;
                case "Clothes":
                    this.title = "Одежда",
                        this.window = window;
                    break;
                case "Hair":
                    this.title = "Волосы",
                        this.window = window;
                    break;
                case "Makeup":
                    this.title = "Макияж",
                        this.window = window;
                    break;
                default:
                    break;
            }
            this.containerShow = true
            api
                .post("CharacterCreater.SetCamera", {
                    camera: this.window,

                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        CanShow(window) {
            switch (window) {
                case "Parents":
                    return this.Windows.includes("all");
                case "Face":
                    return this.Windows.includes("all");
                case "Clothes":
                    return this.Windows.includes("clothes");
                case "Hair":
                    return this.Windows.includes("barber");
                case "Makeup":
                    return this.Windows.includes("barber");

                default:
                    break;
            }
        },
        GetMamImg() {
            let img = `https://nui-img/char_creator_portraits/` + this.mamImgName
            return img
        },
        GetDadImg() {
            let img = `https://nui-img/char_creator_portraits/` + this.dadImgName
            return img
        },
        GetPrevMam() {
            let newEl;
            let currEl = this.mams.find((element) => element.Value == this.mamImgName);
            //console.log('currEl', JSON.stringify(currEl))
            if (currEl != undefined) {
                if (currEl.id > 0) {
                    newEl = this.mams.find((element) => element.id == currEl.id - 1);
                    this.mamImgName = newEl.Value
                    this.mamImgValue = newEl.Name
                    this.mamId = newEl.SkinId
                    this.shapeMix = 5,
                        this.skinMix = 5
                }
                else {
                    newEl = this.mams[this.mams.length - 1];
                    this.mamImgName = newEl.Value
                    this.mamImgValue = newEl.Name
                    this.mamId = newEl.SkinId,
                        this.shapeMix = 5,
                        this.skinMix = 5
                }
            }
            api
                .post("CharacterCreater.SetParent", {
                    ParentFirst: this.mamId,
                    ParentSecond: this.dadId,
                    ShapeMix: this.shapeMix,
                    SkinMix: this.skinMix,
                })
                .catch((e) => {
                    console.log(e.message);
                });
            //console.log('prevmam', this.mamImgName, this.mamImgValue)
        },
        GetNextMam() {
            let newEl;
            let currEl = this.mams.find((element) => element.Value == this.mamImgName);
            if (currEl != undefined) {
                if (currEl.id < this.mams.length - 1) {
                    newEl = this.mams.find((element) => element.id == currEl.id + 1);
                    this.mamImgName = newEl.Value
                    this.mamImgValue = newEl.Name
                    this.mamId = newEl.SkinId,
                        this.shapeMix = 5,
                        this.skinMix = 5
                }
                else {
                    newEl = this.mams[0];
                    this.mamImgName = newEl.Value
                    this.mamImgValue = newEl.Name
                    this.mamId = newEl.SkinId,
                        this.shapeMix = 5,
                        this.skinMix = 5
                }
            }
            api
                .post("CharacterCreater.SetParent", {
                    ParentFirst: this.mamId,
                    ParentSecond: this.dadId,
                    ShapeMix: this.shapeMix,
                    SkinMix: this.skinMix,
                })
                .catch((e) => {
                    console.log(e.message);
                });
            //console.log('nextmam', this.mamImgName, this.mamImgValue)
        },
        GetPrevDad() {
            let newEl;
            let currEl = this.dads.find((element) => element.Value == this.dadImgName);
            if (currEl != undefined) {
                if (currEl.id > 0) {
                    newEl = this.dads.find((element) => element.id == currEl.id - 1);
                    this.dadImgName = newEl.Value
                    this.dadImgValue = newEl.Name
                    this.dadId = newEl.SkinId,
                        this.shapeMix = 5,
                        this.skinMix = 5
                }
                else {
                    newEl = this.dads[this.dads.length - 1];
                    this.dadImgName = newEl.Value
                    this.dadImgValue = newEl.Name
                    this.dadId = newEl.SkinId,
                        this.shapeMix = 5,
                        this.skinMix = 5
                }
            }
            api
                .post("CharacterCreater.SetParent", {
                    ParentFirst: this.mamId,
                    ParentSecond: this.dadId,
                    ShapeMix: this.shapeMix,
                    SkinMix: this.skinMix
                })
                .catch((e) => {
                    console.log(e.message);
                });
            //console.log('prevmam', this.mamImgName)
        },
        GetNextDad() {
            let newEl;
            let currEl = this.dads.find((element) => element.Value == this.dadImgName);
            if (currEl != undefined) {
                if (currEl.id < this.dads.length - 1) {
                    newEl = this.dads.find((element) => element.id == currEl.id + 1);
                    this.dadImgName = newEl.Value
                    this.dadImgValue = newEl.Name
                    this.dadId = newEl.SkinId,
                        this.shapeMix = 5,
                        this.skinMix = 5
                }
                else {
                    newEl = this.dads[0];
                    this.dadImgName = newEl.Value
                    this.dadImgValue = newEl.Name
                    this.dadId = newEl.SkinId,
                        this.shapeMix = 5,
                        this.skinMix = 5
                }
            }
            api
                .post("CharacterCreater.SetParent", {
                    ParentFirst: this.mamId,
                    ParentSecond: this.dadId,
                    ShapeMix: this.shapeMix,
                    SkinMix: this.skinMix
                })
                .catch((e) => {
                    console.log(e.message);
                });
            //console.log('prevmam', this.mamImgName)
        },
        SetShapeMix(value) {
            this.shapeMix = value
            api
                .post("CharacterCreater.SetShapeMix", {
                    ParentFirst: this.mamId,
                    ParentSecond: this.dadId,
                    ShapeMix: this.shapeMix,
                    SkinMix: this.skinMix
                })
                .catch((e) => {
                    console.log(e.message);
                });

        },
        SetSkinMix(value) {
            this.skinMix = value
            api
                .post("CharacterCreater.SetSkinMix", {
                    ParentFirst: this.mamId,
                    ParentSecond: this.dadId,
                    ShapeMix: this.shapeMix,
                    SkinMix: this.skinMix
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetGender(value) {
            api
                .post("CharacterCreater.SetGender", {
                    Gender: value,

                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetPedEarColor(value) {
            let colorValue = this.ConvertColor(value);
            api
                .post("CharacterCreater.SetPedEarColor", {
                    EarColor: colorValue,

                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetPedEarColor2(value) {
            console.log("SetPedEarColor2", value);
            api
                .post("CharacterCreater.SetPedEarColor", {
                    EarColor: value,

                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        ConvertColor(value) {
            let colorValue = -1
            switch (value) {
                case 'green':
                    colorValue = 0
                    break;
                case 'emerald':
                    colorValue = 1
                    break;
                case 'lightblue':
                    colorValue = 2
                    break;
                case 'oceanblue':
                    colorValue = 3
                    break;
                case 'lightbrown':
                    colorValue = 4
                    break;
                case 'darkbrown':
                    colorValue = 5
                    break;
                case 'darkgray':
                    colorValue = 7
                    break;
                case 'lightgray':
                    colorValue = 8
                    break;
                case 'black':
                    colorValue = 12
                    break;
                default:
                    break;
            }
            return colorValue
        },
        SetPedFaceFeatures(name, value) {
            switch (name) {
                case "EyeBrownHigh":
                    this.PedFaceFeatures.EyeBrownHigh = value;
                    break;
                case "EyeBrownForward":
                    this.PedFaceFeatures.EyeBrownForward = value;
                    break;
                case "NoseWidth":
                    this.PedFaceFeatures.NoseWidth = value;
                    break;
                case "NosePeakHigh":
                    this.PedFaceFeatures.NosePeakHigh = value;
                    break;
                case "NosePeakSize":
                    this.PedFaceFeatures.NosePeakSize = value;
                    break;
                case "NoseBoneHigh":
                    this.PedFaceFeatures.NoseBoneHigh = value;
                    break;
                case "NosePeakLowering":
                    this.PedFaceFeatures.NosePeakLowering = value;
                    break;
                case "NoseBoneTwist":
                    this.PedFaceFeatures.NoseBoneTwist = value;
                    break;
                case "CheeksBoneWidth":
                    this.PedFaceFeatures.CheeksBoneWidth = value;
                    break;
                case "CeeksBoneHigh":
                    this.PedFaceFeatures.CeeksBoneHigh = value;
                    break;
                case "CheeksWidth":
                    this.PedFaceFeatures.CheeksWidth = value;
                    break;
                case "LipsThickness":
                    this.PedFaceFeatures.LipsThickness = value;
                    break;
                case "JawBoneWidth":
                    this.PedFaceFeatures.JawBoneWidth = value;
                    break;
                case "JawBoneBackSize":
                    this.PedFaceFeatures.JawBoneBackSize = value;
                    break;
                case "ChinBoneLowering":
                    this.PedFaceFeatures.ChinBoneLowering = value;
                    break;
                case "ChinBoneLenght":
                    this.PedFaceFeatures.ChinBoneLenght = value;
                    break;
                case "ChinBoneSize":
                    this.PedFaceFeatures.ChinBoneSize = value;
                    break;
                case "ChinHole":
                    this.PedFaceFeatures.ChinHole = value;
                    break;
                case "EyesOpening":
                    this.PedFaceFeatures.EyesOpening = value;
                    break;
                case "EarColor":
                    //this.PedFaceFeatures.EarColor = value;
                    break;
                default:
                    break;
            }
            //console.log("PedFaceFeatures", JSON.stringify(this.PedFaceFeatures))
            api
                .post("CharacterCreater.SetPedFaceFeatures", {
                    pedFaceFeatures: JSON.stringify(this.PedFaceFeatures),
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetPedHeadOverlayStyle(type, value) {
            console.log("+++", type, value);
            switch (type) {
                case "Blemishes":
                    this.UpdatePedHeadOverlayStyle(0, value)
                    break;
                case "Beard":
                    this.UpdatePedHeadOverlayStyle(1, value)
                    break;
                case "Eyebrows":
                    this.UpdatePedHeadOverlayStyle(2, value)
                    break;
                case "Ageing":
                    this.UpdatePedHeadOverlayStyle(3, value)
                    break;
                case "MakeUp":
                    this.UpdatePedHeadOverlayStyle(4, value)
                    break;
                case "Blush":
                    this.UpdatePedHeadOverlayStyle(5, value)
                    break;
                case "Complexion":
                    this.UpdatePedHeadOverlayStyle(6, value)
                    break;
                case "SunDamage":
                    this.UpdatePedHeadOverlayStyle(7, value)
                    break;
                case "Lipstick":
                    this.UpdatePedHeadOverlayStyle(8, value)
                    break;
                case "MoleAndFreckles":
                    this.UpdatePedHeadOverlayStyle(9, value)
                    break;
                case "ChestHair":
                    this.UpdatePedHeadOverlayStyle(10, value)
                    break;
                case "BodyBlemishes":
                    this.UpdatePedHeadOverlayStyle(11, value)
                    break;
                case "AddBodyBlemishes":
                    this.UpdatePedHeadOverlayStyle(12, value)
                    break;

                default:
                    break;
            }

        },
        SetPedHeadOverlayOpacity(type, value) {
            switch (type) {
                case "Blemishes":
                    this.UpdatePedHeadOverlayOpacity("Blemishes", 0, value)
                    break;
                case "Beard":
                    this.UpdatePedHeadOverlayOpacity("Beard", 1, value)
                    break;
                case "Eyebrows":
                    this.UpdatePedHeadOverlayOpacity("Eyebrows", 2, value)
                    break;
                case "Ageing":
                    this.UpdatePedHeadOverlayOpacity("Ageing", 3, value)
                    break;
                case "MakeUp":
                    this.UpdatePedHeadOverlayOpacity("MakeUp", 4, value)
                    break;
                case "Blush":
                    this.UpdatePedHeadOverlayOpacity("Blush", 5, value)
                    break;
                case "Complexion":
                    this.UpdatePedHeadOverlayOpacity("Complexion", 6, value)
                    break;
                case "SunDamage":
                    this.UpdatePedHeadOverlayOpacity("SunDamage", 7, value)
                    break;
                case "Lipstick":
                    this.UpdatePedHeadOverlayOpacity("Lipstick", 8, value)
                    break;
                case "MoleAndFreckles":
                    this.UpdatePedHeadOverlayOpacity("MoleAndFreckles", 9, value)
                    break;
                case "ChestHair":
                    this.UpdatePedHeadOverlayOpacity("ChestHair", 10, value)
                    break;
                case "BodyBlemishes":
                    this.UpdatePedHeadOverlayOpacity("BodyBlemishes", 11, value)
                    break;
                case "AddBodyBlemishes":
                    this.UpdatePedHeadOverlayOpacity("AddBodyBlemishes", 12, value)
                    break;

                default:
                    break;
            }
            /*api
            .post("CharacterCreater.SetPedHeadOverlay", {
                pedHeadOverlay: JSON.stringify(this.PedHeadOverlay),
            })
            .catch((e) => {
                console.log(e.message);
            });*/
        },
        UpdatePedHeadOverlayStyle(type, index) {
            //console.log("UpdatePedHeadOverlayStyle", type, index);
            api
                .post("CharacterCreater.SetPedHeadOverlay", {
                    type: type,
                    index: index,

                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        UpdatePedHeadOverlayOpacity(typeName, type, value) {
            /*let el = this.PedHeadOverlay.find((element) => element.Style == type);
            if (el != undefined) {
                el.Opacity = value
            }
            console.log("+***+");
            console.log("pedHeadOverlay", JSON.stringify(this.PedFaceFeatures))
            api
                .post("CharacterCreater.SetPedHeadOverlay", {
                    pedHeadOverlay: JSON.stringify(this.PedHeadOverlay),
                })
                .catch((e) => {
                    console.log(e.message);
                });*/
            //console.log("UpdatePedHeadOverlayOpacity", type, value);
            api
                .post("CharacterCreater.SetPedHeadOverlayOpacity", {
                    type: type,
                    value: value,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        GetPedHeadOverlayStyleMax(type) {
            switch (type) {
                case "Blemishes":
                    return this.Blemishes.MaxValue;

                case "Beard":
                    return this.Beard.MaxValue;
                case "Eyebrows":
                    return this.Eyebrows.MaxValue;
                case "Ageing":
                    return this.Ageing.MaxValue;
                case "MakeUp":
                    return this.MakeUp.MaxValue;
                case "Blush":
                    return this.Blush.MaxValue;
                case "Complexion":
                    return this.Complexion.MaxValue;
                case "SunDamage":
                    return this.SunDamage.MaxValue;
                case "Lipstick":
                    return this.Lipstick.MaxValue;
                case "MoleAndFreckles":
                    return this.MoleAndFreckles.MaxValue;
                case "ChestHair":
                    return this.ChestHair.MaxValue;
                case "BodyBlemishes":
                    return this.BodyBlemishes.MaxValue;
                case "AddBodyBlemishes":
                    return this.AddBodyBlemishes.MaxValue;

                default:
                    break;
            }
        },

        /*GetPedHeadOverlayStyleValue(type){
            console.log("GetPedHeadOverlayStyleValue", this.Blemishes.Index);
            return this.Blemishes.Index
        },
        GetPedHeadOverlayOpacityValue(type){
            console.log("GetPedHeadOverlayOpacityValue", this.Blemishes.Opacity);
            return this.Blemishes.Opacity
        },*/
        CheckSelecting(color) {
            if (this.EarColor == this.ConvertColor(color)) {

                return true
            }
            return false
        },
        CheckMakeUpColorSelecting(color) {
            let curHairColor = 'rgb(' + this.MakeUp.R + ',' + this.MakeUp.G + "," + this.MakeUp.B + ')'
            if (curHairColor == color) {
                return true
            }
            return false
        },
        CheckBlushColorSelecting(color) {
            let curHairColor = 'rgb(' + this.Blush.R + ',' + this.Blush.G + "," + this.Blush.B + ')'
            if (curHairColor == color) {
                return true
            }
            return false
        },
        CheckLipstickColorSelecting(color) {
            let curHairColor = 'rgb(' + this.Blush.R + ',' + this.Lipstick.G + "," + this.Lipstick.B + ')'
            if (curHairColor == color) {
                return true
            }
            return false
        },
        CheckChestHairColorSelecting(color) {
            let curHairColor = 'rgb(' + this.ChestHair.R + ',' + this.ChestHair.G + "," + this.ChestHair.B + ')'
            if (curHairColor == color) {
                return true
            }
            return false
        },
        CheckEyebrowsColorSelecting(col, color) {
            let curHairColor = 'rgb(' + this.Eyebrows.R + ',' + this.Eyebrows.G + "," + this.Eyebrows.B + ')'
            //console.log("CheckEyebrowsColorSelecting", JSON.stringify(col));
            //console.log("CheckEyebrowsColorSelecting",  color, curHairColor);
            if (curHairColor == color) {
                return true
            }
            return false
        },
        CheckBeardColorSelecting(color) {

            let curHairColor = 'rgb(' + this.Beard.R + ',' + this.Beard.G + "," + this.Beard.B + ')'
            //console.log("CheckHairsColorSelecting", color, curHairColor);
            if (curHairColor == color) {
                //console.log("CheckHairsColorSelecting +++++", color, curHairColor, " +++++");
                return true
            }
            return false
        },
        CheckHairsColorSelecting(color) {

            let curHairColor = 'rgb(' + this.Hair.R + ',' + this.Hair.G + "," + this.Hair.B + ')'
            //console.log("CheckHairsColorSelecting", color, curHairColor);
            if (curHairColor == color) {
                //console.log("CheckHairsColorSelecting +++++", color, curHairColor, " +++++");
                return true
            }
            return false
        },
        CheckHairsColorSelecting2(color) {

            let curHairColor = 'rgb(' + this.Hair.R2 + ',' + this.Hair.G2 + "," + this.Hair.B2 + ')'
            //console.log("CheckHairsColorSelecting2", color, curHairColor);
            if (curHairColor == color) {

                return true
            }
            return false
        },
        IsSetColor() {
            if (this.EarColor != undefined) {

                return true
            }
            return false
        },
        GetMaxDrawableValue(value) {
            return this.Components[value].MaxDrawableValue
        },
        GetMaxTexturaValue(value) {
            let currEl = this.Components[value].MaxTextureValue.find((element) => element.DrawableId == this.Components[0].Drawable);
            if (currEl != undefined) return currEl.MaxValue;
            return 0;

        },
        SetDrawableValue(componentId, value) {
            api
                .post("CharacterCreater.SetDrawableValue", {
                    componentId: componentId,
                    value: value,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetTexturaValue(componentId, drawableId, value) {
            api
                .post("CharacterCreater.SetTextureValue", {
                    componentId: componentId,
                    drawableId: drawableId,
                    value: value,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        //props
        GetMaxPropDrawableValue(value) {
            return this.Props[value].MaxDrawableValue
        },
        GetMaxPropTexturaValue(value) {
            let currEl = this.Props[value].MaxTextureValue.find((element) => element.DrawableId == this.Components[0].Drawable);
            if (currEl != undefined) return currEl.MaxValue;
            return 0;

        },
        SetDrawablePropValue(propId, value) {
            api
                .post("CharacterCreater.SetDrawablePropValue", {
                    propId: propId,
                    value: value,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetTexturaPropValue(propId, drawableId, value) {
            //console.log("SetTexturaValue", "componentId ", propId, "drawableId ", drawableId, "value ", value,)
            api
                .post("CharacterCreater.SetTexturePropValue", {
                    propId: propId,
                    drawableId: drawableId,
                    value: value,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        GetHairColor(item) {
            let str = `rgb(` + item.R + `,` + item.G + `,` + item.B + `)`
            //console.log("str", str)
            return str
        },
        GetHairColor2(item) {
            let str = `rgb(` + item.R2 + `,` + item.G2 + `,` + item.B2 + `)`
            //console.log("str", str)
            //console.log("item", JSON.stringify(item))
            return str
        },
        SetHairColor(item) {
            //console.log("SetHairColor", JSON.stringify(item))
            api
                .post("CharacterCreater.SetHairColor", {
                    R: item.R,
                    G: item.G,
                    B: item.B,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetHairColor2(item) {
            //console.log("SetHairColor2", JSON.stringify(item))
            api
                .post("CharacterCreater.SetHairColor2", {
                    R2: item.R,
                    G2: item.G,
                    B2: item.B,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetColorBeard(item) {
            api
                .post("CharacterCreater.SetColorBeard", {
                    R: item.R,
                    G: item.G,
                    B: item.B,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetEyebrowsColor(item) {
            api
                .post("CharacterCreater.SetEyebrowsColor", {
                    R: item.R,
                    G: item.G,
                    B: item.B,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetChestHairColor(item) {
            //console.log("SetChestHairColor", JSON.stringify(item))
            api
                .post("CharacterCreater.SetChestHairColor", {
                    R: item.R,
                    G: item.G,
                    B: item.B,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetMakeUp(item) {
            //console.log("SetChestHairColor", JSON.stringify(item))
            api
                .post("CharacterCreater.SetMakeUp", {
                    R: item.R,
                    G: item.G,
                    B: item.B,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetBlush(item) {
            //console.log("SetChestHairColor", JSON.stringify(item))
            api
                .post("CharacterCreater.SetBlush", {
                    R: item.R,
                    G: item.G,
                    B: item.B,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        SetLipstick(item) {
            //console.log("SetChestHairColor", JSON.stringify(item))
            api
                .post("CharacterCreater.SetLipstick", {
                    R: item.R,
                    G: item.G,
                    B: item.B,
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        HandsUp() {
            api
                .post("CharacterCreater.HandsUp", {
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },
        Save() {
            api
                .post("CharacterCreater.Save", {
                })
                .catch((e) => {
                    console.log(e.message);
                });
        },



    },
    mounted() {
        this.listener = window.addEventListener("message", (event) => {
            //console.log("event", JSON.stringify(event))
            if (event.data.request === "characterCreate.show") {
                this.PedFaceFeatures = event.data.pedFaceFeatures;
                this.EarColor = event.data.EarColor;
                //this.PedHeadOverlay = event.data.pedHeadOverlay;
                this.Blemishes = event.data.Blemishes;
                this.Beard = event.data.Beard;
                this.Eyebrows = event.data.Eyebrows;
                this.Ageing = event.data.Ageing;
                this.MakeUp = event.data.MakeUp;
                this.Blush = event.data.Blush;
                this.Complexion = event.data.Complexion;
                this.SunDamage = event.data.SunDamage;
                this.Lipstick = event.data.Lipstick;
                this.MoleAndFreckles = event.data.MoleAndFreckles;
                this.ChestHair = event.data.ChestHair;
                this.BodyBlemishes = event.data.BodyBlemishes;
                this.AddBodyBlemishes = event.data.AddBodyBlemishes;
                this.Components = event.data.Components;

                this.Props = event.data.Props;

                this.HairsColors = event.data.PedHairColors
                this.HairsColors2 = event.data.PedHairColors
                this.MakeUpColors = event.data.MakeUpColors
                this.Hair = event.data.PedHair
                this.Windows = event.data.Windows
                console.log("HairsColors", JSON.stringify(event.data.HairsColors))

                this.onShow();
            } else if (event.data.request === "PedHeadOverlay.update") {
                this.Blemishes = event.data.Blemishes;
                this.Beard = event.data.Beard;
                this.Eyebrows = event.data.Eyebrows;
                this.Ageing = event.data.Ageing;
                this.MakeUp = event.data.MakeUp;
                this.Blush = event.data.Blush;
                this.Complexion = event.data.Complexion;
                this.SunDamage = event.data.SunDamage;
                this.Lipstick = event.data.Lipstick;
                this.MoleAndFreckles = event.data.MoleAndFreckles;
                this.ChestHair = event.data.ChestHair;
                this.BodyBlemishes = event.data.BodyBlemishes;
                this.AddBodyBlemishes = event.data.AddBodyBlemishes;

            } else if (event.data.request === "PedEyeColor.update") {
                //console.log("PedEyeColor.update", event.data.EarColor)
                this.EarColor = event.data.EarColor;

            } else if (event.data.request === "PedComponents.update") {
                //console.log("PedComponents.update", event.data.EarColor)
                this.Components = event.data.Components;
            } else if (event.data.request === "PedProps.update") {
                //console.log("PedComponents.update", event.data.EarColor)
                this.Props = event.data.Props;
            } else if (event.data.request === "PedHairColor.update") {
                //console.log("PedHairColor.update", event.data.PedHair)
                this.Hair = event.data.PedHair;
            } else if (event.data.request === "MakeUpColors.update") {
                //console.log("PedHairColor.update", event.data.PedHair)
                this.MakeUpColors = event.data.MakeUpColors
            } else if (event.data.request === "characterCreate.hide") {
                this.onClose();
            }
        });
        /*this.listenerKey = window.addEventListener("keydown", (event) => {
            //console.log('keydown', event.key)
            //console.log('event', event)
            if (event.key == "Escape") {
                this.onClose();
            }


        });*/

        this.listenerKey = window.addEventListener("mousemove", (event) => {
            //console.log('mousemove')
            if (this.keys[2]) {
                //console.log('зажата правая кнопка', event.movementX, event.movementY)
                if (event.movementX != 0) {
                    //console.log("event.movementX", event.movementX)
                    api
                        .post("CharacterCreater.MovementX", {
                            delta: event.movementX
                        })
                        .catch((e) => {
                            console.log(e.message);
                        });
                }

                if (event.movementY != 0) {
                    //console.log("event.movementY", event.movementY)
                    api
                        .post("CharacterCreater.MovementY", {
                            delta: event.movementY
                        })
                        .catch((e) => {
                            console.log(e.message);
                        });
                }
            }

        });
        this.listenerKey = window.addEventListener("mousewheel", (event) => {
            //console.log('mousewheel')
            if (this.keys[2]) {
                //console.log('зажата правая кнопка', event.wheelDelta)
                api
                    .post("CharacterCreater.MovementWheel", {
                        delta: event.wheelDelta
                    })
                    .catch((e) => {
                        console.log(e.message);
                    });
            }

        });
        this.listenerKey = window.addEventListener("mousedown", (event) => {
            //console.log('mousedown', event.button, event.buttons)
            this.keys[event.button] = true
        });
        this.listenerKey = window.addEventListener("mouseup", (event) => {
            delete this.keys[event.button];
        });
    }
};


const api = axios.create({
	baseURL: `https://${typeof GetParentResourceName !== 'undefined' ? GetParentResourceName() : 'KCCharacterCreate'}/`,
});

let characterCreate = Vue.createApp(CharacterCreate);
characterCreate.mount("#characterCreate");