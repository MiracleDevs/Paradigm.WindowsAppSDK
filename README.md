# Paradigm.WindowsAppSDK
A framework that provides base classes for WindowsAppSDK applications using WinUI interfaces.

# Nuget publish process
After modifying the solution you can change the version by executing
```shell
$ cd ./build
$ ./increment.version.sh "1.0.0" "1.0.1"
```
where the first argument ("1.0.0") is the current version and the second one ("1.0.1") is the new version number.


To publish to nuget you need to execute the following script
```shell
$ cd ./build
$ ./publish.nuget.sh "{nuget-secret-key}"
```