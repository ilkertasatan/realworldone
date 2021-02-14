Write-host "Login user"  -ForegroundColor DarkGreen

$Email = Read-Host "Enter email"
$SecuredPassword = Read-Host "Enter password" -AsSecureString
$BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($SecuredPassword)
$Password = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)

$Body = @{
    "email" = $Email
    "password" = $Password
}

$Parameters = @{
    Method = "POST"
    Uri =  "http://localhost:5001/api/users/login"
    Body = ($Body | ConvertTo-Json)
    ContentType = "application/json"
}
Invoke-RestMethod @Parameters | ConvertTo-Json -Depth 10

Write-host "Please copy access token to use kitten generator api"  -ForegroundColor DarkGreen