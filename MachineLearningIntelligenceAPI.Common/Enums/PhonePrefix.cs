namespace MachineLearningIntelligenceAPI.Common.Enums
{
    public enum PhonePrefix
    {
        // note any keys that share the same value with be equivalent, even though the values may be evaluated into a different name. eg usa == canada because they're both 1
        Unknown = -1,
        Usa = 1,
        Canada = 1,
        UnitedKingdom = 44,
        Mexico = 52,
        Australia = 61,
        // TODO: rest of LATAM
    }
}
