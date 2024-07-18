using Core.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCCC.Client.Models
{
    public class CharacterCreate
    {
        public class PointModel
        {
            public string Name { set; get; }
            public Marker Marker { set; get; }
            public Blip Blip { set; get; }
            public Npc Npc { set; get; }
            public Interact Interact { set; get; }

        }
        public List<PointModel> Points { set; get; }

        public class CardModel
        {
            public class DescriptionModel
            {
                public int Id { set; get; }
                public string Text { set; get; }
            }

            public int Id { set; get; }
            public string Title { set; get; }
            public List<DescriptionModel> Descriptions { set; get; }
            public float Ratio { set; get; }
            public bool Adrenaline { set; get; } = false;


        }
        public List<CardModel> Cards { set; get; }
        public decimal StartCash { set; get; }
        public decimal StartBank { set; get; }
    }
}
