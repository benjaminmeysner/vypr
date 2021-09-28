# VyprCore.Foundation

## Using Fido
* Add Fido capability `AddFido<TDbContext> where TDbContext : Microsoft.EntityFrameworkCore.DbContext`. This also takes in an optional parameter for configuration options in which to apply to the new Fido instance.
* Add `public DbSet<FidoCredential> FidoCredentials { get; set; }` to your DbContext model and create migration
* Add `modelBuilder.Entity<FidoCredential>().HasKey(m => m.Id);` to OnModelCreating in DbContext model
