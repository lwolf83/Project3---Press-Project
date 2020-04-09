using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserInterface
{
    public static class InputChecker
    {
        public static bool CheckDates(DateTime firstDate, DateTime lastDate)
        {
            if (firstDate <= lastDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckDateFormatFromString(string stringDateFormat)
        {
            try
            {
                DateTime dateTimeFormat = DateTime.ParseExact(stringDateFormat, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateValidAndHigherThanToday(string publicationDateString)
        {
            try
            {
                DateTime publicationDate = DateTime.ParseExact(publicationDateString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                if (publicationDate >= DateTime.Today)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsFieldComplete(string userInput)
        {
            if(string.IsNullOrEmpty(userInput))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool AreAllStringFieldsComplete(IEnumerable<String> stringInputs)
        {
            bool areFieldCompletes = true;
            foreach(string field in stringInputs)
            {
                if(string.IsNullOrEmpty(field))
                {
                    areFieldCompletes = false;
                }
            }
            return areFieldCompletes;
        }

        public static bool IsDecimalFormatValidWithTwoZeroAfterComa(string decimalString)
        {
            try
            {
                decimal newspaperPrice = Convert.ToDecimal(decimalString);
                string[] a = decimalString.Split(new char[] { ',' });
                int decimals = a[1].Length;

                if (newspaperPrice > 0 && (decimals <= 2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPeriodicityLowerThanReferencedDays(string periodicityString, int days)
        {
            if (Convert.ToInt32(periodicityString) <= days)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ControlInputOnlyDecimalCaracters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static void ControlInputOnlyNumbersCaracters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public static void ControlInputOnlyDateCaracters(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9/]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
