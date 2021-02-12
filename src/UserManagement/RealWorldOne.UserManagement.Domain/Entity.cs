namespace RealWorldOne.UserManagement.Domain
{
    public abstract class Entity<TEntityId> : IHaveEntityId<TEntityId>, IMaybeExist
    {
        public TEntityId Id { get; set; }
        public bool Exists() => !Equals(Id, default(TEntityId));
    }
}