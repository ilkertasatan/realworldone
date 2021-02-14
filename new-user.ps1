Write-host "Create a new user"  -ForegroundColor DarkGreen

$Name = Read-Host "Enter user name"
$Email = Read-Host "Enter email"
$SecuredPassword = Read-Host "Enter password" -AsSecureString
$BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($SecuredPassword)
$Password = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)

$Body = @{
    "user_name" = $Name
    "user_email" = $Email
    "user_password" = $Password
}

$Parameters = @{
    Method = "POST"
    Uri =  "http://localhost:5001/api/users"
    Body = ($Body | ConvertTo-Json)
    ContentType = "application/json"
}
Invoke-RestMethod @Parameters | ConvertTo-Json -Depth 10

Write-host "User has been created"  -ForegroundColor DarkGreen