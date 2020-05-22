using Microsoft.AspNet.Identity.EntityFramework;

namespace iSmash.Migrations
{
    internal class SeedManager<T>
    {
        private RoleStore<IdentityRole> roleStore;

        public SeedManager(RoleStore<IdentityRole> roleStore)
        {
            this.roleStore = roleStore;
        }
    }
}