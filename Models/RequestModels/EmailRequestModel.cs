using System.ComponentModel.DataAnnotations;

namespace TP.Models.RequestModels
{
    public class EmailRequestModel
    {
        [MaxLength(25)]
        public string email { get; set; }
    }
}
