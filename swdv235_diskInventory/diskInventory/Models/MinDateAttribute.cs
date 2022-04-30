using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace diskInventory.Models
{
    public class MinDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext vctx)
        {
            if(value is DateTime)
            {
                DateTime dateToCheck = (DateTime)value;
                if(dateToCheck <= DateTime.Today && dateToCheck >= DateTime.Parse("1900/01/01"))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    string msg = base.ErrorMessage ??
                        $"{vctx.DisplayName} must be a valid past date before 1900/01/01";
                    return new ValidationResult(msg);
                }
            }

            return ValidationResult.Success;
        }
    }
}
