FROM mono:latest

RUN \
    apt-get update && \
    apt-get upgrade -y && \
    apt-get install -y && \
    apt-get install -y wget && \
    nuget install NUnit.Runners -o /tmp/nunit -version 2.6.6 && \
    apt-get install -y git && \
    mkdir /opt/logs && \
    mkdir code && \
    cd code && \
    git clone https://github.com/Rassol/Parser.git && \
    nuget restore /code/Parser/Parser/
RUN touch /opt/logs/Build.log && \
    xbuild /code/Parser/Parser/Parser.sln >> /opt/logs/Build.log
RUN touch /opt/logs/Tests.log && \
    mono /tmp/nunit/NUnit.Runners.2.6.6/tools/nunit-console.exe /code/Parser/Parser/Parser.Tests/bin/Debug/Parser.Tests.dll >> /opt/logs/Tests.log
RUN touch /opt/logs/Program.log
