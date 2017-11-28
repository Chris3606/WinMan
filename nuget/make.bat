pushd %~dp0
nuget pack ../WinMan/WinMan.csproj -properties Configuration=Release
popd