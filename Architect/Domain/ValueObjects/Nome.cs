namespace Olympus.Architect.Domain.ValueObjects;

public readonly record struct Nome {

	public string Valor { get; init; }

	public Nome(string valor) {

		if (string.IsNullOrWhiteSpace(valor)) throw new ArgumentException("Nome não pode ser vazio.");
		Valor = valor.Trim();

	}

	public string GetFirstName() {

		return Valor.Split(' ')[0];

	}

	public string GetLastName() {

		var partes = Valor.Split(' ');
		return partes.Length > 1 ? partes[^1] : string.Empty;

	}

}
