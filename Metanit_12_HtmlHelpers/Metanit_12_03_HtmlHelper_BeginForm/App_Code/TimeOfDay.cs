using System.ComponentModel.DataAnnotations;

namespace Metanit_12_03_HtmlHelper_BeginForm.App_Code
{
    public enum TimeOfDay
    {
        [Display(Name = "Утро")]
        Morning,
        [Display(Name = "День")]
        Afternoon,
        [Display(Name = "Вечер")]
        Evening,
        [Display(Name = "Ночь")]
        Night
    }
}
