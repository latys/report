using Report.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Report.Util
{
    [ValueConversion(typeof(string), typeof(String))]
    public class StatusConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            string data = value.ToString();
            using (var db = new Model1())
            {
                var username = db.D_UserInfo.Where(x => x.loginname == data).Select(t => t.username).ToList<string>();
                if(username.Count()>0)
                {
                    return username.ElementAt(0).ToString();
                }
            }
                
            
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            string data = value.ToString();
            using (var db = new Model1())
            {
                var loginname = db.D_UserInfo.Where(x => x.username == data).Select(t => t.loginname).ToList<string>();
                if (loginname.Count() > 0)
                {
                    return loginname.ElementAt(0).ToString();
                }
            }


            
            return null;
        }
    }
}
