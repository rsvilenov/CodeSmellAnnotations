In order for the Roslyn's Release tracking analyzer to work,
the descriptor should be a field and the full "new" expression
should be used (the short one - new("SML00X",...) will break the analysis).

This seems to be a well-known issue:

https://github.com/dotnet/roslyn-analyzers/issues/5957
https://github.com/dotnet/roslyn-analyzers/issues/5890
https://github.com/docopt/docopt.net/pull/161
https://github.com/dotnet/roslyn-analyzers/issues/5828