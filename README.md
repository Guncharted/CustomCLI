# CustomCLI (GunchartedCLI, lol)
My attempt to create own CLI for frustrating things. Inspired by holy mess in Downloads folder.

## How to use
### Configure
Pull repo and change Source and Destination folders in `appsettings.json`.

### Pack the tool
Open Package Manager Console and run this command:

`dotnet pack`

This will create NuGet package and place it in **nupkg** folder within project folder.

### Global installation
Install it as tool globally:

`dotnet tool install -g --add-source ./nupkg hnchr`

If you need to uninstall it use this command:

`dotnet tool uninstall -g hnchr`

### Usage
There is only one functionality: take all files from source directory, sort them by extension and move to destination directory. 
Default directory paths could be specified in `appsettings.json`.

To use with default paths run this command:

`hnchr cld`

where **cld** stands for "clean-directory".

To specify paths use the following command as an example:

`hnchr cld --source "C:\Foo" --destination "D:\Bar"`

or with short option names:

`hnchr cld -s "C:\Foo" -d "D:\Bar"`

