namespace Olympus.Architect.Domain.Entities;

public class Pedido {

	public Guid Id { get; private set; }
	public string Descricao { get; private set; }

	private Pedido() {

		Descricao = null!;

	}

	public Pedido(string descricao) {

		Id = Guid.NewGuid();
		Descricao = descricao;

	}

}
