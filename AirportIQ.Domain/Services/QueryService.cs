using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AirportIQ.Model.Models.QueryBrowser;
using AirportIQ.Model.Models.QueryBrowser.Queries;
using AirportIQ.Domain.Contracts;
using AirportIQ.Data.SqlServer.Repositories;
using AirportIQ.Data;

namespace AirportIQ.Domain.Services
{
    public class QueryService : IQueryService
    {
        public const int MaxResults = 5000; //set due to timeouts trying to get total row count for large views

        #region QueryFactory
        private class QueryFactory
        {
            private static readonly string[] DependentLookupSetKeys = { "CountrySubdivisions" };
            private static readonly Dictionary<string, Type> Queries = new Dictionary<string, Type>
            {
                { QueryKeys.CompanyDivisionAgreement, typeof(CompanyDivisionAgreementQuery) },
                { QueryKeys.CompanyDivisionAgreementAll, typeof(CompanyDivisionAgreementAllQuery) },
                { QueryKeys.CompanyDivisionContacts, typeof(CompanyDivisionContactsQuery) },
                { QueryKeys.CompanyDivisionEmployees, typeof(CompanyDivisionEmployeesQuery) },
                { QueryKeys.CompanyDivisionInfo, typeof(CompanyDivisionInfoQuery) },
                { QueryKeys.GuardPost, typeof(GuardPostQuery) },
                { QueryKeys.SAFE, typeof(SAFEQuery) },
                { QueryKeys.CompanyDivisionPersonBadge, typeof(CompanyDivisionPersonBadgeQuery) },
                { QueryKeys.PersonBackgroundCheck, typeof(PersonBackgroundCheckQuery) },
                { QueryKeys.Audit, typeof(AuditQuery) },
                { QueryKeys.PersonBadge, typeof(PersonBadgeQuery) },
                { QueryKeys.RACU, typeof(RACUQuery) },
                { QueryKeys.BadgeInspections, typeof(BadgeInspectionsQuery) }
            };

            public static Query Create(string queryKey, int userId, bool loadLookupData)
            {
                if (!Queries.ContainsKey(queryKey))
                    throw new ArgumentOutOfRangeException(String.Format("Invalid queryKey '{0}'.", queryKey));

                var query = (Query)Activator.CreateInstance(Queries[queryKey]);
                if (loadLookupData)
                    LoadLookupSets(query, userId);

                return query;
            }

            private static void LoadLookupSets(Query query, int userId)
            {
                if (query == null || String.IsNullOrEmpty(query.SprocName) || query.LookupSets == null)
                    return;

                var storedProcedure = new Data.SqlServer.Initializers.StoredProcedure { StoredProcedureName = query.SprocName };
                storedProcedure.Parameters.Add(new Data.SqlServer.Initializers.StoredProcedureParameter("@userId", Data.SqlServer.Initializers.ParameterType.DBInteger, userId));
                var dataSet = storedProcedure.ExecuteMultipleDataSet();

                query.EditorData.Clear();
                for (var i = 0; i < query.LookupSets.Length; i++)
                {
                    if (DependentLookupSetKeys.Contains(query.LookupSets[i]))
                    {
                        var list = new List<DependentListItem>();
                        var row = dataSet.Tables[i].Rows.Count > 0 ? dataSet.Tables[i].Rows[0] : null;
                        var lastKey = row != null ? row[0].ToString() : String.Empty;
                        var listItem = row != null ? new DependentListItem(row[0].ToString(), row[1].ToString()) : null;

                        if (listItem != null)
                            list.Add(listItem);

                        for (var j = 0; j < dataSet.Tables[i].Rows.Count; j++)
                        {
                            row = dataSet.Tables[i].Rows[j];
                            var groupKey = row[0].ToString();

                            if (groupKey != lastKey)
                            {
                                listItem = new DependentListItem(row[0].ToString(), row[1].ToString());
                                list.Add(listItem);
                                lastKey = groupKey;
                            }

                            listItem.ChildList.Add(new ListItem(row[2].ToString(), row[3].ToString()));
                        }
                        query.EditorData.Add(query.LookupSets[i], list);
                    }
                    else
                    {
                        var list = new List<ListItem>();
                        for (var j = 0; j < dataSet.Tables[i].Rows.Count; j++)
                        {
                            var row = dataSet.Tables[i].Rows[j];
                            list.Add(new ListItem(row[0].ToString(), row[1].ToString()));
                        }
                        query.EditorData.Add(query.LookupSets[i], list);
                    }
                }
            }

            public static List<Query> GetSystemList(int userId)
            {
                return Queries
                    .Select(i => Create(i.Key, userId, false))
                    .OrderBy(i => i.Name)
                    .ToList();
            }
        }
        #endregion

        private List<Query> GetSystemList(int userId)
        {
            return QueryFactory
                        .GetSystemList(userId)
                        .Select(i => ConvertQuery(i)) //convert from subclass to base class to avoid type error binding with object datasource
                        .ToList();
        }

        private IQueryRepository m_Repository;
        public QueryService(IQueryRepository repository)
        {
            if (repository == null)
                throw new ArgumentException("repository cannot be null");
            m_Repository = repository;
        }

        public QueryService()
            : this(new QueryRepository())
        {
        }

        public void Delete(int queryId, int UserID)
        {
            m_Repository.Delete(queryId, UserID);
        }

        public Query Create(string queryKey, int userId)
        {
            return QueryFactory.Create(queryKey, userId, true);
        }

        public Query GetById(int queryId, int userId)
        {
            Query userQuery = m_Repository.GetById(queryId);
            Query sysQuery = QueryFactory.Create(userQuery.QueryKey, userId, true);

            userQuery.EnsureSubset(sysQuery.AvailableColumns);
            userQuery.EditorData = sysQuery.EditorData;

            return userQuery;
        }

        public int Insert(Query query, int UserID)
        {
            return m_Repository.Insert(query, UserID);
        }

        //Convert from subclass to base class to avoid type exception with grid datasource
        private static Query ConvertQuery(Query query)
        {
            Query q = new Query
            {
                Id = query.Id,
                Name = query.Name,
                QueryType = query.QueryType,
                QueryKey = query.QueryKey
            };
            return q;
        }

        public IEnumerable<Query> QueryList(int userId, int startIndex, int maxRows)
        {
            return m_Repository.QueryList(GetSystemList(userId), userId, startIndex, maxRows);
        }

        public void Update(Query query, int UserID)
        {
            m_Repository.Update(query, UserID);
        }

        public int QueryListCount(int userId)
        {
            return m_Repository.QueryListCount(userId) + GetSystemList(userId).Count;
        }

        public string CreateSqlTotalRowCount(Query query, string facilityCode)
        {
            return CreateSql(query, facilityCode, null, null, true);
        }

        public string CreateSql(Query query, string facilityCode, int? startIndex, int? maxRows)
        {
            return CreateSql(query, facilityCode, startIndex, maxRows, false);
        }

        private string CreateSql(Query query, string facilityCode, int? startIndex, int? maxRows, bool isCount)
        {
            System.Text.StringBuilder sbQuery = new System.Text.StringBuilder();
            System.Text.StringBuilder sbWhere = new System.Text.StringBuilder();

            if ((query == null))
                throw new ArgumentException("query cannot be null");

            if (query.SelectedColumns.Count == 0)
                throw new Exception("Query must contain at least one selected column.");

            if (query.FilterByFacilityCode && String.IsNullOrEmpty(facilityCode))
                throw new ArgumentException("facilityCode cannot be empty when FilterByFacilityCode is set to true.");

            if (query.SortColumns.Count == 0)
                query.SortColumns.Add(new SortColumn(query.SelectedColumns[0], SortOrder.Ascending));


            if (isCount)
                sbQuery.Append("SELECT COUNT(*) FROM (");

            sbQuery.Append("SELECT ");
            if (!startIndex.HasValue || !maxRows.HasValue)
            {
                sbQuery.Append("DISTINCT ");

                if (isCount)
                    sbQuery.AppendFormat("TOP {0} ", MaxResults);

                for (int i = 0; i <= query.SelectedColumns.Count - 1; i++)
                {
                    sbQuery.AppendFormat("[{0}] as [{1}]", query.SelectedColumns[i].DisplayColumnName, query.SelectedColumns[i].HeaderText);
                    if ((i < query.SelectedColumns.Count - 1))
                        sbQuery.Append(", ");
                }
                sbQuery.AppendLine();
                sbQuery.AppendLine("FROM " + query.DataSource);
            }
            else
            {
                for (int i = 0; i <= query.SelectedColumns.Count - 1; i++)
                {
                    sbQuery.AppendFormat("[{0}]", query.SelectedColumns[i].HeaderText);
                    if ((i < query.SelectedColumns.Count - 1))
                        sbQuery.Append(", ");
                }
                sbQuery.AppendLine();

                sbQuery.Append("FROM (SELECT ROW_NUMBER() OVER (ORDER BY ");
                for (int i = 0; i <= query.SortColumns.Count - 1; i++)
                {
                    string sortText = null;

                    if (query.SortColumns[i].SortDirection == SortOrder.Ascending)
                    {
                        sortText = "ASC";
                    }
                    else
                    {
                        sortText = "DESC";
                    }

                    sbQuery.AppendFormat("[{0}] {1}", query.SortColumns[i].Column.HeaderText, sortText);
                    if ((i < query.SortColumns.Count - 1))
                        sbQuery.Append(", ");
                }
                sbQuery.AppendLine(String.Format(") rowNum, * FROM (SELECT DISTINCT TOP {0} ", MaxResults));

                for (int i = 0; i <= query.SelectedColumns.Count - 1; i++)
                {
                    sbQuery.AppendFormat("[{0}] as [{1}]", query.SelectedColumns[i].DisplayColumnName, query.SelectedColumns[i].HeaderText);
                    if ((i < query.SelectedColumns.Count - 1))
                        sbQuery.Append(", ");
                }
                sbQuery.AppendLine(" FROM " + query.DataSource);
                //do not append an order by here to the inner query, large results sets will hang forever sorting
            }

            if (query.Filters.Count > 0 || query.FilterByFacilityCode)
            {
                for (int i = 0; i <= query.Filters.Count - 1; i++)
                {
                    if (sbWhere.Length > 0)
                    {
                        switch (query.LinkType)
                        {
                            case LinkTypes.All:
                                sbWhere.Append(" AND ");
                                break;
                            case LinkTypes.Any:
                            case LinkTypes.None:
                                sbWhere.Append(" OR ");
                                break;
                            default:
                                throw new Exception(string.Format("invalid link type key '{0}'", query.LinkType));
                        }
                    }

                    sbWhere.Append(ToQueryText(query.Filters[i]));
                }

                if (query.LinkType == LinkTypes.None && query.Filters.Count > 0)
                {
                    sbWhere.Insert(0, "NOT (");
                    sbWhere.Append(")");
                }

                if (!String.IsNullOrEmpty(facilityCode))
                {
                    var filterText = String.Format("(FacilityCode = '{0}')", facilityCode);
                    if (query.Filters.Count > 0)
                        filterText += " AND (";

                    sbWhere.Insert(0, filterText);
                    if (query.Filters.Count > 0)
                        sbWhere.Append(")");
                }

                sbQuery.AppendLine("WHERE " + sbWhere.ToString());
            }

            if (startIndex.HasValue && maxRows.HasValue)
            {
                sbQuery.AppendLine(") as t) as x");
                sbQuery.AppendLine(String.Format("WHERE rowNum BETWEEN {0} and {1}", startIndex, startIndex + maxRows -1));
            }

            if (isCount)
            {
                sbQuery.Append(") x");
                return sbQuery.ToString();
            }

            sbQuery.Append("ORDER BY ");
            for (int i = 0; i <= query.SortColumns.Count - 1; i++)
            {
                string sortDir = null;

                if (query.SortColumns[i].SortDirection == SortOrder.Ascending)
                {
                    sortDir = "Asc";
                }
                else
                {
                    sortDir = "Desc";
                }

                var sortName = !startIndex.HasValue || !maxRows.HasValue ? query.SortColumns[i].Column.DisplayColumnName : query.SortColumns[i].Column.HeaderText;
                sbQuery.AppendFormat("[{0}] {1}", sortName, sortDir);
                if ((i < query.SortColumns.Count - 1))
                    sbQuery.Append(", ");
            }

            return sbQuery.ToString();
        }


        private string EscapeValue(string value, string fieldType)
        {
            switch (fieldType)
            {
                case DataType.Int:
                case DataType.Float:
                case DataType.Bool:
                    //no escaping for these types
                    return value;
                case DataType.DateTime:
                case DataType.Guid:
                case DataType.String:
                    return string.Format("'{0}'", value != null ? value.Replace("'", "''") : string.Empty);
                default:
                    throw new Exception(string.Format("Invalid fieldType '{0}'", fieldType));
            }
        }

        private string ToQueryText(Filter filter)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int j = 0; j <= filter.Options.Count - 1; j++)
            {
                FilterOption o = filter.Options[j];

                if ((sb.Length > 0))
                    sb.Append(" AND ");

                if ((o.OperatorKey == OperatorType.NotStartsWith | o.OperatorKey == OperatorType.NotContains | o.OperatorKey == OperatorType.NotEndsWith))
                    sb.Append("NOT ");
                sb.Append("(");
                sb.AppendFormat("[{0}] ", o.DatabaseColumn.ColumnName);

                switch (o.OperatorKey)
                {
                    case OperatorType.Equal:
                        sb.AppendFormat("= {0}", EscapeValue(o.FilterValue, o.DatabaseColumn.DataType));
                        if ((o.DatabaseColumn.DataType == DataType.Bool & o.FilterValue == "0"))
                            sb.AppendFormat(" OR [{0}] is null", o.DatabaseColumn.ColumnName);
                        break;
                    case OperatorType.NotEqual:
                        sb.AppendFormat("!= {0}", EscapeValue(o.FilterValue, o.DatabaseColumn.DataType));
                        if ((o.DatabaseColumn.DataType == DataType.Bool & o.FilterValue == "1"))
                            sb.AppendFormat(" OR [{0}] is null", o.DatabaseColumn.ColumnName);
                        break;
                    case OperatorType.LessThan:
                        sb.AppendFormat("< {0}", EscapeValue(o.FilterValue, o.DatabaseColumn.DataType));
                        break;
                    case OperatorType.LessThanEqual:
                        sb.AppendFormat("<= {0}", EscapeValue(o.FilterValue, o.DatabaseColumn.DataType));
                        break;
                    case OperatorType.GreaterThan:
                        sb.AppendFormat("> {0}", EscapeValue(o.FilterValue, o.DatabaseColumn.DataType));
                        break;
                    case OperatorType.GreaterThanEqual:
                        sb.AppendFormat(">= {0}", EscapeValue(o.FilterValue, o.DatabaseColumn.DataType));
                        break;
                    case OperatorType.IsNull:
                        sb.Append("is null");
                        break;
                    case OperatorType.IsNotNull:
                        sb.Append("is not null");
                        break;
                    case OperatorType.StartsWith:
                    case OperatorType.NotStartsWith:
                        sb.AppendFormat("like '{0}%'", o.FilterValue != null ? o.FilterValue.Replace("'", "''") : string.Empty);
                        break;
                    case OperatorType.Contains:
                    case OperatorType.NotContains:
                        sb.AppendFormat("like '%{0}%'", o.FilterValue != null ? o.FilterValue.Replace("'", "''") : string.Empty);
                        break;
                    case OperatorType.EndsWith:
                    case OperatorType.NotEndsWith:
                        sb.AppendFormat("like '%{0}'", o.FilterValue != null ? o.FilterValue.Replace("'", "''") : string.Empty);
                        break;
                    default:
                        throw new ArgumentException(string.Format("Key {0} is not a valid value.", o.OperatorKey));
                }

                sb.Append(")");
            }

            return sb.ToString();
        }
    }
}
