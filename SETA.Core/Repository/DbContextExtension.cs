using System;
using System.Data.Entity;
using EntityState = System.Data.Entity.EntityState;

namespace SETA.Core.Repository
{
    public static class DbContextExtension
    {
        public static void ApplyStateChanges(this DbContext dbContext)
        {
            foreach (var trackableEntry in dbContext.ChangeTracker.Entries<IObjectState>())
            {
                IObjectState state = trackableEntry.Entity;
                trackableEntry.State = ConvertState(state.State);
            }
            //foreach (var dbEntityEntry in dbContext.ChangeTracker.Entries())
            //{
            //    var entityState = dbEntityEntry.Entity as IObjectState;
            //    if (entityState == null)
            //        throw new InvalidCastException(
            //            "All entites must implement " +
            //            "the IObjectState interface, this interface " +
            //            "must be implemented so each entites state" +
            //            "can explicitely determined when updating graphs.");

            //    dbEntityEntry.State = ConvertState(entityState.State);
            //}
        }

        private static EntityState ConvertState(ObjectState state)
        {
            switch (state)
            {
                case ObjectState.Added:
                    return EntityState.Added;
                case ObjectState.Modified:
                    return EntityState.Modified;
                case ObjectState.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;
            }
        }
    }
}
