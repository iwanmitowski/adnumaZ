#!/usr/bin/env bash

cd /app

dotnet tool restore
dotnet libman restore

dotnet ef database update
dotnet watch run
