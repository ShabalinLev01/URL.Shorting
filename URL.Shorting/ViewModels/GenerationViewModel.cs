using System.ComponentModel.DataAnnotations;

namespace URL.Shorting.ViewModels
{
    public class GenerationViewModel
    {
        [Required]
        [DataType(DataType.Url)]
        [Display(Name = "URL")]
        public string Url { get; set; }
    }
}