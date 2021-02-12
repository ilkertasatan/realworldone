namespace RealWorldOne.UserManagement.Domain
{
    public interface IHaveEntityId<TEntityId>
    {
        public TEntityId Id { get; set; }
    }
}