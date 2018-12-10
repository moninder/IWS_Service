using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using AirportIQ.Model.Models.References;

namespace AirportIQ.Data.SqlServer.Repositories.References
{
    /// <summary>
    /// The References Repository
    /// </summary>
    public class ReferenceRepository : IReferenceRepository
    {
        #region Constants
        //0 = schema, 1=table, 2=key, 3=description, 4=abbrv, 5=sortOrder, 6=isActive, 7=startRow, 8=endRow
        private const string FormatSelect = @"select    *
                                              from
                                              (
                                                    select ROW_NUMBER() OVER(ORDER BY [{3}] ASC) AS RowNumber, [{2}] as [Key], [{3}] as [Description], [{4}] as [Abbrv], [{5}] as [SortOrder], [{6}] as [IsActive]
                                                    from            [{0}].[{1}] 
                                                    where           len([{2}]) > 0 and len([{3}]) > 0
                                              ) x
                                              where RowNumber between {7} and {8}";
        private const string FormatInsert = "insert into [{0}].[{1}] ([{2}], [{3}], [{4}], [{5}]) values (@description, @abbrv, @sortOrder, @isActive)";
        private const string FormatUpdate = "update [{0}].[{1}] set [{3}] = @description, [{4}] = @abbrv, [{5}] = @sortOrder, [{6}] = @isActive where [{2}] = @key";
        private const string FormatDelete = "delete from [{0}].[{1}] where [{2}] = @key";
        private const string FormatCount = "select count(*) from [{0}].[{1}] where len([{2}]) > 0 and len([{3}]) > 0";
        private const string FormatExists = "select count(*) from [{0}].[{1}] where [{2}] = @key";
        #endregion

        #region Private Members
        private string _schemaName;
        private string _tableName;
        private string _keyColumn;
        private string _descriptionColumn;
        private string _abbrvColumn;
        private string _sortOrderColumn;
        private string _isActiveColumn;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceRepository"/> class.
        /// </summary>
        /// <param name="schemaName">Name of the schema.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="keyColumn">The key column.</param>
        /// <param name="descColumn">The desc column.</param>
        /// <param name="abbrvColumn">The abbrv column.</param>
        /// <param name="sortOrderColumn">The sort order column.</param>
        /// <param name="IsActiveColumn">The is active column.</param>
        public ReferenceRepository(string schemaName, string tableName, string keyColumn, string descColumn, string abbrvColumn, string sortOrderColumn = "SortOrder", string IsActiveColumn = "IsActive")
        {
            _schemaName = schemaName;
            _tableName = tableName;
            _keyColumn = keyColumn;
            _descriptionColumn = descColumn;
            _abbrvColumn = abbrvColumn;
            _sortOrderColumn = sortOrderColumn;
            _isActiveColumn = IsActiveColumn;
        }

        /// <summary>
        /// Gets the view data.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public IEnumerable<ReferenceViewEntity> GetViewData(int pageNumber, int pageSize)
        {
            var list = new List<ReferenceViewEntity>();
            var cs = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

            using (var conn = new SqlConnection(cs))
            {
                var startRow = ((pageNumber - 1) * pageSize) + 1;
                var endRow = startRow + pageSize - 1;
                var sql = String.Format(FormatSelect, _schemaName, _tableName, _keyColumn, _descriptionColumn, _abbrvColumn, _sortOrderColumn, _isActiveColumn, startRow, endRow);

                conn.Open();
                using (var command = new SqlCommand(sql, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entity = new ReferenceViewEntity
                            {
                                Key = reader["Key"].ToString(),
                                Description = reader["Description"].ToString(),
                                Abbreviation = reader["Abbrv"].ToString(),
                                SortOrder = Convert.ToInt32(reader["SortOrder"]),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            };

                            list.Add(entity);
                        }
                    }
                }
                conn.Close();
            }

            return list;
        }

        /// <summary>
        /// Saves the specified view data.
        /// </summary>
        /// <param name="viewData">The view data.</param>
        public Dictionary<string, string> Save(IEnumerable<ReferenceViewEntity> viewData)
        {
            var cs = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            var results = new Dictionary<string, string>();

            using (var conn = new SqlConnection(cs))
            {
                conn.Open();

                foreach (var item in viewData)
                {
                    if (!item.Action.HasValue)
                        continue;

                    SqlCommand command = new SqlCommand();
                    switch (item.Action.Value)
                    {
                        case ReferenceAction.Insert:
                            command.CommandText = String.Format(FormatInsert, _schemaName, _tableName, _descriptionColumn, _abbrvColumn, _sortOrderColumn, _isActiveColumn);
                            command.Parameters.AddWithValue("@description", item.Description != null ? item.Description.Trim() : null);
                            command.Parameters.AddWithValue("@abbrv", item.Abbreviation != null ? item.Abbreviation.Trim() : null);
                            command.Parameters.AddWithValue("@sortOrder", item.SortOrder);
                            command.Parameters.AddWithValue("@isActive", item.IsActive);
                            break;

                        case ReferenceAction.Update:
                            command.CommandText = String.Format(FormatUpdate, _schemaName, _tableName, _keyColumn, _descriptionColumn, _abbrvColumn, _sortOrderColumn, _isActiveColumn);
                            command.Parameters.AddWithValue("@key", item.Key);
                            command.Parameters.AddWithValue("@description", item.Description != null ? item.Description.Trim() : null);
                            command.Parameters.AddWithValue("@abbrv", item.Abbreviation != null ? item.Abbreviation.Trim() : null);
                            command.Parameters.AddWithValue("@sortOrder", item.SortOrder);
                            command.Parameters.AddWithValue("@isActive", item.IsActive);
                            break;

                        case ReferenceAction.Delete:
                            command.CommandText = String.Format(FormatDelete, _schemaName, _tableName, _keyColumn);
                            command.Parameters.AddWithValue("@key", item.Key);
                            break;
                    }


                    try
                    {
                        command.Connection = conn;
                        command.ExecuteNonQuery();
                        results.Add(item.Key, "true");
                    }
                    catch (SqlException ex)
                    {
                        System.Diagnostics.Trace.WriteLine(ex.ToString());
                        results.Add(item.Key, ex.Message);
                    }
                }

                conn.Close();
            }

            return results;
        }

        /// <summary>
        /// Gets the total row count.
        /// </summary>
        /// <returns></returns>
        public int GetTotalRowCount()
        {
            var cs = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            int count;

            using (var conn = new SqlConnection(cs))
            {
                conn.Open();
                using (var command = new SqlCommand(String.Format(FormatCount, _schemaName, _tableName, _keyColumn, _descriptionColumn), conn))
                {
                    count = (int)command.ExecuteScalar();
                }
                conn.Close();
            }

            return count;
        }

        /// <summary>
        /// Determines if the entity exists
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool Exists(ReferenceViewEntity entity)
        {
            var cs = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            int count;

            //identity field column, no need to check for unique key issues
            if (String.Compare(_keyColumn, _abbrvColumn, true) != 0)
                return false;

            using (var conn = new SqlConnection(cs))
            {
                conn.Open();
                using (var command = new SqlCommand(String.Format(FormatExists, _schemaName, _tableName, _keyColumn), conn))
                {
                    command.Parameters.AddWithValue("@key", entity.Abbreviation);
                    count = (int)command.ExecuteScalar();
                }
                conn.Close();
            }

            return count > 0;
        }
    }
}
