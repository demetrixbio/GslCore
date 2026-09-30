#!/usr/bin/env bash

dotnet restore GslCore.sln
dotnet run --project build -- "$@"

