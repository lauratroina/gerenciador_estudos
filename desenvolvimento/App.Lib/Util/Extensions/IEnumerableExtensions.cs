﻿//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Linq;
//using System.ComponentModel;
//using System.Data;
//using App.Lib.Util.Models;
//using Microsoft.Reporting.WebForms;
//using App.Lib.Util.Enumerator;
//using System.Globalization;

//namespace System.Collections.Generic
//{
    
//    public static class IEnumerableExtensions 
//    {

//        public static ReportDados ToReport<T>(this IEnumerable<T> dados, string fileRdlc, enumReportFormat format, string dataSetName)
//        {
//            ReportViewer rv = new Microsoft.Reporting.WebForms.ReportViewer();
//            rv.ProcessingMode = ProcessingMode.Local;
//            rv.LocalReport.ReportPath = fileRdlc;

//            ReportDataSource reportDataSource = new ReportDataSource();
//            reportDataSource.Name = dataSetName;
//            reportDataSource.Value = ConvertToDataTable(dados);
//            rv.LocalReport.DataSources.Add(reportDataSource);

//            byte[] streamBytes = null;
//            string mimeType = "";
//            string encoding = "";
//            string filenameExtension = "";
//            string[] streamids = null;
//            Warning[] warnings = null;

//            streamBytes = rv.LocalReport.Render(format.ToString(), null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

//            return new ReportDados() { Content = streamBytes, MimeType = mimeType, Encoding = encoding, FileNameExtension = filenameExtension, Warnings = warnings };
//        }

//        public static DataTable ConvertToDataTable<T>(this IEnumerable<T> data)
//        {
//            PropertyDescriptorCollection properties =
//               TypeDescriptor.GetProperties(typeof(T));
//            DataTable table = new DataTable();
//            foreach (PropertyDescriptor prop in properties)
//                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
//            foreach (T item in data)
//            {
//                DataRow row = table.NewRow();
//                foreach (PropertyDescriptor prop in properties)
//                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
//                table.Rows.Add(row);
//            }
//            return table;

//        }

//        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
//        {
//            Random rnd = new Random();
//            return source.OrderBy<T, int>((item) => rnd.Next());
//        }
//    }
        
//}
