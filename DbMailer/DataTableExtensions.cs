namespace DbMailer
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    public static class DataTableExtensions
    {
        public static string ToCsv(this DataTable table, char quoteCharacter = '"')
        {
            var quotedColumns = new List<int>();

            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].DataType == typeof(string))
                {
                    quotedColumns.Add(i);
                }

                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\r\n" : ",");
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    var quoted = quotedColumns.Any(q => q == i);
                    
                    if (quoted)
                    {
                        result.Append(quoteCharacter);
                    }

                    result.Append(row[i].ToString());

                    if (quoted)
                    {
                        result.Append(quoteCharacter);
                    }

                    result.Append(i == table.Columns.Count - 1 ? "\r\n" : ",");
                }
            }

            return result.ToString();
        }
    }
}