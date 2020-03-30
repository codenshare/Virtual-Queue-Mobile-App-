using System.ComponentModel.DataAnnotations;

namespace Virtual_Queue.Controllers
{
    public class SubscribeModel
    {
        [Required]
        public string StoreID { get; set; }

        [Required]
        public string Secret { get; set; }

        [Required]
        public string Response { get; set; }

    }
}