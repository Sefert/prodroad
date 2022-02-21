# icd0021-21-22-s

Database migration and update
~~~sh
dotnet ef migrations add --project DAL.App --startup-project WebApp Initial
dotnet ef database update --project Dal.App --startup-project WebApp
dotnet ef migrations remove InitialCreate --project DAL.App //not working on my m1
dotnet ef database drop --project DAL.App  --startup-project WebApp

dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool update --global dotnet-aspnet-codegenerator

cd WebApp
dotnet aspnet-codegenerator controller -name ActiveNotificationController -actions -m Domain.App.ActiveNotification -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

M-1 .....
sudo rm -r /usr/local/share/dotnet
sudo rm -r /etc/dotnet

Check stuff and assign if needed


export DOTNET_ROOT=/usr/local/share/x64

sudo nano /etc/dotnet/install_location

/usr/local/share/dotnet/dotnet