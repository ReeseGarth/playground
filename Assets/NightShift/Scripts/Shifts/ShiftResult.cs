public readonly struct ShiftResult
{
    public bool IsRequiredCleaningComplete { get; }
    public bool IsOptionalCleaningComplete { get; }
    public int Pay { get; }
    public int Rating { get; }

    public ShiftResult(
        bool isRequiredCleaningComplete,
        bool isOptionalCleaningComplete,
        int pay,
        int rating
    )
    {
        IsRequiredCleaningComplete =
            isRequiredCleaningComplete;

        IsOptionalCleaningComplete =
            isOptionalCleaningComplete;

        Pay = pay;
        Rating = rating;
    }
}