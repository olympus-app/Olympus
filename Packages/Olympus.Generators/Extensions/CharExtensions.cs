namespace Olympus.Generators;

public static class CharExtensions {

	private static bool IsLetterChar(this UnicodeCategory category) {

		switch (category) {

			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber: return true;
			case UnicodeCategory.ClosePunctuation:
			case UnicodeCategory.ConnectorPunctuation:
			case UnicodeCategory.Control:
			case UnicodeCategory.CurrencySymbol:
			case UnicodeCategory.DashPunctuation:
			case UnicodeCategory.DecimalDigitNumber:
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.FinalQuotePunctuation:
			case UnicodeCategory.Format:
			case UnicodeCategory.InitialQuotePunctuation:
			case UnicodeCategory.LineSeparator:
			case UnicodeCategory.MathSymbol:
			case UnicodeCategory.ModifierSymbol:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.OpenPunctuation:
			case UnicodeCategory.OtherNotAssigned:
			case UnicodeCategory.OtherNumber:
			case UnicodeCategory.OtherPunctuation:
			case UnicodeCategory.OtherSymbol:
			case UnicodeCategory.ParagraphSeparator:
			case UnicodeCategory.PrivateUse:
			case UnicodeCategory.SpaceSeparator:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.Surrogate:
			default: break;

		}

		return false;

	}

	public static bool IsIdentifierStartCharacter(this char @char) => @char == '_' || CharUnicodeInfo.GetUnicodeCategory(@char).IsLetterChar();

	public static bool IsIdentifierPartCharacter(this char @char) {

		var category = CharUnicodeInfo.GetUnicodeCategory(@char);

		return category.IsLetterChar()
			|| category == UnicodeCategory.DecimalDigitNumber
			|| category == UnicodeCategory.ConnectorPunctuation
			|| category == UnicodeCategory.Format
			|| category == UnicodeCategory.NonSpacingMark
			|| category == UnicodeCategory.SpacingCombiningMark;

	}

}
