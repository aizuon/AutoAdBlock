namespace AutoUpdater
{
    public enum UpdateResult : byte
    {
        Updated,
        AlreadyUpdated,
        InsufficientPermission,
        NoInternet,
        Failure
    }
}
