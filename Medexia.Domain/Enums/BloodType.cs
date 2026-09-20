using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medexia.Domain.Enums
{
    public enum BloodType
    {
        Unknown = 0,

        [Display(Name = "A+")]
        APositive = 1,

        [Display(Name = "A-")]
        ANegative,

        [Display(Name = "B+")]
        BPositive,

        [Display(Name = "B-")]
        BNegative,

        [Display(Name = "AB+")]
        ABPositive,

        [Display(Name = "AB-")]
        ABNegative,

        [Display(Name = "O+")]
        OPositive,

        [Display(Name = "O-")]
        ONegative
    }
}
