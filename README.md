
1. ## How can we update database after migration?
	```c#
	var host = CreateHostBuilder(args).Build();
			using (var scope=host.Services.CreateScope())
			{
                var services = scope.ServiceProvider;
				try
				{
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
				}
				catch (Exception ex)
				{
					var logger=services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An exception occured during migration");
				}
			}
            host.Run(); 
	```