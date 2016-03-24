using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Portable.FluentRest.Helpers
{
    public class Formatter
    {

        public static bool IsValidEmail(string emailaddress)
        {
            // Instance method:
            //Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            //return true;

            // Static method:
            if (!Regex.IsMatch(emailaddress, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {// Name does not match schema
                return false;
            }
            else { return true; }
        }

        public static bool IsValidZipCode(string zipcode)
        {

            // Static method:
            if (!Regex.IsMatch(zipcode, @"^\d{5}([\-]?\d{4})?$"))
            {// Name does not match schema
                return false;
            }
            {
                return true;

            }
        }

        /// <summary>
        /// Turns seconds into amount of whole minutes
        /// </summary>
        /// <param name="seconds">Integer representation of seconds</param>
        /// <returns></returns>
        public static int SecondsToMinutes(int seconds)
        {
            //int minutes = 0;

            TimeSpan t = TimeSpan.FromSeconds(seconds);

            //string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
            //                t.Hours,
            //                t.Minutes,
            //                t.Seconds,
            //                t.Milliseconds);

            return t.Minutes;
        }

        //public static bool CheckNumber(string strPhoneNumber)
        //{
        //   //string MatchPhoneNumberPattern = "^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"


        //   //     if (!Regex.IsMatch(strPhoneNumber,  "^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")) {// Name does not match schema
        //   //     return true; }
        //   // else {return false;}
        //   //if (strPhoneNumber!= null) return Regex.IsMatch(strPhoneNumber, MatchPhoneNumberPattern );
        //   //else return false;
        //}

        public static string GetFormattedPhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace(" ", "");
            if (phoneNumber == null)
                return string.Empty;

            switch (phoneNumber.Length)
            {
                case 5:
                    return Regex.Replace(phoneNumber, @"(\d{1})(\d{4})", "$1-$2");
                case 6:
                    return Regex.Replace(phoneNumber, @"(\d{2})(\d{4})", "$1-$2");
                case 7:
                    return Regex.Replace(phoneNumber, @"(\d{3})(\d{4})", "$1-$2");
                case 8:
                    return Regex.Replace(phoneNumber, @"(\d{1})(\d{3})(\d{4})", "($1) $2-$3");
                case 9:
                    return Regex.Replace(phoneNumber, @"(\d{2})(\d{3})(\d{4})", "($1) $2-$3");
                case 10:
                    return Regex.Replace(phoneNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                case 11:
                    return Regex.Replace(phoneNumber, @"(\d{1})(\d{3})(\d{3})(\d{4})", "$1 ($2) $3-$4");
                default:
                    return phoneNumber;
            }

        }

        public static string GetFormattedCreditCardNumber(string cardNumber)
        {
            cardNumber = cardNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(".", "").Replace(" ", "");
            if (cardNumber == null)
                return string.Empty;

            switch (cardNumber.Length)
            {
                case 12: //Discover
                    return Regex.Replace(cardNumber, @"(\d{4})(\d{4})(\d{4})", "$1-$2-$3");
                case 14: //Diner's Club
                    return Regex.Replace(cardNumber, @"(\d{4})(\d{6})(\d{4})", "$1-$2-$3");
                case 15: //AMEX
                    return Regex.Replace(cardNumber, @"(\d{4})(\d{6})(\d{5})", "$1-$2-$3");
                case 16: //VISA
                    return Regex.Replace(cardNumber, @"(\d{4})(\d{4})(\d{4})(\d{4})", "$1-$2-$3-$4");
                default:
                    return cardNumber;
            }

        }

        public static string CapitalizeWords(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                return value;

            StringBuilder result = new StringBuilder(value);
            result[0] = char.ToUpper(result[0]);
            for (int i = 1; i < result.Length; ++i)
            {
                if (char.IsWhiteSpace(result[i - 1]))
                    result[i] = char.ToUpper(result[i]);
                else
                    result[i] = char.ToLower(result[i]);
            }
            return result.ToString();
        }


        public static string GetDateDiff(DateTime OrderDate)
        {
            string Diff = string.Empty;

            int days = ((TimeSpan)DateTime.Now.Subtract(OrderDate)).Days;
            int hours = ((TimeSpan)DateTime.Now.Subtract(OrderDate)).Hours;

            if (days < 1)
            {
                if (hours == 0)
                {
                    Diff = ((TimeSpan)DateTime.Now.Subtract(OrderDate)).Minutes.ToString() + " minutes ago";
                }
                else if (hours > 12)
                {
                    Diff = ((TimeSpan)DateTime.Now.Subtract(OrderDate)).Hours.ToString() + " hours ago";
                }
                else
                {
                    Diff = ((TimeSpan)DateTime.Now.Subtract(OrderDate)).Hours.ToString() + " hr " + ((TimeSpan)DateTime.Now.Subtract(OrderDate)).Minutes.ToString() + " min ago";
                }
            }
            else if (days == 1)
            {
                Diff = "Yesterday";
            }
            else if (days > 1)
            {
                Diff = ((TimeSpan)DateTime.Now.Subtract(OrderDate)).Days + " days ago";
            }

            return Diff;
        }




    }
}
