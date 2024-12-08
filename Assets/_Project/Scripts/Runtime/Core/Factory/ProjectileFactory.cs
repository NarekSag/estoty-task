public class ProjectileFactory : BaseFactory<Projectile>
{
    public Projectile CreateProjectile(ConfigContainer.ProjectileConfig config)
    {
        Projectile projectileObject = CreateObject();

        if (projectileObject != null)
        {
            projectileObject.Initialize(config);
        }

        return projectileObject;
    }
}
