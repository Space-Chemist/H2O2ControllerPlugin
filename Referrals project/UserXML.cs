﻿using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Referrals_project
{
    [Serializable()]
        [XmlRoot(ElementName = "Users")]
        public class Users
        {
            [XmlElement("User")] public List<User> User { get; set; }
        }
        
        [Serializable()]
        [XmlRoot(ElementName = "User")]
        public class User
        {
            [XmlElement] public string Name { get; set; }
            [XmlElement] public bool ReferralByUser { get; set; }
            [XmlElement] public bool ReferralByCode { get; set; }
            [XmlElement] public long SteamId { get; set; }
            [XmlElement] public long ReferredBy{ get; set; }
            [XmlElement] public string ReferralCode { get; set; }
            [XmlElement] public List<ReferredDescriptions> ReferredDescriptions { get; set; }
        }

        [Serializable()]
        [XmlRoot(ElementName = "ReferredDescriptions")]
        public class ReferredDescriptions
        {
            [XmlElement] public string ReferredUserName { get; set; }
            [XmlElement] public bool ReferredUserId { get; set; }
        }

    }



 