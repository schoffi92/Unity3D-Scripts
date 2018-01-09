# This is an old branch and it will be rewrited

The branches will be used for versioning and master won't be used anymore.

# Unity3D-Scripts

This project will contain some useful scripts / functions. 
The scripts won't be connected to each other, so you can select what you need and use that only without implementing a whole library to your project.

## Scripts

Some description about the script files...



### ConfigParser

This script can load cfg/ini/inf text files. You can load another config file without clear so you can extend your config.
It has delegated function part so if it cleared or some vlue modified you can get an event from it.

### FPS Counter

If the __FPSCounterEnabled__ set to true and you added a UIText to it this will write the FPS number to the text.
It has delegated function __OnFPSDropDown__ what called when the FPS is under the **MinFPSValue**.
