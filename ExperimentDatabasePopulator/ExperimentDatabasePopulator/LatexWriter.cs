﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDatabasePopulator
{
    class LatexWriter
    {
        public string Path{ get; set;}

        public LatexWriter(string _Path)
        {
            Path = _Path;
        }

        public void OutputTable(DataTable table)
        {
            using (StreamWriter writer = new StreamWriter(Path))
            {
                //Create table specification and column header
                string tableSpec = "|l";
                string header = table.Columns[0].ColumnName;
                for (int i = 1; i < table.Columns.Count; i++)
                {
                    tableSpec += "|l";
                    header += String.Format(" & {0}", table.Columns[i].ColumnName);
                }
                tableSpec += "|";

                //Begin Table: \begin{tabular}[pos]{table spec}
                writer.WriteLine(String.Format("\\begin{{tabular}} {{{0}}}", tableSpec));
                writer.WriteLine("\\hline");

                //Write Header
                writer.WriteLine(header + " \\\\");
                writer.WriteLine("\\hline");

                //Output column headers
                
                foreach (DataRow row in table.Rows)
                {
                    var items = row.ItemArray;
                    string rowText = items.Aggregate((a, b) => a + " & " + b).ToString();
                    writer.Write(rowText);
                    writer.WriteLine(" \\\\");
                }

                //End Table: \end{tabular}
                writer.WriteLine("\\hline");
                writer.WriteLine("\\end{tabular}");
            }
        }

        public void OutputTable(int NumColumns, List<string> ColumnNames, List<Tuple<string,List<AveStd>>> results)
        {
            //Create table specification
            string tableSpec = "1 ";
            for (int i = 1; i < NumColumns; i++) tableSpec += "| 1";
            
            //Output
            using (StreamWriter writer = new StreamWriter(Path))
            {
                //Begin Table: \begin{tabular}[pos]{table spec}
                writer.WriteLine("\\begin{tabular} {{0}}",tableSpec);
                writer.WriteLine("\\hline");

                //Write Header
                var header = ColumnNames.Aggregate((a, b) => a + " & " + b);
                writer.WriteLine(header + " \\\\");
                writer.WriteLine("\\hline");

                //Output results
                for (int i = 0; i < results.Count; i++)
                {
                    writer.Write(results[i].Item1 + " & ");
                    List<AveStd> list = results[i].Item2;

                    string line = list[0].ToString() + " ";
                    for (int j = 1; j < NumColumns; j++) line += String.Format("& {0}",list[j].ToString());
                    line += " \\\\";
                    writer.WriteLine(line);
                }

                //End Table: \end{tabular}
                writer.WriteLine("\\hline");
                writer.WriteLine("\\end{tabular}");
            }
        }
    }
}
