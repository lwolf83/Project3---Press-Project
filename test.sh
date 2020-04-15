cd TestPressProject 
dotnet add package NUnit.Console
dotnet restore TestPressProject.csproj
dotnet build TestPressProject.csproj
dotnet test TestPressProject.csproj