using System.Web.Mvc;

namespace EFEndToEnd.Models.JqGrid
{
	public class JqGridModelBinding : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			var request = controllerContext.HttpContext.Request;
			return new JqGridQueryRequest
			{
				IsSearch = bool.Parse(request["_search"] ?? "false"),
				PageIndex = int.Parse(request["page"] ?? "1"),
				PageSize = int.Parse(request["rows"] ?? "10"),
				SortColumn = request["sidx"] ?? "",
				SortOrder = request["sord"] ?? "asc",
				GridFilter = JqGridFilter.NewFilter(
					request["filters"], 
					request["searchField"], 
					request["searchString"], 
					request["searchOper"])
			};
		}
	}
}
