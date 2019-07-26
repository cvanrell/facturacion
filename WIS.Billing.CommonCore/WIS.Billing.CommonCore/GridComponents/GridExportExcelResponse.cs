using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIS.CommonCore.App;

namespace WIS.CommonCore.GridComponents
{
    public class GridExportExcelResponse : ComponentQuery
    {
        public string FileName { get; set; }
        public string ExcelContent { get; set; }

        public void SetExcelContent(byte[] content)
        {
            this.ExcelContent = Convert.ToBase64String(content);
        }

        public byte[] GetExcelContent()
        {
            return Convert.FromBase64String(this.ExcelContent);
        }
    }
}
