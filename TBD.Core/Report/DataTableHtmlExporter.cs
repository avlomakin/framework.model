using System;
using System.Data;
using System.Text;

namespace TBD.Core
{

    /// <summary>
    /// TODO: (avlomakin) add customization like colors, margins, css etc
    /// </summary>
    public class CDataTableHtmlExporter
    {
        private readonly DataTable _tableForExport;

        public CDataTableHtmlExporter( DataTable tableForExport )
        {
            _tableForExport = tableForExport;
        }

        public String GetHtml()
        {
            //https://stackoverflow.com/questions/19682996/datatable-to-html-table

            if (_tableForExport.Rows.Count == 0) return ""; // enter code here

            StringBuilder builder = new StringBuilder();
            builder.Append("<html>");
            builder.Append("<head>");
            builder.Append("<title>");
            builder.Append("Page-");
            builder.Append(Guid.NewGuid());
            builder.Append("</title>");
            builder.Append("</head>");
            builder.Append("<body>");
            builder.Append("<table border='1px' cellpadding='5' cellspacing='0' style='font-size:20px'");
            builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
            builder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn c in _tableForExport.Columns)
            {
                builder.Append("<td align='left' valign='top'><b>");
                builder.Append(c.ColumnName);
                builder.Append("</b></td>");
            }
            builder.Append("</tr>");
            foreach (DataRow r in _tableForExport.Rows)
            {
                builder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn c in _tableForExport.Columns)
                {
                    builder.Append("<td align='left' valign='top'>");
                    builder.Append(r[c.ColumnName]);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            builder.Append("</body>");
            builder.Append("</html>");

            return builder.ToString();

//            String html = "<table>";
//
//            html += "<tr>";
//            for (Int32 i = 0; i < _tableForExport.Columns.Count; i++)
//                html += "<td>" + _tableForExport.Columns[ i ].ColumnName + "</td>";
//
//            html += "</tr>";
//
//            for (Int32 i = 0; i < _tableForExport.Rows.Count; i++)
//            {
//                html += "<tr>";
//                for (Int32 j = 0; j < _tableForExport.Columns.Count; j++)
//                    html += "<td>" + _tableForExport.Rows[ i ][ j ] + "</td>";
//                html += "</tr>";
//            }
//
//            html += "</table>";
//            return html;
        }

    }
}