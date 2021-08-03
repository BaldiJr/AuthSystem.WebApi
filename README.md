# AuthSystem.WebApi

# Informações
Frameworks: Asp.Net 5/Entity Framework  
Database: SQLite  
Pattern: Repository 
Testes: XUnit

# Docker for Windows Commands
docker build -t auth-system -f Dockerfile .\AuthSystem.WebApi\  
docker container run --rm -it -p 5000:80 -e ASPNETCORE_URLS="http://+" auth-system




