using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dsafa.WpfColorPicker.ValidationRules
{
    internal class ColorNumericInputValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || (string)value == "")
            {
                return new ValidationResult(false, "Please enter a number");
            }

            return ValidationResult.ValidResult;
        }
    }
}
