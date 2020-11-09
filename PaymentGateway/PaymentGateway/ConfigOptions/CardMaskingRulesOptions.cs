namespace PaymentGateway.ConfigOptions
{
    public class CardMaskingRulesOptions
    {
        public const string CardMaskingRules = "CardMaskingRules";

        public int MaskingStartIndex { get; set; }
        public int NumberOfMaskedChars { get; set; }
        public char MaskingChar { get; set; }
    }
}
