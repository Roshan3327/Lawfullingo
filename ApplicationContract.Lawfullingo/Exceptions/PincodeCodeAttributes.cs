using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic.Domain.Exceptions
{
    public class PincodeCodeAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string pincode = value.ToString();

                // Check if pincode is numeric and 6 digits long
                if (!IsNumeric(pincode) || pincode.Length != 6)
                {
                    return new ValidationResult("Pincode must be numeric and 6 digits long.");
                }
            }

            return ValidationResult.Success;
        }

        // Helper method to check if a string is numeric
        private bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}