 MsBuild.exe .\src\Retrier.sln /t:Build /p:Configuration=Release /p:TargetFramework=v4.0
 
 rm .\src\Retrier\Retrier*.*.*.nupkg
 nuget pack .\src\Retrier\Retrier.nuspec

