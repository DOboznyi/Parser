FROM microsoft/dotnet-framework:4.7.2-runtime-windowsservercore-ltsc2016

SHELL ["powershell"]

# Note: Get MSBuild 12.
RUN Invoke-WebRequest "https://download.microsoft.com/download/9/B/B/9BB1309E-1A8F-4A47-A6C5-ECF76672A3B3/BuildTools_Full.exe" -OutFile "$env:TEMP\BuildTools_Full.exe" -UseBasicParsing
RUN &  "$env:TEMP\BuildTools_Full.exe" /Silent /Full

# Note: Get nuget.
RUN Invoke-WebRequest "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile "C:\windows\nuget.exe" -UseBasicParsing
RUN Set-Alias nuget "$rootPath\nuget.exe" -Scope Global -Verbose

# Note: Get chocolatey.
RUN Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))

# Note: Get git.
RUN choco install -y git

# Note: Get code, packages and build it.
RUN git clone https://github.com/Rassol/Parser.git
RUN nuget restore 'C:\Parser\Parser\' 
RUN ["C:\\Program Files (x86)\\msbuild\\12.0\\bin\\msbuild.exe", "C:\\Parser\\Parser\\Parser.sln"]