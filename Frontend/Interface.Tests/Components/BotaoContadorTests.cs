using Olympus.Frontend.Interface.Components;
using Radzen;
using Radzen.Blazor;

namespace Olympus.Frontend.Interface.Tests.Components;

public class BotaoContadorTests : TestContext {

	public BotaoContadorTests() {

		Services.AddRadzenComponents();

	}

	[Fact]
	public void BotaoContador_AoSerClicado_DeveIncrementarOContador() {

		// Arrange (Organizar)
		var button = RenderComponent<BotaoContador>();

		// Act (Agir)
		button.FindComponent<RadzenButton>().Find("button").Click();

		// Assert (Verificar)
		button.Find("p").MarkupMatches("<p>Cliques: 1</p>");

	}

}
