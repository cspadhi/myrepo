
using EFEndToEnd.Common;
using System.Web.Mvc;

namespace EFEndToEnd.Models.JqGrid
{
    [ModelBinder(typeof(JqGridModelBinding))]
    public class JqGridQueryRequest : ICriteria
    {
        public bool IsSearch { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public string SortColumn { get; set; }

        public string SortOrder { get; set; }

        public JqGridFilter GridFilter { get; set; }

        public string GetFieldData(string fieldName)
        {
            if (IsSearch && GridFilter != null)
            {
                return GridFilter.GetFieldData(fieldName);
            }
            return string.Empty;
        }

        public string FilterColumn
        {
            get
            {
                if (IsSearch)
                {
                    return GridFilter.rules[0].field;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
