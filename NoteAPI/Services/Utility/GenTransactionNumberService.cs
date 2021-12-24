using System;

namespace NoteAPI.Service.Utility
{
    public class GenTransactionNumberService
    {
        public string GenTransactionNumber(string inputMaxTransactionNumber, DateTime dateTimeNoew)
        {
            string strGenGenTransactionNumber = "";
            string currentYear = dateTimeNoew.ToString("yyyy");
            string currentMonth = dateTimeNoew.ToString("MM");
            Int64 intGenTransactionNo = 0;

            try
            {
                if (inputMaxTransactionNumber != "")
                {
                    string maxApplicationNuber = inputMaxTransactionNumber;

                    string maxApplicationNo_Year = maxApplicationNuber.Trim().Substring(0, 4);
                    string maxApplicationNo_Month = maxApplicationNuber.Trim().Substring(5, 2);
                    string maxApplicationNo_Running = maxApplicationNuber.Trim().Substring(7, 7);

                    if ((currentYear != maxApplicationNo_Year) || (currentMonth != maxApplicationNo_Month))
                        intGenTransactionNo += 1;
                    else
                        intGenTransactionNo = Convert.ToInt64(maxApplicationNo_Running) + 1;
                }
                else
                    intGenTransactionNo += 1;

                strGenGenTransactionNumber = currentYear.Trim() + "-" + currentMonth.Trim() + intGenTransactionNo.ToString().Trim().PadLeft(7,'0');

                return strGenGenTransactionNumber;
            }
            catch (NullReferenceException)
            {
                intGenTransactionNo += 1;
                strGenGenTransactionNumber = currentYear.Trim() + "-" + currentMonth.Trim() + intGenTransactionNo.ToString().Trim().PadLeft(7, '0');

                return strGenGenTransactionNumber;
            }
        }
    }
}