# QUBeyond-Hackaton
This solution has been built for the QUBeyond coding challenge.

USAGE  
Building  
You could build the solution using Visual Studio or using the hackaton-build.bat included in root folder fo the solution. 
That bat restores the nugget packages and built in release mode.

Running  
Once built, you could go to the release folder of Hackaton project and run Hackaton.exe
Without parameters, it will use the "default.json" file in that same folder as input file to be processed. the results will be generated in "output.json" file.
You can send parameters to modify those paths. First parameter is the input file and the second parameter is the output file.
Also, you can modify the default path for input/output changing the path in the app.config.
