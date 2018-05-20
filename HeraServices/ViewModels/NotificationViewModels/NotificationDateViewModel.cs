﻿using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeraServices.ViewModels.NotificationViewModels
{
    public class NotificationDateViewModel
    {
        public string Resumed { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
