using System;

namespace EFEndToEnd.Models.JqGrid
{

    public class JqGridFilter
    {
        public string groupOp { get; set; }

        public JqGridRule[] rules { get; set; }

        public static JqGridFilter NewFilter(string jsonData, string searchField, string searchString, string searchOper)
        {
            JqGridFilter returnValue = null;
            if (!string.IsNullOrEmpty(jsonData))
            {
                returnValue = Newtonsoft.Json.JsonConvert.DeserializeObject<JqGridFilter>(jsonData);
            }
            else
            {
                returnValue =
                    new JqGridFilter()
                        {
                            groupOp = "AND",
                            rules = new[] { new JqGridRule() { data = searchString, field = searchField, op = searchOper } }
                        };
            }
            return returnValue;
        }

        public string GetFieldData(string fieldName)
        {
            var returnValue = string.Empty;
            if (rules.Length > 0 && rules[0].field.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
            {
                returnValue = rules[0].data;
            }
            return returnValue;
        }
    }
}

