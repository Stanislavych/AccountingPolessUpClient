﻿using AccountingPolessUp.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingPolessUp.Models
{
    public partial class Employment
    {
        static ParticipantsService _participantsService = new ParticipantsService();
        static List<Participants> participants = _participantsService.Get();

        public string nameMentor
        {
            get
            {
                var mentor = participants.FirstOrDefault(x => x.Id == IdMentor);
                return mentor?.Individuals.FIO ?? string.Empty;
            }
        }
    }
}
