const Identity = {
    data() {
        return {
            identityShow:false, 
            firstname:"",
            lastname:"",
            sex:"male",
            dob:"",
            placeb:"",
            nationality:"",
            hight:180,
            cards:[
                {
                    "Id":1,
                    "Title":"Агрессивный лидер с замашками садиста.",
                    "Descriptions":[
                        {"Id":1, "Text":"Агрессия - агрессивный."},
                        {"Id":2, "Text":"Потребность в коллективе - потребность в большом коллективе."},
                        {"Id":3, "Text":"Коммуникабельность - легко устанавливает контакт."},
                        {"Id":4, "Text":"Развитие навыков механической работы - рукожоп."},
                        {"Id":5, "Text":"Ценности - материальные ценности."},
                        {"Id":6, "Text":"Тяга к приключениям - необходим постоянный адреналин."},
                    ],
                    "Adrenaline": true,
                    "Ratio":0.5
                },
                {
                    "Id":2,
                    "Title":"Отступник. Активный представитель организованной группы людей.",
                    "Descriptions":[
                        {"Id":1, "Text":"Агрессия - агрессивный."},
                        {"Id":2, "Text":"Потребность в коллективе - потребность в большом коллективе."},
                        {"Id":3, "Text":"Коммуникабельность - низкие способности."},
                        {"Id":4, "Text":"Развитие навыков механической работы - низкие навыки."},
                        {"Id":5, "Text":"Ценности - материальные ценности."},
                        {"Id":6, "Text":"Тяга к приключениям - иногда любит пощекотать нервы."},
                    ],
                    "Ratio":1
                },
                {
                    "Id":3,
                    "Title":"Ведомый любитель спокойного завтрашнего дня (типичный цивил).",
                    "Descriptions":[
                        {"Id":1, "Text":"Агрессия - спокойный."},
                        {"Id":2, "Text":"Потребность в коллективе - безразлично."},
                        {"Id":3, "Text":"Коммуникабельность - средние способности."},
                        {"Id":4, "Text":"Развитие навыков механической работы - средние навыки."},
                        {"Id":5, "Text":"Ценности - ориентирован на мораль."},
                        {"Id":6, "Text":"Тяга к приключениям - безразлично."},
                    ],
                    "Ratio":1.3
                },
                {
                    "Id":4,
                    "Title":"Уставший от жизни волк-одиночка с богатым бэкграундом.",
                    "Descriptions":[
                        {"Id":1, "Text":"Агрессия - агрессивный."},
                        {"Id":2, "Text":"Потребность в коллективе - одиночка."},
                        {"Id":3, "Text":"Коммуникабельность - избегает общения."},
                        {"Id":4, "Text":"Развитие навыков механической работы - высокие навыки."},
                        {"Id":5, "Text":"Ценности - материальные ценности, мораль на подсознательном уровне."},
                        {"Id":6, "Text":"Тяга к приключениям - иногда любит пощекотать нервы."},
                    ],
                    "Ratio":2
                },
            ],
            activeCard:-1,
            
        }
    },

methods: {
    SetFirstName(value){
        this.firstname = value
    },
    SetLastName(value){
        this.lastname = value
    },
    SetDob(value){
        this.dob = value
    },
    SetPlaceB(value){
        this.placeb = value
    },
    SetNat(value){
        this.nationality = value
    },
    SetHight(value){
        this.hight = value
    },
    SetSex(value){
        this.sex = value
    },
    OkBtn(){
        console.log("Identity.Create")
        api
        .post("Identity.Create", {
            firstname: this.firstname,
            lastname: this.lastname,
            sex: this.sex,
            dob: this.dob,
            placeb: this.placeb,
            nationality: this.nationality,
            hight: this.hight,
            activeCard: this.activeCard,
            
        })
        .catch((e) => {
          console.log(e.message);
        });
    },
    CancelBtn(){
        api
        .post("Identity.Cancel", {
            
            
        })
        .catch((e) => {
          console.log(e.message);
        });
    },
    Reset(){
        this.firstname = ""
        this.lastname = ""
        this.sex = "male"
        this.dob = ""
        this.placeb = ""
        this.nationality = ""
        this.hight = 180
        this.activeCard = -1
        
    },
    onShow(){
        
        this.identityShow = true;
    },
    checkCardSelected(item){
        console.log("checkCardSelected", item.Id, this.activeCard )
        if(item.Id == this.activeCard) {
            return true
        }
        return false;
    },
    onCardSelect(item) {
        this.activeCard = item.Id
    }
},
mounted() {
        this.listener = window.addEventListener("message", (event) => {
        if (event.data.request === "identity.show") {
            //console.log("cards", JSON.stringify(event.data.cards))
            this.cards = event.data.cards
            this.onShow();
        
        } else if (event.data.request === "identity.hide") {
            this.identityShow = false;
        }
        });
       
    }
};



let identity = Vue.createApp(Identity);
identity.mount("#identity");