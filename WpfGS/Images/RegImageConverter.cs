using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;

namespace WpfGS
{
	internal class RegImageConverter : IValueConverter
	{
		public object Convert(object o, Type type, object parameter, CultureInfo culture)
		{
            string str=((Node)o).Kind;

            if (str == "folder")
                    return "/Images/folder.png";
            else if(str== "drum")
                    return "/Images/drum.png";
            else if(str== "dataString")
                return "/Images/dataString.png";
            else if(str== "data")
                return "/Images/data.png";
            else
                return "/Images/data.png";
				
		}


		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

}
