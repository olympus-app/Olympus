namespace Olympus.Generators;

public sealed class SourceTextReader(SourceText text) : TextReader {

	private readonly SourceText Text = text;

	private int Position;

	public override int Read(char[] buffer, int index, int count) {

		var remaining = Text.Length - Position;

		var charactersToRead = Math.Min(remaining, count);

		Text.CopyTo(Position, buffer, index, charactersToRead);

		Position += charactersToRead;

		return charactersToRead;

	}

}
