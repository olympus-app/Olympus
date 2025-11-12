using Microsoft.AspNetCore.OData.Query;

namespace Olympus.Core.Backend.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class ODataQueryAttribute : EnableQueryAttribute {

	public ODataQueryAttribute(ODataQueryType Config) : base() {

		switch (Config) {

			case ODataQueryType.Page:
				AllowedQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.Expand | AllowedQueryOptions.Filter | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top | AllowedQueryOptions.Skip | AllowedQueryOptions.SkipToken | AllowedQueryOptions.Count;
				EnableConstantParameterization = true;
				EnableCorrelatedSubqueryBuffering = true;
				EnsureStableOrdering = true;
				HandleReferenceNavigationPropertyExpandFilter = true;
				MaxAnyAllExpressionDepth = 1;
				MaxExpansionDepth = 3;
				MaxNodeCount = 10;
				MaxOrderByNodeCount = 10;
				MaxTop = 100;
				PageSize = 100;
				break;

			case ODataQueryType.Item:
				AllowedQueryOptions = AllowedQueryOptions.Select | AllowedQueryOptions.Expand;
				EnableConstantParameterization = true;
				EnableCorrelatedSubqueryBuffering = true;
				HandleReferenceNavigationPropertyExpandFilter = true;
				MaxAnyAllExpressionDepth = 1;
				MaxExpansionDepth = 5;
				MaxNodeCount = 10;
				break;

			default:
				AllowedQueryOptions = AllowedQueryOptions.None;
				break;

		}

	}

}
