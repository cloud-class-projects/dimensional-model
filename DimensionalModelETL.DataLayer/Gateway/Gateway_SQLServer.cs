using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DimensionalModelETL.DataLayer.Gateway
{
    /// <summary>
    /// Mediates between application and SQL Server
    /// </summary>
    public class Gateway_SQLServer
    {
        /// <summary>
        /// Returns a lazy load enumerable.  Row is only read when enumerable is read
        /// </summary>
        public static IEnumerable<IDataRecord> StreamData(string connectionString, string command)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(command, con))
                {
                    con.Open();
                    
                    cmd.CommandTimeout = 5 * 60;
                    var rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        yield return (IDataRecord)rdr;
                    }

                    con.Close();
                }
            }
        }


        /// <summary>
        /// Does a bulk query
        /// </summary>
        public static DataTable QueryData(string connectionString, string command)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(command, con))
                {
                    con.Open();

                    cmd.CommandTimeout = 5 * 60;
                    DataSet ds = new DataSet();
                    new SqlDataAdapter(cmd).Fill(ds);

                    return ds.Tables[0];
                }
            }
        }


        /// <summary>
        /// Performs a batch insert against SQL Server
        /// </summary>
        public static void InsertRecords(string connectionString, List<Dictionary<string, object>> rows, string table)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                foreach (var row in rows)
                {
                    string insertStatement = "INSERT INTO " + table + " (";
                    insertStatement += string.Join(",", row.Keys);
                    insertStatement += ") VALUES (";
                    insertStatement += string.Join(",", row.Keys.Select(x => "@" + x));
                    insertStatement += ");";

                    using (var cmd = new SqlCommand(insertStatement, con))
                    {
                        foreach (var kvp in row)
                        {
                            cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value == null ? DBNull.Value : kvp.Value);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
        }


        /// <summary>
        /// Truncates a specified table
        /// </summary>
        public static void TruncateTable(string connectionString, string tableToTruncate)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("TRUNCATE TABLE " + tableToTruncate, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
