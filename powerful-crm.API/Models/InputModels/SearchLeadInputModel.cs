﻿using powerful_crm.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powerful_crm.API.Models.InputModels
{
    public class SearchLeadInputModel
    {
        public string FirstName { get; set; }
        public SearchType TypeSearchFirstName { get; set; }
        public string LastName { get; set; }
        public SearchType TypeSearchLastName { get; set; }
        public string Email { get; set; }
        public SearchType TypeSearchEmail { get; set; }
        public string Login { get; set; }
        public SearchType TypeSearchLogin { get; set; }
        public string Phone { get; set; }
        public SearchType TypeSearchPhone { get; set; }
        public string StartBirthDate { get; set; }
        public string EndBirthDate { get; set; }
        public string CityName { get; set; }
        public SearchType TypeSearchCityName { get; set; }
    }
}