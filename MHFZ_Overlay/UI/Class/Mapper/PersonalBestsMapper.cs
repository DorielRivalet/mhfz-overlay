﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHFZ_Overlay.UI.Class.Mapper
{
    public class PersonalBestsMapper
    {
        public string WeaponType { get; set; }
        public long Attempts { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ActualOverlayMode { get; set; }
        public long RunID { get;set; }
        public long TimeLeft { get; set; }

        public PersonalBestsMapper(string weaponType, long attempts, DateTime createdAt, string ActualOverlayMode, long runID, long timeLeft)
        {
            this.WeaponType = weaponType;
            this.Attempts = attempts;
            this.CreatedAt = createdAt;
            this.ActualOverlayMode= ActualOverlayMode;
            this.RunID = runID;
            this.TimeLeft = timeLeft;
        }
    }
}