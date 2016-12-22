using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using abacanet.diamond.domain.model;
using abacanet.diamond.infrastructure.Exceptions;
using System.Text.RegularExpressions;

namespace abacanet.diamond.infrastructure.files
{
    public class ExcelReader : IExcelReader
    {
        private const string rootIgnoredColumn = "income";
        private string _file;
        private static Regex _numberRegex;

        public ExcelReader(string file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            this._file = file;

            _numberRegex = new Regex(@"^\d{3,4}");
        }

        public ProfitAndLossModel Read()
        {
            var lines = GetLines();
            var root = new ProfitAndLossModel { Text = Path.GetFileName(_file) };

            ProfitAndLossModel last = null;
            foreach (var current in lines)
            {
                if (last == null)
                {
                    root.Children.Add(current);
                    current.Parent = root;
                }
                else if (current.Column > last.Column)
                {
                    current.Parent = last;
                    last.Children.Add(current);
                }
                else if (current.Column == last.Column)
                {
                    current.Parent = last.Parent;
                    current.Parent.Children.Add(current);
                }
                else
                {
                    current.Parent = last.Parent.Parent;
                    current.Parent.Children.Add(current);
                }

                last = current;
            }

            return root;
        }

        private IList<ProfitAndLossModel> GetLines()
        {
            var table = ReadFile();

            var lines = new List<ProfitAndLossModel>();
            for (int row = 1; row < table.Rows.Count; row++)
            {
                var line = new ProfitAndLossModel();
                for (int column = 0; column < 15; column++)
                    ValuateLine(table, line, row, column);

                lines.Add(line);
            }

            ClearLines(lines);
            return lines;
        }

        private static void ValuateLine(DataTable table, ProfitAndLossModel line, int row, int column)
        {
            var cell = table.Rows[row][column];
            if (!string.IsNullOrEmpty(cell.ToString()))
            {
                if (string.IsNullOrWhiteSpace(line.Text))
                {
                    var match = _numberRegex.Match(cell.ToString());
                    line.Number = match.Success ? Convert.ToInt32(match.Value) : 0;
                    line.Text = cell.ToString().Trim();
                    line.Row = row;
                    line.Column = column;
                }
                else
                    line.Value = Convert.ToDecimal(cell);
            }
        }

        private static void ClearLines(List<ProfitAndLossModel> lines)
        {
            var incomeColunm = lines
                            .Where(l => !string.IsNullOrWhiteSpace(l.Text) &&
                                        l.Text.ToLower().Equals(rootIgnoredColumn))
                            .Select(s => s.Column)
                            .FirstOrDefault();

            lines.RemoveAll(l => l.Column <= incomeColunm || string.IsNullOrWhiteSpace(l.Text));
        }

        private DataTable ReadFile()
        {
            FileStream stream = File.Open(_file, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = excelReader.AsDataSet();

            if (result.Tables.Count == 0)
                new InvalidSheetException(_file);

            return result.Tables[1];
        }
    }
}
