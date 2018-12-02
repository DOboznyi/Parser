[![Build Status](https://travis-ci.com/Rassol/Parser.svg?token=xAHsQX5aMZqa1NArNh4q&branch=master)](https://travis-ci.com/Rassol/Parser)
[![Build status](https://ci.appveyor.com/api/projects/status/wqdtj84fomv95an4?svg=true)](https://ci.appveyor.com/project/Rassol/parser)
[![Sonarcloud Status](https://sonarcloud.io/api/project_badges/measure?project=Rassol_Parser&metric=alert_status)](https://sonarcloud.io/dashboard?id=Rassol_Parser)
[![Code Coverage](https://codecov.io/gh/Rassol/Parser/branch/master/graphs/badge.svg)](https://codecov.io/gh/Rassol/Parser)
# Parser
A program which can parse [law](http://zakon.rada.gov.ua/laws/file/2341-14) into folders with text ([Example](https://github.com/Rassol/test_zakon)).

# Windows
## 1. Preparation 

1.	You are need a MSBuild (Install [Build Tools for Visual Studio 2017](https://visualstudio.microsoft.com/ru/downloads/))
2.	You are need a [nuget](https://www.nuget.org/downloads)
3.	You are need a [git](https://git-scm.com/download/win) 
4.	You need to clone this repo:
```bash
mkdir <yourPath>
cd <yourPath>
git clone git@github.com:Rassol/Parser.git
```

## 2. Build&Compile

1.	Copy nuget to your folder with solution (<yourPath>\Parser\Parser)
2.	cd <yourPath>\Parser\Parser
3.	nuget restore
4. 	cd <path to the MSBuild.exe> (as example C:\Program Files (x86)\Microsoft Visual Studio\<year>\<Enterprise\Community>\MSBuild\<version>\Bin\)
5.	msbuild <yourPath>\Parser\Parser\Parser.sln

## 3. Execute 

1.	cd Parser\bin\Release\
2.	Parser.exe

# Debian
## 1. Preparation 

1.	You need a [Mono](https://www.mono-project.com/download/stable/#download-lin-debian)
2.	You need nuget ($ sudo apt install nuget)
3.	You need git ($ sudo apt-get install git)
4.	You need to clone this repo:
```bash
mkdir <yourPath>
cd <yourPath>
git clone git@github.com:Rassol/Parser.git
```

## 2. Build&Compile
```bash
cd <yourPath>\Parser\Parser
nuget restore
xbuild
```
## 3. Execute

For execute you need Wine and a litle bit time. If you execute our programm you can make a tutorial and send it to us.

# Docker

## 1. Preparation 

1.	You need git
2.	You need to clone this repo:
```bash
mkdir <yourPath>
cd <yourPath>
git clone git@github.com:Rassol/Parser.git
```
	
## 2. Build&Run
### Linux host

```bash
cd .\Parser\dockerfiles\mono
docker build -t my-image .
docker run --mount source=<name of new volume>,target=/opt/logs -v <dir for input data>/:/opt/input -it --name my_container my-image 
```

## 2. Watch logs
### Linux host

1.	To watch Tests.log:
```bash
cat /var/lib/docker/volumes/<name of new volume>/_data/Tests.log
```
2.	To watch Build.log:
```bash
cat /var/lib/docker/volumes/<name of new volume>/_data/Build.log
```

## 3. Run programm
### Linux host

1.	Attach the container:
```bash
docker attach my_container
```
2.	Run the program:
```bash
mono /code/Parser/Parser/Parser/bin/Release/Parser.exe xxx xxx >> /opt/logs/Program.log\": stat bash mono /code/Parser/Parser/Parser/bin/Release/Parser.exe xxx xxx >> /opt/logs/Program.log
```