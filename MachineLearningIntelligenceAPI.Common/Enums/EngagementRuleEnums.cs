namespace MachineLearningIntelligenceAPI.Common.Enums
{

    public enum ActionType
    {
        Like = 1,       // like comments in own posts, others posts, like other posts, like own posts,
        Reply = 2,      // reply to comments, DMs, popular posts in the community, popular comments in the community's top posts.
        Follow = 3,     // follow top comments, follow to posts in community,
        Unfollow = 4,   // unfollow?
        Block = 5,      // block someone based on their comment/ activity. Eg no swearing, certain flagged words. Also removes any content on the offending instance if possible
        Remove = 6,     // remove someone from your following or just the content. Scope has to be profileWide to remove someone from your following. It's like a soft block, they can still interact but the content is removed.
    }

    public enum EngagementRuleScope     // EngagementJobs shouldn't have different types here? Shouldn't combine 1 job profile-wide with non-profile wide?
    {
        ProfileWide = 1,    // eg inbox/ DMs
        NonProfileWide = 2,
    }
}
