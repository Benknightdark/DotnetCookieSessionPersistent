``` bash
# Migrate ProtectionKeys DB
dotnet ef migrations add AddDataProtectionKeys --context MyKeysContext
dotnet ef database update --context MyKeysContext
# Create Session Cache Table
dotnet sql-cache create "server=.,9487;Database=PersistCookie;user id=sa;password=YourStrong!Passw0rd" dbo SessionCache

```