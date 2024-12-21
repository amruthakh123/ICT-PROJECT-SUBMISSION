using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace restrauntBooking
{
    public class TurfUser
    {
        [JsonIgnore]
        [Key]
        public int userid { get; set; }
        public string usertype { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }


    public class LoginRequest
    {
        public string usertype { get; set; }

        public string email { get; set; }

        public string password { get; set; }
    }

    public class Slot
    {
        [Key]
        public int slotid { get; set; }
        public string slottime { get; set; }
        public string slotdate { get; set; }
        public string status { get; set; }
    }


    public class AllSlots
    {
        [JsonIgnore]
        [Key]
        public int slotid { get; set; }
        public string slottime { get; set; }
        public string slotdate { get; set; }
        public string status { get; set; }
    }

    public class SlotBooking
    {
        [JsonIgnore]
        [Key]
        public int bookingid { get; set; } // Primary key, auto-increment
        public int slotid { get; set; } 
        public string custname { get; set; }
    }






}