Start-Process -FilePath "cmd.exe" -ArgumentList "/C dotnet run --project .\TestePonta.Api\"
Start-Process -FilePath "cmd.exe" -ArgumentList "/C dotnet run --project .\TestePonta.App\"

Start-Process "https://localhost:7001/swagger/index.html"
Start-Process "https://localhost:7080/"