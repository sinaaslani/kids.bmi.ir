using System;
using System.Data;
using System.IO;
using Kids.Utility.Csv;

namespace Kids.Utility.CSVUtility
{
    public class CSVUtil
    {
        public DataTable CvsParser(string prmFilePath, ref string prmStrErr)
        {

            StreamReader varStreamReader = new StreamReader(prmFilePath);

            using (CsvReader csv = new CsvReader(varStreamReader, true))
            {
                DataTable prmDTblResult = new DataTable();
                foreach (string varColumn in csv.GetFieldHeaders())
                    prmDTblResult.Columns.Add(varColumn, Type.GetType("System.String"));

                while (csv.ReadNextRecord())
                {
                    DataRow varRow = prmDTblResult.NewRow();
                    for (int i = 0; i < csv.FieldCount; i++)
                        varRow[i] = csv[i];

                    prmDTblResult.Rows.Add(varRow);
                }
                return prmDTblResult;
            }

        }


    }
}
