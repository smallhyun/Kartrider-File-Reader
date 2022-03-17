﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartRider.Record
{
    public class PlayerInfo
    {
        public string PlayerName { get; set; } = "KartRider";
        public string ClubName { get; set; } = "Default Club";
        public PlayerEquipment Equipment { get; set; } = new PlayerEquipment();//0x32 if version>=11 else 0x2C
    }
}
